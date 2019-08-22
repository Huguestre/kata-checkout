using checkout.Business;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace checkout_tests.Business
{
    public class ItemManagerTests
    {
        private static readonly Mock<ILogger<ItemManagerTests>> _mockLogger = new Mock<ILogger<ItemManagerTests>>();

        [Fact]
        public void Constructor()
        {
            IItemManager sut = new ItemManagerImpl(_mockLogger.Object);
            Assert.NotNull(sut);
        }

        [Fact]
        public void Constructor_NullLog()
        {
            Assert.Throws<ArgumentNullException>(() => new ItemManagerImpl(null));
        }
    }
}
