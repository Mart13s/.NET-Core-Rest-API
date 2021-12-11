using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MartynasDRestAPI.Auth.Model;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MartynasDRestAPI.Controllers
{
    [ApiController]
    [Route("api/store/{storeItemId}/reviews")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IStoreItemsRepository _storeRepository;
        private readonly IMapper _mapper;

        public ReviewController(IReviewsRepository reviewsRepository, IStoreItemsRepository storeRepository, IMapper mapper)
        {

            _storeRepository = storeRepository;
            _reviewsRepository = reviewsRepository;
            _mapper = mapper;

        }

        [HttpGet]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll(int storeItemId)
        {
            if (await _storeRepository.Get(storeItemId) == null) return NotFound($" Store item with id {storeItemId} not found. ");


            return Ok((await _reviewsRepository.GetAll(storeItemId)).Select(o => _mapper.Map<ReviewDto>(o)));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<ReviewDto>> Get(int storeItemId, int id)
        {
            var store = await _storeRepository.Get(storeItemId);

            if (store == null) return NotFound($" Store item with id {storeItemId} was not found. ");
            var review = await _reviewsRepository.Get(storeItemId, id);
            if (review == null) return NotFound($"Review with id {id} not found.");

            return Ok(_mapper.Map<ReviewDto>(await _reviewsRepository.Get(storeItemId, id)));

        }

        [HttpPost]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<ReviewDto>> Create(int storeItemId, CreateReviewDto dto )
        {
            var store = await _storeRepository.Get(storeItemId);
            if (store == null) return NotFound($" Store item with id {storeItemId} was not found. ");

            var review = _mapper.Map<Review>(dto);
            review.item = store;

            await _reviewsRepository.Create(review);

            // 201 Created
            return Created($"/api/store/{storeItemId}/reviews/{review.id}", _mapper.Map<ReviewDto>(review));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<ReviewDto>> Patch(int storeItemId, int id, CreateReviewDto dto)
        {

            var store = await _storeRepository.Get(storeItemId);
            if (store == null) return NotFound($" Store item with id {storeItemId} was not found. ");

            var review = await _reviewsRepository.Get(storeItemId, id);
            if (review == null) return NotFound($" Review with id {id} not found.");

            _mapper.Map(dto, review);
            review.item = store;

            await _reviewsRepository.Patch(id, review);

            return Ok(_mapper.Map<ReviewDto>(review));

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult> Delete(int storeItemId, int id)
        {
            var store = await _storeRepository.Get(storeItemId);
            if (store == null) return NotFound($" Store item with id {storeItemId} was not found. ");

            var review = await _reviewsRepository.Get(storeItemId, id);
            if (review == null) return NotFound($" Review with id {id} not found.");

            await _reviewsRepository.Delete(storeItemId, id);

            // 204
            return NoContent();
        }
    }
}
