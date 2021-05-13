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
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // GET: /<controller>/
        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.PageName = "Category";
            ViewBag.Breadcrumbs = "<li class=\"breadcrumb-item\"><a href=\"#\">Category</a></li><li class=\"breadcrumb-item active\">Index</li>";
            var categories = await _unitOfWork.Repository<Category>().ListAllAsync();
            var categoryDto = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDto>>(categories);
            return View(categoryDto);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var categories = await _unitOfWork.Repository<Category>().ListAllAsync();
            var categoryDto = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryDto>>(categories);
            return Ok(categoryDto);
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var spec = new CategorySpecification(id);

            var categories = await _unitOfWork.Repository<Category>().GetEntitiesWithSpec(spec);
            var categoryDto = _mapper.Map<Category, CategoryDto>(categories);
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody]CategoryDto model)
        {
            if (string.IsNullOrEmpty(model.CategoryName)) return BadRequest(new ApiResponse(400,"Category Name was not supplied"));

            var spec = new CategorySpecification(model.CategoryName.ToLower());
            var categories = await _unitOfWork.Repository<Category>().GetEntitiesWithSpec(spec);
            if(categories != null ) return Conflict(new ApiResponse(209, "Category already exist"));

            model.CreatedDate = DateTime.Now;
            var categoryDto = _mapper.Map<CategoryDto, Category>(model);
            _unitOfWork.Repository<Category>().Add(categoryDto);
            int result = await _unitOfWork.Complete();
            if(result == 1)
            {
                return Ok(new ApiResponse(200, "Category Successfully Created"));
            }

            return BadRequest(new ApiResponse(500,"An error occurred when adding Category"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory([FromBody] CategoryDto model)
        {
            if (string.IsNullOrEmpty(model.CategoryName)) return BadRequest(new ApiResponse(400, "Category Name was not supplied"));

            var specwithId = new CategorySpecification((int)model.Id);
            var categoryWithId = await _unitOfWork.Repository<Category>().GetEntitiesWithSpec(specwithId);
            if (categoryWithId == null) return BadRequest(new ApiResponse(400, "Category does not exist"));

            var spec = new CategorySpecification(model.CategoryName.ToLower());
            var categories = await _unitOfWork.Repository<Category>().GetEntitiesWithSpec(spec);
            if (categories != null) return Conflict(new ApiResponse(209, "Category already exist"));

            categoryWithId.UpdateDate = DateTime.Now;
            var categoryDto = _mapper.Map<CategoryDto, Category>(model);
            categoryWithId.CategoryName = model.CategoryName;
            _unitOfWork.Repository<Category>().Update(categoryWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Category Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Updating Category"));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var specwithId = new CategorySpecification((int)id);
            var categoryWithId = await _unitOfWork.Repository<Category>().GetEntitiesWithSpec(specwithId);
            if (categoryWithId == null) return BadRequest(new ApiResponse(400, "Category does not exist"));
            _unitOfWork.Repository<Category>().Delete(categoryWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Category Successfully Deleted"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Deleting Category"));
        }
    }
}
