namespace ProducerConsumer;

public class Program
{
    static Random random = new Random();

    private static Func<string> ProduceApplication = () =>
    {
        var length = random.Next(50);
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    };

    public static int Main(string[] args)
    {
        int numberOfConsumers = 1;
        int numberOfProducers = 1;

        int maxPauseTime = 8;

        var manager = new Manager<string>(numberOfConsumers, numberOfProducers, maxPauseTime, ProduceApplication);

        manager.Work();

        return 0;
    }
}