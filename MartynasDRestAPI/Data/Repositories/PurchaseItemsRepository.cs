using MartynasDRestAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Repositories
{
    public interface IPurchaseItemsRepository
    {
        Task<PurchaseItem> Create(PurchaseItem p);
        Task DeletePurchase(int purchaseID);
        Task<PurchaseItem> Get(int purchaseID, int storeItemID);
        Task<IEnumerable<PurchaseItem>> GetAll(int purchaseID);
    }

    public class PurchaseItemsRepository : IPurchaseItemsRepository
    {

        private readonly RestAPIContext _restApiContext;

        public PurchaseItemsRepository(RestAPIContext restApiContext)
        {
            _restApiContext = restApiContext;
        }

        public async Task<IEnumerable<PurchaseItem>> GetAll(int purchaseID)
        {
            return await _restApiContext.purchaseItems.Where(o => o.purchaseID == purchaseID).ToListAsync();
        }


        public async Task<PurchaseItem> Get(int purchaseID, int storeItemID)
        {
            return await _restApiContext.purchaseItems.FirstOrDefaultAsync(o => o.purchaseID == purchaseID && o.storeItemID == storeItemID);
        }
        public async Task<PurchaseItem> Create(PurchaseItem p)
        {
            if (p != default(PurchaseItem)
                && await _restApiContext.purchases.FirstOrDefaultAsync(o => o.id == p.purchaseID) != null
                && await _restApiContext.storeItems.FirstOrDefaultAsync(o => o.id == p.storeItemID) != null
                && await _restApiContext.purchaseItems.FirstOrDefaultAsync(o => o.purchaseID == p.purchaseID && o.storeItemID == p.storeItemID) == null)
            {
                _restApiContext.purchaseItems.Add(p);
                await _restApiContext.SaveChangesAsync();
                return p;
            }

            return null;

        }


        public async Task DeletePurchase(int purchaseID)
        {
            var purchaseItems = await _restApiContext.purchaseItems.Where(o => o.purchaseID == purchaseID).ToListAsync();

            if (purchaseItems != null)
            {
                foreach(var item in purchaseItems)
                {
                    _restApiContext.purchaseItems.Remove(item);
                }
          
                await _restApiContext.SaveChangesAsync();
            }

        }


    }
}
