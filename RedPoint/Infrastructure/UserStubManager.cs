//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Web;
//using RedPoint.Exceptions;
//using RedPoint.Models;

//namespace RedPoint.Infrastructure
//{

//    ///<summary>
//    ///Provides methods to manage and retrieve data from database table 'UserStubs'
//    ///</summary>
//    public class UserStubManager
//    {
//       readonly ApplicationDbContext _db = new ApplicationDbContext();

//        ///<summary>
//        ///Creates and returns UserStub for the provided ApplicationUser
//        ///</summary>
//        public UserStub CreateUserStub(ApplicationUser user)
//        {
//            UserStub userStub = new UserStub()
//            {
//                AppUserId = user.Id,
//                AppUserName = user.UserName
//            };

//            try
//            {
//                _db.UserStubs.Add(userStub);
//                _db.SaveChanges();
//            }
//            catch (Exception ex)
//            {
//                Debug.Print(ex.Source + " " + ex.GetType().ToString() + " " + ex.Message);
//            }

//            return userStub;
//        }

//        /// <summary>
//        /// Creates and returns UserStub for the provided ApplicationUser Id
//        /// </summary>
//        /// <param name="userId"></param>
//        /// <returns></returns>
//        public UserStub CreateUserStub(string userId)
//        {
//            ApplicationUser user = _db.Users.FirstOrDefault(u => u.Id == userId);

//            if(user is null)
//            {
//                throw new ApplicationUserNotFoundException("ID: " + userId);
//            }

//            UserStub userStub = new UserStub()
//            {
//                AppUserId = user.Id,
//                AppUserName = user.UserName
//            };
            
//            try
//            {
//                _db.UserStubs.Add(userStub);
//                _db.SaveChanges();
//            }
//            catch(Exception ex)
//            {
//                Debug.Print(ex.Source + " " + ex.GetType().ToString() + " " + ex.Message);
//            }

//            return userStub;
//        }
//    }
//}