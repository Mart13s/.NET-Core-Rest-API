using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Annotations;
using MartynasDRestAPI.Auth.Model;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Dtos.Auth;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MartynasDRestAPI.Controllers
{

    [ApiController]
    [Route("api/purchases")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly UserManager<RestUser> _userManager;
        private readonly IStoreItemsRepository _storeItemsRepository;
        private readonly IPurchaseItemsRepository _purchaseItemsRepository;
        private readonly IMapper _mapper;

        public PurchaseController(UserManager<RestUser> userManager, IPurchaseItemsRepository purchaseItemsRepository, IPurchaseRepository purchaseRepository, IStoreItemsRepository storeItemsRepository, IMapper mapper)
        {
            _purchaseItemsRepository = purchaseItemsRepository;
            _purchaseRepository = purchaseRepository;
            _userManager = userManager;
            _storeItemsRepository = storeItemsRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetAll()
        {
            var purchases = (await _purchaseRepository.GetAll());
            List<PurchaseDto> dtos = new List<PurchaseDto>();
          
            foreach(var p in purchases)
            {

                PurchaseDto pdto = new PurchaseDto()
                {
                    id = p.id,
                    buyerID = p.buyerID,
                    totalCost = p.totalCost,
                    totalItemCount = p.totalItemCount,
                    items = new List<StoreItemDto>()
                };

                var purchaseItems = await _purchaseItemsRepository.GetAll(p.id);

                foreach(var purchaseItem in purchaseItems)
                {
                    var storeItem = await _storeItemsRepository.Get(purchaseItem.storeItemID);
                    StoreItemDto dto = new StoreItemDto()
                    {
                        id = storeItem.id,
                        imageUrl = storeItem.imageUrl,
                        description = storeItem.description,
                        itemName = storeItem.itemName,
                        price = storeItem.price,
                        qty = purchaseItem.count
                    };

                    pdto.items.Add(dto);
                }

                dtos.Add(pdto);
            }

            return Ok(dtos);

        }

        [HttpGet("{id}")]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<PurchaseDto>> Get(int id)
        {
            var p = await _purchaseRepository.Get(id);
            if (p == null) return NotFound($" Purchase with id {id} not found. ");

            PurchaseDto pdto = new PurchaseDto()
            {
                id = p.id,
                buyerID = p.buyerID,
                totalCost = p.totalCost,
                totalItemCount = p.totalItemCount,
                items = new List<StoreItemDto>()
            };

            var purchaseItems = await _purchaseItemsRepository.GetAll(p.id);

            foreach (var purchaseItem in purchaseItems)
            {
                var storeItem = await _storeItemsRepository.Get(purchaseItem.storeItemID);
                StoreItemDto dto = new StoreItemDto()
                {
                    id = storeItem.id,
                    imageUrl = storeItem.imageUrl,
                    description = storeItem.description,
                    itemName = storeItem.itemName,
                    price = storeItem.price,
                    qty = purchaseItem.count
                };

                pdto.items.Add(dto);
            }

            return Ok(pdto);

        }

        [HttpPost]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<PurchaseDto>> Create(CreatePurchaseDto dto)
        {
            int userid = dto.buyerID;
            var buyer = await _userManager.FindByIdAsync(userid.ToString());
            if (buyer == null) return NotFound($" User with id {userid} not found.");

            if (dto == null || dto.items.Count == 0) return BadRequest(" No items in purchase. ");

            Purchase purchase = new Purchase();
            purchase.buyerID = userid;
            var p = await _purchaseRepository.Create(purchase);
            

            foreach (StoreItemDto storeItem in dto.items)
            {
               
                var currentItem = await _storeItemsRepository.Get(storeItem.id);
                if (currentItem == null) return NotFound($" Store item with id {storeItem.id} not found. ");

                var purchaseItem = await _purchaseItemsRepository.Create(
                    new PurchaseItem()
                    {
                        purchaseID = p.id,
                        storeItemID = currentItem.id,
                        count = storeItem.qty
                    });

                if (purchaseItem == null) return BadRequest($" Purchase item already exists. ");
                //p.items.Add(purchaseItem);
            }

            var purchaseDto = new PurchaseDto();
            purchaseDto.id = p.id;
            purchaseDto.buyerID = userid;
            purchaseDto.totalCost = 0;
            purchaseDto.totalItemCount = 0;

            purchaseDto.items = new List<StoreItemDto>();
            purchaseDto.items.Clear();

            foreach(var storeItem in purchase.items)
            {

                purchaseDto.totalItemCount += 1;
                var storeItem2 = (await _storeItemsRepository.Get(storeItem.storeItemID));
                purchaseDto.totalCost += storeItem2.price * storeItem.count;

                
                purchaseDto.items.Add(new StoreItemDto()
                {
                    id = storeItem2.id,
                    itemName = storeItem2.itemName,
                    qty = storeItem.count,
                    description = storeItem2.description,
                    price = storeItem2.price,
                    imageUrl = storeItem2.imageUrl
                });
                
            }

            p.totalCost = purchaseDto.totalCost;
            p.totalItemCount = purchaseDto.totalItemCount;
            p.items = purchase.items;
            p.buyer = buyer;
            await _purchaseRepository.Patch(p.id, p);

            return Created($"/api/purchases/{purchase.id}", purchaseDto);

            
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<PurchaseDto>> Patch(int id, PurchaseDto dto)
        {
            int userid = dto.buyerID;
            var buyer = await _userManager.FindByIdAsync(userid.ToString());
            if (buyer == null) return NotFound($" User with id {userid} not found.");

            var purchase = await _purchaseRepository.Get(id);
            if (purchase == null) return NotFound($" Purchase item with id '{id}' not found.");

            var patchingPurchase = _mapper.Map<Purchase>(dto);
            patchingPurchase.buyer = buyer;
            
            await _purchaseRepository.Patch(id, patchingPurchase);

            return Ok(dto);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<PurchaseDto>> Delete(int id)
        {
            var purchase = await _purchaseRepository.Get(id);
            if (purchase == null) return NotFound($" Purchase with id {id} not found.");

            await _purchaseItemsRepository.DeletePurchase(id);
            await _purchaseRepository.Delete(id);

            // 204
            return NoContent();

        }



    }
}
