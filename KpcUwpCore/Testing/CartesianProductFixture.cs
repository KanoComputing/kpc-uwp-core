/**
 * CartesianProductFixture.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;


namespace KanoComputing.KpcUwpCore.Testing {

    public class CartesianProductFixture : Attribute, ITestDataSource {

        private readonly ITestDataSource fixture1;
        private readonly ITestDataSource fixture2;

        public CartesianProductFixture(Type fixture1, Type fixture2) {
            this.fixture1 = Activator.CreateInstance(fixture1) as ITestDataSource;
            this.fixture2 = Activator.CreateInstance(fixture2) as ITestDataSource;
        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo) {
            IEnumerable<object> fix1Data = this.fixture1.GetData(methodInfo).Select(x => x[0]);
            IEnumerable<object> fix2Data = this.fixture2.GetData(methodInfo).Select(x => x[0]);

            foreach (object data1 in fix1Data) {
                foreach (object data2 in fix2Data) {
                    yield return new object[] {
                        data1, data2
                    };
                }
            }
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data) {
            if (data == null || data.Length < 2) {
                return null;
            }
            return string.Format(
                CultureInfo.CurrentCulture,
                "{0} - ({1}), ({2})",
                methodInfo.Name,
                TestHelpers.GetObjectStr(data[0]),
                TestHelpers.GetObjectStr(data[1])
            );
        }
    }
}
