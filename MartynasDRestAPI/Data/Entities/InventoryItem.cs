using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Entities
{
    public class InventoryItem
    {
        public int id { get; set; }

        [ForeignKey("owner")]
        public int ownerID { get; set; }
        public User owner { get; set; }
        public string itemName { get; set; }
        public string description { get; set; }
        public string imageUrl { get; set; }
 
    }
}
