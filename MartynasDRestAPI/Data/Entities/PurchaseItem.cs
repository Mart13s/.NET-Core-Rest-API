using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Entities
{
    public class PurchaseItem
    {
        [ForeignKey("purchase")]
        public int purchaseID { get; set; }
        public Purchase purchase { get; set; }

        [ForeignKey("storeItem")]
        public int storeItemID { get; set; }
        public StoreItem storeItem { get; set; }
        public int count { get; set; }
       
    }
}
