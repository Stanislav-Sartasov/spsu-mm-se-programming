using System;
using System.Collections.Generic;
using System.Reflection;
using BasicLibrary;

namespace Task4.sdk
{
    public interface IPluginBot
    {
        public List<Gamester> LoadBots(Assembly assembly);
    }
}
