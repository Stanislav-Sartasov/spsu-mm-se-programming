using System.Collections.Generic;
using Core;

namespace CommandResolverLib
{
    public interface ICommandCreator
    {
        ICommand Create(string commandLine);

        IReadOnlyDictionary<string, string> GetLocalVariables();
        ICommand CreateSTD(string[] complex);
    }
}
