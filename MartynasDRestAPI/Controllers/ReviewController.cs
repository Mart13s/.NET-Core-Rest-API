using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MartynasDRestAPI.Controllers
{
    [ApiController]
    [Route("api/store/{storeItemId}/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly  IReviewsRepository _reviewsRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewsRepository reviewsRepository, IMapper mapper)
        {
            _reviewsRepository = reviewsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ReviewDto>> GetAll(int storeItemId)
        {
            return (await _reviewsRepository.GetAll(storeItemId)).Select(o => _mapper.Map<ReviewDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDto>> Get(int storeItemId, int id)
        {
            var review = _reviewsRepository.Get(storeItemId, id);
            if (review == null) return NotFound($"Review with id {id} not found.");

            return Ok(_mapper.Map<ReviewDto>(await _reviewsRepository.Get(storeItemId, id)));
        }

        [HttpPost]
        public async Task<ActionResult<ReviewDto>> Create(int storeItemId, CreateReviewDto dto )
        {
            var review = _mapper.Map<Review>(dto);
            await _reviewsRepository.Create(review);

            // 201 Created
            return Created($"/api/store/{storeItemId}/reviews/{review.id}", _mapper.Map<ReviewDto>(review));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ReviewDto>> Patch(int storeItemId, int id, CreateReviewDto dto)
        {
            var review = await _reviewsRepository.Get(storeItemId, id);
            if (review == null) return NotFound($" Review with id {id} not found.");

            _mapper.Map(dto, review);
            await _reviewsRepository.Patch(review);

            return Ok(_mapper.Map<ReviewDto>(review));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int storeItemId, int id)
        {
            var review = await _reviewsRepository.Get(storeItemId, id);
            if (review == null) return NotFound($" Review with id {id} not found.");

            await _reviewsRepository.Delete(id);

            // 204
            return NoContent();
        }
    }
}
