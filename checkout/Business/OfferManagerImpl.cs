using checkout.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Business
{
    /// <summary>
    /// Managing offers
    /// </summary>
    public class OfferManagerImpl : IOfferManager
    {
        private readonly ILogger _log;

        /// <summary>
        /// TODO: Hardcoded offers at the moment
        /// </summary>
        private readonly List<Offer> offers = new List<Offer>()
        {
            new Offer(){Id="A99",Price=1.30m,Qtity=3},
            new Offer(){Id="B15",Price=0.45m,Qtity=2}
        };

        public OfferManagerImpl(ILogger logger)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        /// Get first offer found for the given item
        /// </summary>
        /// <param name="item">An item</param>
        /// <returns>The first offer found for that item or null if none</returns>
        public Offer GetOfferForItem(Item item)
        {
            Offer offer = null;

            try
            {
                if(item==null)
                    throw new ArgumentNullException(nameof(item));

                if (offers.Any(o => o.Id == item.Id))//any offer for this item
                {
                    offer = offers.First(o => o.Id == item.Id);//offer for this item
                }
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex, $"{nameof(OfferManagerImpl)}.{nameof(GetOfferForItem)} failed");
            }
            return offer;
        }

        public List<Offer> GetOffers()
        {
            return offers;
        }
    }
}
