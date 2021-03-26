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
using Windows.System.Profile;


namespace KanoComputing.KpcUwpCore.PlatformDetection {

    public class PlatformIdentifiers : IPlatformIdentifiers {

        /// <summary>
        /// Returns an SID specific to the device and publisher of the calling app.
        /// The string value is hashed before returning.
        /// </summary>
        public string GetDeviceId() {
            return this.Hash(
                CryptographicBuffer.EncodeToBase64String(
                    SystemIdentification.GetSystemIdForPublisher().Id));
        }

        /// <summary>
        /// Returns an SID specific to the current OS user.
        /// The string value is hashed before returning.
        /// </summary>
        public string GetUserId() {
            return this.Hash(
                WindowsIdentity.GetCurrent().User.ToString());
        }

        /// <summary>
        /// Returns an SID specific to the current logon session for the current user.
        /// The string value is hashed before returning.
        /// </summary>
        public string GetSessionId() {
            return this.Hash(
                WindowsLogonSession.GetSid());
        }

        private string Hash(string data) {
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

            // The SID structure that identifies the user that is currently associated
            // with the specified object. If no user is associated with the object,
            // the value returned in the buffer pointed to by lpnLengthNeeded is zero.
            // Note that SID is a variable length structure. You will usually make a
            // call to GetUserObjectInformation to determine the length of the SID
            // before retrieving its value.
            private const int UOI_USER_SID = 4;

            // Retrieves the thread identifier of the calling thread.
            [DllImport("kernel32.dll")]
            private static extern int GetCurrentThreadId();

            // Retrieves a handle to the desktop assigned to the specified thread.
            [DllImport("user32.dll")]
            private static extern IntPtr GetThreadDesktop(int dwThreadId);

            // Retrieves information about the specified window station or desktop object.
            [DllImport("user32.dll")]
            private static extern bool GetUserObjectInformation(
                IntPtr hObj,
                int nIndex,
                [MarshalAs(UnmanagedType.LPArray)] byte[] pvInfo,
                int nLength,
                out uint lpnLengthNeeded);

            // Converts a security identifier (SID) to a string format suitable for
            // display, storage, or transmission.
            [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
            private static extern bool ConvertSidToStringSid(
                [MarshalAs(UnmanagedType.LPArray)] byte[] pSID,
                out IntPtr ptrSid);

            /// <summary>
            /// Returns the Logon Session SID string.
            /// </summary>
            public static string GetSid() {
                byte[] buffer = new byte[100];
                IntPtr ptrSid;
                string sidString = "";
                uint lengthNeeded;

                IntPtr hdesk = GetThreadDesktop(GetCurrentThreadId());
                GetUserObjectInformation(hdesk, UOI_USER_SID, buffer, 100, out lengthNeeded);
                if (!ConvertSidToStringSid(buffer, out ptrSid))
                    throw new System.ComponentModel.Win32Exception();

                try {
                    sidString = Marshal.PtrToStringAuto(ptrSid);
                } catch {
                }
                return sidString;
            }
        }
    }
}
