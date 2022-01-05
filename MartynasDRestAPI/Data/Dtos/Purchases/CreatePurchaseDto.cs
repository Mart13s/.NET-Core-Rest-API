using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartynasDRestAPI.Data.Entities;

namespace MartynasDRestAPI.Data.Dtos
{
    public record CreatePurchaseDto(
    int buyerID,
    List<StoreItemDto> items,
    Decimal totalCost,
    int totalItemCount
    );
}
