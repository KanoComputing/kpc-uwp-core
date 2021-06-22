/**
 * Serialiser.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;
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
        /// response gets deserialised into.</typeparam>
        /// <param name="serialisedData">A JSON object serialised to a string.</param>
        /// <returns>The JSON object deserialised to a Contract object.</returns>
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
        /// serialised.</typeparam>
        /// <param name="contract">An object instance of the type provided.</param>
        /// <returns>A JSON string serialisation of the object instance provided.</returns>
        public string SerialiseDataContractToJson<Contract>(Contract contract) {
            var serialiser = new DataContractJsonSerializer(typeof(Contract));
            using (var stream = new MemoryStream()) {
                serialiser.WriteObject(stream, contract);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Validate a DataContract object against a given JSON schema.
        /// </summary>
        /// <typeparam name="Contract">The type of the [DataContract] object being
        /// serialised.</typeparam>
        /// <param name="contract">An object instance of the type provided.</param>
        /// <param name="jsonSchema">A JSON schema to validate the object against.</param>
        /// <param name="errors">Validation error messages.</param>
        /// <returns>Whether the DataContract respects the JSON schema when
        /// serialised.</returns>
        public bool DataContractAbidesByJsonSchema<Contract>(
            Contract contract, string jsonSchema, out IList<string> errors) {

            return this.SerialisedDataContractAbidesByJsonSchema(
                this.SerialiseDataContractToJson<Contract>(contract),
                jsonSchema,
                out errors);
        }

        /// <summary>
        /// Validate a serialised DataContract object against a given JSON schema.
        /// </summary>
        /// <param name="serialisedData">A JSON object serialised to a string.</param>
        /// <param name="jsonSchema">A JSON schema to validate the data against.</param>
        /// <param name="errors">Validation error messages.</param>
        /// <returns>Whether the serialised DataContract respects the JSON schema.</returns>
        public bool SerialisedDataContractAbidesByJsonSchema(
            string serialisedData, string jsonSchema, out IList<string> errors) {

            JObject data = JObject.Parse(serialisedData);

#pragma warning disable CS0618  // Type or member is obsolete
            JsonSchema schema = JsonSchema.Parse(jsonSchema);
            bool result = data.IsValid(schema, out errors);
#pragma warning restore CS0618 // Type or member is obsolete

            return result;
        }
    }
}
