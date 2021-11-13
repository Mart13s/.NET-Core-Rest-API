using MartynasDRestAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Repositories
{
    public interface IStoreItemsRepository
    {
        Task<IEnumerable<StoreItem>> GetAll();
        Task<StoreItem> Get(int id);
        Task<StoreItem> Create(StoreItem u);
        Task<StoreItem> Patch(int id, StoreItem u);
        Task Delete(int id);
    }

    public class StoreItemsRepository : IStoreItemsRepository
    {
        private RestAPIContext _restApiContext;

        public StoreItemsRepository(RestAPIContext restApiContext)
        {
            _restApiContext = restApiContext;
        }

        public async Task<IEnumerable<StoreItem>> GetAll()
        {
            return await _restApiContext.storeItems.ToListAsync();
        }


        public async Task<StoreItem> Get(int id)
        {
            return await _restApiContext.storeItems.FirstOrDefaultAsync(o => o.id == id);

        }
        public async Task<StoreItem> Create(StoreItem u)
        {
            if (u != default(StoreItem))
            {
                _restApiContext.Add(u);
            }

            await _restApiContext.SaveChangesAsync();
            return u;
                
        }

        public async Task<StoreItem> Patch(int id, StoreItem u)
        {
            var item = await _restApiContext.storeItems.FirstOrDefaultAsync(o => o.id == id);

            if(item != default(StoreItem) && u != null)
            {
                u.id = item.id;
                item = u;

                await _restApiContext.SaveChangesAsync();
                return u;
            }

            return null;
        }

        public async Task Delete(int id)
        {
            var item = await _restApiContext.storeItems.FirstOrDefaultAsync(o => o.id == id);

            if( item != null )
            {
                _restApiContext.Remove(item);
                await _restApiContext.SaveChangesAsync();
            }

        }
    }
}
