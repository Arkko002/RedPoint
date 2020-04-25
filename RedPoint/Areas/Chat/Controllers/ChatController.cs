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
            //TODO!!! Include all of the stubs in ServerStub, send everything in one request

            var dto = _chatService.GetServerData(serverId, dataDtoFactory);

            return Ok(dto);
        }
    }
}