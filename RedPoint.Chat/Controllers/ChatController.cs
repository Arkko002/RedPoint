using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedPoint.Chat.Data;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Chat.Dto;
using RedPoint.Chat.Services;
using RedPoint.Chat.Services.DtoFactories;

namespace RedPoint.Chat.Controllers
{
    /// <summary>
    /// Controller for chat functionality that doesn't require real-time communication with the server.
    /// </summary>
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatControllerService _chatService;

        public ChatController(IChatControllerService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet]
        [Route("chat/user/current")]
        public IActionResult GetCurrentUser([FromServices] IChatDtoFactory<ChatUser, CurrentUserDto> dtoFactory)
        {
            //TODO 
            var user = _chatService.GetCurrentUser(dtoFactory);

            return Ok(user);
        }

        [HttpGet]
        [Route("chat/user/{id}")]
        public IActionResult GetChatUser(string id,
            [FromServices] IChatDtoFactory<ChatUser, UserInfoDto> userDtoFactory,
            [FromServices] IChatEntityRepositoryProxy<ChatUser, ChatDbContext> repo)
        {
            var user = _chatService.GetChatUser(id, userDtoFactory, repo);

            return Ok(user);
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
            [FromServices] IChatDtoFactory<Server, ServerDataDto> dataDtoFactory,
            [FromServices] IChatEntityRepositoryProxy<Server, ChatDbContext> repo)
        {
            var dto = _chatService.GetServerData(serverId, dataDtoFactory, repo);

            return Ok(dto);
        }

        /// <summary>
        /// Gets messages from a given channel, on a given server.
        /// </summary>
        /// <param name="channelId">ID of the channel that is part of the server</param>
        /// <param name="messageDtoFactory">Factory handling creation of MessageDto</param>
        /// <returns></returns>
        [HttpGet]
        [Route("chat/server/{serverId}/{channelId}")]
        public IActionResult GetChannelMessages(int channelId,
            [FromServices] IChatDtoFactory<Message, MessageDto> messageDtoFactory,
            [FromServices] IChatEntityRepositoryProxy<Channel, ChatDbContext> repo)
        {
            var dto = _chatService.GetChannelMessages(channelId, messageDtoFactory, repo);

            return Ok(dto);
        }
    }
}
