using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos
{
    public record InventoryItemDto(
     
        int id,
        string itemName,
        string description,
        string imageUrl
        );
}
