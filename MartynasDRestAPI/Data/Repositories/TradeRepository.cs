using MartynasDRestAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
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
        private readonly RestAPIContext _restApiContext;

        public TradeRepository(RestAPIContext restApiContext)
        {
            _restApiContext = restApiContext;
        }

        public async Task<IEnumerable<Trade>> GetAll()
        {
            return await _restApiContext.trades.ToListAsync();

        }

        public async Task<Trade> Get(int id)
        {
            return await _restApiContext.trades.FirstOrDefaultAsync(o => o.id == id);
        }

        public async Task<Trade> Create(Trade trade)
        {
            if (trade != null) _restApiContext.trades.Add(trade);

            await _restApiContext.SaveChangesAsync();

            return trade;
        }

        public async Task<Trade> Patch(int id, Trade trade)
        {
            return trade;
        }

        public async Task Delete(int id)
        {
            var item = await _restApiContext.trades.FirstOrDefaultAsync(o => o.id == id);
            if(item != null)
            {
                _restApiContext.trades.Remove(item);
                await _restApiContext.SaveChangesAsync();
            }
        }
    }
}
