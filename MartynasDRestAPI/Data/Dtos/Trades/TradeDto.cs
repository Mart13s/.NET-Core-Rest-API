using MartynasDRestAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MartynasDRestAPI.Data.Entities.Trade;

namespace MartynasDRestAPI.Data.Dtos
{
    public class TradeDto
    {
        public int id { get; set; }
        public int senderID { get; set; }
        public int receiverID { get; set; }
        public DateTime date { get; set; }
        public List<int> senderItems { get; set; }
        public List<int> receiverItems { get; set; }
        public TradeState status
        {
            get; set;
        }

        public TradeDto()
        {

        }

    }
}
