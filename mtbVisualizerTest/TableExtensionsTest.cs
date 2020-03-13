﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MtbVisualizer.ViewModels;

namespace MtbVisualizerTest
{
    [TestClass]
    public class TableExtensionsTest
    {
        [TestMethod]
        public void Test_ToStringWithOptionalHours_40_Minutes()
        {
            double seconds = 2400;
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

            var result = timeSpan.ToStringWithOptionalHours();

            Assert.AreEqual("40:00", result);
        }

        [TestMethod]
        public void Test_ToStringWithOptionalHours_2_Hours_40_Minutes()
        {
            double seconds = 9600;
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

            var result = timeSpan.ToStringWithOptionalHours();

            Assert.AreEqual("2:40:00", result);
        }
    }
}
