using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkout.Models;

namespace checkout.Business
{
    public interface IBasketManager
    {
        decimal GetTotal(Basket basket);
    }
}
