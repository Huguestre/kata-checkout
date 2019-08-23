using checkout.Business;
using checkout.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace checkout_tests.Business
{
    public class ItemManagerTests
    {
        private static readonly Mock<ILogger<ItemManagerTests>> _mockLogger = new Mock<ILogger<ItemManagerTests>>();

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
            IItemManager sut = new ItemManagerImpl(_mockLogger.Object);
            Assert.NotNull(sut);
        }

        [Fact]
        public void Constructor_NullLog()
        {
            Assert.Throws<ArgumentNullException>(() => new ItemManagerImpl(null));
        }

        [Fact]
        public void AddOneItemToBasket()
        {
            IItemManager sut = new ItemManagerImpl(_mockLogger.Object);

            Basket myBasket = new Basket()
            {
                Id = "My test basket"
            };

            Assert.True(sut.AddItemToBasket(aValidApple, ref myBasket,1));
            Assert.NotEmpty(myBasket.Items);
            Assert.True(myBasket.Items.Values.First() == 1);
        }

        [Fact]
        public void Add2Apples_1Biscuit_1Coconut()
        {
            IItemManager sut = new ItemManagerImpl(_mockLogger.Object);

            int numberOfApples = 2;

            Basket myBasket = new Basket()
            {
                Id = "My test basket"
            };

            Assert.True(sut.AddItemToBasket(aValidApple, ref myBasket, numberOfApples));
            Assert.True(sut.AddItemToBasket(aValidBiscuit, ref myBasket));
            Assert.True(sut.AddItemToBasket(aValidCoconut, ref myBasket));

            Assert.True(myBasket.Items
                .First(kvp => kvp.Key.Id == aValidApple.Id)
                .Value == numberOfApples);

            Assert.True(myBasket.Items
                .First(kvp => kvp.Key.Id == aValidBiscuit.Id)
                .Value == 1);

            Assert.True(myBasket.Items
                .First(kvp => kvp.Key.Id == aValidCoconut.Id)
                .Value == 1);
        }

        [Fact]
        public void AddRandomToBasket()
        {
            IItemManager sut = new ItemManagerImpl(_mockLogger.Object);
         
            Basket myBasket = new Basket()
            {
                Id = "My test basket"
            };

            sut.AddItemToBasket(aValidCoconut, ref myBasket);
            sut.AddItemToBasket(aValidApple, ref myBasket);
            sut.AddItemToBasket(aValidCoconut, ref myBasket);
            sut.AddItemToBasket(aValidBiscuit, ref myBasket);
            sut.AddItemToBasket(aValidCoconut, ref myBasket);
            sut.AddItemToBasket(aValidBiscuit, ref myBasket,4);
            sut.AddItemToBasket(aValidApple, ref myBasket,3);

            Assert.True(myBasket.Items
                .First(kvp => kvp.Key.Id == aValidApple.Id)
                .Value == 4);

            Assert.True(myBasket.Items
                .First(kvp => kvp.Key.Id == aValidBiscuit.Id)
                .Value == 5);

            Assert.True(myBasket.Items
                .First(kvp => kvp.Key.Id == aValidCoconut.Id)
                .Value == 3);
        }

    }
}
