﻿using DSLNG.PEAR.Services.Interfaces;
using DSLNG.PEAR.Services.Requests.User;
using DSLNG.PEAR.Services.Responses.User;
using DSLNG.PEAR.Web.DependencyResolution;
using DSLNG.PEAR.Web.ViewModels;
using DSLNG.PEAR.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DSLNG.PEAR.Web.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string message)
        {
            ViewBag.message = message;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserLoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                if (IsValid(user.Email, user.Password))
                {
                    //FormsAuthentication.SetAuthCookie(user.Username, false);
                    var sessionData = (UserProfileSessionData)this.Session["LoginUser"];
                    var RedirectUrl = sessionData.RedirectUrl;
                    if (RedirectUrl != null)
                    {
                        return Redirect(RedirectUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Login Data");
                }

            }
            else
            {
                ModelState.AddModelError("", "Incorrect Login Credential");
            }
            return View(user);
        }

        private bool IsValid(string email, string password)
        {
            var user = _userService.Login(new LoginUserRequest { Email = email, Password = password });
            if (user != null)
            {
                /* Try Get Current User Role
                 */
                //this._createRole(user.RoleName);
                //this._userAddToRole(user.Username, user.RoleName);
                var profileData = new UserProfileSessionData { UserId = user.Id, Email = user.Email, Name = user.Username, RoleId = user.RoleId, RoleName = user.RoleName, RedirectUrl = user.ChangeModel, IsSuperAdmin = user.IsSuperAdmin };
                this.Session["LoginUser"] = profileData;
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return user.IsSuccess;
            }
            return false;
        }

        private void _userAddToRole(string Username, string RoleName)
        {
            if (!Roles.IsUserInRole(Username, RoleName))
            {
                Roles.AddUserToRole(Username, RoleName);
            }
        }

        private void _createRole(string rolename)
        {
            if (!Roles.RoleExists(rolename))
            {
                Roles.CreateRole(rolename);
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            var viewModel = new ChangePasswordViewModel();
            var user = _userService.GetUserByName(new GetUserByNameRequest { Name = User.Identity.Name });
            viewModel.Id = user.Id;
            return View(viewModel);
        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //todo call change password service
                var response = _userService.ChangePassword(new ChangePasswordRequest { Id = model.Id, Old_Password = model.Old_Password, New_Password = model.New_Password });

                //ViewBag.Message = response.Message;
                @TempData["Message"] = response.Message;
                return RedirectToAction("Validate", new { Message = response.Message });
            }
            else
            {
                ModelState.AddModelError("", "Incorrect Login Credential");
            }
            return View(model);
        }

        public ActionResult CheckPassword(ChangePasswordViewModel model)
        {
            var response = new UpdateUserResponse();
            response = _userService.CheckPassword(new CheckPasswordRequest { Name = User.Identity.Name, Password = model.Old_Password });
            if (response.IsSuccess)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Validate(string Message)
        {
            ViewBag.Message = Message;
            return View();
        }

        public ActionResult ResetPassword() {
            
            return View();
        }
    }
}