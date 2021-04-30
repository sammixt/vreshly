using System;
using BLL.Entities;
using AutoMapper;
using com.vreshly.Dtos;
using Microsoft.Extensions.Configuration;

namespace com.vreshly.Helper
{
    public class BrandUrlRsolver : IValueResolver<Brand, BrandDto, string>
    {
        private readonly IConfiguration _config;

        public BrandUrlRsolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Brand source, BrandDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.BrandLogo))
            {
                return $"{_config["AppUrl"]}//Uploads//Brand//{source.BrandLogo}";
            }
            return null;
        }
    }
}
