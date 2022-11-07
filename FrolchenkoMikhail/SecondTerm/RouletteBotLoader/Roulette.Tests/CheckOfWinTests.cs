using NUnit.Framework;


namespace Roulette.Tests
{
	public class CheckOfWinTests
	{
		[Test]
		public void HitTheSectorTest()
		{
			Game.SpintheDrum();
			int min = (Game.victoryCell - 1) % 37;
			int max = (Game.victoryCell + 1) % 37;
			Assert.IsTrue(CheckOfWin.HitTheSector(min, max));

			Assert.Pass();
		}

		[Test]
		public void CheckofWinTest()
		{
			for (int victoryCell = 1; victoryCell < 37; victoryCell++)
			{
				Game.victoryCell = victoryCell;
				Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Single, victoryCell));
				Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Split, victoryCell));
				Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Split, victoryCell - 1));

				if (((victoryCell < 10 || victoryCell > 18 && victoryCell < 28) && victoryCell % 2 == 1) ||
								((victoryCell > 11 && victoryCell < 19 || victoryCell > 29) && victoryCell % 2 == 0))
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Red, victoryCell));
				else
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Black, victoryCell));
				if (victoryCell % 2 == 0)
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Even, victoryCell));
				else
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Odd, victoryCell));
				if (0 < victoryCell && victoryCell < 13)
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.FirstDozen, victoryCell));
				else if (13 <= victoryCell && victoryCell < 25)
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.SecondDozen, victoryCell));
				else
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.ThirdDozen, victoryCell));
			}
			Assert.Pass();
		}

		[Test]
		public void GetCoefficientTest()
		{
			for (int typeOfBet = 0; typeOfBet < 12; typeOfBet++)
			{
				if ((TypeOfBet)typeOfBet >= TypeOfBet.FirstDozen)
					Assert.AreEqual(2, CheckOfWin.GetCoefficient((TypeOfBet)typeOfBet));
				else if ((TypeOfBet)typeOfBet >= TypeOfBet.Red)
					Assert.AreEqual(1, CheckOfWin.GetCoefficient((TypeOfBet)typeOfBet));
				else if ((TypeOfBet)typeOfBet == TypeOfBet.Basket)
					Assert.AreEqual(11, CheckOfWin.GetCoefficient((TypeOfBet)typeOfBet));
				else if ((TypeOfBet)typeOfBet == TypeOfBet.Split)
					Assert.AreEqual(18, CheckOfWin.GetCoefficient((TypeOfBet)typeOfBet));
				else 
					Assert.AreEqual(35, CheckOfWin.GetCoefficient((TypeOfBet)typeOfBet));
			}

			Assert.Pass();
		}
	}
}