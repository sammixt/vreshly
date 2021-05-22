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
    public class BrandController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BrandController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }


        // GET: /<controller>

        public async Task<IActionResult> Index()
        {
            var brands = await _unitOfWork.Repository<Brand>().ListAllAsync();
            var brandsDto = _mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<BrandDto>>(brands);
            return View(brandsDto);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrands()
        {
            var brands = await _unitOfWork.Repository<Brand>().ListAllAsync();
            var BrandDto = _mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<BrandDto>>(brands);
            return Ok(BrandDto);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetIndexBrands(int take)
        {
            var brands = await _unitOfWork.Repository<Brand>().ListAllAsync();
            var BrandDto = _mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<BrandDto>>(brands);
            BrandDto = BrandDto.Take(take).ToList();
            return Ok(BrandDto);
        }

        [HttpGet]
        public async Task<ActionResult<BrandDto>> GetBrand(int id)
        {
            var spec = new BrandSpecification(id);

            var brands = await _unitOfWork.Repository<Brand>().GetEntitiesWithSpec(spec);
            var BrandDto = _mapper.Map<Brand, BrandDto>(brands);
            return Ok(BrandDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddBrand([FromForm] BrandDto model)
        {
            string uniqueFileName = null;
            if (string.IsNullOrEmpty(model.BrandName)) return BadRequest(new ApiResponse(400, "Brand Name was not supplied"));

            var spec = new BrandSpecification(model.BrandName.ToLower());
            var brands = await _unitOfWork.Repository<Brand>().GetEntitiesWithSpec(spec);
            if (brands != null) return Conflict(new ApiResponse(209, "Brand already exist"));
            uniqueFileName = ProcessUploadedFile(model);
            model.CreatedDate = DateTime.Now;
            model.BrandLogo = uniqueFileName;
            var BrandDto = _mapper.Map<BrandDto, Brand>(model);
            _unitOfWork.Repository<Brand>().Add(BrandDto);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Brand Successfully Created"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when adding Brand"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBrand([FromForm] BrandDto model)
        {
            if (string.IsNullOrEmpty(model.BrandName)) return BadRequest(new ApiResponse(400, "Brand Name was not supplied"));

            var specwithId = new BrandSpecification((int)model.Id);
            var BrandWithId = await _unitOfWork.Repository<Brand>().GetEntitiesWithSpec(specwithId);
            if (BrandWithId == null) return BadRequest(new ApiResponse(400, "Brand does not exist"));

            var spec = new BrandSpecification(model.BrandName.ToLower());
            var brands = await _unitOfWork.Repository<Brand>().GetEntitiesWithSpec(spec);
            if (brands != null) return Conflict(new ApiResponse(209, "Brand already exist"));

            if (model.UploadImage != null)
            {
                if (BrandWithId.BrandLogo != null)
                {
                    string filePath = Path.Combine(GetPathAndFilename(), BrandWithId.BrandLogo);
                    System.IO.File.Delete(filePath);
                }

                model.BrandLogo = ProcessUploadedFile(model);
            }

            BrandWithId.UpdateDate = DateTime.Now;
            BrandWithId.BrandName = model.BrandName;
            _unitOfWork.Repository<Brand>().Update(BrandWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Brand Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Updating Brand"));
        }

        private string ProcessUploadedFile(BrandDto model)
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
            return @$"{this.webHostEnvironment.WebRootPath}/Uploads/Brand/";
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var specwithId = new BrandSpecification((int)id);
            var BrandWithId = await _unitOfWork.Repository<Brand>().GetEntitiesWithSpec(specwithId);
            if (BrandWithId == null) return BadRequest(new ApiResponse(400, "Brand does not exist"));

            var CurrentImage =  Path.Combine(GetPathAndFilename(), BrandWithId.BrandLogo);
            _unitOfWork.Repository<Brand>().Delete(BrandWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                if (System.IO.File.Exists(CurrentImage))
                    {
                        System.IO.File.Delete(CurrentImage);
                    }
                return Ok(new ApiResponse(200, "Brand Successfully Deleted"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Deleting Brand"));
        }
    }
}
