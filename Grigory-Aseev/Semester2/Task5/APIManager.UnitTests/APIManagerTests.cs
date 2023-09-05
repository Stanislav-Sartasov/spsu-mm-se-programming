using NUnit.Framework;
using System.Collections.Generic;

namespace APIManagerTools.UnitTests
{
    public class APIManagerTests
    {
        private List<string> errors;

        [SetUp]
        public void Setup()
        {
            errors = new List<string>();
            errors.Add("Failed to create a web request at the specified URL, check its correctness.");
            errors.Add("The site is unavailable for some reason or cannot give response.");
            errors.Add("Could not read data from the response stream from the server.");
        }

        [Test]
        public void GetGoodResponseTest()
        {
            string message, url = "https://api.openweathermap.org/data/2.5/weather?id=498817&units=metric&lang=en&appid=b2f3a2c14cb241b45d8dd2adb52381e2";
            System.Exception? e;
            (message, e) = APIManager.GetResponse(url);
            Assert.IsNull(e);
            Assert.IsNotNull(message);
            Assert.IsFalse(errors.Contains(message));
        }

        [Test]
        public void GetBadResponseTest()
        {
            string message, url = "It means that a Black Stalker has appeared in the Zone...";
            System.Exception? e;
            (message, e) = APIManager.GetResponse(url);
            Assert.IsNotNull(e);
            Assert.Contains(message, errors);
        }
    }
}