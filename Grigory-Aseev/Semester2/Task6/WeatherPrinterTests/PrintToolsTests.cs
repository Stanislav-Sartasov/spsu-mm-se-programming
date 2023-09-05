using NUnit.Framework;
using SiteInterfaces;
using System.Collections.Generic;
using Sites;
using System.Reflection;

namespace PrintTools.UnitTests
{
    public class PrintToolsTests
    {
        private List<ISite> sites;

        [SetUp]
        public void Setup()
        {
            sites = new List<ISite>();
            sites.Add(new OpenWeatherMap());
            sites.Add(new TomorrowIO());
            sites.Add(new StormGlassIO());
        }

        [Test]
        public void PrintGoodData()
        {
            ProcessSites(sites);
        }

        [Test]
        public void PrintBadData()
        {
            for (int i = 0; i < 3; i++)
            {
                typeof(ASite).GetProperty("url", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(sites[i], "HAHAHAHAAHHAAHAHAHHAH");
            }

            ProcessSites(sites);
        }

        private void ProcessSites(List<ISite> sites)
        {
            for (int i = 0; i < 3; i++)
            {
                sites[i].GetRequest();
                sites[i].Parse();
            }
            Task6.PrintTools.PrintForecast(sites);
        }

        [Test]
        public void PrintCentrallyTest()
        {
            Task6.PrintTools.PrintCentrally("lalala");
        }
    }
}