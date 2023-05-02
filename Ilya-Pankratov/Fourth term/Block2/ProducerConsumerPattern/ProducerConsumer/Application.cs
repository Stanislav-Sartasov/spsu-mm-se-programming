namespace ProducerConsumer;

public class Application
{
    public DateTime Time { get; }
    public int SquareMeters { get; }
    public CoverType CoverType { get; }
    private bool isUproved;

    public Application(int squareMeters, CoverType type, DateTime date)
    {
        Time = date;
        this.SquareMeters = squareMeters;
        CoverType = type;
    }

    public void Approve()
    {
        isUproved = true;
    }

    public void Decline()
    {
        isUproved = false;
    }
}