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
using com.vreshly.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class SettingsController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ILogger logger;

        public SettingsController(IUnitOfWork _unitOfWork,IMapper _mapper, IWebHostEnvironment webHostEnvironment,
            ILogger logger)
        {
            this._unitOfWork = _unitOfWork;
            this._mapper = _mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.logger = logger;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Contact()
        {
            ViewBag.PageName = "Contacts";
            ViewBag.Breadcrumbs = "<li class=\"breadcrumb-item\"><a href=\"#\">Settings</a></li><li class=\"breadcrumb-item active\">Contact</li>";

            ContactSpecification spec = new ContactSpecification();
            var contact = await _unitOfWork.Repository<Contact>().GetEntitiesWithSpec(spec);
            var contactDto = _mapper.Map<Contact, ContactDto>(contact);
            return View(contactDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress([FromBody]ContactAddressDto address)
        {
            ContactSpecification spec = new ContactSpecification();
            var contact = await _unitOfWork.Repository<Contact>().GetEntitiesWithSpec(spec);
            if(contact == null)
            {
                var mappedContact = _mapper.Map<ContactAddressDto, Contact>(address);
                _unitOfWork.Repository<Contact>().Add(mappedContact);

            }
            else
            {
                contact.Address = address.Address ?? contact.Address;
                contact.City = address.City ?? contact.City;
                contact.State = address.State ?? contact.State;
                contact.Country = address.Country ?? contact.Country;
                contact.Email = address.Email ?? contact.Email;
                contact.PhoneNumber = address.PhoneNumber ?? contact.PhoneNumber;
                _unitOfWork.Repository<Contact>().Update(contact);
            }

            int commit = await _unitOfWork.Complete();
            if (commit > 0)
                return Ok(new { message = "address update" });
            else
                return BadRequest(new ApiException(400, "An error occurred"));
        }

        [HttpPost]
        public async Task<IActionResult> AddSocialMedia([FromBody] ContactSocialMedia social)
        {
            ContactSpecification spec = new ContactSpecification();
            var contact = await _unitOfWork.Repository<Contact>().GetEntitiesWithSpec(spec);
            if (contact == null)
            {
                var mappedContact = _mapper.Map<ContactSocialMedia, Contact>(social);
                _unitOfWork.Repository<Contact>().Add(mappedContact);

            }
            else
            {
                contact.Facebook = social.Facebook ?? contact.Facebook;
                contact.Instagram = social.Instagram ?? contact.Instagram;
                contact.Youtube = social.Youtube ?? contact.Youtube;
                contact.Twitter = social.Twitter ?? contact.Twitter;
                _unitOfWork.Repository<Contact>().Update(contact);
            }

            int commit = await _unitOfWork.Complete();
            if (commit > 0)
                return Ok(new { message = "social media handle" });
            else
                return BadRequest(new ApiException(400, "An error occurred"));
        }

        public async Task<IActionResult> Banner()
        {
            ViewBag.PageName = "Banner";
            ViewBag.Breadcrumbs = "<li class=\"breadcrumb-item\"><a href=\"#\">Settings</a></li><li class=\"breadcrumb-item active\">Banner</li>";
            BannerSpecification spec = new BannerSpecification();
            var banner = await _unitOfWork.Repository<Banner>().GetEntitiesWithSpec(spec);
            var mapped = _mapper.Map<Banner, BannerDto> (banner);
            return View(mapped);
        }

        [HttpPost]
        public async Task<ActionResult> AddSlider([FromForm] BannerInputDto input)
        {
            if (input.UploadImage == null) return BadRequest(new ApiResponse(400, "Provide Image"));
            string fileName = ProcessUploadedFile(input);

            if(string.IsNullOrEmpty(fileName)) return BadRequest (new ApiResponse(400, "An Error occurred"));

            BannerSpecification spec = new BannerSpecification();
            var banner = await _unitOfWork.Repository<Banner>().GetEntitiesWithSpec(spec);

            BannerDto model = BindFileName(input.SliderTypes, input, fileName);
            if (banner == null)
            {
                var mapped = _mapper.Map<BannerDto, Banner>(model);
                 _unitOfWork.Repository<Banner>().Add(mapped);
            }
            else
            {
                banner.ImageOne = model.ImageOne ?? banner.ImageOne;
                banner.ImageTwo = model.ImageTwo ?? banner.ImageTwo;
                banner.ImageThree = model.ImageThree ?? banner.ImageThree;
                banner.ImageFour = model.ImageFour ?? banner.ImageFour;
                banner.TitleOne = model.TitleOne ?? banner.TitleOne;
                banner.TitleTwo = model.TitleTwo ?? banner.TitleTwo;
                banner.TitleThree = model.TitleThree ?? banner.TitleThree;
                banner.TitleFour = model.TitleFour ?? banner.TitleFour;
                banner.SubTitleOne = model.SubTitleOne ?? banner.SubTitleOne;
                banner.SubTitleTwo = model.SubTitleTwo ?? banner.SubTitleTwo;
                banner.SubTitleThree = model.SubTitleThree ?? banner.SubTitleThree;
                banner.SubTitleFour = model.SubTitleFour ?? banner.SubTitleFour;
                banner.SubPageImage = model.SubPageImage ?? banner.SubPageImage;

                _unitOfWork.Repository<Banner>().Update(banner);
            }
            int commit = await _unitOfWork.Complete();
            if (commit > 0)
                return Ok(new { message = "Updated" });
            else
                return BadRequest(new ApiResponse(400, "An error occurred"));
        }

        private string ProcessUploadedFile(BannerInputDto model)
        {
            string uniqueFileName = null;

            if (model.UploadImage != null)
            {
                uniqueFileName = GetNewFileName(model.SliderTypes);
                string filePath = Path.Combine(
                   model.SliderTypes == SliderTypes.SubPageBanner
                   ? GetPathAndFilenameFoSubPageBanner()
                   :GetPathAndFilenameForBanner(),
                    uniqueFileName);
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
                    logger.Error(ex);
                }

            }

            return uniqueFileName;
        }

        private string GetPathAndFilenameForBanner()
        {
            return @$"{this.webHostEnvironment.WebRootPath}/frontendassets/images/slider/";
        }

        private string GetPathAndFilenameFoSubPageBanner()
        {
            return @$"{this.webHostEnvironment.WebRootPath}/frontendassets/images/bg/";
        }

        private string GetNewFileName(SliderTypes slider)
        {
            return slider switch
            {
                SliderTypes.BannerOne => "1-1.jpg",
                SliderTypes.BannerTwo => "1-2.jpg",
                SliderTypes.BannerThree => "2-1.jpg",
                SliderTypes.BannerFour => "2-2.jpg",
                SliderTypes.SubPageBanner => "1-1.jpg",
                _ => null
            };
        }

        private BannerDto BindFileName(SliderTypes slider, BannerInputDto model, string file)
        {
            BannerDto bannerDto = new BannerDto();
            switch(slider)
            {

                case SliderTypes.BannerOne:
                    bannerDto.ImageOne = file;
                    bannerDto.SubTitleOne = model.SubTitle;
                    bannerDto.TitleOne = model.Title;
                break;
                case SliderTypes.BannerTwo:
                    bannerDto.ImageTwo = file;
                    bannerDto.SubTitleTwo = model.SubTitle;
                    bannerDto.TitleTwo = model.Title;
                    break;
                case SliderTypes.BannerThree:
                    bannerDto.ImageThree = file;
                    bannerDto.SubTitleThree = model.SubTitle;
                    bannerDto.TitleThree = model.Title;
                    break;
                case SliderTypes.BannerFour:
                    bannerDto.ImageFour = file;
                    bannerDto.SubTitleFour = model.SubTitle;
                    bannerDto.TitleFour = model.Title;
                    break;
                case SliderTypes.SubPageBanner:
                    bannerDto.SubPageImage = file;
                    break;
                
            };

            return bannerDto;
        }

        public async Task<IActionResult> Educative()
        {
            ViewBag.PageName = "Educative";
            ViewBag.Breadcrumbs = "<li class=\"breadcrumb-item\"><a href=\"#\">Settings</a></li><li class=\"breadcrumb-item active\">Educative</li>";
            var spec = new EducativeSpecification(EducativeType.Importance);
            var educative = await _unitOfWork.Repository<Educative>().ListAsync(spec);
            var educativeDto = _mapper.Map<IReadOnlyList<Educative>, IReadOnlyList<EducativeDto>>(educative);
            return View(educativeDto);
        }

        public async Task<IActionResult> AboutUs()
        {
            ViewBag.PageName = "About Us";
            ViewBag.Breadcrumbs = "<li class=\"breadcrumb-item\"><a href=\"#\">Settings</a></li><li class=\"breadcrumb-item active\">About Us</li>";
            var spec = new EducativeSpecification(EducativeType.AboutUs);
            var educative = await _unitOfWork.Repository<Educative>().ListAsync(spec);
            var educativeDto = _mapper.Map<IReadOnlyList<Educative>, IReadOnlyList<EducativeDto>>(educative);
            return View(educativeDto);
        }

        [HttpGet]
        public async Task<ActionResult<EducativeDto>> GetEducative(int id)
        {
            var spec = new EducativeSpecification(id);

            var educative = await _unitOfWork.Repository<Educative>().GetEntitiesWithSpec(spec);
            var educativeDto = _mapper.Map<Educative, EducativeDto>(educative);
            return Ok(educativeDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddEducative([FromForm]EducativeDto educativeDto)
        {
            string uniqueFileName = null;
            if (string.IsNullOrEmpty(educativeDto.Title)) return BadRequest(new ApiResponse(400, "Title was not supplied"));
            if (string.IsNullOrEmpty(educativeDto.Content)) return BadRequest(new ApiResponse(400, "Content was not supplied"));
            if (string.IsNullOrEmpty(educativeDto.VideoLink)) return BadRequest(new ApiResponse(400, "Video Link is required"));

            uniqueFileName = ProcessUploadedFile(educativeDto);
            educativeDto.CreatedDate = DateTime.Now;
            educativeDto.ImageUrl = uniqueFileName;
            var educativeMapped = _mapper.Map<EducativeDto, Educative>(educativeDto);
            educativeMapped.EducativeType = (EducativeType)Enum.Parse(typeof(EducativeType), educativeDto.EducativeType, true);
            _unitOfWork.Repository<Educative>().Add(educativeMapped);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Content Created"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Creating Content"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEducative([FromForm] EducativeDto model)
        {
            if (string.IsNullOrEmpty(model.Title)) return BadRequest(new ApiResponse(400, "Title was not supplied"));
            if (string.IsNullOrEmpty(model.Content)) return BadRequest(new ApiResponse(400, "Content was not supplied"));
            if (string.IsNullOrEmpty(model.VideoLink)) return BadRequest(new ApiResponse(400, "Video Link is required"));

            var specwithId = new EducativeSpecification((int)model.Id);
            var EducativeWithId = await _unitOfWork.Repository<Educative>().GetEntitiesWithSpec(specwithId);
            if (EducativeWithId == null) return BadRequest(new ApiResponse(400, "Content does not exist"));

           
            if (model.UploadImage != null)
            {
                if (EducativeWithId.ImageUrl != null)
                {
                    string filePath = Path.Combine(GetPathAndFilename(), EducativeWithId.ImageUrl);
                    System.IO.File.Delete(filePath);
                }

                model.ImageUrl = ProcessUploadedFile(model);
            }

            EducativeWithId.UpdateDate = DateTime.Now;
            EducativeWithId.Title = model.Title ?? EducativeWithId.Title;
            EducativeWithId.Content = model.Content ?? EducativeWithId.Content;
            EducativeWithId.VideoLink = model.VideoLink ?? EducativeWithId.VideoLink;
            _unitOfWork.Repository<Educative>().Update(EducativeWithId);
            int result = await _unitOfWork.Complete();
            if (result >0 )
            {
                return Ok(new ApiResponse(200, "Content Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Updating Content"));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEducative(int id)
        {
            var specwithId = new EducativeSpecification(id);
            var EducativeWithId = await _unitOfWork.Repository<Educative>().GetEntitiesWithSpec(specwithId);
            if (EducativeWithId == null) return BadRequest(new ApiResponse(400, "Content does not exist"));

            var CurrentImage = Path.Combine(GetPathAndFilename(), EducativeWithId.ImageUrl);
            _unitOfWork.Repository<Educative>().Delete(EducativeWithId);
            int result = await _unitOfWork.Complete();
            if (result > 0)
            {
                if (System.IO.File.Exists(CurrentImage))
                {
                    System.IO.File.Delete(CurrentImage);
                }
                return Ok(new ApiResponse(200, "Content Successfully Deleted"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Deleting Content"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEducativeStatus(int id)
        {
            var specwithId = new EducativeSpecification(id);
            
            var EducativeWithId = await _unitOfWork.Repository<Educative>().GetEntitiesWithSpec(specwithId);
           
            if (EducativeWithId == null) return BadRequest(new ApiResponse(400, "Content does not exist"));

            var ActiveStatusSpec = new EducativeSpecification(EducativeWithId.EducativeType,true);
            var EducativeCurrentActive = await _unitOfWork.Repository<Educative>().GetEntitiesWithSpec(ActiveStatusSpec);
            if (EducativeCurrentActive != null)
            {
                EducativeCurrentActive.Status = false;
                _unitOfWork.Repository<Educative>().Update(EducativeCurrentActive);
            }
            
            EducativeWithId.Status = EducativeWithId.Status ? false : true;
            _unitOfWork.Repository<Educative>().Update(EducativeWithId);
            int result = await _unitOfWork.Complete();
            if (result > 0)
            {
                return Ok(new ApiResponse(200, "Content Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Updating Content"));
        }

        private string ProcessUploadedFile(EducativeDto model)
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
                    logger.Error(ex);
                }

            }

            return uniqueFileName;
        }

        private string GetPathAndFilename()
        {
            return @$"{this.webHostEnvironment.WebRootPath}/frontendassets/images/feature/";
        }
    }
}
