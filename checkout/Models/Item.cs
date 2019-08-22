using System;

namespace checkout.Models
{
    /// <summary>
    /// Base type for scanned items
    /// </summary>
    public class Item
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}