using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MartynasDRestAPI.Data.Dtos
{
    public record StoreItemDto(
    int id,
    string itemName,
    string description,
    Decimal price,
    int qtyLeft,
    string imageUrl
    );
}
