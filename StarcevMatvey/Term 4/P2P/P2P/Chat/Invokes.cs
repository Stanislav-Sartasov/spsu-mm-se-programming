namespace P2P.Chat
{
    public class Invokes
    {
        public delegate void messengeEvent(string messenge);
        public event messengeEvent MessengeInvoke;

        public Invokes()
        {
            MessengeInvoke = (string x) => { };
        }

        public Invokes(messengeEvent ev)
        {
            MessengeInvoke = ev;
        }

        public Invokes WithEvent(messengeEvent ev) => new Invokes(ev);

        public void Invoke(string x) => MessengeInvoke(x);
    }
}
