using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.DtoFactories;

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
        [Route("chat/servers")]
        public IActionResult GetUserServers([FromServices] ServerIconDtoFactory iconDtoFactory)
        {
            var dtoList = _chatService.GetUserServers(iconDtoFactory);

            return Ok(dtoList);
        }

        [HttpGet]
        [Route("chat/server/{serverId}")]
        public IActionResult GetServer(int serverId,
            [FromServices] ServerDataDtoFactory dataDtoFactory)
        {
            var dto = _chatService.GetServerData(serverId, dataDtoFactory);

            return Ok(dto);
        }

        [HttpGet]
        [Route("chat/server/{serverId}/{channelId}")]
        public IActionResult GetChannelMessages(int channelId, int serverId,
            [FromServices] MessageDtoFactory messageDtoFactory)
        {
            var dto = _chatService.GetChannelMessages(channelId, serverId, messageDtoFactory);

            return Ok(dto);
        }
    }
}