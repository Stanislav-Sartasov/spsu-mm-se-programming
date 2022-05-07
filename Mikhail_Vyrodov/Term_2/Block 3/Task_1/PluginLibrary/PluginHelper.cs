using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DecksLibrary;
using BlackjackLibrary;

namespace PluginLibrary
{
    public class PluginHelper
    {
        private Assembly assembly = null;
        public IPlayer[] players = new IPlayer[3];

        public string LastExceptionMessage { get; private set; }

        public PluginHelper(Assembly asm)
        {
            assembly = asm;
        }

        public void CreatePlayer(object[] parameters, int playerNumber)
        {
            try
            {
                IPlayer player;
                var types = assembly.GetTypes();
                int i = 0;
                foreach(var type in types)
                {
                    if (type.GetInterface("IPlayer") == typeof(IPlayer))
                    {
                        if (i == playerNumber)
                        {
                            player = (IPlayer)Activator.CreateInstance(type, parameters);
                            players[i] = player;
                            i += 1;
                        }
                        else
                        {
                            i += 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
