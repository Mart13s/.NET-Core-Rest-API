using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Entities;

namespace MartynasDRestAPI.Data
{
    public class RestAPIProfile : Profile
    {
        public RestAPIProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<PatchUserDto, User>();

            CreateMap<StoreItem, StoreItemDto>();
            CreateMap<CreateStoreItemDto, StoreItem>();

            CreateMap<Review, ReviewDto>();
            CreateMap<CreateReviewDto, Review>();

            CreateMap<Purchase, PurchaseDto>();
            CreateMap<PurchaseDto, Purchase>();

            CreateMap<Trade, TradeDto>();
            CreateMap<CreateTradeDto, Trade>();

            CreateMap<InventoryItem, InventoryItemDto>();
            CreateMap<InventoryItemDto, InventoryItem>();

        }
    }
}
