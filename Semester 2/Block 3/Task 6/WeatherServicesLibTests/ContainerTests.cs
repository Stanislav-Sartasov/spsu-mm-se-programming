using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WeatherServicesLib;

namespace WeatherServicesLibTests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void GetAvailableServicesListTest()
        {
            var container = new Container(new List<WeatherServices> { WeatherServices.Stormglass });
            var services = container.GetAvailableServicesList();

            bool b = true;
            foreach (var service in services)
            {
                if (service.Name != WeatherServices.Stormglass)
                {
                    b = false;
                }
            }

            Assert.IsTrue(b);
        }
    }
}
