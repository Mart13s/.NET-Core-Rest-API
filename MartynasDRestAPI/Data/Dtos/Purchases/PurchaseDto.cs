using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MartynasDRestAPI.Data.Entities;

namespace MartynasDRestAPI.Data.Dtos
{
    public class PurchaseDto { 
    public int id { get; set; }
    public int buyerID { get; set; } 
    public List<StoreItemDto> items { get; set; }
    public Decimal totalCost { get; set; }
    public int totalItemCount { get; set; }

    public PurchaseDto()
     {

     }
    
    }
}
