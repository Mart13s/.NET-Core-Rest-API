using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos
{
    public record ReviewDto(int id, int rating, string reviewName, string reviewText);

}
