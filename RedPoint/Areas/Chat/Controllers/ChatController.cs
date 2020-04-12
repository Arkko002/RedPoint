using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Areas.Chat.Services;
using RedPoint.Services;
using RedPoint.Utilities.DtoFactories;

namespace RedPoint.Areas.Chat.Controllers
{
    [Authorize]
    [Area("chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatControllerService _chatService;

        public ChatController(IChatControllerService chatService)
        {
            _chatService = chatService;
            _chatService.AssignApplicationUser(User);
        }

        [HttpGet]
        public IActionResult GetUserServers([FromServices] ServerDtoFactory dtoFactory)
        {
            var dtoList = _chatService.GetUserServers(dtoFactory);

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
        public IActionResult GetChannelMessages([FromBody] int channelId,
            [FromBody] int serverId,
            [FromServices] MessageDtoFactory dtoFactory)
        {
            var dtoList = _chatService.GetChannelMessages(channelId, serverId, dtoFactory);

            return Ok(dtoList);
        }
    }
}