using F0.Talks.AsyncAwait.Services;

namespace F0.Talks.AsyncAwait.Tests.NUnit;

[TestFixture]
public class UnitTests
{
    [Test]
    public async Task GetAsync()
    {
        int value = await AsyncService.GetAsync(2);

        Assert.That(value, Is.EqualTo(2));
    }

    [Test]
    public void ThrowAsync()
    {
        InvalidOperationException ex = Assert.ThrowsAsync<InvalidOperationException>(() => ExceptionService.ThrowAsync());
        Assert.That(ex.Message, Is.EqualTo("ThrowAsync"));
    }
}
