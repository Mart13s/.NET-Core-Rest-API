using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos
{
    public class StoreItemDto {
        public int id { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public Decimal price { get; set; }
        public int qty { get; set; }
        public string imageUrl { get; set; }

        public StoreItemDto()
        {

        }
    }
}
