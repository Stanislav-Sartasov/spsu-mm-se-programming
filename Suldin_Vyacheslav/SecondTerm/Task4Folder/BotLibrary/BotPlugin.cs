using System;
using System.Collections.Generic;
using System.Reflection;
using BasicLibrary;
using Task4.Sdk;


namespace BotLibrary
{
    public class BotPlugin : IPluginBot
    {
        public List<Gamester> LoadBots(Assembly assembly)
        {
            List<Gamester> bots = new List<Gamester>();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.BaseType.Name != "Bot") continue;
                for (int i = 1; i <= 3; i++)
                {
                    var constructor = type.GetConstructor(new Type[] { typeof(int) });
                    Gamester bot = (Gamester)constructor.Invoke(new object[] { i });
                    bots.Add(bot);
                }
            }

            return bots;
        }
    }
}
