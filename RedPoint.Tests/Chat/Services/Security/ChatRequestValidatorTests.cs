using System.Collections.Generic;
using Moq;
using RedPoint.Chat.Exceptions.Security;
using RedPoint.Chat.Models.Chat;
using RedPoint.Chat.Models.Errors;
using RedPoint.Chat.Services.Security;
using Xunit;

namespace RedPoint.Tests.Chat.Services.Security
{
    public class ChatRequestValidatorTests
    {
        public ChatRequestValidatorTests()
        {
            _validator = new Mock<ChatRequestValidator>();
        }

        private readonly Mock<ChatRequestValidator> _validator;

        [Fact]
        public void NoChannelPermission_ShouldThrowLackOfPermission()
        {
            var user = new ChatUser();

            var group = new Group();
            group.Users.Add(user);

            var server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group> { group };

            var channel = new Channel();
            server.Channels.Add(channel);
            channel.Groups = new List<Group> { group };

            user.Groups = new List<Group> { group };

            Assert.Throws<LackOfPermissionException>(() =>
                _validator.Object.IsChannelRequestValid(channel, server, user, PermissionTypes.CanView));
        }

        [Fact]
        public void NoServerPermission_ShouldThrowLackOFPermission()
        {
            var user = new ChatUser();

            var group = new Group();
            group.Users.Add(user);

            var server = new Server();
            server.Users.Add(user);

            Assert.Throws<LackOfPermissionException>(() =>
                _validator.Object.IsServerRequestValid(server, user, PermissionTypes.CanView));
        }

        [Fact]
        public void UserNotInChannelsServer_ShouldThrowUserMembership()
        {
            var user = new ChatUser();
            var server = new Server();
            var channel = new Channel();

            Assert.Throws<UserMembershipException>(() =>
                _validator.Object.IsChannelRequestValid(channel, server, user, PermissionTypes.CanView));
        }

        [Fact]
        public void UserNotInServer_ShouldThrowUserMembership()
        {
            var server = new Server();
            var user = new ChatUser();

            Assert.Throws<UserMembershipException>(() =>
                _validator.Object.IsServerRequestValid(server, user, PermissionTypes.IsAdmin));
        }

        [Fact]
        public void ValidChannelRequest_ShouldReturn()
        {
            var user = new ChatUser();

            var group = new Group();
            group.GroupPermissions |= PermissionTypes.IsAdmin;
            group.Users.Add(user);

            var server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group> { group };

            var channel = new Channel();
            server.Channels.Add(channel);
            channel.Groups = new List<Group> { group };

            user.Groups = new List<Group>();
            user.Groups.Add(group);
            
           _validator.Object.IsChannelRequestValid(channel, server, user, PermissionTypes.CanView);
           
           //Will only assert on valid request
           Assert.True(true);
        }

        [Fact]
        public void ValidServerRequest_ShouldReturn()
        {
            var user = new ChatUser();

            var group = new Group();
            group.GroupPermissions |= PermissionTypes.IsAdmin;
            group.Users.Add(user);

            user.Groups = new List<Group> { group };

            var server = new Server();
            server.Users.Add(user);
            server.Groups = new List<Group> { group };

            _validator.Object.IsServerRequestValid(server, user, PermissionTypes.IsAdmin);

            //Will only assert on valid request
            Assert.True(true);
        }
    }
}