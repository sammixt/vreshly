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
    public class AdminMailController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminMailController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index(int? type)
        {
           
            ViewBag.Type = "Email";
            MessageSpecification spec = null;
            if(type == null)
            {
                ViewBag.MType = 0;
                spec = new MessageSpecification(MessageType.Inbox);
            }
            else
            {
                ViewBag.MType = type;
                MessageType messageType = (MessageType)type;
                spec = new MessageSpecification(messageType);
            }
                
            var message = await _unitOfWork.Repository<Message>().ListAsync(spec);
            var messageDto = _mapper.Map<IReadOnlyList<Message>, IReadOnlyList<MessageDto>>(message);
            return View(messageDto);
        }

        public async Task<IActionResult> GetMessage(int msgId)
        {
            MessageSpecification spec = new MessageSpecification(msgId);
            var message = await _unitOfWork.Repository<Message>().GetEntitiesWithSpec(spec);
            if(message == null) return BadRequest(new ApiResponse(400, "Message not found"));

            message.ReadStatus = true;
            message.UpdateDate = DateTime.Now;
            _unitOfWork.Repository<Message>().Update(message);
            await _unitOfWork.Complete();
            var contactDto = _mapper.Map<Message, MessageDto>(message);
            return Ok(contactDto);
        }

        public async Task<IActionResult> Delete(int msgId)
        {
            MessageSpecification spec = new MessageSpecification(msgId);
            var message = await _unitOfWork.Repository<Message>().GetEntitiesWithSpec(spec);
            if (message == null) return BadRequest(new ApiResponse(400, "Message not found"));

            if(message.MessageType == MessageType.Trash)
            {
                _unitOfWork.Repository<Message>().Delete(message);
            }
            else
            {
                message.MessageType = MessageType.Trash;
                _unitOfWork.Repository<Message>().Update(message);
            }
            await _unitOfWork.Complete();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Send([FromBody] MessageDto contact)
        {
            if (string.IsNullOrEmpty(contact.Email)) return BadRequest(new ApiResponse(400, "Please supply an Email"));
            if (string.IsNullOrEmpty(contact.Body)) return BadRequest(new ApiResponse(400, "Please supply an Message"));
            if (string.IsNullOrEmpty(contact.Subject)) return BadRequest(new ApiResponse(400, "Please supply an Subject"));
            if (string.IsNullOrEmpty(contact.FullName)) return BadRequest(new ApiResponse(400, "Please supply an Full Name"));

            contact.CreatedDate = DateTime.Now;
            
            var contactDto = _mapper.Map<MessageDto, Message>(contact);
            _unitOfWork.Repository<Message>().Add(contactDto);
            int result = await _unitOfWork.Complete();
            if (result > 0)
            {
                return Ok(new ApiResponse(200, "Message Sent"));
            }

            return BadRequest(new ApiResponse(500, "An error occurred when Sending Message"));
        }

        public async Task<ActionResult> CountMessage()
        {
            MessageSpecification spec = new MessageSpecification();
            var message = await _unitOfWork.Repository<Message>().ListAsync(spec);

            int totalInbox = message.Count(x => x.MessageType == MessageType.Inbox);
            int totalTrash = message.Count(x => x.MessageType == MessageType.Trash);
            int totalSent = message.Count(x => x.MessageType == MessageType.Sent);

            return Ok(new
            {
                inboxCount = totalInbox,
                trashCount = totalTrash,
                sentCount = totalSent
            });
        }
    }
}
