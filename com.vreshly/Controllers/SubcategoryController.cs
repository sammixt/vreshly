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
    public class SubcategoryController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        // GET: /<controller>/
        public SubcategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {

            var spec = new SubCategorySpecification();
            var categories = await _unitOfWork.Repository<SubCategory>().ListAsync(spec);
            var subcategoryDto = _mapper.Map<IReadOnlyList<SubCategory>, IReadOnlyList<SubCategoryDto>>(categories);
            return View(subcategoryDto);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetSubCategories()
        {
            var categories = await _unitOfWork.Repository<SubCategory>().ListAllAsync();
            var subcategoryDto = _mapper.Map<IReadOnlyList<SubCategory>, IReadOnlyList<SubCategoryDto>>(categories);
            return Ok(subcategoryDto);
        }

        [HttpGet]
        public async Task<ActionResult<CategoryDto>> GetSubCategory(int id)
        {
            var spec = new SubCategorySpecification(id);

            var categories = await _unitOfWork.Repository<SubCategory>().GetEntitiesWithSpec(spec);
            var categoryDto = _mapper.Map<SubCategory, SubCategoryDto>(categories);
            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddSubCategory([FromBody] SubCategoryDto model)
        {
            if (string.IsNullOrEmpty(model.SubCategoryName)) return BadRequest(new ApiResponse(400, "Sub Category Name was not supplied"));

            var spec = new SubCategorySpecification((int)model.CategoryId, model.SubCategoryName.ToLower());
            var categories = await _unitOfWork.Repository<SubCategory>().GetEntitiesWithSpec(spec);
            if (categories != null) return Conflict(new ApiResponse(209, "Sub Category already exist for Category Supplied"));

            model.CreatedDate = DateTime.Now;
            var categoryDto = _mapper.Map<SubCategoryDto, SubCategory>(model);
            _unitOfWork.Repository<SubCategory>().Add(categoryDto);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Sub Category Successfully Created"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when adding Sub Category"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSubCategory([FromBody] SubCategoryDto model)
        {
            if (string.IsNullOrEmpty(model.SubCategoryName)) return BadRequest(new ApiResponse(400, "Sub Category Name was not supplied"));

            var specwithId = new SubCategorySpecification((int)model.Id);
            var categoryWithId = await _unitOfWork.Repository<SubCategory>().GetEntitiesWithSpec(specwithId);
            if (categoryWithId == null) return BadRequest(new ApiResponse(400, "Sub Category does not exist"));

            var spec = new SubCategorySpecification((int)model.CategoryId, model.SubCategoryName.ToLower());
            var categories = await _unitOfWork.Repository<SubCategory>().GetEntitiesWithSpec(spec);
            if (categories != null) return Conflict(new ApiResponse(209, "Sub Category already exist for Category Supplied"));

            categoryWithId.UpdateDate = DateTime.Now;
            var categoryDto = _mapper.Map<SubCategoryDto, SubCategory>(model);
            categoryWithId.SubCategoryName = model.SubCategoryName;
            categoryWithId.CategoryId = model.CategoryId;
            _unitOfWork.Repository<SubCategory>().Update(categoryWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Sub Category Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Updating Sub Category"));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var specwithId = new SubCategorySpecification((int)id);
            var categoryWithId = await _unitOfWork.Repository<SubCategory>().GetEntitiesWithSpec(specwithId);
            if (categoryWithId == null) return BadRequest(new ApiResponse(400, "Sub Category does not exist"));
            _unitOfWork.Repository<SubCategory>().Delete(categoryWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Sub Category Successfully Deleted"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Deleting Sub Category"));
        }
    }
}
