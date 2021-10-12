using System;
using AutoMapper;
using BLL.Entities;
using BLL.Entities.Identity;
using BLL.Entities.OrderAggregate;
using com.vreshly.Dtos;

namespace com.vreshly.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<SubCategory, SubCategoryDto>()
                .ForMember(dest => dest.Category, o => o.MapFrom(s => s.Category.CategoryName));

            CreateMap<SubCategoryDto, SubCategory>();

            CreateMap<Brand, BrandDto>()
                .ForMember(b => b.BrandLogo, o => o.MapFrom<BrandUrlRsolver>());
            CreateMap<BrandDto, Brand>();

            CreateMap<VariableDetail, VariableDetailDto>()
                .ForMember(x => x.Variable, o => o.MapFrom(s => s.Variable.VariableName));
            CreateMap<VariableDetailDto, VariableDetail>();
            CreateMap<Variable, VariableDto>()
                .ForMember(x => x.Product, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(x => x.VariableDetails, o => o.MapFrom(s => s.VariableDetails));
            CreateMap<VariableDto, Variable>();

            CreateMap<Product, ProductDto>()
                .ForMember(x => x.Category, o => o.MapFrom(s => s.Category.CategoryName))
                .ForMember(b => b.MainImage, o => o.MapFrom<ProductUrlResolver,string>(src => src.MainImage))
                .ForMember(b => b.ImageOne, o => o.MapFrom<ProductUrlResolver, string>(src => src.ImageOne))
                .ForMember(b => b.ImageTwo, o => o.MapFrom<ProductUrlResolver, string>(src => src.ImageTwo))
                .ForMember(b => b.ImageThree, o => o.MapFrom<ProductUrlResolver, string>(src => src.ImageThree))
                .ForMember(b => b.ImageFour, o => o.MapFrom<ProductUrlResolver, string>(src => src.ImageFour))
                .ForMember(sub => sub.SubCategory, o => o.MapFrom(s => s.SubCategory.SubCategoryName))
                .ForMember(b => b.Brand, o => o.MapFrom(s => s.Brand.BrandName))
                .ForMember(b => b.variables, o => o.MapFrom(s => s.variables));

            CreateMap<Banner, BannerDto>()
                .ForMember(x => x.ImageOne, o => o.MapFrom<BannerResolver, string>(src => src.ImageOne))
                .ForMember(x => x.ImageTwo, o => o.MapFrom<BannerResolver, string>(src => src.ImageTwo))
                .ForMember(x => x.ImageThree, o => o.MapFrom<BannerResolver, string>(src => src.ImageThree))
                .ForMember(x => x.ImageFour, o => o.MapFrom<BannerResolver, string>(src => src.ImageFour))
                .ForMember(x => x.SubPageImage, o => o.MapFrom<SubBannerResolver, string>(src => src.SubPageImage));
            CreateMap<BannerDto, Banner>();

            CreateMap<Educative, EducativeDto>()
                .ForMember(x => x.ImageUrl, o => o.MapFrom<EducativeResolver, string>(src => src.ImageUrl));

            CreateMap<EducativeDto, Educative>();

            CreateMap<ProductDto, Product>();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(x => x.Role, o => o.MapFrom(s => s.Role.Name));
            CreateMap<UserDto, User>();
            CreateMap<UserInformation, UserInformationDto>().ReverseMap();
            CreateMap<User, UserDetailDto>();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order, OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemUrlResover>());
            CreateMap<WishList, WishListDto>().ReverseMap();
            CreateMap<NewsLetterSubscription, NewsLetterDto>().ReverseMap();
            CreateMap<RecurringOrder, RecurringOrderDto>().ReverseMap();
            CreateMap<AppUser, CustomersDto>()
                .ForMember(s => s.DisplayName, o => o.MapFrom(d => d.DisplayName))
                .ForMember(c => c.Street, o => o.MapFrom(s => s.Address.Street))
                .ForMember(c => c.City, o => o.MapFrom(s => s.Address.City))
                .ForMember(c => c.State, o => o.MapFrom(s => s.Address.State))
                .ForMember(c => c.ZipCode, o => o.MapFrom(s => s.Address.ZipCode))
                .ForMember(c => c.PhoneNumber, o => o.MapFrom(s => s.Address.PhoneNumber))
                .ForMember(c => c.FirstName, o => o.MapFrom(s => s.Address.FirstName))
                .ForMember(c => c.LastName, o => o.MapFrom(s => s.Address.LastName));

            CreateMap<Message, MessageDto>().ReverseMap();
            CreateMap<Contact, ContactDto>().ReverseMap();

            CreateMap<ContactSocialMedia, Contact>();
            CreateMap<ContactAddressDto, Contact>();
           

        }
    }
}
