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
    [Route("api/purchases")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IMapper _mapper;

        public PurchaseController(IPurchaseRepository purchaseRepository, IMapper mapper)
        {
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<PurchaseDto>> GetAll()
        {
            return (await _purchaseRepository.GetAll()).Select(o => _mapper.Map<PurchaseDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> Get(int id)
        {
            var purchase = _purchaseRepository.Get(id);
            if (purchase == null) return NotFound($" Purchase with an id {id} was not found.");

            return _mapper.Map<PurchaseDto>(await _purchaseRepository.Get(id));
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseDto>> Create(PurchaseDto dto)
        {
            var purchase = _mapper.Map<Purchase>(dto);
            await _purchaseRepository.Create(purchase);

            return Created($"/api/purchases/{purchase.id}",_mapper.Map<PurchaseDto>(purchase));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<PurchaseDto>> Patch(int id, PurchaseDto dto)
        {
            var purchase = await _purchaseRepository.Get(id);
            if (purchase == null) return NotFound($" Purchase item with id '{id}' not found.");

            _mapper.Map(dto, purchase);
            await _purchaseRepository.Patch(id, purchase);

            return Ok(_mapper.Map<PurchaseDto>(purchase));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PurchaseDto>> Delete(int id)
        {
            var purchase = await _purchaseRepository.Get(id);
            if (purchase == null) return NotFound($" Purchase with id {id} not found.");

            await _purchaseRepository.Delete(id);

            // 204
            return NoContent();

        }



    }
}
