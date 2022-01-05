using MartynasDRestAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Repositories
{
    public interface ITradeItemRepository
    {
        Task<TradeItem> Create(TradeItem t);
        Task DeleteTradeItem(int tradeID);
        Task<TradeItem> Get(int tradeID, int itemID);
        Task<IEnumerable<TradeItem>> GetAll(int tradeID);
    }

    public class TradeItemRepository : ITradeItemRepository
    {
        private readonly RestAPIContext _restApiContext;

        public TradeItemRepository(RestAPIContext restApiContext)
        {
            _restApiContext = restApiContext;
        }

        public async Task<IEnumerable<TradeItem>> GetAll(int tradeID)
        {
            return await _restApiContext.tradeItems.Where(o => o.tradeID == tradeID).ToListAsync();
        }


        public async Task<TradeItem> Get(int tradeID, int itemID)
        {
            return await _restApiContext.tradeItems.FirstOrDefaultAsync(o => o.tradeID == tradeID && o.itemID == itemID);
        }
        public async Task<TradeItem> Create(TradeItem t)
        {
            if (t != default(TradeItem)
                &&  _restApiContext.trades.FirstOrDefault(o => o.id == t.tradeID) != null
                &&  _restApiContext.inventoryItems.FirstOrDefault(o => o.id == t.itemID) != null
                &&  _restApiContext.tradeItems.FirstOrDefault(o => o.tradeID == t.tradeID && o.itemID == t.itemID) == null)
            {
                _restApiContext.tradeItems.Add(t);
                await _restApiContext.SaveChangesAsync();
                return t;
            }

            return null;

        }


        public async Task DeleteTradeItem(int tradeID)
        {
            var tradeItems = await _restApiContext.tradeItems.Where(o => o.tradeID == tradeID).ToListAsync();

            if (tradeItems != null)
            {
                foreach (var item in tradeItems)
                {
                    _restApiContext.tradeItems.Remove(item);
                }

                await _restApiContext.SaveChangesAsync();
            }

        }

    }
}
