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

        /// <summary>
        /// Get total from basket without offers
        /// </summary>
        /// <param name="basket">basket to compute total from</param>
        /// <returns>Total</returns>
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

        /// <summary>
        /// Get total from basket with offers
        /// </summary>
        /// <param name="basket">basket to compute total from</param>
        /// <param name="offers">Offers available</param>
        /// <returns>Total</returns>
        public decimal GetTotal(Basket basket, IOfferManager offerManager)
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
                    Offer offer = offerManager.GetOfferForItem(kvp.Key);

                    if (offer == null)
                    {
                        total += GetTotalForItem(kvp);
                    }
                    else
                    {
                        total += GetTotalForItem(kvp, offer);
                    }
                }
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex, $"{nameof(BasketManagerImpl)}.{nameof(GetTotal)} failed");
            }
            return total;
        }

        private decimal GetTotalForItem(KeyValuePair<Item, int> kvp, Offer offer)
        {
            decimal total = 0.00m;

            if (kvp.Value >= offer.Qtity)//qtity in basket >= qtity required for an offer
            {
                total += kvp.Value / offer.Qtity * offer.Price;
                total += kvp.Value % offer.Qtity * kvp.Key.Price;
            }
            else
            {
                total = GetTotalForItem(kvp);
            }
            return total;
        }

        private decimal GetTotalForItem(KeyValuePair<Item, int> kvp)
        {
            return kvp.Key.Price * kvp.Value;
        }
    }
}
