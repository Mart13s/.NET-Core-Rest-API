using MartynasDRestAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
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
        Task<Review> Patch(int id, Review r);
        Task Delete(int storeItemID, int id);
    }

    public class ReviewsRepository : IReviewsRepository
    {

        private readonly RestAPIContext _restContext;

        public ReviewsRepository(RestAPIContext restContext)
        {
            _restContext = restContext;
        }

        public async Task<IEnumerable<Review>> GetAll(int storeItemId)
        {
            var storeItem = await _restContext.storeItems.FirstOrDefaultAsync(o => o.id == storeItemId);

            if(storeItem != null)
            {

                return await _restContext.reviews.Where(o => o.item == storeItem).ToListAsync();

            }

            return null;

        }


        public async Task<Review> Get(int storeItemId, int reviewId)
        {

            var storeItem = await _restContext.storeItems.FirstOrDefaultAsync(o => o.id == storeItemId);

            if (storeItem != null)
            {
                return await _restContext.reviews.FirstOrDefaultAsync(o => o.item == storeItem && o.id == reviewId);
              
            }

            return null;

        }

        public async Task<Review> Create(Review review)
        {
            if (review == null || review.item == null) return null;

            var storeItem = await _restContext.storeItems.FirstOrDefaultAsync(o => o.id == review.item.id);
            
            if (storeItem != null)
            {

                _restContext.reviews.Add(review);
                await _restContext.SaveChangesAsync();

                return review;

            }

            return null;
        }

        public async Task<Review> Patch(int id, Review review)
        {

            if (review == null || review.item == null) return null;

            var storeItem = await _restContext.storeItems.FirstOrDefaultAsync(o => o.id == review.item.id);

            if (storeItem != null)
            {

                var reviewToPatch = await _restContext.reviews.FirstOrDefaultAsync(o => o.id == id);
                
                if(reviewToPatch != null)
                {
                    review.id = id;
                    reviewToPatch = review;

                    await _restContext.SaveChangesAsync();

                    return review;


                }

            }

            return null;

        }

        public async Task Delete(int storeItemID, int id)
        {

            var storeItem = await _restContext.storeItems.FirstOrDefaultAsync(o => o.id == storeItemID);

            if(storeItem != null)
            {

                var reviewToRemove = await _restContext.reviews.FirstOrDefaultAsync(o => o.id == id);

                if( reviewToRemove != null )
                {
                    _restContext.reviews.Remove(reviewToRemove);
                    await _restContext.SaveChangesAsync();
                } 


            }


        }

    }
}
