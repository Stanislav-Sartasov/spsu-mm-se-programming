using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherClasses
{
    public interface IRequestURLFiller
    {
        public void FillRequestURL(IWebServerHelper webHelper);
    }
}
