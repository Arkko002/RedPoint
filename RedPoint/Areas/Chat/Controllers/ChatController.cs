using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Areas.Chat.Hubs;
using RedPoint.Areas.Chat.Models.Dto;
using RedPoint.Areas.Chat.Services;
using RedPoint.Exceptions;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint.Areas.Chat.Controllers
{
    [Authorize]
    [Area("chat")]
    public class ChatController : ControllerBase
    {
        private readonly ChatService _chatService;
        private readonly IHubContext<ChatHub, IChatHub> _chatHub;

        public ChatController(ChatService chatService, IHubContext<ChatHub, IChatHub> chatHub)
        {
            _chatService = chatService;
            _chatHub = chatHub;
        }

        [HttpGet]
        public IActionResult GetUserServers([FromServices] ServerDtoFactory dtoFactory)
        {
            var dtoList = _chatService.GetUserServers(dtoFactory, User);

            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult AddServer([FromBody] ServerDto server)
        {
            _chatService.TryAddingServer(server);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetServerChannels([FromBody] int serverId,
            [FromServices] ChannelDtoFactory dtoFactory)
        {
            _chatService.ValidateServerRequest(serverId, User);
            var dtoList = _chatService.GetServerChannels(dtoFactory);

            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult AddChannel([FromBody] int serverId,
            [FromBody] ChannelDto channel)
        {
            _chatService.TryAddingChannel(serverId, channel);

            return Ok();
        }

        [HttpGet]
        public IActionResult GetServerUserList([FromBody] int serverId,
            [FromServices] UserDtoFactory dtoFactory)
        {
            _chatService.ValidateServerRequest(serverId, User);
            var dtoList = _chatService.GetServerUserList(dtoFactory);

            return Ok(dtoList);
        }

        [HttpGet]
        public IActionResult GetChannelMessages([FromBody] int channelId,
            [FromBody] int serverId,
            [FromServices] MessageDtoFactory dtoFactory)
        {
            _chatService.ValidateChannelRequest(channelId, serverId, User);
            var dtoList = _chatService.GetChannelMessages(dtoFactory);

            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult AddMessage([FromBody] int channelId,
            [FromBody] MessageDto message)
        {
            _chatService.TryAddingMessage(channelId, message);

            return Ok();
        }
    }
}