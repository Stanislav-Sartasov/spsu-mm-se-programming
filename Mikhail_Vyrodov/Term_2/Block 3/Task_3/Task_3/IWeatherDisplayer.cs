using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public interface IWeatherDisplayer
    {
        public bool DisplayWeather();

        public string Answer { get; }
        public ConsoleWriter Writer { get; }
        public IWebServerHelper WebHelper { get; }
        public IResponseReader RespReader { get; }
    }
}
