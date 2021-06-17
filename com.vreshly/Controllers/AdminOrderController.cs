using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace com.vreshly.Controllers
{
    public class AdminOrderController : Controller
    {
        public async Task<IActionResult> Orders()
        {
            return View();
        }
    }
}