using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities;
using BLL.Interface;
using BLL.Specifications;
using com.vreshly.Dtos;
using com.vreshly.Errors;
using com.vreshly.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class ShopController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ShopController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            ViewBag.ClassName = "shop-wrapper";
            return View();
        }

        public async Task<IActionResult> ProductDetail(int productId)
        {
            
            var spec = new ProductSpecification(productId);
            var products = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(spec);
            var productsDto = _mapper.Map<Product, ProductDto>(products);
            ProductSpecParams productSpec = new ProductSpecParams
            {
                CategoryId = (int)productsDto.CategoryId,
                sort = "prodName"
            };
            var relatedProdSpec = new ProductSpecification(productSpec);
            var relateProducts = await _unitOfWork.Repository<Product>().ListAsync(relatedProdSpec);

            var relateProductsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(relateProducts);

            ProductDetailDto productDetailDto = new ProductDetailDto
            {
                ProductDetails = productsDto,
                RelatedProducts = relateProductsDto,
                Suggestions = relateProductsDto
            };
            return View(productDetailDto);
        }

        public async Task<IActionResult> WishList()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<WishListDto>>> GetWishList()
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            WishListSpecification spec = new WishListSpecification(email);
            var wishlist = await _unitOfWork.Repository<WishList>().ListAsync(spec);
            var wishlistDto = _mapper.Map<IReadOnlyList<WishList>, IReadOnlyList<WishListDto>>(wishlist);
            return Ok(wishlistDto);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateWishList([FromBody] WishListDto wishList)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();

            var mappedList = _mapper.Map<WishListDto, WishList>(wishList);
            mappedList.user = email;
            mappedList.CreatedDate = DateTime.Now;

            _unitOfWork.Repository<WishList>().Add(mappedList);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Product added to wishlist"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when adding Product to wishlist"));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWishlist([FromBody] WishListDto wishList)
        {
            WishListSpecification spec = new WishListSpecification(wishList.Id);
            var currentWishList = await _unitOfWork.Repository<WishList>().GetEntitiesWithSpec(spec);
            if (currentWishList == null) return NotFound(new ApiResponse(404));
            _unitOfWork.Repository<WishList>().Delete(currentWishList);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, $"{currentWishList.Product.ProductName} Successfully Deleted"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Deleting wishlist"));
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
