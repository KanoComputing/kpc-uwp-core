/**
 * SerialiserFixtures.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System.Collections.Generic;
using static KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.Serialisation.UserContracts;


namespace KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.Serialisation {

    public class SerialiserFixtures {

        public static IEnumerable<object[]> Users() {
            yield return new object[] {
                "{" +
                "    \"attributes\": null," +
                "    \"id\": \"Something\"," +
                "    \"loot\": null" +
                "}",
                new LoginSession {
                    Id = "Something"
                }
            };
            yield return new object[] {
                "{" +
                "    \"attributes\": {" +
                "        \"consent\": true," +
                "        \"time\": 100" +
                "    }," +
                "    \"id\": \"Unique\"," +
                "    \"loot\": null" +
                "}",
                new LoginSession {
                    Id = "Unique",
                    Attributes = new UserAttributes {
                        Consent = true,
                        Time = 100
                    }
                }
            };
            yield return new object[] {
                "{" +
                "   \"attributes\": {" +
                "       \"consent\": false," +
                "       \"time\": 9001" +
                "   }," +
                "   \"id\": \"MarryHadALittleLamb\"," +
                "   \"loot\": {" +
                "       \"items\": [" +
                "           {" +
                "               \"name\": \"Hat\"," +
                "               \"value\": 10" +
                "           }," +
                "           {" +
                "               \"name\": \"Glasses\"," +
                "               \"value\": 15" +
                "           }" +
                "       ]" +
                "    }" +
                "}",
                new LoginSession {
                    Id = "MarryHadALittleLamb",
                    Attributes = new UserAttributes {
                        Consent = false,
                        Time = 9001
                    },
                    Loot = new LootList {
                        Items = new Loot[] {
                            new Loot {
                                Name = "Hat",
                                Value = 10
                            },
                            new Loot {
                                Name = "Glasses",
                                Value = 15
                            }
                        }
                    }
                }
            };
        }
    }
}
