using System;
using System.Diagnostics;
using RedPoint.Areas.Identity.Models;
using RedPoint.Data;

namespace RedPoint.Areas.Chat.Services
{

    //TODO Possibly obsolete, stub generation in AppUser constructor

    ///<summary>
    ///Provides methods to manage and retrieve data from database table 'UserStubs'.
    ///</summary>
    public static class UserStubManager
    {
        /// <summary>
        /// Checks if ApplicationUser has an UserDTO assigned, creates one if not.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="db"></param>
        public static void CheckIfUserStubExists(ApplicationUser user, ApplicationDbContext db)
        {
            if (user.UserDto is null)
            {
                user.UserDto = new UserDTO()
                {
                    AppUserId = user.Id,
                    AppUserName = user.UserName
                };

                try
                {
                    db.UserStubs.Add(user.UserDto);
                    db.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Debug.Print(e.ToString());
                }
            }
        }
    }
}