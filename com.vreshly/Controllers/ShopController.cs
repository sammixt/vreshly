﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class ShopController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            ViewBag.ClassName = "shop-wrapper";
            return View();
        }

        public async Task<IActionResult> ProductDetail(int productId)
        {
            return View();
        }

        public async Task<IActionResult> WishList()
        {
            return View();
        }

        public async Task<IActionResult> Cart()
        {
            return View();
        }

        public async Task<IActionResult> Checkout()
        {
            return View();
        }
    }
}