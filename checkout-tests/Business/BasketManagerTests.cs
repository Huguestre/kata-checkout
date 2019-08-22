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

        [Fact]
        public void GetBasketTotal()
        {
            IBasketManager sut = new BasketManagerImpl(_mockLogger.Object);

            Basket myBasket = new Basket()
            {
                Id = "My test basket",
                Items = new Dictionary<Item, int>(){
                    {aValidApple, 2 },
                    {aValidBiscuit,2 },
                    {aValidCoconut, 1 }
                }
            };

            Assert.True(sut.GetTotal(myBasket) == 2.20m);
        }
    }
}
