using System.Collections.Generic;
using System.Linq;
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

        //TODO Rework for IEnumerable changes, remove casting
        [Fact]
        public void NoChannelPermission_ShouldThrowLackOfPermission()
        {
            var user = new ChatUser();

            var group = new Group();
            group.Users.ToList().Add(user);

            var server = new Server("Test");
            server.Users.ToList().Add(user);
            server.Groups = new List<Group> { group };

            var channel = new Channel(server, "Test");
            server.Channels.ToList().Add(channel);
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
            group.Users.ToList().Add(user);

            var server = new Server("Test");
            server.Users.ToList().Add(user);

            Assert.Throws<LackOfPermissionException>(() =>
                _validator.Object.IsServerRequestValid(server, user, PermissionTypes.CanView));
        }

        [Fact]
        public void UserNotInChannelsServer_ShouldThrowUserMembership()
        {
            var user = new ChatUser();
            var server = new Server("Test");
            var channel = new Channel(server, "Test");

            Assert.Throws<UserMembershipException>(() =>
                _validator.Object.IsChannelRequestValid(channel, server, user, PermissionTypes.CanView));
        }

        [Fact]
        public void UserNotInServer_ShouldThrowUserMembership()
        {
            var server = new Server("Test");
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
            group.Users.ToList().Add(user);

            var server = new Server("Test");
            server.Users.ToList().Add(user);
            server.Groups = new List<Group> { group };

            var channel = new Channel(server, "Test");
            server.Channels.ToList().Add(channel);
            channel.Groups = new List<Group> { group };

            user.Groups = new List<Group>();
            user.Groups.ToList().Add(group);
            
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
            group.Users.ToList().Add(user);

            user.Groups = new List<Group> { group };

            var server = new Server("Test");
            server.Users.ToList().Add(user);
            server.Groups = new List<Group> { group };

            _validator.Object.IsServerRequestValid(server, user, PermissionTypes.IsAdmin);

            //Will only assert on valid request
            Assert.True(true);
        }
    }
}