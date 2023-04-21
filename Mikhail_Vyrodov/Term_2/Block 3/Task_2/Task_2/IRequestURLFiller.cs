using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public interface IRequestURLFiller
    {
        public void FillRequestURL(IWebServerHelper webHelper);
    }
}
