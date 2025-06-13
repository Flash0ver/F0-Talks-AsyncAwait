using F0.Talks.AsyncAwait.Services;

namespace F0.Talks.AsyncAwait.Tests.MSTest;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public async Task GetAsync()
    {
        int value = await AsyncService.GetAsync(3);

        Assert.AreEqual(3, value);
    }

    [TestMethod]
    public async Task ThrowAsync()
    {
        Func<Task> act = () => ExceptionService.ThrowAsync();

        InvalidOperationException ex = await Assert.ThrowsExactlyAsync<InvalidOperationException>(act);
        Assert.AreEqual("ThrowAsync", ex.Message);
    }
}
