using checkout.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Business
{
    public class BasketManagerImpl : IBasketManager
    {
        private readonly ILogger _log;

        public BasketManagerImpl(ILogger logger)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public decimal GetTotal(Basket basket)
        {
            decimal total = 0.00m;
            try
            {
                if (basket == null)
                {
                    throw new ArgumentNullException(nameof(basket));
                }

                if (!basket.Items.Any())
                {
                    return total;
                }

                foreach (KeyValuePair<Item, int> kvp in basket.Items)
                {
                    total += kvp.Key.Price * kvp.Value;
                }
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex, $"{nameof(BasketManagerImpl)}.{nameof(GetTotal)} failed");
            }
            return total;
        }
    }
}
