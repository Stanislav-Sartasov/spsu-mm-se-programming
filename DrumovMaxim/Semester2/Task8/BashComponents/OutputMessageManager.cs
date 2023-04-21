using System.Text;

namespace BashComponents;

public class OutputMessageManager
{
    private StringBuilder builder;

    public OutputMessageManager()
    {
        builder = new StringBuilder();
    }

    public void AddNewMessage(String message)
    {
        builder.Append(message);
    }

    public String GetMessage()
    {
        var message = builder.ToString();
        builder.Clear();
        return message;
    }
}