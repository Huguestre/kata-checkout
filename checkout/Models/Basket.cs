using System;
using System.Collections.Generic;

namespace checkout.Models
{
    /// <summary>
    /// Base type for shopping basket
    /// </summary>
    public class Basket
    {
        public string Id { get; set; }

        /// <summary>
        /// Dictionary of items
        /// Key is item
        /// Value is quantity associated
        /// </summary>
        public Dictionary<Item, int> Items { get; set; } = new Dictionary<Item, int>();
    }
}