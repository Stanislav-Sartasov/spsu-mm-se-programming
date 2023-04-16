namespace ConsumerProducer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2) throw new Exception("Not enough arguments");

            var prCount = Utils.GetPositiveInt(args[0]);
            var cnCount = Utils.GetPositiveInt(args[1]);

            if (prCount * cnCount == 0) throw new Exception("One of arguments is not a positive integer");

            var tasks = new List<Data<string>>();
            var locker = new TASLock();

            var consumers = new Consumer[cnCount]
                .Select((x, i) => new Consumer(locker, tasks, $"consumer number {i}"))
                .ToArray();

            var produsers = new Producer[prCount]
                .Select((x, i) => new Producer(locker, tasks, $"producer number {i}"))
                .ToArray();

            Console.ReadKey();

            consumers.ToList().ForEach(x => x.Join());
            produsers.ToList().ForEach(x => x.Join());

            tasks.ForEach(x => Console.WriteLine(x.Inf));
        }
    }
}