using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using checkout.Business;
using checkout.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace checkout.Controllers
{
    /// <summary>
    /// Get total of current basket with and without offers
    /// Add an item to the current basket
    /// </summary>
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly ILogger _log;

        private static Basket basket = new Basket() { Id = "Unique Demo Basket" };

        private readonly string[] validItems = { "A99", "B15", "C40" };

        public BasketController(ILogger<BasketController> logger)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Get the totals of the current basket with and without offers
        /// </summary>
        /// <returns>Totals</returns>
        [HttpGet]
        public string GetTotal()
        {
            string total = "";
            try
            {
                IBasketManager basketManager = new BasketManagerImpl(_log);
                IOfferManager offerManager = new OfferManagerImpl(_log);

                string totalWithoutOffers = basketManager.GetTotal(basket).ToString();
                string totalWithOffers = basketManager.GetTotal(basket,offerManager).ToString();

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"Total without offers: {totalWithoutOffers}");
                sb.AppendLine($"Total with offers: {totalWithOffers}");

                total = sb.ToString();
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex, $"{nameof(BasketController)}.{nameof(GetTotal)} failed");
            }
            return total;
        }

        /// <summary>
        /// Scan (Add) an item to the current basket
        /// </summary>
        /// <remarks>
        /// Only supported items are A99, B15, C40
        /// </remarks>
        /// <param name="item">Item to add</param>
        /// <returns>New totals</returns>
        [HttpPost]
        public string Post([FromBody]Item item)
        {
            string message = "";

            try
            {
                if (item == null)
                {
                    throw new FormatException("Item is null");
                }

                if (!validItems.Contains(item.Id))
                {
                    throw new FormatException($"Item of id {item.Id} is not supported");
                }

                if (item.Price <= 0.00m)
                {
                    throw new FormatException($"Price can't be null or negative");
                }

                if (String.IsNullOrEmpty(item.Name))
                {
                    throw new FormatException($"Name can't be empty");
                }

                IItemManager itemManager = new ItemManagerImpl(_log);
                bool success = itemManager.AddItemToBasket(item, ref basket);

                
                IBasketManager basketManager = new BasketManagerImpl(_log);
                IOfferManager offerManager = new OfferManagerImpl(_log);
                string total = basketManager.GetTotal(basket, offerManager).ToString();

                StringBuilder sb = new StringBuilder();

                if (success)
                {
                    sb.AppendLine($"Successfully added item {item.Name} to basket {basket.Id}");
                }
                else
                {
                    sb.AppendLine($"Failed to add item {item.Name} to basket {basket.Id}");
                }

                sb.AppendLine($"Current total with offers is: £{total}");

                message = sb.ToString();
            }
            catch (FormatException fex)
            {
                _log.LogInformation(fex, "Bad Item");
                message = fex.Message;
            }
            catch (Exception ex)
            {
                _log.LogCritical(ex, "Failed to add an item");
                message = ex.Message;
            }

            return message;
        }
    }
}
