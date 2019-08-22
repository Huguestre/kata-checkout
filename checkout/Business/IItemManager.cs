using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkout.Models;

namespace checkout.Business
{
    public interface IItemManager
    {
        bool AddItemToBasket(Item item, ref Basket basket, int quantity = 1);
    }
}
