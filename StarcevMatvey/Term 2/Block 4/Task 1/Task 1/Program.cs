namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var bash = new MyBash();
            var painter = new Painter();

            painter.Draw("\n");

            while (true)
            {
                painter.DrawBlue($"{Environment.UserName} {Environment.MachineName} ");
                painter.DrawYellow($"{bash.Path}\n");
                painter.Draw($"{bash.Invite}> ");

                bash.InitCommands(new Reader());
                var errors = bash.GetErrorMessenges();

                if (errors.Count != 0)
                {
                    foreach (var error in errors)
                    {
                        painter.DrawMagneta(error + "\n");
                    }
                    painter.Draw("\n");
                }
                else
                {
                    bash.ExecuteCommands();
                    errors = bash.GetErrorMessenges();
                    painter.Draw(bash.Output);
                    foreach (var error in errors)
                    {
                        painter.DrawMagneta(error + "\n");
                    }
                    painter.Draw("\n");
                }
            }
        }
    }
}