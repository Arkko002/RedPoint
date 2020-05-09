using System.Collections.Generic;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services.Security;
using Xunit;

namespace RedPoint.Tests.Chat
{
    public class ChatRequestValidatorTests
    {
        private readonly ChatRequestValidator _validator;
        
        public ChatRequestValidatorTests()
        {
            _validator = new ChatRequestValidator();
        }
        
        [Fact]
        public void ValidServerRequest_ShouldReturnNoErrorType()
        {
            ApplicationUser user = new ApplicationUser();
            
            Group group = new Group();
            group.GroupPermissions.Add(PermissionType.IsAdmin);
            group.Users.Add(user);
            
            user.Groups = new List<Group>();
            user.Groups.Add(group);
            
            Server server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group>();
            server.Groups.Add(group);
            
            var returnErorr = _validator.IsServerRequestValid(server, user, PermissionType.IsAdmin);

            Assert.True(returnErorr.ErrorType == ChatErrorType.NoError);
        }

        [Fact]
        public void UserNotInServer_ShouldReturnUserNotInServerType()
        {
            Server server = new Server();
            ApplicationUser user = new ApplicationUser();

            var returnError = _validator.IsServerRequestValid(server, user, PermissionType.IsAdmin);
            
            Assert.True(returnError.ErrorType == ChatErrorType.UserNotInServer);
        }

        [Fact]
        public void NoServerPermission_ShouldReturnNoPermissionType()
        {
            ApplicationUser user = new ApplicationUser();
            
            Group group = new Group();
            group.Users.Add(user);
            
            Server server = new Server();
            server.Users.Add(user);

            var returnError = _validator.IsServerRequestValid(server, user, PermissionType.CanView);
            
            Assert.True(returnError.ErrorType == ChatErrorType.NoPermission);
        }

        [Fact]
        public void ValidChannelRequest_ShouldReturnNoErrorType()
        {
            ApplicationUser user = new ApplicationUser();
            
            Group group = new Group();
            group.GroupPermissions.Add(PermissionType.IsAdmin);
            group.Users.Add(user);
            
            Server server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group>();
            server.Groups.Add(group);
            
            Channel channel = new Channel();
            server.Channels.Add(channel);
            channel.Groups = new List<Group>();
            channel.Groups.Add(group);
            
            user.Groups = new List<Group>();
            user.Groups.Add(group);
            
            var returnError = _validator.IsChannelRequestValid(channel, server, user, PermissionType.CanView);
            
            Assert.True(returnError.ErrorType == ChatErrorType.NoError);
        }

        [Fact]
        public void UserNotInChannelsServer_ShouldReturnUserNotInServerType()
        {
            ApplicationUser user = new ApplicationUser();
            Server server = new Server();
            Channel channel = new Channel();

            var returnError = _validator.IsChannelRequestValid(channel, server, user, PermissionType.CanView);
            
            Assert.True(returnError.ErrorType == ChatErrorType.UserNotInServer);
        }

        [Fact]
        public void NoChannelPermission_ShouldReturnNoPermissionType()
        {
            ApplicationUser user = new ApplicationUser();
            
            Group group = new Group();
            group.Users.Add(user);
            
            Server server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group>();
            server.Groups.Add(group);
            
            Channel channel = new Channel();
            server.Channels.Add(channel);
            channel.Groups = new List<Group>();
            channel.Groups.Add(group);
            
            user.Groups = new List<Group>();
            user.Groups.Add(group);
            
            var returnError = _validator.IsChannelRequestValid(channel, server, user, PermissionType.CanView);
            
            Assert.True(returnError.ErrorType == ChatErrorType.NoPermission);
        }
    }
}