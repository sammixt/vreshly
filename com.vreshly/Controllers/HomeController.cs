using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using com.vreshly.Models;
using BLL.Specifications;
using BLL.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using BLL.Entities;
using com.vreshly.Dtos;

namespace com.vreshly.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment, ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var spec = new ProductSpecification(true);
            var products = await _unitOfWork.Repository<Product>().ListAsync(spec);

            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            var productOutput = productsDto.Take(20).ToList();
            return PartialView(productOutput);
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
