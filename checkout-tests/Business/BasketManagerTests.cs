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
    public class BasketManagerTests
    {
        private static readonly Mock<ILogger<BasketManagerTests>> _mockLogger = new Mock<ILogger<BasketManagerTests>>();
        
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

        [Fact]
        public void Constructor()
        {
            IBasketManager sut = new BasketManagerImpl(_mockLogger.Object);
            Assert.NotNull(sut);
        }

        [Fact]
        public void Constructor_NullLog()
        {
            Assert.Throws<ArgumentNullException>(() => new BasketManagerImpl(null));
        }

        [Theory]
        [InlineData(2, 2, 1, 2.20)]
        [InlineData(1, 1, 1, 1.40)]
        public void GetBasketTotal_NoOffer(int apples, int biscuits, int coconuts, decimal expectedBasketTotal)
        {
            IBasketManager sut = new BasketManagerImpl(_mockLogger.Object);

            Basket myBasket = new Basket()
            {
                Id = "My test basket",
                Items = new Dictionary<Item, int>(){
                    {aValidApple, apples },
                    {aValidBiscuit,biscuits },
                    {aValidCoconut, coconuts }
                }
            };

            Assert.True(sut.GetTotal(myBasket) == expectedBasketTotal);
        }

        [Theory]
        [InlineData(3, 2, 1, 2.35)]
        [InlineData(1, 1, 1, 1.40)]
        [InlineData(10, 10, 10, 12.65)]
        public void GetBasketTotal_Offers(int apples, int biscuits, int coconuts, decimal expectedBasketTotal)
        {
            IBasketManager sut = new BasketManagerImpl(_mockLogger.Object);
            IOfferManager offerManager = new OfferManagerImpl(_mockLogger.Object);

            Basket myBasket = new Basket()
            {
                Id = "My test basket",
                Items = new Dictionary<Item, int>(){
                    {aValidApple, apples },
                    {aValidBiscuit,biscuits },
                    {aValidCoconut, coconuts }
                }
            };

            Assert.True(sut.GetTotal(myBasket,offerManager) == expectedBasketTotal);
        }
    }
}
