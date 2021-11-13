using MartynasDRestAPI.Data.Entities;
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
        public async Task<IEnumerable<Purchase>> GetAll()
        {
            return new List<Purchase>
            {
                new Purchase() {

                    id = 1,
                    totalCost = 100.00m,
                    totalItemCount = 2,

                    item = new List<StoreItem>() {
                
                    new StoreItem
                    {
                    id = 1,
                    itemName = "First item",
                    description = "First item description",
                    price = 1.00m,
                    qtyLeft = 1,
                    imageUrl = "No Image"

                },

                new StoreItem
                {
                    id = 2,
                    itemName = "Second item",
                    description = "Second item description",
                    price = 2.00m,
                    qtyLeft = 2,
                    imageUrl = "No Image"

                }

                }

                },

                new Purchase() {

                    id = 2,
                    totalCost = 200.00m,
                    totalItemCount = 2,

                    item = new List<StoreItem>() {

                        new StoreItem
                        {
                            id = 3,
                            itemName = "Third item",
                            description = "Third item description",
                            price = 1.00m,
                            qtyLeft = 1,
                            imageUrl = "No Image"

                        },

                        new StoreItem
                        {
                            id = 4,
                            itemName = "Fourth item",
                            description = "Fourth item description",
                            price = 2.00m,
                            qtyLeft = 2,
                            imageUrl = "No Image"

                        }

                    }

                }


            };
        }


        public async Task<Purchase> Get(int id)
        {
            return new Purchase()
            {

                id = 1,
                totalCost = 100.00m,
                totalItemCount = 2,

                item = new List<StoreItem>()
                {

                    new StoreItem
                    {
                        id = 1,
                        itemName = "First item",
                        description = "First item description",
                        price = 1.00m,
                        qtyLeft = 1,
                        imageUrl = "No Image"

                    },

                    new StoreItem
                    {
                        id = 2,
                        itemName = "Second item",
                        description = "Second item description",
                        price = 2.00m,
                        qtyLeft = 2,
                        imageUrl = "No Image"

                    }

                }

            };
        }
        public async Task<Purchase> Create(Purchase p)
        {
            return new Purchase()
            {

                id = 33,
                totalCost = 155.18m,
                totalItemCount = 48,

                item = new List<StoreItem>()
                {

                    new StoreItem
                    {
                        id = 1,
                        itemName = "First item",
                        description = "First item description",
                        price = 1.00m,
                        qtyLeft = 1,
                        imageUrl = "No Image"

                    },

                    new StoreItem
                    {
                        id = 2,
                        itemName = "Second item",
                        description = "Second item description",
                        price = 2.00m,
                        qtyLeft = 2,
                        imageUrl = "No Image"

                    }

                }

            };

        }

        public async Task<Purchase> Patch(int id, Purchase p)
        {
            return p;

        }

        public async Task Delete(int id)
        {

        }
    }
}
