using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Areas.Chat.Services;
using RedPoint.Exceptions;
using RedPoint.Utilities.DtoFactories;

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
            try
            {
                _chatService.ValidateServerRequest(serverId, User);
            }
            catch (RequestInvalidException)
            {
                return Unauthorized();
            }

            var dtoList = _chatService.GetServerChannels(dtoFactory);

            return Ok(dtoList);
        }

        [HttpGet]
        public IActionResult GetServerUserList([FromBody] int serverId,
            [FromServices] UserDtoFactory dtoFactory)
        {
            try
            {
                _chatService.ValidateServerRequest(serverId, User);
            }
            catch (RequestInvalidException)
            {
                return Unauthorized();
            }

            var dtoList = _chatService.GetServerUserList(dtoFactory);

            return Ok(dtoList);
        }

        [HttpGet]
        public IActionResult GetChannelMessages([FromBody] int channelId,
            [FromBody] int serverId,
            [FromServices] MessageDtoFactory dtoFactory)
        {
            try
            {
                _chatService.ValidateChannelRequest(channelId, serverId, User);
            }
            catch (RequestInvalidException)
            {
                return Unauthorized();
            }

            var dtoList = _chatService.GetChannelMessages(dtoFactory);

            return Ok(dtoList);
        }
    }
}