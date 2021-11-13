using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos
{
    public record PatchUserDto
        (
            [Required] string firstname,
            [Required] string lastname,
            string email,
            string phone
            );
}
