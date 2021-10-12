using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities;
using BLL.Interface;
using BLL.Specifications;
using com.vreshly.Dtos;
using com.vreshly.Errors;
using com.vreshly.Helper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace com.vreshly.Controllers
{
    public class AuthController : Controller
    {
        // GET: /<controller>/

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AuthController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            webHostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return PartialView();
        }

        //[Authorize(AuthenticationSchemes = "Cookies")]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult Dashboard()
        {
            //CheckNewClaimsUsage();
            return View();
        }

        private  void CheckNewClaimsUsage()
        {
            //ClaimsPrincipal currentClaimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
            //Claim nameClaim = currentClaimsPrincipal.FindFirst(ClaimTypes.Name);
            //var value = nameClaim.Value;
            var userName = User.Claims
                           .First(c => c.Type == "UserName").Value;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<ActionResult> GetSessionDetails()
        {
            var _userName = User.Claims
                           .First(c => c.Type == "UserName").Value;
            var _email = User.Claims
                           .First(c => c.Type == "Email").Value;
            return Ok(new
            {
                username = _userName,
                email = _email
            });
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromBody]UserDto model)
        {
            if (string.IsNullOrEmpty(model.Password)) return BadRequest(new ApiResponse(400, "Password was not supplied"));
            if (string.IsNullOrEmpty(model.Email)) return BadRequest(new ApiResponse(400, "Email was not supplied"));

            var spec = new UserSpecification(model.Email.ToLower(), model.Password);
            var user = await _unitOfWork.Repository<User>().GetEntitiesWithSpec(spec);

            if(user != null)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim("UserId", Convert.ToString(user.Id.ToString())));
                identity.AddClaim(new Claim("UserName", user.Username));
                identity.AddClaim(new Claim("Email", user.Email));
                var status = (UserStatus)user.Status;
                identity.AddClaim(new Claim("UserStatus", status.ToString() ));
                identity.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties()
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                    IsPersistent = true,
                    AllowRefresh = true
                };
                //props.IsPersistent = model.RememberMe;
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
                return Ok(new ApiResponse(200, "User Successfully Logged on"));
            }
            else
            {
                return Ok(new ApiResponse(401, "Invalid User Name or Password"));
            }
        }

        
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(Index));
        }
    }
}
