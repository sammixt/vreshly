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
