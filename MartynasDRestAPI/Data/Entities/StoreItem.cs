using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Entities
{
    public class StoreItem
    {
        public int id { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }

        public Decimal price { get; set; }
        public int qty { get; set; }
    }
}
