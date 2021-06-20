using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IDashboardService
    {
        Task<int> GetNewOrders();
        Task<int> GetTotalSales();
        Task<decimal> GetTotalRevenue();
        Task<int> GetTotalProduct();
        Task<int> GetTotalCustomers();
        Task<decimal> GetTotalRevenuePerMonth();
    }
}