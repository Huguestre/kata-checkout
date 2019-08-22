using System;

namespace checkout.Models
{
    /// <summary>
    /// Base type for special offers
    /// </summary>
    public class Offer
    {
        public string Id { get; set; }

        /// <summary>
        /// Quantity required to activate offer
        /// </summary>
        public int Qtity { get; set; }

        /// <summary>
        /// Price to allocate for the given offer
        /// </summary>
        public decimal Price { get; set; }
    }
}