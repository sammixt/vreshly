using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities.Identity;
using BLL.Interface;
using com.vreshly.Dtos;
using com.vreshly.EmailProcessor;
using com.vreshly.Errors;
using com.vreshly.Extensions;
using com.vreshly.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly IReadTemplate readTemplate;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            ITokenService tokenService,IMapper mapper, IReadTemplate readTemplate)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
            this.readTemplate = readTemplate;
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

            if (!ModelState.IsValid)
            {
                var modelErrors = new List<string>();
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
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

        [Authorize]
        [HttpPut]
        public async Task<ActionResult> UpdateCustomer([FromBody] RegisterDto registerDto)
        {
            
            var user = await _userManager.FindByEmailFromClaimsPrinciple(HttpContext.User);
            if (user == null) return BadRequest(new ApiResponse(404, "Email does not exist"));

            if(string.IsNullOrEmpty(registerDto.DisplayName)) return BadRequest(new ApiResponse(404, "Display name is required"));

            user.DisplayName = registerDto.DisplayName;

            if (!string.IsNullOrEmpty(registerDto.Password))
            {
                if (ModelState.GetValidationState("Password") == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                {
                    var modelErrors = new List<string>();
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var modelError in modelState.Errors)
                        {
                            modelErrors.Add(modelError.ErrorMessage);
                        }
                    }
                    return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
                }
                var newPassword = _userManager.PasswordHasher.HashPassword(user, registerDto.Password);
                user.PasswordHash = newPassword;
            }
            
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok( new 
            {
                message = "record updated"
            });
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        public async Task<ActionResult> ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendLink([FromBody]ForgotPasswordModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse(400, "Email is required"));

            var user = await _userManager.Users.Where(x=> x.Email == model.Email.ToLower()).Include(x => x.Address).FirstOrDefaultAsync();
            if (user == null) return BadRequest(new ApiResponse(204, "Email doesnot exist"));

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            //send email
            var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

            ResetPasswordModel resetModel = new ResetPasswordModel()
            {
                FullName = $"{user.Address?.FirstName} {user.Address?.LastName}",
                Email = user.Email,
                ResetLink = callback
            };
            readTemplate.SendMailResetPassword("Reset Password", resetModel, TemplateFiles.ResetPasswordTemplate);
            return Ok(new ApiResponse(200, "Reset link has been sent to your email"));


        }

        public async Task<ActionResult> ResetPassword(String token, string email)
        {
            PasswordResetModel passwordReset = new PasswordResetModel
            {
                Token = token,
                Email = email
            };
            return View(passwordReset);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody]PasswordResetModel resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                var modelErrors = new List<string>();
                var errors = ModelState.Values.Select(x => x.Errors).ToList();
               
                foreach (var modelError in errors)
                {
                    modelErrors.Add(modelError.FirstOrDefault().ErrorMessage);
                }
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = modelErrors });
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordModel.Email);
            if (user == null)
                return BadRequest(new ApiResponse(204, "Email doesnot exist"));

            var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token, resetPasswordModel.Password);
            if (!resetPassResult.Succeeded)
            {
               
                //foreach (var error in resetPassResult.Errors)
                //{
                //    ModelState.TryAddModelError(error.Code, error.Description);
                //}

                return BadRequest(new ApiResponse(500, "An error occurred while processing"));
            }

            return Ok(new ApiResponse(200, "Password reset successful, you can login"));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDto>> Logon([FromBody]LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return Ok(new CustomerDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            });
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
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


        // [Authorize]
        // [HttpPut]
        // public async Task<ActionResult<AddressDto>> UpdateUserAddress([FromBody]AddressDto address)
        // {
        //     var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);

        //     foreach(var userAddress in user.Address)
        //     {
        //         if(userAddress.Id == address.Id)
        //         {
                    // user.Address.FirstName = address.FirstName;
                    // user.Address.City = address.City;
                    // user.Address.LastName = address.LastName;
                    // user.Address.State = address.State;
                    // user.Address.Street = address.Street;
                    // user.Address.ZipCode = address.ZipCode;
                    // user.Address.PhoneNumber = address.PhoneNumber;
        //         }
        //     }

        //     var result = await _userManager.UpdateAsync(user);

        //     if (result.Succeeded) return Ok(address);

        //     return BadRequest("Problem Updating the User");
        // }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<AddressDto>> AddUserAddress([FromBody] AddressDto address)
        {
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            Address userAddress = _mapper.Map<AddressDto, Address>(address);
            if(user.Address == null){
                user.Address = userAddress;
                user.Address.AppUserId = user.Id;
            }else{
                user.Address.FirstName = address.FirstName;
                user.Address.City = address.City;
                user.Address.LastName = address.LastName;
                user.Address.State = address.State;
                user.Address.Street = address.Street;
                user.Address.ZipCode = address.ZipCode;
                user.Address.PhoneNumber = address.PhoneNumber;
            }
            

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded) return Ok(address);

            return BadRequest("Problem Adding the User");
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            
            var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
            return _mapper.Map<Address,AddressDto>(user.Address);
        }

        // [Authorize]
        // [HttpGet]
        // public async Task<ActionResult<IReadOnlyList<AddressDto>>> GetUserAddresses()
        // {

        //     var user = await _userManager.FindUserByClaimsPrincipleWithAddressAsync(HttpContext.User);
        //     return Ok(_mapper.Map<IReadOnlyList<Address>, IReadOnlyList<AddressDto>>(user.Address));
        // }

        public async Task<IActionResult> Information()
        {
            return View();
        }

    }
}
