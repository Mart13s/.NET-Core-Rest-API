using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MartynasDRestAPI.Data.Dtos;
using MartynasDRestAPI.Data.Dtos.Auth;
using MartynasDRestAPI.Data.Entities;

namespace MartynasDRestAPI.Data
{
    public class RestAPIProfile : Profile
    {
        public RestAPIProfile()
        {
            CreateMap<UserInternal, UserDto>();
            CreateMap<CreateUserDto, UserInternal>();
            CreateMap<PatchUserDto, UserInternal>();

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

            CreateMap<RestUser, RestUserDto>();

        }
    }
}
