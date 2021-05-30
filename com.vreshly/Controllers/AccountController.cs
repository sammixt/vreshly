using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities.Identity;
using BLL.Interface;
using com.vreshly.Dtos;
using com.vreshly.Errors;
using com.vreshly.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService,IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody]RegisterDto registerDto)
        {
            if (CheckEmailExist(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email Address is in user" } });
            }

            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return new CustomerDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Logon([FromBody]LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new CustomerDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpGet]
        public async Task<ActionResult<Boolean>> CheckEmailExist([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<CustomerDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);

            return new CustomerDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }


        [Authorize]
        [HttpPut]
        public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromBody]AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);

            foreach(var userAddress in user.Address)
            {
                if(userAddress.Id == address.Id)
                {
                    userAddress.FirstName = address.FirstName;
                    userAddress.City = address.City;
                    userAddress.LastName = address.LastName;
                    userAddress.State = address.State;
                    userAddress.Street = address.Street;
                    userAddress.ZipCode = address.ZipCode;
                }
            }

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(address);

            return BadRequest("Problem Updating the User");
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AddressDto>> AddUserAddress([FromBody] AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            Address userAddress = _mapper.Map<AddressDto, Address>(address);
            user.Address.Add(userAddress);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(address);

            return BadRequest("Problem Adding the User");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address,AddressDto>(user.Address?.FirstOrDefault());
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AddressDto>>> GetUserAddresses()
        {

            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            return Ok(_mapper.Map<IReadOnlyList<Address>, IReadOnlyList<AddressDto>>(user.Address));
        }

        public async Task<IActionResult> Information()
        {
            return View();
        }

    }
}
