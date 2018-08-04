using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineBookStore.BO
{
    public class UserProfileBO
    {
        OnlineBookStoreEntities context = new OnlineBookStoreEntities();
        public UserProfile GetUser(int userId)
        {
            return context.UserProfiles.Where(u => u.PKUserId == userId).SingleOrDefault();
        }
        public UserProfile AuthenticateUser(string emailID)
        {
            try
            {
                UserProfile objUserProfile = context.UserProfiles.Where(u => u.EmailAddress == emailID).SingleOrDefault();
                return objUserProfile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<UserProfile> GetUsers(string emailId = "", bool? isActive = null)
        {
            try
            {
                IQueryable<UserProfile> qry = context.UserProfiles;
                if (emailId != "")
                    qry = qry.Where(u => u.EmailAddress.Contains(emailId));
                if (isActive != null)
                    qry = qry.Where(u => u.IsActive == isActive);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertUser(UserProfile objUser)
        {
            try
            {
                context.UserProfiles.Add(objUser);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateUser(UserProfile objUser)
        {
            try
            {
                context.Entry(objUser).State = EntityState.Modified;
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteUser(int userId)
        {
            try
            {
                UserProfile objUser = context.UserProfiles.Find(userId);
                context.UserProfiles.Remove(objUser);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}