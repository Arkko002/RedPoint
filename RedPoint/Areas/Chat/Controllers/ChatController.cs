using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Identity.Models;
using RedPoint.Areas.Utilities.DtoFactories;
using RedPoint.Data;
using RedPoint.Data.UnitOfWork;

namespace RedPoint.Areas.Chat.Controllers
{
    [Authorize]
    [Area("chat")]
    public class ChatController : ControllerBase
    {
        private ChatService _chatService;

        public ChatController(ChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        public IActionResult GetUserServers([FromServices] ServerDtoFactory dtoFactory)
        {
            var dtoList = _chatService.GetUserServers(dtoFactory, User);

            return Ok(dtoList);
        }

        [HttpGet]
        public IActionResult GetServerChannels([FromBody] int serverId,
            [FromServices] ChannelDtoFactory dtoFactory)
        {
            var dtoList = _chatService.GetServerChannels(serverId, dtoFactory);

            return Ok(dtoList);
        }

        [HttpGet]
        public IActionResult GetServerUserList([FromBody] int serverId,
            [FromServices] UserDtoFactory dtoFactory)
        {
            var dtoList = _chatService.GetServerUserList(serverId, dtoFactory);

            return Ok(dtoList);
        }

        [HttpGet]
        public IActionResult GetChannelMessages([FromBody] int channelId)
        {
            //TODO

        }
    }
}