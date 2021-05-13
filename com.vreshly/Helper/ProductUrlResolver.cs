using System;
using BLL.Entities;
using AutoMapper;
using com.vreshly.Dtos;
using Microsoft.Extensions.Configuration;

namespace com.vreshly.Helper
{
    public class ProductUrlResolver : IMemberValueResolver<Product, ProductDto, string, string>
    {
        private readonly IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        //public string Resolve(Brand source, BrandDto destination, string destMember, ResolutionContext context)
        //{
        //    if (!string.IsNullOrEmpty(source.BrandLogo))
        //    {
        //        return $"{_config["AppUrl"]}//Uploads//Brand//{source.BrandLogo}";
        //    }
        //    return null;
        //}

        //public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        //{
        //    if (!string.IsNullOrEmpty(source.MainImage))
        //    {
        //        return $"{_config["AppUrl"]}//Uploads//Product//{destMember}";
        //    }
        //    else if (!string.IsNullOrEmpty(source.ImageOne))
        //    {
        //        return $"{_config["AppUrl"]}//Uploads//Product//{destMember}";
        //    }
        //    else if (!string.IsNullOrEmpty(source.ImageTwo))
        //    {
        //        return $"{_config["AppUrl"]}//Uploads//Product//{destMember}";
        //    }
        //    else if (!string.IsNullOrEmpty(source.ImageThree))
        //    {
        //        return $"{_config["AppUrl"]}//Uploads//Product//{destMember}";
        //    }
        //    else if (!string.IsNullOrEmpty(source.ImageFour))
        //    {
        //        return $"{_config["AppUrl"]}//Uploads//Product//{destMember}";
        //    }
        //    return null;
        //}

        public string Resolve(Product source, ProductDto destination, string sourceMember, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return $"{_config["AppUrl"]}//Uploads//Product//{sourceMember}";
            }
            return null;
        }
    }
}
