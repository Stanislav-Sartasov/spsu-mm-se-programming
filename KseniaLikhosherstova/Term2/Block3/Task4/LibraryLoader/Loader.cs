using System;
using System.Reflection;
using Game.Players;


namespace LibraryLoader
{
    public class Loader
    {
        public static List<Player> Bots;
        public static List<Player> LoadBots(string path)
        {

            Bots = new List<Player>();

            Assembly asm = Assembly.LoadFrom(path); 
            Type[] type = asm.GetExportedTypes();
            for (int i = 0; i < type.Length; i++)
            {
                Type? t = type[i];

                Player bot = (Player)Activator.CreateInstance(t, new object[] { t.Name, 1500 });
                Bots.Add(bot);
            }

            return Bots;
        }
    }
}