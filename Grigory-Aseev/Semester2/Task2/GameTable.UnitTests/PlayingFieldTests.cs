using NUnit.Framework;

namespace GameTable.UnitTests
{
    public class PlayingFieldTests
    {
        Bots.StandartBot bot = new Bots.StandartBot();

        [Test]
        public void CreateTest()
        {
            PlayingField field = new PlayingField(bot);

            Assert.AreEqual(100, field.MinBet);
            Assert.AreEqual(8, field.NumberOfDecks);
        }

        [Test]
        public void PlayTest()
        {
            PlayingField field = new PlayingField(bot);
            field.Play();

            Assert.AreEqual(PlayerStructure.PlayerState.Stop, bot.State);
        }
    }
}
