using Player;

namespace BotInterface
{
    public interface IBot : IPlayer
    {
        public string Name { get; }
    }
}