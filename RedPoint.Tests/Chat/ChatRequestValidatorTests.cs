using System.Collections.Generic;
using RedPoint.Areas.Account.Models;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services.Security;
using Xunit;

namespace RedPoint.Tests.Chat
{
    public class ChatRequestValidatorTests
    {
        public ChatRequestValidatorTests()
        {
            _validator = new ChatRequestValidator();
        }

        private readonly ChatRequestValidator _validator;

        [Fact]
        public void NoChannelPermission_ShouldReturnNoPermissionType()
        {
            var user = new ApplicationUser();

            var group = new Group();
            group.Users.Add(user);

            var server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group> {group};

            var channel = new Channel();
            server.Channels.Add(channel);
            channel.Groups = new List<Group> {group};

            user.Groups = new List<Group> {group};

            var returnError = _validator.IsChannelRequestValid(channel, server, user, PermissionType.CanView);

            Assert.True(returnError.ErrorType == ChatErrorType.NoPermission);
        }

        [Fact]
        public void NoServerPermission_ShouldReturnNoPermissionType()
        {
            var user = new ApplicationUser();

            var group = new Group();
            group.Users.Add(user);

            var server = new Server();
            server.Users.Add(user);

            var returnError = _validator.IsServerRequestValid(server, user, PermissionType.CanView);

            Assert.True(returnError.ErrorType == ChatErrorType.NoPermission);
        }

        [Fact]
        public void UserNotInChannelsServer_ShouldReturnUserNotInServerType()
        {
            var user = new ApplicationUser();
            var server = new Server();
            var channel = new Channel();

            var returnError = _validator.IsChannelRequestValid(channel, server, user, PermissionType.CanView);

            Assert.True(returnError.ErrorType == ChatErrorType.UserNotInServer);
        }

        [Fact]
        public void UserNotInServer_ShouldReturnUserNotInServerType()
        {
            var server = new Server();
            var user = new ApplicationUser();

            var returnError = _validator.IsServerRequestValid(server, user, PermissionType.IsAdmin);

            Assert.True(returnError.ErrorType == ChatErrorType.UserNotInServer);
        }

        [Fact]
        public void ValidChannelRequest_ShouldReturnNoErrorType()
        {
            var user = new ApplicationUser();

            var group = new Group();
            group.GroupPermissions.Add(PermissionType.IsAdmin);
            group.Users.Add(user);

            var server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group> {group};

            var channel = new Channel();
            server.Channels.Add(channel);
            channel.Groups = new List<Group> {group};

            user.Groups = new List<Group>();
            user.Groups.Add(group);

            var returnError = _validator.IsChannelRequestValid(channel, server, user, PermissionType.CanView);

            Assert.True(returnError.ErrorType == ChatErrorType.NoError);
        }

        [Fact]
        public void ValidServerRequest_ShouldReturnNoErrorType()
        {
            var user = new ApplicationUser();

            var group = new Group();
            group.GroupPermissions.Add(PermissionType.IsAdmin);
            group.Users.Add(user);

            user.Groups = new List<Group> {group};

            var server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group> {group};
            
            var returnError = _validator.IsServerRequestValid(server, user, PermissionType.IsAdmin);

            Assert.True(returnError.ErrorType == ChatErrorType.NoError);
        }
    }
}