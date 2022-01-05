using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Entities
{
    public class TradeItem
    {
        [ForeignKey("trade")]
        public int tradeID { get; set; }

        [NotMapped]
        public Trade trade { get; set; }

        [ForeignKey("item")]
        public int itemID { get; set; }
        [NotMapped]
        public InventoryItem item { get; set; }
    }
}
