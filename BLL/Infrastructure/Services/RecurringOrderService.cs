using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Entities.Identity;
using BLL.Entities.OrderAggregate;
using BLL.Interface;
using BLL.Specifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BLL.Infrastructure.Services
{
    public class RecurringOrderService : IRecurringOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;

        public RecurringOrderService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<int> CreateRecurringOrder(RecurringOrder order)
        {
            order.NextDelievery = DateTime.Now.AddDays(GetDaysToAdd(order.Frequency));
            _unitOfWork.Repository<RecurringOrder>().Add(order);
            int result = await _unitOfWork.Complete();
            return result;
        }

        public async Task<IReadOnlyList<RecurringOrder>> GetRecurringOrderByEmail(string email)
        {
            RecurringOrderSpecification spec = new RecurringOrderSpecification(email);
            var recurringOrders = await _unitOfWork.Repository<RecurringOrder>().ListAsync(spec);
            return recurringOrders;
        }

        public async Task<RecurringOrder> GetRecurringOrderById(long id)
        {
            RecurringOrderSpecification spec = new RecurringOrderSpecification(id);
            var recurringOrders = await _unitOfWork.Repository<RecurringOrder>().GetEntitiesWithSpec(spec);
            return recurringOrders;
        }

        public async Task<int> DeleteRecurringOrder(long id)
        {
            RecurringOrderSpecification spec = new RecurringOrderSpecification(id);
            var recurringOrders = await _unitOfWork.Repository<RecurringOrder>().GetEntitiesWithSpec(spec);
            _unitOfWork.Repository<RecurringOrder>().Delete(recurringOrders);
            int result = await _unitOfWork.Complete();
            return result;
        }

        public async Task UpdateRecurringOrder(RecurringOrder order)
        {
            _unitOfWork.Repository<RecurringOrder>().Update(order);
            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<RecurringOrder>> GetAllRecurringOrders()
        {
            RecurringOrderSpecification spec = new RecurringOrderSpecification();
            var recurringOrders = (IEnumerable<RecurringOrder>)await _unitOfWork.Repository<RecurringOrder>().ListAsync(spec);
            return recurringOrders;
        }

        public async Task<AppUser> GetCustomersInfo(string email)
        {
            var userDetails = await Task.Run(() => _userManager.Users.Where(x => x.Email == email).Include(x => x.Address).FirstOrDefault());
            return userDetails;
        }

        public int GetDaysToAdd(RecurringFrequency frequency)
        {
            return frequency switch
            {
                RecurringFrequency.Daily => 1,
                RecurringFrequency.Weekly => 7,
                RecurringFrequency.BiWeekly => 3,
                RecurringFrequency.Monthly => 30,
                RecurringFrequency.BiMonthly => 15,
                RecurringFrequency.Quaterly => 90,
                _ => 0
            };

        }

    }
}
