using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Entities.Identity;
using BLL.Entities.OrderAggregate;

namespace BLL.Interface
{
    public interface IRecurringOrderService
    {
        Task<int> CreateRecurringOrder(RecurringOrder order);
        Task<IReadOnlyList<RecurringOrder>> GetRecurringOrderByEmail(string email);
        Task<int> DeleteRecurringOrder(long id);
        Task UpdateRecurringOrder(RecurringOrder order);
        Task<IEnumerable<RecurringOrder>> GetAllRecurringOrders();
        Task<AppUser> GetCustomersInfo(string email);
        Task<RecurringOrder> GetRecurringOrderById(long id);
        int GetDaysToAdd(RecurringFrequency frequency);


    }
}