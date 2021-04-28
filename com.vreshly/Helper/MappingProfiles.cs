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

            CreateMap<Brand, BrandDto>().ReverseMap();

        }
    }
}
