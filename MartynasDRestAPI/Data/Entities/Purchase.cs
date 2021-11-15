using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Entities
{
    public class Purchase
    {
        public int id { get; set; }

        [ForeignKey("buyer")]
        public int buyerID { get; set; }
        public UserInternal buyer { get; set; }
        public List<PurchaseItem> items { get; set; }
        public Decimal totalCost { get; set; }
        public int totalItemCount { get; set; }
    }
}
