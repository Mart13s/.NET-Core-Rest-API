using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MartynasDRestAPI.Auth.Model;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Dtos.Auth;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace MartynasDRestAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {

        private readonly UserManager<RestUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(IMapper mapper, UserManager<RestUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<IEnumerable<RestUserDto>> GetAll()
        {
            /*foreach (var claim in User.Claims)
            {
                Console.WriteLine(" Claim type: " + claim.Type + " Claim value:" + claim.Value );
            }

            foreach (var usr in _userManager.Users)
            {
                Console.WriteLine(usr.Id);
            }*/

            return (await _userManager.Users.ToListAsync()).Select(o => _mapper.Map<RestUserDto>(o));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = RestUserRoles.RegisteredCustomer + "," + RestUserRoles.Admin)]
        public async Task<ActionResult<RestUserDto>> Get(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound($"User with id '{id}' doesn\'t exist.");
            return Ok(_mapper.Map<RestUserDto>(user));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = RestUserRoles.Admin)]

        public async Task<ActionResult<RestUserDto>> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null) return NotFound($"User with id '{id}' doesn\'t exist.");

            await _userManager.DeleteAsync(user);

            // 204
            return NoContent();

        }

    }
}
