/**
 * FeaturePriorityFixtures.cs
 *
 * Copyright (c) 2021 Kano Computing Ltd.
 * License: https://opensource.org/licenses/MIT
 */


using System;
using System.Collections.Generic;


namespace KanoComputing.KpcUwpCore.Tests.Fixtures.KpcUwpCore.Features {

    public class FeaturePriorityFixtures {

        /// <returns>
        /// PriorityList, InstallationStatus, CurrentApplication, HasPriority
        /// </returns>
        public static IEnumerable<object[]> PriorityListWithStatus() {
            // 1
            yield return new object[] {
                new List<Tuple<string, Uri>> {
                    new Tuple<string, Uri>("Company.App1_qwer", new Uri("app1:")),
                    new Tuple<string, Uri>("Company.App2_asdf", new Uri("app2:")),
                    new Tuple<string, Uri>("Company.App3_zxcv", new Uri("app3:")),
                    new Tuple<string, Uri>("Company.App4_tyui", new Uri("app4:"))
                },
                new Dictionary<string, bool> {
                    { "Company.App1_qwer", true },
                    { "Company.App2_asdf", true },
                    { "Company.App3_zxcv", false },
                    { "Company.App4_tyui", false },
                },
                "Company.App1_qwer",
                true
            };
            // 2
            yield return new object[] {
                new List<Tuple<string, Uri>> {
                    new Tuple<string, Uri>("Company.App1_qwer", new Uri("app1:")),
                    new Tuple<string, Uri>("Company.App2_asdf", new Uri("app2:")),
                    new Tuple<string, Uri>("Company.App3_zxcv", new Uri("app3:")),
                    new Tuple<string, Uri>("Company.App4_tyui", new Uri("app4:"))
                },
                new Dictionary<string, bool> {
                    { "Company.App1_qwer", false },
                    { "Company.App2_asdf", true },
                    { "Company.App3_zxcv", false },
                    { "Company.App4_tyui", false },
                },
                "Company.App1_qwer",
                false
            };
            // 3
            yield return new object[] {
                new List<Tuple<string, Uri>> {
                    new Tuple<string, Uri>("Company.App1_qwer", new Uri("app1:")),
                    new Tuple<string, Uri>("Company.App2_asdf", new Uri("app2:")),
                    new Tuple<string, Uri>("Company.App3_zxcv", new Uri("app3:")),
                    new Tuple<string, Uri>("Company.App4_tyui", new Uri("app4:"))
                },
                new Dictionary<string, bool> {
                    { "Company.App1_qwer", true },
                    { "Company.App2_asdf", true },
                    { "Company.App3_zxcv", false },
                    { "Company.App4_tyui", false },
                },
                "Company.App2_asdf",
                false
            };
            // 4
            yield return new object[] {
                new List<Tuple<string, Uri>> {
                    new Tuple<string, Uri>("Company.App1_qwer", new Uri("app1:")),
                    new Tuple<string, Uri>("Company.App2_asdf", new Uri("app2:")),
                    new Tuple<string, Uri>("Company.App3_zxcv", new Uri("app3:")),
                    new Tuple<string, Uri>("Company.App4_tyui", new Uri("app4:"))
                },
                new Dictionary<string, bool> {
                    { "Company.App1_qwer", false },
                    { "Company.App2_asdf", true },
                    { "Company.App3_zxcv", false },
                    { "Company.App4_tyui", false },
                },
                "Company.App2_asdf",
                true
            };
            // 5
            yield return new object[] {
                new List<Tuple<string, Uri>> {
                    new Tuple<string, Uri>("Company.App1_qwer", new Uri("app1:")),
                    new Tuple<string, Uri>("Company.App2_asdf", new Uri("app2:")),
                    new Tuple<string, Uri>("Company.App3_zxcv", new Uri("app3:")),
                    new Tuple<string, Uri>("Company.App4_tyui", new Uri("app4:"))
                },
                new Dictionary<string, bool> {
                    { "Company.App1_qwer", false },
                    { "Company.App2_asdf", false },
                    { "Company.App3_zxcv", false },
                    { "Company.App4_tyui", false },
                },
                "Company.App4_tyui",
                false
            };
            // 6
            yield return new object[] {
                new List<Tuple<string, Uri>> {
                    new Tuple<string, Uri>("Company.App1_qwer", new Uri("app1:")),
                    new Tuple<string, Uri>("Company.App2_asdf", new Uri("app2:")),
                    new Tuple<string, Uri>("Company.App3_zxcv", new Uri("app3:")),
                    new Tuple<string, Uri>("Company.App4_tyui", new Uri("app4:"))
                },
                new Dictionary<string, bool> {
                    { "Company.App1_qwer", false },
                    { "Company.App2_asdf", false },
                    { "Company.App3_zxcv", false },
                    { "Company.App4_tyui", true },
                },
                "Company.App4_tyui",
                true
            };
            // 7
            yield return new object[] {
                new List<Tuple<string, Uri>> {
                    new Tuple<string, Uri>("Company.App1_qwer", new Uri("app1:")),
                    new Tuple<string, Uri>("Company.App2_asdf", new Uri("app2:")),
                    new Tuple<string, Uri>("Company.App3_zxcv", new Uri("app3:")),
                    new Tuple<string, Uri>("Company.App4_tyui", new Uri("app4:"))
                },
                new Dictionary<string, bool> {
                    { "Company.App1_qwer", true },
                    { "Company.App2_asdf", true },
                    { "Company.App3_zxcv", false },
                    { "Company.App4_tyui", true },
                },
                "Company.App5_ghjk",
                false
            };
        }
    }
}
