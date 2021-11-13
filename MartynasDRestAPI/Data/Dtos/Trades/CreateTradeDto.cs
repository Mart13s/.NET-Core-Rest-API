using MartynasDRestAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static MartynasDRestAPI.Data.Entities.Trade;

namespace MartynasDRestAPI.Data.Dtos
{
    public record CreateTradeDto
    (
        [Required]
        string senderUsername,
        [Required]
        string receiverUsername,
        List<InventoryItemDto> senderItems,
        List<InventoryItemDto> receiverItems,
        DateTime date,
        TradeState status

    );
}
