/**
 * ISerialiser.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


namespace KanoComputing.KpcUwpCore.Serialisation {

    public interface ISerialiser {

        Contract DeserialiseJsonToDataContract<Contract>(string serialisedData);
        string SerialiseDataContractToJson<Contract>(object obj);
    }
}
