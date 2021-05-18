/**
 * TestSerialiser.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Serialisation;
using KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.Serialisation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using static KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.Serialisation.UserContracts;


namespace KanoComputing.KpcUwpCore.Tests.Unit.KpcUwpCore.Serialisation {

    [TestClass]
    public class TestSerialiser {

        [DataTestMethod]
        [DynamicData(nameof(SerialiserFixtures.Users),
                     typeof(SerialiserFixtures), DynamicDataSourceType.Method)]
        public void TestDeserialiseJsonToDataContract(string data, LoginSession expected) {
            ISerialiser serialiser = new Serialiser();

            LoginSession actual = serialiser.DeserialiseJsonToDataContract<LoginSession>(data);

            Assert.AreEqual(
                expected, actual,
                "Deserialisation did not produce the expected object." +
                $" The returned object serialised is:" +
                $" {serialiser.SerialiseDataContractToJson<LoginSession>(actual)}" +
                $" The given object serialised is: " +
                $" {serialiser.SerialiseDataContractToJson<LoginSession>(expected)}");
        }

        [DataTestMethod]
        [DynamicData(nameof(SerialiserFixtures.Users),
                     typeof(SerialiserFixtures), DynamicDataSourceType.Method)]
        public void TestSerialiseDataContractToJson(string expected, LoginSession obj) {
            ISerialiser serialiser = new Serialiser();

            string actual = serialiser.SerialiseDataContractToJson<LoginSession>(obj);

            Assert.AreEqual(
                JObject.Parse(expected).ToString(),
                JObject.Parse(actual).ToString(),
                "The two serialised JSONs are not equivalent.");
        }
    }
}
