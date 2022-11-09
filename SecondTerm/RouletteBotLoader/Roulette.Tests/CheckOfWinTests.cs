using NUnit.Framework;

namespace Roulette.Tests
{
	public class CheckOfWinTests
	{
		[Test]
		public void HitTheSectorTest()
		{
			Game.SpintheDrum();
			int min = (Game.VictoryCell - 1) % 37;
			int max = (Game.VictoryCell + 1) % 37;
			Assert.IsTrue(CheckOfWin.HitTheSector(min, max));

			Assert.Pass();
		}

		[Test]
		public void CheckofWinTest()
		{
			for (int VictoryCell = 1; VictoryCell < 37; VictoryCell++)
			{
				Game.VictoryCell = VictoryCell;
				Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Single, VictoryCell));
				Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Split, VictoryCell));
				Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Split, VictoryCell - 1));

				if (((VictoryCell < 10 || VictoryCell > 18 && VictoryCell < 28) && VictoryCell % 2 == 1) ||
								((VictoryCell > 11 && VictoryCell < 19 || VictoryCell > 29) && VictoryCell % 2 == 0))
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Red, VictoryCell));
				else
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Black, VictoryCell));
				if (VictoryCell % 2 == 0)
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Even, VictoryCell));
				else
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.Odd, VictoryCell));
				if (0 < VictoryCell && VictoryCell < 13)
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.FirstDozen, VictoryCell));
				else if (13 <= VictoryCell && VictoryCell < 25)
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.SecondDozen, VictoryCell));
				else
					Assert.IsTrue(CheckOfWin.CheckofWin(TypeOfBet.ThirdDozen, VictoryCell));
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