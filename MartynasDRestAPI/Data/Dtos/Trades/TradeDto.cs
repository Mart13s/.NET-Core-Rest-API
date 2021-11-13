using MartynasDRestAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MartynasDRestAPI.Data.Entities.Trade;

namespace MartynasDRestAPI.Data.Dtos
{
    public record TradeDto
    (
        string senderUsername,
        string receiverUsername,
        DateTime date,
        List<InventoryItemDto> senderItems,
        List<InventoryItemDto> receiverItems,
        TradeState status

    );
}
