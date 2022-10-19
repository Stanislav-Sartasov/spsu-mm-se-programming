using System.Text;

namespace Task8.Commands
{
    internal interface ICommand
    {
        string Name { get; }
        StringBuilder Motion(string arguments);
    }
}
