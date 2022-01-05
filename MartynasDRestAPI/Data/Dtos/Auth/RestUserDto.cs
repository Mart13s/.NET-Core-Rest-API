using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos.Auth
{
    public record RestUserDto(int Id, string UserName, string Email);
}
