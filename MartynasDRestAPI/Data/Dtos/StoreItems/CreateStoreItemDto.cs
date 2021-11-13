using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos
{
    public record CreateStoreItemDto(
        [Required]string itemName,
        [Required]string description,
        [Required]Decimal price,
        int qtyLeft,
        string imageUrl
        );
}
