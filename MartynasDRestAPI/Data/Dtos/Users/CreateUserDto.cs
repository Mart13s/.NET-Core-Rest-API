using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MartynasDRestAPI.Data.Dtos
{
    public record CreateUserDto(
        [Required] string username,
        [Required] string passwordHash,
        [Required] string firstname,
        [Required] string lastname,
        string email,
        string phone
        );
}
