using System;
using AutoMapper;
using BLL.Entities;
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

            CreateMap<ProductDto, Product>();

            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(x => x.Role, o => o.MapFrom(s => s.Role.Name));
            CreateMap<UserDto, User>();
            CreateMap<UserInformation, UserInformationDto>().ReverseMap();
            CreateMap<User, UserDetailDto>();

        }
    }
}
