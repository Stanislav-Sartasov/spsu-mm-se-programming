using System.Reflection;
using CasinoLib;

namespace BotPluginsLib
{
    public class BotPluginLoader
    {
        public List<BotPlayer> LoadedBots { get; private set; }

        public BotPluginLoader(String directoryPath, Int64 startBalance)
        {
            LoadedBots = new List<BotPlayer>();

            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine("Папка не найдена. Боты не были добавлены.");
                return;
            }
            Console.WriteLine("Папка была найдена. Переходим к поиску плагинов...");

            String[] dllFiles = Directory.GetFiles(directoryPath, "*.dll");

            if (dllFiles.Length == 0)
            {
                Console.WriteLine("В папке не было найдено плагинов с расширением .dll, боты не были добавлены.");
                return;
            }

            Console.WriteLine("Найдено .dll файлов: "+dllFiles.Length.ToString());

            foreach (String dllFile in dllFiles)
            {
                Assembly asm = Assembly.LoadFrom(dllFile);
                foreach (var type in asm.GetTypes())
                {
                    if (type.BaseType == typeof(BotPlayer))
                    {
                        object[] args = new object[] {startBalance, };
                        LoadedBots.Add((BotPlayer) Activator.CreateInstance(type, args));
                    }
                }
            }
        }
    }
}