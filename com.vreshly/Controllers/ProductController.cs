using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities;
using BLL.Interface;
using BLL.Specifications;
using com.vreshly.Dtos;
using com.vreshly.Errors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class ProductController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var spec = new ProductSpecification();
            var products = await _unitOfWork.Repository<Product>().ListAsync(spec);
            var productsDto = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            return View(productsDto);
        }

        public async Task<IActionResult> ProductDetail(int id)
        {
            var spec = new ProductSpecification(id);
            var products = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(spec);
            var productsDto = _mapper.Map<Product, ProductDto>(products);
            return View(productsDto);
            
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct([FromBody]ProductDto model)
        {
            if (string.IsNullOrEmpty(model.ProductName)) return BadRequest(new ApiResponse(400, "Category Name was not supplied"));

            var spec = new ProductSpecification(model.ProductName.ToLower());
            var categories = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(spec);
            if (categories != null) return Conflict(new ApiResponse(209, "Product already exist"));

            model.CreatedDate = DateTime.Now;
            var productDto = _mapper.Map<ProductDto, Product>(model);
            _unitOfWork.Repository<Product>().Add(productDto);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Product Successfully Created"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when adding Product"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateGeneralInfo([FromBody] ProductDto model)
        {
            if (string.IsNullOrEmpty(model.ProductName)) return BadRequest(new ApiResponse(400, "Product Name was not supplied"));
            if (string.IsNullOrEmpty(model.ProductCodes)) return BadRequest(new ApiResponse(400, "Product Code was not supplied"));
            if (string.IsNullOrEmpty(model.ProductSummary)) return BadRequest(new ApiResponse(400, "Product Summary was not supplied"));
            if (string.IsNullOrEmpty(model.ProductDescription)) return BadRequest(new ApiResponse(400, "Product Description was not supplied"));

            var specwithId = new ProductSpecification((int)model.Id);
            var productWithId = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(specwithId);
            if (productWithId == null) return BadRequest(new ApiResponse(400, "Product does not exist"));

            //var spec = new ProductSpecification(model.ProductName.ToLower());
            //var categories = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(spec);
            //if (categories != null) return Conflict(new ApiResponse(209, "Product already exist"));

            productWithId.UpdateDate = DateTime.Now;
            productWithId.ProductName = model.ProductName;
            productWithId.ProductCodes = model.ProductCodes;
            productWithId.ProductSummary = model.ProductSummary;
            productWithId.ProductDescription = model.ProductDescription;
            _unitOfWork.Repository<Product>().Update(productWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Product General Info Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Product General Info"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateDataInfo([FromBody] ProductDto model)
        {
            if (model.CategoryId == 0) return BadRequest(new ApiResponse(400, "Product Name was not supplied"));
            if (model.SubCategoryId == 0) return BadRequest(new ApiResponse(400, "Product Code was not supplied"));
            if (model.BrandId == 0) return BadRequest(new ApiResponse(400, "Product Summary was not supplied"));


            var specwithId = new ProductSpecification((int)model.Id);
            var productWithId = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(specwithId);
            if (productWithId == null) return BadRequest(new ApiResponse(400, "Product does not exist"));

            //var spec = new ProductSpecification(model.ProductName.ToLower());
            //var categories = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(spec);
            //if (categories != null) return Conflict(new ApiResponse(209, "Product already exist"));

            productWithId.UpdateDate = DateTime.Now;
            productWithId.BrandId = model.BrandId;
            productWithId.CategoryId = model.CategoryId;
            productWithId.SubCategoryId = model.SubCategoryId;
            productWithId.SellingPrice = model.SellingPrice;
            productWithId.DiscountPrice = model.DiscountPrice;
            productWithId.Quantity = model.Quantity;
            productWithId.IsBestSeller = model.IsBestSeller;
            productWithId.IsFeaturedProduct = model.IsFeaturedProduct;
            _unitOfWork.Repository<Product>().Update(productWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Product General Info Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Product General Info "));
        }

        [HttpPut]
        public async Task<ActionResult> AddImage([FromForm] ProductDto model)
        {
            var specwithId = new ProductSpecification((int)model.Id);
            var productWithId = await _unitOfWork.Repository<Product>().GetEntitiesWithSpec(specwithId);
            if (productWithId == null) return BadRequest(new ApiResponse(400, "Product does not exist"));

            if (model.UploadImage != null)
            {
                if(model.ImageType == ImageTypes.MainImage)
                {
                    if (productWithId.MainImage != null)
                    {
                        string filePath = Path.Combine(GetPathAndFilename(), productWithId.MainImage);
                        System.IO.File.Delete(filePath);
                    }
                    productWithId.MainImage = ProcessUploadedFile(model);
                }
                if (model.ImageType == ImageTypes.ImageOne)
                {
                    if (productWithId.ImageOne != null)
                    {
                        string filePath = Path.Combine(GetPathAndFilename(), productWithId.ImageOne);
                        System.IO.File.Delete(filePath);
                    }
                    productWithId.ImageOne = ProcessUploadedFile(model);
                }
                if (model.ImageType == ImageTypes.ImageTwo)
                {
                    if (productWithId.ImageTwo != null)
                    {
                        string filePath = Path.Combine(GetPathAndFilename(), productWithId.ImageTwo);
                        System.IO.File.Delete(filePath);
                    }
                    productWithId.ImageTwo = ProcessUploadedFile(model);
                }
                if (model.ImageType == ImageTypes.ImageThree)
                {
                    if (productWithId.ImageThree != null)
                    {
                        string filePath = Path.Combine(GetPathAndFilename(), productWithId.ImageThree);
                        System.IO.File.Delete(filePath);
                    }
                    productWithId.ImageThree = ProcessUploadedFile(model);
                }
                if (model.ImageType == ImageTypes.ImageFour)
                {
                    if (productWithId.ImageFour != null)
                    {
                        string filePath = Path.Combine(GetPathAndFilename(), productWithId.ImageFour);
                        System.IO.File.Delete(filePath);
                    }
                    productWithId.ImageFour = ProcessUploadedFile(model);
                }


            }

            
            _unitOfWork.Repository<Product>().Update(productWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Image Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Updating Image"));
        }

        private string ProcessUploadedFile(ProductDto model)
        {
            string uniqueFileName = null;

            if (model.UploadImage != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.UploadImage.FileName;
                string filePath = Path.Combine(GetPathAndFilename(), uniqueFileName);
                //filePath = filePath.Replace("\\/", "\\");
                try
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.UploadImage.CopyTo(fileStream);
                    }
                }
                catch (Exception ex)
                {

                }

            }

            return uniqueFileName;
        }

        private string GetPathAndFilename()
        {
            return @$"{this.webHostEnvironment.WebRootPath}/Uploads/Product/";
        }
    }
}
