/**
 * TestHelpers.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;


namespace KanoComputing.KpcUwpCore.Testing {

    public class TestHelpers {

        public static IEnumerable<IEnumerable<T>> GetPowerSet<T>(IEnumerable<T> input) {
            var seed = new List<IEnumerable<T>>() { Enumerable.Empty<T>() }
              as IEnumerable<IEnumerable<T>>;

            return input.Aggregate(seed, (a, b) =>
              a.Concat(a.Select(x => x.Concat(new List<T>() { b }))));
        }

        public static string GetObjectStr(object obj) {
            if (obj is IEnumerable<object>) {
                IEnumerable<object> listObj = obj as IEnumerable<object>;
                return String.Join(",", listObj);
            }
            return obj.ToString();
        }

        public static IEnumerable<T> UnpackFixtureData<T>(ITestDataSource fixture, Action method) where T : class {
            return fixture.GetData(method.Method)
                .Select(x => x[0] as T);
        }

        public static void AssertJObjectContains<T>(JObject obj, string key, T prop, bool isRequired = false) {
            if (!obj.ContainsKey(key)) {
                Assert.IsFalse(isRequired, $"The key {key} is required but not present in the JObject");
                Assert.AreEqual(default, prop, $"{key} was not in the JObject but wasn't default in the object");
                return;
            }
            Assert.AreEqual(obj[key].ToObject<T>(), prop, $"{key} didn't match");
        }
    }
}
