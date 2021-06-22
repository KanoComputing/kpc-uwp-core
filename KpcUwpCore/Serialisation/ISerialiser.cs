/**
 * ISerialiser.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System.Collections.Generic;


namespace KanoComputing.KpcUwpCore.Serialisation {

    public interface ISerialiser {

        Contract DeserialiseJsonToDataContract<Contract>(string serialisedData);
        string SerialiseDataContractToJson<Contract>(Contract contract);

        bool DataContractAbidesByJsonSchema<Contract>(
            Contract contract, string jsonSchema, out IList<string> errors);
        bool SerialisedDataContractAbidesByJsonSchema(
            string serialisedData, string jsonSchema, out IList<string> errors);
    }
}
