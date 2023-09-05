using System.Text;
using Core.Network;
using Newtonsoft.Json;

namespace Core.Chat;

public static class MessageConverter
{
    public static string MessageEnd => "<EOM>"; // End of message

    public static SendData Serialize(object anyObject)
    {
        var jsonStr = JsonConvert.SerializeObject(anyObject);
        var msgString = string.Concat(jsonStr, MessageEnd);
        var message = new SendData();
        message.Data = Encoding.UTF8.GetBytes(msgString);
        Encoding.UTF8.GetBytes(msgString, 0, jsonStr.Length, message.Data, 0);
        return message;
    }

    public static T? Deserialize<T>(byte[] data)
    {
        var mshString = Encoding.UTF8.GetString(data);
        var jsonString = mshString.Replace(MessageEnd, string.Empty);
        return JsonConvert.DeserializeObject<T>(jsonString);
    }
}