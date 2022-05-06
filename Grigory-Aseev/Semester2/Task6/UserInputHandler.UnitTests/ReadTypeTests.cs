using NUnit.Framework;
using Sites;
using System;

namespace UserInputHandler.UnitTests
{
    public class ReadTypeTests
    {

        [Test]
        public void ReadGoodTypesTest()
        {
            Assert.AreEqual((typeof(OpenWeatherMap), false), Task6.UserInputHandler.ReadType("openweathermap"));
            Assert.AreEqual((typeof(TomorrowIO), false), Task6.UserInputHandler.ReadType("TomorrowIO"));
            Assert.AreEqual((typeof(StormGlassIO), false), Task6.UserInputHandler.ReadType("        StormglASSio    "));
        }

        [Test]
        public void ReadBadTypesTest()
        {
            Type? type;
            bool condition;
            (type, condition) = Task6.UserInputHandler.ReadType("lalalaal");
            Assert.IsFalse(condition);
            Assert.IsNull(type);

            (type, condition) = Task6.UserInputHandler.ReadType("exit");
            Assert.IsTrue(condition);
            Assert.IsNull(type);

            (type, condition) = Task6.UserInputHandler.ReadType("   exIt           ");
            Assert.IsTrue(condition);
            Assert.IsNull(type);
        }
    }
}