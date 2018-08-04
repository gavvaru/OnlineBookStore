using OnlineBookStore.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineBookStore.Controllers
{
    public class AccountController : Controller
    {
        UserProfileBO userProfileBO = new UserProfileBO();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnurl)
        {
            if (ModelState.IsValid)
            {
                UserProfile objUserProfile = userProfileBO.AuthenticateUser(model.EmailAddress);
                if (objUserProfile != null)
                {
                    if (!objUserProfile.IsActive)
                    {
                        ModelState.AddModelError("", "Account Deactivated");
                        return View();
                    }
                    if (model.Password == objUserProfile.Password)
                    {
                        string userData = objUserProfile.PKUserId + "^" + "1" + "^" + "Admin" + "^" + objUserProfile.FirstName + "^" + objUserProfile.FirstName + "^" + objUserProfile.LastName + "^" + objUserProfile.EmailAddress + "^" + objUserProfile.Password;
                        System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(userData), new string[] { userData });
                        FormsAuthentication.RedirectFromLoginPage(Helper.UserData, model.RememberMe);
                        if (returnurl != null)
                        {
                            return Redirect(returnurl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Password");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Doesnot Exists");
                    return View();
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserProfile objUser)
        {
            if (ModelState.IsValid)
            {
                //user account by default is inactive
                //admin activates the account
                objUser.IsActive = false;
                userProfileBO.InsertUser(objUser);
                return RedirectToAction("Login");
            }
            return View(User);
        }
       
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}