using AutoMapper;
using MartynasDRestAPI.Auth;
using MartynasDRestAPI.Auth.Model;
using MartynasDRestAPI.Data.Dtos.Auth;
using MartynasDRestAPI.Data.Entities;
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
    [AllowAnonymous]
    [Route("api")]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<RestUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ITokenManager _tokenManager;

        public AuthController(UserManager<RestUser> userManager, IMapper mapper, ITokenManager tokenManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterUserDto registerUserDto)
        {
            var user = await _userManager.FindByNameAsync(registerUserDto.UserName);

            if (user != null) return BadRequest(" Request invalid.");

            var newUser = new RestUser() {

                UserName = registerUserDto.UserName,
                Email = registerUserDto.Email,
                PhoneNumber = registerUserDto.Phone,
              

            };

            var createUserResult = await _userManager.CreateAsync(newUser, registerUserDto.Password);

            if(!createUserResult.Succeeded)
            {
                return BadRequest(" Create user failed. " + createUserResult.Errors.ToString());
            }

            await _userManager.AddToRoleAsync(newUser, RestUserRoles.RegisteredCustomer);

            return CreatedAtAction(nameof(Register), _mapper.Map<RestUserDto>(newUser));

        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);

            if (user == null) return BadRequest(" Incorrect login credentials. ");

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordValid) return BadRequest(" Incorrect login credentials. ");

            var accessToken = await _tokenManager.CreateAccessTokenAsync(user);

            return Ok(new SuccessfulLoginResponseDto(accessToken));
        }


    }
}
