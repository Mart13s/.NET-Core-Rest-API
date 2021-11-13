using AutoMapper;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Controllers
{
    [ApiController]
    [Route("api/trades")]
    public class TradeController : ControllerBase
    {

        private readonly ITradeRepository _tradeRepository;
        private readonly IMapper _mapper;

        public TradeController(ITradeRepository tradeRepository, IMapper mapper)
        {
            _tradeRepository = tradeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<TradeDto>> GetAll()
        {
            return (await _tradeRepository.GetAll()).Select(o => _mapper.Map<TradeDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TradeDto>> Get(int id)
        {
            var trade = await _tradeRepository.Get(id);
            if (trade == null) return NotFound($" Trade with with id {id} doesn\'t exist.");
            return Ok(_mapper.Map<TradeDto>(trade));
        }

        [HttpPost]
        public async Task<ActionResult<TradeDto>> Post(CreateTradeDto dto)
        {
            var trade = _mapper.Map<Trade>(dto);
            await _tradeRepository.Create(trade);

            // 201 Created
            return Created($"/api/trades/{trade.id}", _mapper.Map<TradeDto>(trade));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TradeDto>> Patch(int id, CreateStoreItemDto dto)
        {
            var trade = await _tradeRepository.Get(id);
            if (trade == null) return NotFound($" Trade with id '{id}' not found.");

            _mapper.Map(dto, trade);

            await _tradeRepository.Patch(id, trade);

            return Ok(_mapper.Map<TradeDto>(trade));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TradeDto>> Delete(int id)
        {
            var trade = await _tradeRepository.Get(id);
            if (trade == null) return NotFound($" Trade with id {id} not found.");

            await _tradeRepository.Delete(id);

            // 204
            return NoContent();

        }


    }
}
