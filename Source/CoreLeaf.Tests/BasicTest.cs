using Xunit;

namespace CoreLeaf.Tests
{
    /// <summary>
    /// Basic test provides some simplistic, and easy to verify tests
    /// to ensure that our build and test CI system is working
    /// </summary>
    public class BasicTest
    {
        [Fact]
        public void AlwaysPass()
        {
            Assert.True(true);
        }
    }
}
