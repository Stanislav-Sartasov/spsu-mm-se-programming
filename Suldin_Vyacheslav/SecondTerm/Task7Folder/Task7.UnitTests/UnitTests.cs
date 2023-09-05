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

namespace Task7.UnitTests
{
    public class UnitTests
    {
        [SetUp]
        public void Setup()
        {
        }
       
        [Test]
        public void LinksTest()
        {

            var services = new List<JSONParser> { new TomorrowIOParser(""), new OpenWeatherParser(""), new StormGlassParser(""), new GisMeteoParser("") };

            foreach (var service in services)
            {
                service.SetKey("Key");
                var link = service.Link;
                if (!link.Contains("https://api.")) Assert.Fail();
                else
                {
                    if (service.Headers != null)
                    {
                        if (!service.Headers[0].Contains("Key")) Assert.Fail();
                    } 
                    else if (!link.Contains("Key")) Assert.Fail();
                }
                var header = service.Headers;
            }

            Assert.Pass();
        }

        [Test]
        public void GetRequestTets()
        {

            string[] links = new string[] { "https://www.amazon.com/", "https://key-seo.com/404", "http://hwproj.me/courses/65", null };
            List<string>[] headers = new List<string>[] { new List<string>() { "key:header" }, null };

            foreach (string link in links)
            {
                foreach (var header in headers)
                {
                    GetRequest gr = new GetRequest(link, header);
                    string statement = gr.Send();
                    switch (link)
                    {
                        case "https://www.amazon.com/":
                            {
                                if (statement != "AllFine" || gr.GetResponce() == null)
                                {
                                    Assert.Fail();
                                }
                                break;
                            }
                        case "https://key-seo.com/404":
                            {
                                if (statement.Contains(ErrorType.NotFound.ToString()))
                                {
                                    Assert.Fail();
                                }
                                break;
                            }
                        case "http://hwproj.me/courses/65":
                            {
                                if (statement.Contains(ErrorType.BadGateway.ToString()))
                                {
                                    Assert.Fail();
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

            }
            Assert.Pass();
        }

    }
}