using GameTable.BetCreator;
using GameTable.TitleEnum;


namespace GameTable.BotStructure
{
    public abstract class ChoosingBot : CommonBot
    {
        static Random rnd = new Random();
        public BetHeader betHeader { get; set; }
        private protected int cash;
        public ChoosingBot(string name, int balance, int counter) : base(name, balance, counter)
        {
            if (name == "WideStrideBot" || name == "ThomasDonaldBot" || name == "ProgressionSeriesBot")
            {
                ChooseTitle("EqualChances");
            }
            else
            {
                var value = rnd.RandomEnumVal<TitlesEnum>();
                ChooseTitle(value.ToString());
            }
        }

        public void ChooseTitle(string title)
        {
            IBetCreator creator = Choosing(title);
            betHeader = new BetHeader(creator);
        }

        public IBetCreator Choosing(string title)
        {
            return title switch
            {
                "EqualChances" => new EqualChancesCreator(),
                "Dozen" => new DozenBetCreator(),
                "Number" => new NumberBetCreator(),
                _ => throw new NotSupportedException(),
            };

        }
    }
}
