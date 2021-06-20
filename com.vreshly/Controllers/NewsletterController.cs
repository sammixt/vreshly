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
    public class NewsletterController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NewsletterController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var spec = new NewsLetterSpecification();
            var subsctiption = await _unitOfWork.Repository<NewsLetterSubscription>().ListAsync(spec);
            var subscribtionDto = _mapper.Map<IReadOnlyList<NewsLetterSubscription>, IReadOnlyList<NewsLetterDto>>(subsctiption);
            return View(subscribtionDto);
        }

        public async Task<ActionResult> Subscribe([FromBody] NewsLetterDto subsctiption)
        {
            if(string.IsNullOrEmpty(subsctiption.Email)) return BadRequest(new ApiResponse(400, "Please supply an Email"));

            NewsLetterSpecification spec = new NewsLetterSpecification(subsctiption.Email.ToLower().Trim());
            var hasRecord = await _unitOfWork.Repository<NewsLetterSubscription>().GetEntitiesWithSpec(spec);
            if(hasRecord != null) return BadRequest(new ApiResponse(400, "Email already exist"));

            subsctiption.CreatedDate = DateTime.Now;
            var subscribtionDto = _mapper.Map<NewsLetterDto, NewsLetterSubscription>(subsctiption);
            _unitOfWork.Repository<NewsLetterSubscription>().Add(subscribtionDto);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Subscribed"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Subscribing"));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubscription(int id)
        {
            var specwithId = new NewsLetterSpecification((long)id);
            var subsctiptionWithId = await _unitOfWork.Repository<NewsLetterSubscription>().GetEntitiesWithSpec(specwithId);
            if (subsctiptionWithId == null) return BadRequest(new ApiResponse(400, "Subscription does not exist"));
            _unitOfWork.Repository<NewsLetterSubscription>().Delete(subsctiptionWithId);
            int result = await _unitOfWork.Complete();
            if (result == 1)
            {
                return Ok(new ApiResponse(200, "Subscription Successfully Deleted"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Deleting Subscription"));
        }
    }
}
