namespace ProducerConsumer;

public class Application
{
    public DateTime Time { get; }
    public int squareMeters { get; }
    public CoverType coverType { get; }
    private bool isUproved;

    public Application(int squareMeters, CoverType type, DateTime date)
    {
        Time = date;
        this.squareMeters = squareMeters;
        coverType = type;
    }

    public void Uprove()
    {
        isUproved = true;
    }

    public void Decline()
    {
        isUproved = false;
    }
}
