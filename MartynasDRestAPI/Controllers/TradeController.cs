using AutoMapper;
using MartynasDRestAPI.Auth.Model;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ITradeItemRepository _tradeItemRepository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public TradeController(ITradeRepository tradeRepository, IUsersRepository usersRepository, ITradeItemRepository tradeItemRepository, IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _tradeRepository = tradeRepository;
            _tradeItemRepository = tradeItemRepository;
            _inventoryRepository = inventoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<TradeDto>>> GetAll()
        {
            var trades = await _tradeRepository.GetAll();

            if (trades == null) return NoContent();

            List<TradeDto> tradeDtos = new List<TradeDto>();

            foreach(Trade t in trades)
            {
                TradeDto tdto = new TradeDto();
                tdto.id = t.id;
                tdto.senderID = t.senderID;
                tdto.receiverID = t.receiverID;
                tdto.date = t.date;
                tdto.status = t.status;
                tdto.senderItems = new List<int>();
                tdto.receiverItems = new List<int>();

                var tradeIts = await _tradeItemRepository.GetAll(t.id);
                
                foreach(var it in tradeIts)
                {
                    var invItem = await _inventoryRepository.GetByID(it.itemID);

                    if (invItem == null) return NotFound(" Trade item does not exist. ");

                    TradeItem ti = new TradeItem();
                    ti.itemID = invItem.id;
                    ti.tradeID = t.id;

                    if (tdto.senderID == invItem.ownerID) tdto.senderItems.Add(ti.itemID);
                    else tdto.receiverItems.Add(ti.itemID);
                }

                tradeDtos.Add(tdto);
            }

            return Ok(tradeDtos);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer)]
        public async Task<ActionResult<TradeDto>> Get(int id)
        {
            var t = await _tradeRepository.Get(id);
            if (t == null) return NotFound($" Trade with with id {id} doesn\'t exist.");

            TradeDto tdto = new TradeDto();
            tdto.id = t.id;
            tdto.senderID = t.senderID;
            tdto.receiverID = t.receiverID;
            tdto.date = t.date;
            tdto.status = t.status;
            tdto.senderItems = new List<int>();
            tdto.receiverItems = new List<int>();

            var tradeIts = await _tradeItemRepository.GetAll(t.id);

            foreach (var it in tradeIts)
            {
                var invItem = await _inventoryRepository.GetByID(it.itemID);

                if (invItem == null) return NotFound(" Trade item does not exist. ");

                TradeItem ti = new TradeItem();
                ti.itemID = invItem.id;
                ti.tradeID = t.id;

                if (tdto.senderID == invItem.ownerID) tdto.senderItems.Add(ti.itemID);
                else tdto.receiverItems.Add(ti.itemID);
            }

            return Ok(tdto);

        }

        [HttpPost]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer)]
        public async Task<ActionResult<TradeDto>> Post(TradeDto dto)
        {

            if (dto == null) return BadRequest(" No trade data provided.");
            Trade trade = new Trade();
            trade.date = DateTime.Now;
            trade.senderID = dto.senderID;
            trade.receiverID = dto.receiverID;
            trade.status = dto.status;
            trade.senderItems = new List<TradeItem>();
            trade.receiverItems = new List<TradeItem>();


            if (dto.senderID == dto.receiverID) return BadRequest(" Sender and receiver is the same user. ");
            if ((await _usersRepository.Get(dto.senderID)) == null) return NotFound($" Sender with id {dto.senderID} not found.");
            if ((await _usersRepository.Get(dto.receiverID)) == null) return NotFound($" Receiver with id {dto.receiverID} not found.");

            if (( dto.senderItems == null || dto.senderItems.DefaultIfEmpty() == null)
               && (dto.receiverItems == null || dto.receiverItems.DefaultIfEmpty() == null))
            {
                return BadRequest(" No need to make a trade with no items exchanged. ");
            }

            foreach (int itemID in dto.senderItems)
            {
                TradeItem tradeItem = new TradeItem();
                tradeItem.itemID = itemID;
                trade.senderItems.Add(tradeItem);

            }

            foreach (int itemID in dto.receiverItems)
            {
                TradeItem tradeItem = new TradeItem();
                tradeItem.itemID = itemID;
                trade.receiverItems.Add(tradeItem);

            }

            var createdTrade = await _tradeRepository.Create(trade);

            if (createdTrade == null) return BadRequest(" Trade hasn't been created. ");

            foreach(var item in createdTrade.senderItems)
            {
                item.tradeID = createdTrade.id;
                var createdTradeItem = await _tradeItemRepository.Create(item);
                if (createdTradeItem == null) return BadRequest(" Trade item hasn't been created. ");
            }

            foreach (var item in createdTrade.receiverItems)
            {
                item.tradeID = createdTrade.id;
                var createdTradeItem =  await _tradeItemRepository.Create(item);
                if (createdTradeItem == null) return BadRequest(" Trade item hasn't been created. ");
            }

            dto.date = DateTime.Now;
            dto.id = createdTrade.id;
            // 201 Created
            return Created($"/api/trades/{trade.id}", dto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<TradeDto>> Delete(int id)
        {
            var trade = await _tradeRepository.Get(id);

            if (trade == null) return NotFound($" Trade with id {id} not found.");

            await _tradeItemRepository.DeleteTradeItem(id);
            await _tradeRepository.Delete(id);

            // 204
            return NoContent();

        }


    }
}
