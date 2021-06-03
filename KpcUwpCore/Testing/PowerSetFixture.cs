/**
 * PowerSetFixture.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;


namespace KanoComputing.KpcUwpCore.Testing {

    public class PowerSetFixture : Attribute, ITestDataSource {

        private readonly ITestDataSource fixture;
        private readonly Type dataType;

        public PowerSetFixture(Type fixture, Type dataType) {
            this.fixture = Activator.CreateInstance(fixture) as ITestDataSource;
            this.dataType = dataType;
        }

        public IEnumerable<object[]> GetData(MethodInfo methodInfo) {
            IEnumerable<IEnumerable<object>> powerSet = TestHelpers.GetPowerSet<object>(
                this.fixture.GetData(methodInfo).Select(x => x[0])
            );

            foreach (IEnumerable<object> objs in powerSet) {
                // We do all this to allow implicit conversion of the output to List<DataType>
                Type genericListType = typeof(List<>).MakeGenericType(this.dataType);
                IList retObjs = (IList)Activator.CreateInstance(genericListType);

                foreach (object obj in objs) {
                    retObjs.Add(obj);
                }

                yield return new object[] { retObjs };
            }
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data) {
            if (data == null || data.Length == 0)
                return null;

            return string.Format(
                CultureInfo.CurrentCulture,
                "{0} - ({1})",
                methodInfo.Name,
                TestHelpers.GetObjectStr(data[0])
            );
        }
    }
}
