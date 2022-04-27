using NUnit.Framework;
using PluginLibrary;
using System.Reflection;
using DecksLibrary;

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
            // Invalid player Type in creating player
            string errorMessage = "Value cannot be null. (Parameter 'type')";
            testHelper.CreatePlayer("invalid type", null);
            Assert.AreEqual(errorMessage, testHelper.LastExceptionMessage);

            // Invalid parameters in implementing method
            Decks playingCards = new Decks();
            playingCards.FillCards();
            testHelper.CreatePlayer("Player", new object[] { (byte)8, (uint)1600, playingCards, (uint)16 });

            testHelper.ImplementMethod("FillAttrs", null);
            errorMessage = "Parameter count mismatch.";
            Assert.AreEqual(errorMessage, testHelper.LastExceptionMessage);

            // Incorrect property name in receiving property
            Assert.IsNull(testHelper.ReceiveProperty("incorrect name"));
            errorMessage = "Object reference not set to an instance of an object.";
            Assert.AreEqual(errorMessage, testHelper.LastExceptionMessage);
        }
    }
}