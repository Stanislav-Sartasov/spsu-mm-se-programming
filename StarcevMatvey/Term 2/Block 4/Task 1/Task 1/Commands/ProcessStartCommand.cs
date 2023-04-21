using System.Diagnostics;

namespace Task_1.Commands
{
    public class ProcessStartCommand : IExecutable
    {
        public ProcessStartCommand()
        {

        }

        public MyBash Execute(MyBash bash)
        {
            var b = new MyBash(bash);
            var a = b.CurrentArguments.Last();
            var arg = b.GetLocalVar(a);
            var path = b.Path + arg;
            var process = new Process();
            try
            {
                process.StartInfo.FileName = path;
                process.Start();
                process.WaitForExit();
            }
            catch
            {
                try
                {
                    process.StartInfo.FileName = path + ".exe";
                    process.Start();
                    process.WaitForExit();
                }
                catch
                {
                    var errors = b.GetErrorMessenges();
                    errors.Add($"{b.Invite}: {a}: Command not found");
                    b = b.WithErrorMessenges(errors);
                }
            }

            return b;
        }
    }
}
