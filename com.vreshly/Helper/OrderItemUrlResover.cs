using System;
using AutoMapper;
using BLL.Entities.OrderAggregate;
using com.vreshly.Dtos;
using Microsoft.Extensions.Configuration;

namespace com.vreshly.Helper
{
    public class OrderItemUrlResover : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderItemUrlResover(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
            {
                return $"{_config["AppUrl"]}//Uploads//Product//{source.ItemOrdered.PictureUrl}";
            }
            return null;
        }
    }
}
