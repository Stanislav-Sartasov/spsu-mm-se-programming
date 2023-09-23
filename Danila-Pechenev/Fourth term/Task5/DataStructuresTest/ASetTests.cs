public abstract class SetTests
{
    protected abstract DataStructures.ISet<int> set { get; }

    [Test]
    public void Add()
    {
        var thread1 = new Thread(() =>
        {
            for (int i = 0; i < 1000; i++)
            {
                set.Add(i);
            }
        });
        var thread2 = new Thread(() =>
        {
            for (int i = 500; i < 1500; i++)
            {
                set.Add(i);
            }
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        for (int i = 0; i < 1500; i++)
        {
            Assert.That(set.Contains(i), Is.True);
            set.Remove(i);
        }
    }

    [Test]
    public void Remove()
    {
        set.Add(42);

        Assert.That(set.Remove(42), Is.True);
    }

    [Test]
    public void Count()
    {
        var items = Enumerable.Range(1, 1000).ToList();
        items.ForEach(item => set.Add(item));

        Assert.That(set.Count(), Is.EqualTo(items.Count));
    }
}