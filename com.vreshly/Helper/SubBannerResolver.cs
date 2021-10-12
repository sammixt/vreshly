using AutoMapper;
using BLL.Entities;
using com.vreshly.Dtos;
using Microsoft.Extensions.Configuration;

namespace com.vreshly.Helper
{
    public class SubBannerResolver : IMemberValueResolver<Banner, BannerDto, string, string>
    {
        private readonly IConfiguration _config;
        public SubBannerResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Banner source, BannerDto destination, string sourceMember, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(sourceMember))
            {
                return $"{_config["AppUrl"]}//frontendassets//images//bg//{sourceMember}";

            }
            return null;
        }
    }
}
