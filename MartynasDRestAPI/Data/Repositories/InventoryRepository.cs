using MartynasDRestAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Repositories
{
    public interface IInventoryRepository
    {
        Task<InventoryItem> Create(int inventoryID, InventoryItem invItem);
        Task Delete(int inventoryID, int id);
        Task<InventoryItem> Get(int inventoryID, int id);
        Task<InventoryItem> GetByID(int id);
        Task<IEnumerable<InventoryItem>> GetAll(int inventoryID);
        Task<InventoryItem> Patch(int inventoryID, InventoryItem invItem);
    }

    public class InventoryRepository : IInventoryRepository
    {
        private readonly RestAPIContext _restApiContext;

        public InventoryRepository(RestAPIContext restApiContext)
        {
            _restApiContext = restApiContext;
        }

        public async Task<IEnumerable<InventoryItem>> GetAll(int inventoryID)
        {
            var user = await _restApiContext.users.FirstOrDefaultAsync(o => o.Id == inventoryID);
            
            if(user != null)
            {
                return await _restApiContext.inventoryItems.Where(o => o.owner == user).ToListAsync();
            }

            return null;
        }

        public async Task<InventoryItem> Get(int inventoryID, int id)
        {
            var user = await _restApiContext.users.Where(o => o.Id == inventoryID).FirstOrDefaultAsync();

            if (user != null)
            {
                return await _restApiContext.inventoryItems
                    .FirstOrDefaultAsync(o => o.owner == user && o.id == id);
            }

            return null;
        }

        public async Task<InventoryItem> GetByID(int id)
        {

                return await _restApiContext.inventoryItems.FirstOrDefaultAsync(o => o.id == id);
     
        }

        public async Task<InventoryItem> Create(int inventoryID, InventoryItem invItem)
        {
            var user = await _restApiContext.users.FirstOrDefaultAsync(o => o.Id == inventoryID);

            if(user != null)
            {
                invItem.owner = user;
                _restApiContext.inventoryItems.Add(invItem);
                await _restApiContext.SaveChangesAsync();
                return invItem;
            }

            return null;

        }

        public async Task<InventoryItem> Patch(int inventoryID, InventoryItem invItem)
        {
            var user = await _restApiContext.users.FirstOrDefaultAsync(o => o.Id == inventoryID);

            if (user != null)
            {
                var item = await _restApiContext.inventoryItems.FirstOrDefaultAsync(o => o == invItem);
                if (item != null && item.owner == user)
                {
                    // Setting ItemID and owner respectively
                    invItem.id = item.id;
                    invItem.owner = item.owner;

                    item = invItem;
                    
                    await _restApiContext.SaveChangesAsync();
                    return invItem;
                }

                return null;

                
            }

            return null;

        }

        public async Task Delete(int inventoryID, int id)
        {
            var user = await _restApiContext.users.FirstOrDefaultAsync(o => o.Id == inventoryID);

            if (user != null)
            {
                var item = await _restApiContext.inventoryItems
                    .FirstOrDefaultAsync(o => o.owner == user && o.id == id);

                if (item != null && item.owner == user)
                {
                    _restApiContext.Remove(item);
                    await _restApiContext.SaveChangesAsync();
                }


            }

        }

    }
}
