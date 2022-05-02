using System;

namespace BABASH
{
    public class ECHOCommand : Command
    {
        public ECHOCommand(string[] args, Session commandSession)
        {
            session = commandSession;
            Name = "echo";
            parametres = args;
        }

        public override void Execute()
        {
            foreach (string arg in parametres)
            {
                stdOut += " " + arg;
            }
            stdOut = stdOut == null ? "": stdOut[1..];
        }
    }
}