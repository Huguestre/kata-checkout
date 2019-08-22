using checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Business
{
    public interface IOfferManager
    {
        Offer GetOfferForItem(Item item);

        List<Offer> GetOffers();
    }
}
