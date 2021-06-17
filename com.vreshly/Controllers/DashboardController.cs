using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Mvc;

namespace com.vreshly.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashbaordService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashbaordService = dashboardService;
        }

        public async Task<ActionResult> GetDashboardData()
        {
            int totalSales = await _dashbaordService.GetTotalSales();
            decimal totalRevenue = await _dashbaordService.GetTotalRevenue();
            int totalCustomers = await _dashbaordService.GetTotalCustomers();
            int totalProducts = await _dashbaordService.GetTotalProduct();

            return Ok(new 
            {
                sales = totalSales,
                revenues = totalRevenue,
                products = totalProducts,
                customers = totalCustomers
            });
        }

    }
}