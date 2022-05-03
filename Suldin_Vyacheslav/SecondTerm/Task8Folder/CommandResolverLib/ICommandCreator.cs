using System.Collections.Generic;
using Core;

namespace CommandResolverLib
{
    public interface ICommandCreator
    {
        ICommand Create(string commandLine);

        string GetLocalVariable(string key);
        ICommand CreateSTD(string[] complex);
    }
}
