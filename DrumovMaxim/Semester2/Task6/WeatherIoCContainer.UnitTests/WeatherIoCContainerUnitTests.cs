using NUnit.Framework;
using WeatherManagerAPI;
using Weather;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IoCContainer.UnitTests
{
    public class WeatherIoCContainerUnitTests
    {
        List<bool> sitesButtonATest, sitesButtonBTest, sitesButtonCTest;

        [SetUp]
        public void Setup()
        {
            sitesButtonATest = new List<bool>() { true, true };
            sitesButtonBTest = new List<bool>() { true, true };
            sitesButtonCTest = new List<bool>() { true, true };
        }

        [Test]
        public void ChooseSitesTest()
        {
            ConsoleKey[] key = new ConsoleKey[] { ConsoleKey.A, ConsoleKey.B, ConsoleKey.C };
            List<bool>[] results = new List<bool>[] { sitesButtonATest, sitesButtonBTest, sitesButtonCTest };

            PrinterMenu.PrintMenu();

            for (int i = 0; i < 3; i++)
            {
                PrinterMenu.ChooseSites(results[i], key[i]);
            }

            Assert.AreEqual(results[0][0], true);
            Assert.AreEqual(results[0][1], false);

            Assert.AreEqual(results[1][0], false);
            Assert.AreEqual(results[1][1], true);

            Assert.AreEqual(results[2][0], true);
            Assert.AreEqual(results[2][1], true);

        }

        [Test]
        public void GetTypesContainerTest()
        {
            var container = new WeatherIoCContainer();
            AManagerAPI testTomorrowIO = new TomorrowIO();
            AManagerAPI testStormGlassIO = new StormGlassIO();
            
            List<AManagerAPI> apiBothSites = container.GetTypesContainer(sitesButtonCTest).ToList();
            PrinterMenu.ChooseSites(sitesButtonBTest, ConsoleKey.B);
            List<AManagerAPI> apiOneSite = container.GetTypesContainer(sitesButtonBTest).ToList();
            
            List<Type> typesBothSites = apiBothSites.Select(Type => Type.GetType()).ToList();
            List<Type> typeOneSite = apiOneSite.Select(Type => Type.GetType()).ToList();

            Assert.IsTrue(typesBothSites.Count == 2);
            Assert.IsTrue(typeOneSite.Count == 1);

            Assert.AreEqual(typesBothSites[0], testTomorrowIO.GetType());
            Assert.AreEqual(typesBothSites[1], testStormGlassIO.GetType());
            Assert.AreEqual(typeOneSite[0], testStormGlassIO.GetType());
        }
    }
}