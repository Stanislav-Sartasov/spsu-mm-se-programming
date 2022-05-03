using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Core
{
    public class Session 
    {

        public IHandler Handler = new ConsoleHandler();

        private ICommandResolver cr;

        public Session(ICommandResolver commandResolver)
        {
            cr = commandResolver;
        }

        public int Start()
        {
            while (true)
            {
                var line = Handler.GetLine();

                var responce = cr.Resolve(line);
                if (responce == null)
                    break;
                Handler.Show(responce);
            }
            return 0;
        }
    }
}
