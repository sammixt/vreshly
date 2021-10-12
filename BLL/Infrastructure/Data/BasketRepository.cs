using System;
using System.Text.Json;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Interface;
using BLL.Specifications;
//using StackExchange.Redis;

namespace BLL.Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        IUnitOfWork _unitOfWork;
        public BasketRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //private readonly IDatabase _database;

        //public BasketRepository(IConnectionMultiplexer redis)
        //{
        //    _database = redis.GetDatabase();
        //}

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            AppRedisSpecification spec = new AppRedisSpecification(basketId);
            var data = await _unitOfWork.Repository<AppRedis>().GetEntitiesWithSpec(spec);
            if (data == null) return false;
            
            _unitOfWork.Repository<AppRedis>().Delete(data);
            var deleted = await _unitOfWork.Complete();
            return (deleted > 0);   
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            //var data = await _database.StringGetAsync(basketId);
            AppRedisSpecification spec = new AppRedisSpecification(basketId);
            var data = await _unitOfWork.Repository<AppRedis>().GetEntitiesWithSpec(spec);
            if (data == null) return null;

            return JsonSerializer.Deserialize<CustomerBasket>(data.Value);

            //return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            AppRedisSpecification spec = new AppRedisSpecification(basket.Id);
            var dataExist = await _unitOfWork.Repository<AppRedis>().GetEntitiesWithSpec(spec);
            if (dataExist == null)
            {
                var expiryDate = DateTimeOffset.UtcNow.AddDays(20);
                string data = JsonSerializer.Serialize(basket);
                AppRedis redis = new AppRedis();
                redis.Id = basket.Id;
                redis.Value = data;
                redis.Expiry = expiryDate;
                redis.CreatedDate = DateTime.Now;
                _unitOfWork.Repository<AppRedis>().Add(redis);
            }
            else
            {
                dataExist.Value = JsonSerializer.Serialize(basket);
                _unitOfWork.Repository<AppRedis>().Update(dataExist);
            }
            
            var created = await _unitOfWork.Complete();
            //var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),TimeSpan.FromDays(30));

            if (created < 1) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
