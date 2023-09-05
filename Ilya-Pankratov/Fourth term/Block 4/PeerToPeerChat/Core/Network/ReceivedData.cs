using System.Net.Sockets;

namespace Core.Network;

public class ReceivedData
{
    public byte[] Buffer { get; }
    private List<byte> receivedData { get; }
    public int BufferSize { get; }
    public Socket? Handler { get; set; }
    public byte[] GetBytes => receivedData.ToArray();

    public ReceivedData()
    {
        Buffer = new byte[1024];
        BufferSize = 1024;
        receivedData = new List<byte>();
    }

    public void SaveReceivedData()
    {
        receivedData.AddRange(Buffer);
    }
}