using MartynasDRestAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Repositories
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetAll();
        Task<Purchase> Get(int id);
        Task<Purchase> Create(Purchase p);
        Task<Purchase> Patch(int id, Purchase p);
        Task Delete(int id);
    }

    public class PurchaseRepository : IPurchaseRepository
    {

        private readonly RestAPIContext _restApiContext;

        public PurchaseRepository(RestAPIContext restAPIContext) 
        {
            _restApiContext = restAPIContext;
        }

        public async Task<IEnumerable<Purchase>> GetAll()
        {
            return await _restApiContext.purchases.ToListAsync();
        }


        public async Task<Purchase> Get(int id)
        {
            return await _restApiContext.purchases.FirstOrDefaultAsync(o => o.id == id);
        }
        public async Task<Purchase> Create(Purchase p)
        {
            if(p != default(Purchase))
            {
                _restApiContext.purchases.Add(p);
                await _restApiContext.SaveChangesAsync();
                return p;
            }

            return null;

        }

        public async Task<Purchase> Patch(int id, Purchase p)
        {
            var purchase = await _restApiContext.purchases.FirstOrDefaultAsync(o => o.id == id);

            if(purchase != null && p != null && p.buyer == purchase.buyer)
            {
                purchase = p;
                await _restApiContext.SaveChangesAsync();
                return p;
            }

            return null;

        }

        public async Task Delete(int id)
        {
            var purchase = await _restApiContext.purchases.FirstOrDefaultAsync(o => o.id == id);

            if(purchase != null)
            {
                _restApiContext.Remove(purchase);
                await _restApiContext.SaveChangesAsync();
            }

        }
    }
}
