using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Entities
{
    public class Trade
    {
        public enum TradeState {
            sent = 0,
            accepted = 1,
            declined = 2,
            cancelled = 3,
            failed = 4
        }

        public int id { get; set; }
        public DateTime date { get; set; }
        public User sender { get; set; }
        public User receiver { get; set; }
        public string senderUsername { get; set; }
        public string receiverUsername { get; set; }
        public List<InventoryItem> senderItems { get; set; }
        public List<InventoryItem> receiverItems { get; set; }
        public TradeState status { get; set; }

    }
}
