﻿using System.Drawing;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using RedPoint.Areas.Chat.Models;
using RedPoint.Areas.Chat.Services;
using RedPoint.Areas.Chat.Services.Facades;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Hubs
{
    public class ServerHub : Hub<IServerHub>
    {
        private readonly ServerFacade _server;

        public ServerHub(UserManager<ApplicationUser> userManager, ApplicationDbContext db, HubUserInputValidator inputValidator)
        {
           _server = new ServerFacade(db, userManager, inputValidator);
        }

        /// <summary>
        /// Creates a Server and adds it to database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="image"></param>
        /// <param name="isVisible"></param>
        public async Task AddServer(string name, string description, Bitmap image, bool isVisible)
        {
            var server = await _server.AddServer(Context.UserIdentifier, name, description, isVisible, image);
            if (server is null)
            {
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, server.Name);
            await Clients.Group(server.Name).AddServer(server.ServerStub);
        }

        /// <summary>
        /// Removes the Server with given id from database
        /// </summary>
        /// <param name="id"></param>
        public async Task RemoveServer(int id)
        {
            var resultTuple = await _server.RemoveServer(Context.UserIdentifier, id);
            if (resultTuple is null)
            {
                await Clients.Caller.ServerDoesntExist();
                return;
            }

            if (resultTuple.Value.canManageServers)
            {
                await Clients.Caller.NoRemovePermission();
                return;
            }
                       
            await Clients.Group(resultTuple.Value.server.Name).RemoveServer(resultTuple.Value.server.ServerStub);
        }

        /// <summary>
        /// Adds user to the Server's list of users
        /// </summary>
        /// <param name="id"></param>
        public async Task JoinServer(int id)
        {
            var resultTuple = await _server.JoinServer(Context.UserIdentifier, id);
            if (resultTuple is null)
            {
                await Clients.Caller.ServerDoesntExist();
                return;
            }

            if (resultTuple.Value.userAlreadyInServer)
            {
                return;
            }

            resultTuple.Value.server.Groups[0].Users.Add(resultTuple.Value.user.UserDto);
            await Groups.AddToGroupAsync(Context.ConnectionId, resultTuple.Value.server.Name);
            await Clients.Caller.JoinServer(id);
        }

        /// <summary>
        /// Remove's user from the Server's list of users
        /// </summary>
        /// <param name="id"></param>
        public async Task LeaveServer(int id)
        {
            var resultTuple = await _server.LeaveServer(Context.UserIdentifier, id);
            if (resultTuple is null)
            {
                await Clients.Caller.ServerDoesntExist();
                return;
            }

            if (resultTuple.Value.userNotInServer)
            {
                return;
            }

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, resultTuple.Value.server.Name);
            await Clients.Caller.LeaveServer(id);
        }
    }

    public interface IServerHub
    {
        Task RemoveServer(ServerStub serverStub);
        Task AddServer(ServerStub serverStub);
        Task LeaveServer(int id);
        Task JoinServer(int id);
        Task NoRemovePermission();
        Task ServerDoesntExist();
    }
}