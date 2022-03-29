using Cards;

namespace BotStructure
{
    public interface IBot
    { 
        int Balance { get; set; }
        List<Hand> Hands { get; set; }
        void Play(Card croupierOpenCard, Shoes shoes);
    }
}