using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MartynasDRestAPI.Auth.Model;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Entities;
using MartynasDRestAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace MartynasDRestAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = RestUserRoles.Admin)]
    public class UserController : ControllerBase
    {
        private readonly  IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public UserController(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return (await _usersRepository.GetAll()).Select(o => _mapper.Map<UserDto>(o));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> Get(int id)
        {
            var user = await _usersRepository.Get(id);
            if (user == null) return NotFound($"User with id '{id}' doesn\'t exist.");
            return Ok(_mapper.Map<UserDto>(user));
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(CreateUserDto dto)
        {
            var user = _mapper.Map<UserInternal>(dto);
            await _usersRepository.Create(user);

            // 201 Created
            return Created($"/api/users/{user.id}", _mapper.Map<UserDto>(user));
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<UserDto>> Patch(int id, PatchUserDto dto)
        {
            var user = await _usersRepository.Get(id);
            if (user == null) return NotFound($" User with id '{id}' not found.");

            _mapper.Map(dto, user);

            await _usersRepository.Patch(id, user);

            return Ok(_mapper.Map<UserDto>(user));

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> Delete(int id)
        {
            var user = await _usersRepository.Get(id);
            if (user == null) return NotFound($" User with id '{id}' not found.");

            await _usersRepository.Delete(id);

            // 204
            return NoContent();

        }

    }
}
