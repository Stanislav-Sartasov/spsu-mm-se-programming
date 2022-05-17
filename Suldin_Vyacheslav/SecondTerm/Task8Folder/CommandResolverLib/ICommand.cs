using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandResolverLib
{
    public interface ICommand
    {
        public bool IsExit();
        public void Run();
        public void SetStdIn(string stdIn);
        public string GetStdOut();
        public string GetErrorMessage();
        public int GetErrorCode();        
    }
}
