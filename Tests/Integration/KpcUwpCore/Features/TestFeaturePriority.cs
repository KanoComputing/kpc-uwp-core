/**
 * TestFeaturePriority.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using KanoComputing.KpcUwpCore.Features;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace KanoComputing.KpcUwpCore.Tests.Integration.KpcUwpCore.Features {

    [TestClass]
    public class TestFeaturePriority {

        [TestMethod]
        public async Task TestHaveFeaturePriorityAsync() {
            IFeaturePriority feature = new FeaturePriority(new List<Tuple<string, Uri>> {
                new Tuple<string, Uri>("Company.App1_qwer", new Uri("app1:")),
                new Tuple<string, Uri>("Company.App2_asdf", new Uri("app2:")),
                new Tuple<string, Uri>("Company.App3_zxcv", new Uri("app3:")),
                new Tuple<string, Uri>("Company.App4_tyui", new Uri("app4:"))
            });
            Assert.IsFalse(
                await feature.HaveFeaturePriorityAsync());
        }
    }
}
