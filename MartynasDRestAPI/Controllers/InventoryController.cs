using AutoMapper;
using MartynasDRestAPI.Auth.Model;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Dtos.Auth;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Controllers
{
    [ApiController]
    [Route("api/users/{userID}/inventory")]
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryRepository _inventoryRepository;
        //private readonly IUsersRepository _userRepository;
        private readonly UserManager<RestUser> _userManager;
        private readonly IMapper _mapper;

        public InventoryController(IInventoryRepository inventoryRepository, UserManager<RestUser> userManager, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<InventoryItemDto>>> GetAll(int userID)
        {
            if (await _userManager.FindByIdAsync(userID.ToString() ) == null) return NotFound($" User with id {userID} not found. ");

            return Ok((await _inventoryRepository.GetAll(userID)).Select(o => _mapper.Map<InventoryItemDto>(o)));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<InventoryItemDto>> Get(int userID, int id)
        {
            if (await _userManager.FindByIdAsync(userID.ToString()) == null) return NotFound($" User with id {userID} not found. ");
            var invItem = _inventoryRepository.Get(userID, id);
            if (invItem == null) return NotFound($"Inventory item with id {id} not found.");

            return Ok(_mapper.Map<InventoryItemDto>(await _inventoryRepository.Get(userID, id)));
        }

        [HttpPost]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<InventoryItemDto>> Create(int userID, InventoryItemDto dto)
        {
            var usr = await _userManager.FindByIdAsync(userID.ToString());
            if (usr == null) return NotFound($" User with id {userID} not found. ");

            dto.id = default(int);
            var invItem = _mapper.Map<InventoryItem>(dto);
            invItem.owner = usr;
            invItem.ownerID = userID;
            await _inventoryRepository.Create(userID, invItem);

            // 201 Created
            return Created($"/api/user/{userID}/inventory/{invItem.id}", _mapper.Map<InventoryItemDto>(invItem));
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult<InventoryItemDto>> Patch(int userID, int id, InventoryItemDto dto)
        {
            var usr = await _userManager.FindByIdAsync(userID.ToString());
            if (usr == null) return NotFound($" User with id {userID} not found. ");

            var invItem = await _inventoryRepository.Get(userID, id);
            if (invItem == null) return NotFound($" Inventory item with id {id} not found.");

            _mapper.Map(dto, invItem);

            // Setting id prior to patching
            invItem.id = id;
            invItem.ownerID = userID;
            invItem.owner = usr;

            await _inventoryRepository.Patch(userID, invItem);

            return Ok(_mapper.Map<InventoryItemDto>(invItem));

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]
        public async Task<ActionResult> Delete(int userID, int id)
        {
            if (await _userManager.FindByIdAsync(userID.ToString()) == null) return NotFound($" User with id {userID} not found. ");

            var invItem = await _inventoryRepository.Get(userID, id);
            if (invItem == null) return NotFound($" Inventory item with id {id} not found.");

            await _inventoryRepository.Delete(userID, id);

            // 204
            return NoContent();
        }

    }
}
