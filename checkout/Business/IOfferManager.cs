using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkout.Models;

namespace checkout.Business
{
    public interface IOfferManager
    {
        Offer GetOfferForItem(Item item);
    }
}
