using MartynasDRestAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Repositories
{
    public interface ITradeRepository
    {
        Task<Trade> Create(Trade trade);
        Task Delete(int id);
        Task<Trade> Get(int id);
        Task<IEnumerable<Trade>> GetAll();
        Task<Trade> Patch(int id, Trade trade);
    }

    public class TradeRepository : ITradeRepository
    {
        public async Task<IEnumerable<Trade>> GetAll()
        {
            return new List<Trade>()
            {
                new Trade()
                {

                id = 1,
                senderUsername = "User 1",
                receiverUsername = "User 2",
                date = DateTime.Now,
                receiverItems = new List<InventoryItem>(),
                senderItems = new List<InventoryItem>(),
                status = Trade.TradeState.accepted

                },

                new Trade()
                {

                id = 2,
                senderUsername = "User 1",
                receiverUsername = "User 2",
                date = DateTime.Now,
                receiverItems = new List<InventoryItem>(),
                senderItems = new List<InventoryItem>(),
                status = Trade.TradeState.accepted,
                

                 }

            };

        }

        public async Task<Trade> Get(int id)
        {
            return new Trade()
            {
                id = 1,
                senderUsername = "User 1",
                receiverUsername = "User 2",
                date = DateTime.Now,
                receiverItems = new List<InventoryItem>(),
                senderItems = new List<InventoryItem>(),
                status = Trade.TradeState.accepted

            };
        }

        public async Task<Trade> Create(Trade trade)
        {
            return trade;
        }

        public async Task<Trade> Patch(int id, Trade trade)
        {
            return trade;
        }

        public async Task Delete(int id)
        {
            return;
        }
    }
}
