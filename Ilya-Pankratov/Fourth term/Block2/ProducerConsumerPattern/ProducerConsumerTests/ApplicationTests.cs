using ProducerConsumer;

namespace ProducerConsumerTests;

public class ApplicationTests
{
    [Test]
    public void InitializationTest()
    {
        var squareMeter = 100;
        var type = CoverType.Wallpaper;
        var time = DateTime.Now;
        var application = new Application(squareMeter, type, time);
        Assert.That(application.coverType, Is.EqualTo(type));
        Assert.That(application.Time, Is.EqualTo(time));
        Assert.That(application.squareMeters, Is.EqualTo(squareMeter));
    }
}
