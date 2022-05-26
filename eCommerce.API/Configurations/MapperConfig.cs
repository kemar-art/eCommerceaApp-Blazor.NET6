using AutoMapper;
using eCommerce.API.Date;
using eCommerce.API.Models_DTOs.Cart;
using eCommerce.API.Models_DTOs.CartItem;
using eCommerce.API.Models_DTOs.Categorey;
using eCommerce.API.Models_DTOs.Customer;
using eCommerce.API.Models_DTOs.Order;
using eCommerce.API.Models_DTOs.OrderDetail;
using eCommerce.API.Models_DTOs.Payment;
using eCommerce.API.Models_DTOs.Product;
using eCommerce.API.Models_DTOs.Products;
using eCommerce.API.Models_DTOs.Supplier;
using eCommerce.API.Models_DTOs.User;

namespace eCommerce.API.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        { 
            CreateMap<CartCreateDto, Cart>().ReverseMap();
            CreateMap<CartReadOnlyDto, Cart>().ReverseMap();
            CreateMap<CartUpdateDto, Cart>().ReverseMap();

            CreateMap<CartItemCreateDto, CartItem>().ReverseMap();
            CreateMap<CartItemReadOnlyDto, CartItem>().ReverseMap();
            CreateMap<CartItemUpdateDto, CartItem>().ReverseMap();

            CreateMap<CategoryCreateDto, Category>().ReverseMap();
            CreateMap<CategoreyReadOnlyDto, Category>().ReverseMap();
            CreateMap<CategoreyUpdateDto, Category>().ReverseMap();

            CreateMap<CustomerCreateDto, Customer>().ReverseMap();
            CreateMap<CustomerReadOnlyDto, Customer>().ReverseMap();
            CreateMap<CustomerUpdateDto, Customer>().ReverseMap();

            CreateMap<OrderCreateDto, Order>().ReverseMap();
            CreateMap<OrderReadOnlyDto, Order>().ReverseMap();
            CreateMap<OrderUpdateDto, Order>().ReverseMap();

            CreateMap<OrderDetailReadOnlyDto, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailReadOnlyDto, OrderDetail>().ReverseMap();
            CreateMap<OrderDetailUpdateDto, OrderDetail>().ReverseMap();

            CreateMap<PaymentCreateDto, Payment>().ReverseMap();
            CreateMap<PaymentReadOnlyDto, Payment>().ReverseMap();
            CreateMap<PaymentUpdateDto, Payment>().ReverseMap();

            CreateMap<ProductCreateDto, Product>().ReverseMap();
            CreateMap<ProductReadOnlyDto, Product>().ReverseMap();
            CreateMap<ProductUpdateDto, Product>().ReverseMap();

            CreateMap<SupplierCreateDto, Supplier>().ReverseMap();
            CreateMap<SupplierReadOnlyDto, Supplier>().ReverseMap();
            CreateMap<SupplierUpdateDto, Supplier>().ReverseMap();

            CreateMap<ApiUser, AuthUserDto>().ReverseMap();
        }
    }
}
