using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.DtoFactories;

namespace RedPoint.Chat.Controllers
{
    /// <summary>
    /// Controller for chat functionality that doesn't require real-time communication with the server.
    /// </summary>
    [Authorize]
    [Area("chat")]
    public class ChatController : ControllerBase
    {
        private readonly IChatControllerService _chatService;

        public ChatController(IChatControllerService chatService)
        {
            _chatService = chatService;
        }

        /// <summary>
        /// Gets a list of servers assigned to current user.
        /// Current user is provided by the <c>UserManager</c> using <c>ClaimsPrincipal</c>>.
        /// </summary>
        /// <param name="iconDtoFactory">Factory handling creation of ServerIconDto.</param>
        /// <returns>Returns a list of ServerIconDto</returns>
        [HttpGet]
        [Route("chat/servers")]
        public IActionResult GetUserServers([FromServices] ServerIconDtoFactory iconDtoFactory)
        {
            var dtoList = _chatService.GetUserServers(iconDtoFactory);

            return Ok(dtoList);
        }

        /// <summary>
        /// Gets data of a server with provided ID.
        /// </summary>
        /// <param name="serverId">ID of a server</param>
        /// <param name="dataDtoFactory">Factory handling creation of ServerDataDto</param>
        /// <returns>Returns ServerDataDto</returns>
        [HttpGet]
        [Route("chat/server/{serverId}")]
        public IActionResult GetServer(int serverId,
            [FromServices] ServerDataDtoFactory dataDtoFactory)
        {
            var dto = _chatService.GetServerData(serverId, dataDtoFactory);

            return Ok(dto);
        }

        /// <summary>
        /// Gets messages from a given channel, on a given server.
        /// </summary>
        /// <param name="channelId">ID of the channel that is part of the server</param>
        /// <param name="serverId">ID of the server</param>
        /// <param name="messageDtoFactory">Factory handling creation of MessageDto</param>
        /// <returns></returns>
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