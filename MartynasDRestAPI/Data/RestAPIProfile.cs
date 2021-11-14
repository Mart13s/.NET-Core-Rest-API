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

            CreateMap<Purchase, PurchaseDto>()
                .ForSourceMember(source => source.items, opt => opt.DoNotValidate());
            CreateMap<PurchaseDto, Purchase>()
                .ForSourceMember(source => source.buyerID, opt => opt.DoNotValidate())
                .ForSourceMember(source => source.items, opt => opt.DoNotValidate());
            CreateMap<CreatePurchaseDto, Purchase>()
                .ForSourceMember(source => source.buyerID, opt => opt.DoNotValidate())
                .ForSourceMember(source => source.items, opt => opt.DoNotValidate());

            CreateMap<Trade, TradeDto>();

            CreateMap<InventoryItem, InventoryItemDto>();
            CreateMap<InventoryItemDto, InventoryItem>();

        }
    }
}
