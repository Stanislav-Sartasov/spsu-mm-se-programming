namespace SetTests;

public abstract class SetTests
{
    protected abstract Sets.ISet<int> set { get; }
    private Random random;

    [SetUp]
    public void SetUp()
    {
        random = new Random();
    }

    [Test]
    public void Add()
    {
        var threadArrays = new List<List<int>>(3)
            .Select(_ => new List<int>(100)
                .Select(_ => random.Next()).ToList())
            .ToList();
        
        var threads = new List<Thread>(3)
            .Select((_, index) =>
            new Thread(() => threadArrays[index].ForEach(elem => set.Add(elem)))).ToList();
        
        threads.ForEach(thread => thread.Start());
        Thread.Sleep(100);
        threads.ForEach(thread => thread.Join());
        
        Assert.That(threadArrays.SelectMany(x => x).All(set.Contains), Is.True);
    }
    
    [Test]
    public void RemoveExisting()
    {
        var elem = random.Next();
        set.Add(elem);
        
        Assert.That(set.Remove(elem), Is.True);
    }
    
    [Test]
    public void RemoveNonExistent()
    {
        Assert.That(set.Remove(random.Next()), Is.False);
    }

    [Test]
    public void Count()
    {
        var array = Enumerable.Range(1, 100).ToList();
        array.ForEach(item => set.Add(item));
        
        Assert.That(set.Count(), Is.EqualTo(array.Count));
    }
}