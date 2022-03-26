namespace CasinoLib
{
    public abstract class Player
    {
        public Int64 Balance { get; set; }

        public Player(Int64 money = 0)
        {
            Balance = money;
        }

        //Вернёт true, если игра удалась и false, если баланс игрока = 0 или его недостаточно для реализации стратегии
        public abstract bool PlaceBetAndPlay(Roulette game);
    }
}
