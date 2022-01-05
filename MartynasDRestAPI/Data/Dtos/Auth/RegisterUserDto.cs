using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos.Auth
{
    public record RegisterUserDto(
        [Required] string UserName,
        [Required] string Password,
        [Required][EmailAddress] string Email,
        string Phone,
        string Firstname,
        string Lastname
        );
}
