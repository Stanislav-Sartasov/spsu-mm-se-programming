using SessionManager;

namespace BashTask
{
    static class Program
    {
        static void Main()
        {
            Session session = new Session();

            session.Start();
        }
    }
}