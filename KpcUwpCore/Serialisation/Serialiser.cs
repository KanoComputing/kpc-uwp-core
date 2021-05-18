/**
 * Serialiser.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;


namespace KanoComputing.KpcUwpCore.Serialisation {

    public class Serialiser : ISerialiser {

        /// <summary>
        /// Deserialises a stringified JSON object into the provided DataContract object.
        /// Throws an exception if deserialisation fails.
        /// </summary>
        /// <typeparam name="Contract">A [DataContract] object into which the JSON
        /// response gets deserialised into</typeparam>
        /// <param name="serialisedData">A JSON object serialised to a string</param>
        /// <returns>The JSON object deserialised to a Contract object</returns>
        public Contract DeserialiseJsonToDataContract<Contract>(string serialisedData) {
            var serialiser = new DataContractJsonSerializer(typeof(Contract));
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(serialisedData))) {
                return (Contract)serialiser.ReadObject(stream);
            }
        }

        /// <summary>
        /// Serialise a DataContract object to a stringified JSON.
        /// Throws an exception if serialisation fails.
        /// </summary>
        /// <typeparam name="Contract">The type of the [DataContract] object being
        /// serialised</typeparam>
        /// <param name="obj">An object instance of the type provided</param>
        /// <returns>A JSON string serialisation of the object instance provided</returns>
        public string SerialiseDataContractToJson<Contract>(object obj) {
            var serialiser = new DataContractJsonSerializer(typeof(Contract));
            using (var stream = new MemoryStream()) {
                serialiser.WriteObject(stream, obj);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }
    }
}
