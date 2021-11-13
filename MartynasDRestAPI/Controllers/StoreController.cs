using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MartynasDRestAPI.Controllers
{
    [ApiController]
    [Route("api/store")]
    public class StoreController : ControllerBase
    {
        private readonly  IStoreItemsRepository _storeRepository;
        private readonly IMapper _mapper;

        public StoreController(IStoreItemsRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<StoreItemDto>> GetAll()
        {
            return (await _storeRepository.GetAll()).Select(o => _mapper.Map<StoreItemDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StoreItemDto>> Get(int id)
        {
            var storeItem = await _storeRepository.Get(id);
            if (storeItem == null) return NotFound($"Store item with id {id} doesn\'t exist.");
            return Ok(_mapper.Map<StoreItemDto>(storeItem));
        }

        [HttpPost]
        public async Task<ActionResult<StoreItemDto>> Post(CreateStoreItemDto dto)
        {
            var storeItem = _mapper.Map<StoreItem>(dto);
            await _storeRepository.Create(storeItem);

            // 201 Created
            return Created($"/api/store/{storeItem.id}", _mapper.Map<StoreItemDto>(storeItem));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<StoreItemDto>> Patch(int id, CreateStoreItemDto dto)
        {
            var storeItem = await _storeRepository.Get(id);
            if (storeItem == null) return NotFound($" Store item with id '{id}' not found.");

            _mapper.Map(dto, storeItem);

            await _storeRepository.Patch(id, storeItem);

            return Ok(_mapper.Map<StoreItemDto>(storeItem));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StoreItemDto>> Delete(int id)
        {
            var storeItem = await _storeRepository.Get(id);
            if (storeItem == null) return NotFound($" Store item with id {id} not found.");

            await _storeRepository.Delete(id);

            // 204
            return NoContent();

        }

    }
}
