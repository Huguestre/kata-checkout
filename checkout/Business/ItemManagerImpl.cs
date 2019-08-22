using checkout.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Business
{
    public class ItemManagerImpl : IItemManager
    {
        private readonly ILogger _log;

        public ItemManagerImpl(ILogger logger)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Add an item to the basket
        /// </summary>
        /// <param name="item">item to add</param>
        /// <param name="basket">basket to add to</param>
        /// <param name="quantity">quntity of this item to add</param>
        /// <returns>true if succeeded</returns>
        public bool AddItemToBasket(Item item, ref Basket basket, int quantity = 1)
        {
            bool success = false;
            try
            {
                if (item == null)
                {
                    throw new ArgumentNullException(nameof(item));
                }

                if (basket == null)
                {
                    throw new ArgumentNullException(nameof(basket));
                }

                if (basket.Items.Any(k => k.Key.Id == item.Id))
                {
                    var currentItem = basket.Items.First(k => k.Key.Id == item.Id);
                    int currentCount = currentItem.Value;

                    int newCount = currentCount + quantity;
                    basket.Items.Remove(currentItem.Key);
                    basket.Items.Add(item, newCount);
                }
                else
                {
                    basket.Items.Add(item, quantity);
                }
                success = true;
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex, $"{nameof(ItemManagerImpl)}.{nameof(AddItemToBasket)} failed");
            }
            return success;
        }
    }
}
