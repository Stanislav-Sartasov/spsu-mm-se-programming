using Bash;

namespace BashDemo
{
    public static class Program
    {
        public static void Main()
        {
            IBash bash = new BashSession();
            bash.Start();
        }
    }

}


