/**
 * PlatformIdentifiers.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.System.Profile;


namespace KanoComputing.KpcUwpCore.PlatformDetection {

    public class PlatformIdentifiers : IPlatformIdentifiers {

        internal const string APP_SESSION_KEY = "AppSessionId";

        /// <summary>
        /// Returns an SID specific to the device and publisher of the calling app.
        /// The string value is hashed before returning.
        /// </summary>
        public string GetDeviceId() {
            return Hash(
                CryptographicBuffer.EncodeToBase64String(
                    SystemIdentification.GetSystemIdForPublisher().Id));
        }

        /// <summary>
        /// Returns an SID specific to the current OS user.
        /// The string value is hashed before returning.
        /// </summary>
        public string GetUserId() {
            return Hash(
                WindowsIdentity.GetCurrent().User.ToString());
        }

        /// <summary>
        /// Returns an SID specific to the current logon session for the current user.
        /// The string value is hashed before returning.
        /// </summary>
        public string GetSessionId() {
            return Hash(
                WindowsLogonSession.GetSid());
        }

        /// <summary>
        /// Generates a new application session ID and returns it.
        /// </summary>
        public string RefreshAppSessionId() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
            string token = GenerateUniqueToken();

            if (localSettings.Values.ContainsKey(APP_SESSION_KEY)) {
                localSettings.Values.Remove(APP_SESSION_KEY);
            }
            localSettings.Values.Add(APP_SESSION_KEY, token);
            return token;
        }

        /// <summary>
        /// Returns the previously generated application session ID.
        /// One will be created if it never was never refreshed.
        /// </summary>
        public string GetAppSessionId() {
            ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey(APP_SESSION_KEY)) {
                return localSettings.Values[APP_SESSION_KEY] as string;
            }
            return this.RefreshAppSessionId();
        }

        private static string GenerateUniqueToken() {
            // Remove dashes to match implementation in kbc-utils.
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        private static string Hash(string data) {
            using (HashAlgorithm algorithm = SHA256.Create()) {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte currentByte in algorithm.ComputeHash(Encoding.UTF8.GetBytes(data))) {
                    // Format each byte of the hashed string to hexadecimal.
                    stringBuilder.Append(currentByte.ToString("X2"));
                }
                return stringBuilder.ToString();
            }
        }

        private class WindowsLogonSession {

            public const uint SE_GROUP_LOGON_ID = 0xC0000000;  // from winnt.h
            public const int TokenGroups = 2;  // from TOKEN_INFORMATION_CLASS

            private enum TOKEN_INFORMATION_CLASS {
                TokenUser = 1,
                TokenGroups,
                TokenPrivileges,
                TokenOwner,
                TokenPrimaryGroup,
                TokenDefaultDacl,
                TokenSource,
                TokenType,
                TokenImpersonationLevel,
                TokenStatistics,
                TokenRestrictedSids,
                TokenSessionId,
                TokenGroupsAndPrivileges,
                TokenSessionReference,
                TokenSandBoxInert,
                TokenAuditPolicy,
                TokenOrigin
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct SID_AND_ATTRIBUTES {
                public IntPtr Sid;
                public uint Attributes;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct TOKEN_GROUPS {
                public int GroupCount;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
                public SID_AND_ATTRIBUTES[] Groups;
            };

            [DllImport("advapi32.dll", SetLastError = true)]
            private static extern bool GetTokenInformation(
                IntPtr TokenHandle,
                TOKEN_INFORMATION_CLASS TokenInformationClass,
                IntPtr TokenInformation,
                int TokenInformationLength,
                out int ReturnLength);

            // Using IntPtr for pSID instead of Byte[].
            [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool ConvertSidToStringSid(IntPtr pSID, out IntPtr ptrSid);

            [DllImport("kernel32.dll")]
            private static extern IntPtr LocalFree(IntPtr hMem);

            public static string GetSid() {
                int TokenInfLength = 0;
                // First call gets lenght of TokenInformation.
                bool Result = GetTokenInformation(
                    WindowsIdentity.GetCurrent().Token,
                    TOKEN_INFORMATION_CLASS.TokenGroups,
                    IntPtr.Zero,
                    TokenInfLength,
                    out TokenInfLength);
                IntPtr TokenInformation = Marshal.AllocHGlobal(TokenInfLength);
                Result = GetTokenInformation(
                    WindowsIdentity.GetCurrent().Token,
                    TOKEN_INFORMATION_CLASS.TokenGroups,
                    TokenInformation,
                    TokenInfLength,
                    out TokenInfLength);

                if (!Result) {
                    Marshal.FreeHGlobal(TokenInformation);
                    return string.Empty;
                }

                string retVal = string.Empty;
                TOKEN_GROUPS groups = (TOKEN_GROUPS)Marshal.PtrToStructure(TokenInformation, typeof(TOKEN_GROUPS));
                int sidAndAttrSize = Marshal.SizeOf(new SID_AND_ATTRIBUTES());

                for (int i = 0; i < groups.GroupCount; i++) {
                    SID_AND_ATTRIBUTES sidAndAttributes = (SID_AND_ATTRIBUTES)Marshal.PtrToStructure(
                        new IntPtr(TokenInformation.ToInt64() + i * sidAndAttrSize + IntPtr.Size),
                        typeof(SID_AND_ATTRIBUTES));

                    if ((sidAndAttributes.Attributes & SE_GROUP_LOGON_ID) == SE_GROUP_LOGON_ID) {
                        IntPtr pstr = IntPtr.Zero;
                        ConvertSidToStringSid(sidAndAttributes.Sid, out pstr);
                        retVal = Marshal.PtrToStringAuto(pstr);
                        LocalFree(pstr);
                        break;
                    }
                }
                Marshal.FreeHGlobal(TokenInformation);
                return retVal;
            }
        }
    }
}
