using F0.Talks.AsyncAwait.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace F0.Talks.AsyncAwait.XUnit
{
    public class UnitTests
    {
        [Fact]
        public async Task GetAsync()
        {
            var value = await AsyncService.GetAsync(1);

            Assert.Equal(1, value);
        }

        [Fact]
        public async Task ThrowAsync()
        {
            Func<Task> act = () => ExceptionService.ThrowAsync();

            InvalidOperationException ex = await Assert.ThrowsAsync<InvalidOperationException>(act);
            Assert.Equal("ThrowAsync", ex.Message);
        }
    }
}
