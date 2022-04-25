using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Moq;
using WebLibrary;
using Parsers;
using TomorrowIO;
using StormGlass;
using GisMeteo;
using OpenWeather;
using System.Collections.Generic;
using System.Reflection;
using IoC;

namespace Task6.UnitTests
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void JsonHolderTest()
        {
            var test = new JSONParser();
            Assert.AreEqual(null, test.GetAddres());
            Assert.AreEqual(null, test.GetHeaders());
        }

        [Test]
        public void IocConteinerTest()
        {
            var dp = new DependencyResolver();

            dp.GetService<StormGlassParser>("some key");
            
            dp.Remove<TomorrowIOParser>();

            Assert.AreEqual(3, dp.GetParsers().Count);

            dp.Restore<TomorrowIOParser>();

            Assert.AreEqual(4, dp.GetParsers().Count);
        }


    }
}