using NUnit.Framework;
using System.Reflection;
using PluginLibrary;

namespace PluginLibrary.UintTests
{
    public class PluginLibraryTests
    {

        [Test]
        public void LibraryLoaderTest()
        {
            // Invalid path
            LibraryLoader testLoader = new LibraryLoader("invalid path");
            Assert.AreEqual(testLoader.LoadLibrary(), null);
        }

        [Test]
        public void PluginHelperTest()
        {
            LibraryLoader testLoader = new LibraryLoader("../../../../BotsLibrary/BotsLibrary.dll");
            Assembly asm = testLoader.LoadLibrary();
            PluginHelper testHelper = new PluginHelper(asm);
            // Invalid parameters in creating player
            string errorMessage = "Constructor on type 'BotsLibrary.CardsCounterStrategy' not found.";
            testHelper.CreatePlayer(null, 0);
            Assert.AreEqual(errorMessage, testHelper.LastExceptionMessage);
        }
    }
}