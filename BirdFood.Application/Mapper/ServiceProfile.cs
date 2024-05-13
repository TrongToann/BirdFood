using AutoMapper;
using BirdFood.Contract.Abstraction.Shared;
using BirdFood.Contract.DTOs.AccountDTO;
using BirdFood.Contract.DTOs.Auth;
using BirdFood.Contract.DTOs.FoodDTO;
using BirdFood.Contract.DTOs.OrderDTO;
using BirdFood.Contract.DTOs.Shop;
namespace BirdFood.Application.Mapper
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile() {
            CreateMap<Domain.Entities.Account, CreateAccountDTO>().ReverseMap();
            CreateMap<Domain.Entities.Account, AccountDTO>().ReverseMap();
            CreateMap<Domain.Entities.Account, RegisterDTO>().ReverseMap();
            CreateMap<Domain.Entities.Shop, IShopDTO>().ReverseMap();

            CreateMap<PageResult<Domain.Entities.Shop>, PageResult<Contract.Service.Shop.Response>>().ReverseMap();
            CreateMap<Domain.Entities.Shop, Contract.Service.Shop.Response>().ReverseMap().MaxDepth(1).PreserveReferences();

            //Products
            CreateMap<Domain.Entities.Food, IFoodDTO>().ReverseMap();
            CreateMap<Domain.Entities.Food, FoodDTO>().ReverseMap();

            //Cart
            //CreateMap<Domain.Entities.Cart, Contract.Service.Cart.Response>().ReverseMap().MaxDepth(1).PreserveReferences();
            //CreateMap<Domain.Entities.CartProduct, CartProductDTO>()
            //    .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product)).MaxDepth(1).PreserveReferences();
            //CreateMap<Domain.Entities.Cart, ICartDTO>().ReverseMap();
            //CreateMap<Domain.Entities.Cart, Contract.Service.Cart.Response>().ReverseMap().MaxDepth(1).PreserveReferences();

            //Order
            CreateMap<Domain.Entities.Order, IOrderDTO>().ReverseMap();
            CreateMap<Domain.Entities.Order, Contract.Service.Order.Response>().ReverseMap().MaxDepth(1).PreserveReferences();
            CreateMap<Domain.Entities.OrderFoods, OrderFoodDTO>()
                .ForMember(dest => dest.Food, opt => opt.MapFrom(src => src.Food)).MaxDepth(1).PreserveReferences();
            CreateMap<Domain.Entities.OrderShipping, OrderShippingDTO>().MaxDepth(1).PreserveReferences();
            CreateMap<Domain.Entities.OrderStatus, OrderStatusDTO>().MaxDepth(1).PreserveReferences();
        }
    }
}
