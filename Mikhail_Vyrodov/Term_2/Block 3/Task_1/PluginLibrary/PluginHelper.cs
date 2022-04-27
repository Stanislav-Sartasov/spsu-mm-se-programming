using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using DecksLibrary;

namespace PluginLibrary
{
    public class PluginHelper
    {
        private Assembly assembly = null;
        private Type playerType;
        private object player;

        public string LastExceptionMessage { get; private set; }

        public PluginHelper(Assembly asm)
        {
            assembly = asm;
        }

        public void CreatePlayer(string playerType, object[] parameters)
        {
            try
            {
                this.playerType = assembly.GetType("BotsLibrary." + playerType);
                player = Activator.CreateInstance(this.playerType, parameters);
            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public void ImplementMethod(string methodName, object[] parameters = null)
        {
            try
            {
                MethodInfo method = playerType.GetMethod(methodName);
                method.Invoke(player, parameters);
            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
                Console.WriteLine(ex.Message);
                return;
            }
        }

        public object ReceiveProperty(string propertyName)
        {
            try
            {
                PropertyInfo property = playerType.GetProperty(propertyName);
                object propValue = property.GetValue(player);
                return propValue;
            }
            catch (Exception ex)
            {
                LastExceptionMessage = ex.Message;
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
