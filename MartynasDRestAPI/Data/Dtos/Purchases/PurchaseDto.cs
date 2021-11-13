using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartynasDRestAPI.Data.Entities;

namespace MartynasDRestAPI.Data.Dtos
{
    public record PurchaseDto(
    List<StoreItemDto> items,
    Decimal totalCost,
    int totalItemCount
    );
}
