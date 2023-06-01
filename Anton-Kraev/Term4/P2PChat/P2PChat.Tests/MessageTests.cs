using P2PChat.Core;

namespace P2PChatTests;

public class Tests
{
    [Test]
    public void SerializeDeserializeTest()
    {
        var msg = new Message(MessageType.Text, "127.0.0.1:3000", "Hello");
        var serialized = msg.Serialize();
        var deserialized = Message.Deserialize(serialized, serialized.Length);
            
        Assert.AreEqual(msg.Type, deserialized.Type);
        Assert.AreEqual(msg.Sender, deserialized.Sender);
        Assert.AreEqual(msg.Text, deserialized.Text);
    }
}