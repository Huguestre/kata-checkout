using checkout.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace checkout.Business
{
    public interface IBasketManager
    {
        decimal GetTotal(Basket basket);

        decimal GetTotal(Basket basket, IOfferManager offerManager);
    }
}
