using checkout.Business;
using checkout.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace checkout_tests.Business
{
    public class OfferManagerTests
    {
        private static readonly Mock<ILogger<OfferManagerTests>> _mockLogger = new Mock<ILogger<OfferManagerTests>>();

        #region valid items
        private static Item aValidApple = new Item()
        {
            Id = "A99",
            Name = "Apple",
            Price = 0.50m
        };

        private static Item aValidBiscuit = new Item()
        {
            Id = "B15",
            Name = "Biscuit",
            Price = 0.30m
        };

        private static Item aValidCoconut = new Item()
        {
            Id = "C40",
            Name = "Coconut",
            Price = 0.60m
        };
        #endregion

        private readonly List<Item> validItems = new List<Item>() { aValidApple, aValidBiscuit, aValidCoconut };

        [Fact]
        public void Constructor()
        {
            IOfferManager sut = new OfferManagerImpl(_mockLogger.Object);
            Assert.NotNull(sut);
        }

        [Fact]
        public void Constructor_NullLog()
        {
            Assert.Throws<ArgumentNullException>(() => new OfferManagerImpl(null));
        }

        /// <summary>
        /// Check hard-coded offers in the offermanager are retrieved
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="expectedPrice"></param>
        /// <param name="expectedQuantity"></param>
        [Theory]
        [InlineData("A99", 1.30, 3)]
        [InlineData("B15", 0.45, 2)]
        public void GetOfferForItem(string itemId, decimal expectedPrice, int expectedQuantity)
        {
            IOfferManager sut = new OfferManagerImpl(_mockLogger.Object);

            Item item = validItems.First(i => i.Id == itemId);
            Offer offer = sut.GetOfferForItem(item);

            Assert.True(offer.Price == expectedPrice);
            Assert.True(offer.Qtity == expectedQuantity);
        }

        /// <summary>
        /// No offer on coconuts
        /// </summary>
        [Fact]
        public void GetOfferForItem_NoOffer()
        {
            IOfferManager sut = new OfferManagerImpl(_mockLogger.Object);

            Item item = aValidCoconut;
            Assert.Null(sut.GetOfferForItem(item));
        }

        [Fact]
        public void GetOfferForItem_NullItem()
        {
            IOfferManager sut = new OfferManagerImpl(_mockLogger.Object);
            Assert.Null(sut.GetOfferForItem(null));
        }
    }
}
