/**
 * UserContracts.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System.Linq;
using System.Runtime.Serialization;


namespace KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.Serialisation {

    /// <summary>
    /// Examples of what data contracts could look like for a "user".
    /// </summary>
    public class UserContracts {

        [DataContract]
        public sealed class LoginSession {

            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "attributes")]
            public UserAttributes Attributes { get; set; }

            [DataMember(Name = "loot")]
            public LootList Loot { get; set; }

            public override bool Equals(object obj) {
                if (obj == null)
                    return false;
                LoginSession other = (LoginSession)obj;
                if (ReferenceEquals(this, other))
                    return true;
                if (this.GetType() != other.GetType())
                    return false;
                return this.Id == other.Id
                    && (ReferenceEquals(this.Attributes, other.Attributes)
                        || this.Attributes != null
                        && this.Attributes.Equals(other.Attributes))
                    && (ReferenceEquals(this.Loot, other.Loot)
                        || this.Loot != null
                        && this.Loot.Equals(other.Loot));
            }

            public override int GetHashCode() {
                return (this.Id, this.Attributes, this.Loot).GetHashCode();
            }
        }

        [DataContract]
        public sealed class UserAttributes {

            [DataMember(Name = "consent")]
            public bool Consent { get; set; }

            [DataMember(Name = "time")]
            public long Time { get; set; }

            public override bool Equals(object obj) {
                if (obj == null)
                    return false;
                UserAttributes other = (UserAttributes)obj;
                if (ReferenceEquals(this, other))
                    return true;
                if (this.GetType() != other.GetType())
                    return false;
                return this.Consent == other.Consent
                    && this.Time == other.Time;
            }

            public override int GetHashCode() {
                return (this.Consent, this.Time).GetHashCode();
            }
        }

        [DataContract]
        public sealed class LootList {

            public LootList() {
                this.Items = new Loot[] { };
            }

            [DataMember(Name = "items")]
            public Loot[] Items { get; set; }

            public override bool Equals(object obj) {
                if (obj == null)
                    return false;
                LootList other = (LootList)obj;
                if (ReferenceEquals(this, other))
                    return true;
                if (this.GetType() != other.GetType())
                    return false;
                return this.Items.SequenceEqual(other.Items);
            }

            public override int GetHashCode() {
                return this.Items.GetHashCode();
            }
        }

        [DataContract]
        public sealed class Loot {

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "value")]
            public int Value { get; set; }

            public override bool Equals(object obj) {
                if (obj == null)
                    return false;
                Loot other = (Loot)obj;
                if (ReferenceEquals(this, other))
                    return true;
                if (this.GetType() != other.GetType())
                    return false;
                return this.Name == other.Name
                    && this.Value == other.Value;
            }

            public override int GetHashCode() {
                return (this.Name, this.Value).GetHashCode();
            }
        }
    }
}
