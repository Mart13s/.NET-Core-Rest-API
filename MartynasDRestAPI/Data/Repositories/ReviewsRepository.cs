using MartynasDRestAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Repositories
{
    public interface IReviewsRepository
    {
        Task<IEnumerable<Review>> GetAll(int storeItemId);
        Task<Review> Get(int storeItemId, int reviewId);
        Task<Review> Create(Review review);
        Task<Review> Patch(Review r);
        Task Delete(int id);
    }

    public class ReviewsRepository : IReviewsRepository
    {
        public async Task<IEnumerable<Review>> GetAll(int storeItemId)
        {
            return new List<Review>()
            {
                new Review()
                {
                    id = 1,
                    rating = 10,
                    reviewText = "Good first item review.",
                },

                new Review()
                {
                    id = 2,
                    rating = 5,
                    reviewText = "Good second item review.",
                }
            };
        }


        public async Task<Review> Get(int storeItemId, int reviewId)
        {
            return
                new Review()
                {
                    id = reviewId,
                    rating = 8,
                    reviewText = "Good Third item review.",
                };

        }
        public async Task<Review> Create(Review review)
        {
            return review;

        }

        public async Task<Review> Patch(Review r)
        {
            return r;

        }

        public async Task Delete(int id)
        {

        }
    }
}
