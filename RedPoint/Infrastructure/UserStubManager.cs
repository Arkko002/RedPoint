using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using RedPoint.Data;
using RedPoint.Exceptions;
using RedPoint.Models;
using RedPoint.Models.Chat_Models;

namespace RedPoint.Infrastructure
{

    //TODO Possibly obsolete, stub generation in AppUser constructor

    ///<summary>
    ///Provides methods to manage and retrieve data from database table 'UserStubs'.
    ///</summary>
    public static class UserStubManager
    {
        /// <summary>
        /// Checks if ApplicationUser has an UserStub assigned, creates one if not.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="db"></param>
        public static void CheckIfUserStubExists(ApplicationUser user, ApplicationDbContext db)
        {
            if (user.UserStub is null)
            {
                user.UserStub = new UserStub()
                {
                    AppUserId = user.Id,
                    AppUserName = user.UserName
                };

                try
                {
                    db.UserStubs.Add(user.UserStub);
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