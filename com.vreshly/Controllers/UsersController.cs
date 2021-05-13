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
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class UsersController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> GetRoles()
        {
            var roles = await _unitOfWork.Repository<Role>().ListAllAsync();
            var rolesDto = _mapper.Map<IReadOnlyList<Role>, IReadOnlyList<RoleDto>>(roles);
            return Ok(rolesDto);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] UserDto model)
        {
            if (string.IsNullOrEmpty(model.Username)) return BadRequest(new ApiResponse(400, "Username was not supplied"));
            if (string.IsNullOrEmpty(model.FullName)) return BadRequest(new ApiResponse(400, "Fullname was not supplied"));
            if (string.IsNullOrEmpty(model.Email)) return BadRequest(new ApiResponse(400, "Email was not supplied"));

            var spec = new UserSpecification(model.Username.ToLower());
            var categories = await _unitOfWork.Repository<User>().GetEntitiesWithSpec(spec);
            if (categories != null) return Conflict(new ApiResponse(209, "Username already exist"));

            //model.CreatedDate = DateTime.Now;
            model.Status = (int)UserStatus.Active;
            var userDT = _mapper.Map<UserDto, User>(model);
            userDT.CreatedDate = DateTime.Now;
            _unitOfWork.Repository<User>().Add(userDT);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "User Successfully Created"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when adding User"));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserDto>>> GetUsers()
        {
            var spec = new UserSpecification();
            var users = await _unitOfWork.Repository<User>().ListAsync(spec);
            var userDto = _mapper.Map<IReadOnlyList<User>, IReadOnlyList<UserDto>>(users);
            return Ok(new {data = userDto});
        }

        public async Task<ActionResult> EditUser(int id)
        {
            var spec = new UserSpecification(id);
            var users = await _unitOfWork.Repository<User>().GetEntitiesWithSpec(spec);
            var userDto = _mapper.Map<User, UserDetailDto>(users);
            return View(userDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserInfo([FromBody] UserDto model)
        {
            if (string.IsNullOrEmpty(model.FullName)) return BadRequest(new ApiResponse(400, "Please provide full name"));
            if (string.IsNullOrEmpty(model.Username)) return BadRequest(new ApiResponse(400, "Please provide User Name"));
            if (string.IsNullOrEmpty(model.Email)) return BadRequest(new ApiResponse(400, "Please provide Email"));
            
            var specwithId = new UserSpecification((int)model.Id);
            var userDetails = await _unitOfWork.Repository<User>().GetEntitiesWithSpec(specwithId);
            if (userDetails == null) return BadRequest(new ApiResponse(400, "User does not exist"));

            if(userDetails.Username.Trim().ToLower() != model.Username.Trim().ToLower())
            {
                var spec = new UserSpecification(model.Username.ToLower());
                var user = await _unitOfWork.Repository<User>().GetEntitiesWithSpec(spec);
               if(user != null) return Conflict(new ApiResponse(209, "Username already exist"));
            }


            userDetails.UpdateDate = DateTime.Now;
            userDetails.Username = model.Username;
            userDetails.FullName = model.FullName;
            userDetails.RoleId = model.RoleId;
            userDetails.Status = model.Status;
            userDetails.Email = model.Email;
            _unitOfWork.Repository<User>().Update(userDetails);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "User Info Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Product General Info"));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUserAddressInfo([FromBody] UserInformationDto model)
        {
            

            var specwithId = new UserInformationSpecification(model.UserId);
            var userDetails = await _unitOfWork.Repository<UserInformation>().GetEntitiesWithSpec(specwithId);
            if (userDetails == null)
            {
                var userDT = _mapper.Map<UserInformationDto, UserInformation>(model);
                userDT.CreatedDate = DateTime.Now;
                _unitOfWork.Repository<UserInformation>().Add(userDT);
                int insertresult = await _unitOfWork.Complete();
                if (insertresult == 1)
                {
                    return Ok(new ApiResponse(200, "User Contact Update"));
                }

                return BadRequest(new ApiResponse(500, "An error occurred when Updating Users Contact"));
            }
            else
            {
                userDetails.DateOfBirth = model.DateOfBirth;
                userDetails.AddressLineOne = model.AddressLineOne;
                userDetails.AddressLineTwo = model.AddressLineTwo;
                userDetails.City = model.City;
                userDetails.Country = model.Country;
                userDetails.Gender = model.Gender;
                userDetails.PhoneNumber = model.PhoneNumber;
                userDetails.State = model.State;
                userDetails.UpdateDate = DateTime.Now;
                _unitOfWork.Repository<UserInformation>().Update(userDetails);
                int result = await _unitOfWork.Complete();
                if (result == 1)
                {
                    return Ok(new ApiResponse(200, "User Info Successfully Updated"));

                }
                return BadRequest(new ApiResponse(500, "An error occurred when User Contact Info"));
            }
  
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePassword([FromBody] UserDto model)
        {

            
            if(!model.PasswordMatch) return BadRequest(new ApiResponse(400, "Password and Confirm Password does not match"));

            var specwithId = new UserSpecification((int)model.Id);
            var userDetails = await _unitOfWork.Repository<User>().GetEntitiesWithSpec(specwithId);
            if (userDetails == null) return BadRequest(new ApiResponse(400, "User does not exist"));

            userDetails.UpdateDate = DateTime.Now;
            userDetails.Password = model.Password;
            
            _unitOfWork.Repository<User>().Update(userDetails);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Password Info Successfully Updated"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when updating  Password"));

        }
    }
}
