using HMS.Areas.Dashboard.ViewModels;
using HMS.Data;
using HMS.Entities;
using HMS.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static HMS.ViewModels.PageViewModels;

namespace HMS.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRolesManager _roleManager;

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public HMSRolesManager RolesManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().GetUserManager<HMSRolesManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRolesManager rolesManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RolesManager = rolesManager;
        }

        // GET: Dashboard/Accomodations
        public async Task<ActionResult> Index(string searchTerm, string roleID, int? page)
        {

            int recordSize = 5;
            page = page ?? 1;
            UsersListingViewModel model = new UsersListingViewModel();
            model.SearchTerm = searchTerm;
            model.RoleID = roleID;
            model.Roles = RolesManager.Roles.ToList();
            model.Users = await SearchUsers(searchTerm, roleID, page.Value, recordSize);
            var totalRecords = await SearchUsersCount(searchTerm, roleID);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        public async Task<IEnumerable<HMSUser>> SearchUsers(string searchTerm, string roleID, int page, int recordSize)
        {

            var skip = (page - 1) * recordSize;
            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Fullname.ToLower().Contains(searchTerm.ToLower()));
                if (users.Count() == 0)
                {
                    users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
                }
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                var role = await RolesManager.FindByIdAsync(roleID);
                var userIDs = role.Users.Select(x => x.UserId).ToList();
                users = users.Where(x => userIDs.Contains(x.Id));

                // users = users.Where(a => a.Roles.Select(x => x.RoleId).Contains(roleID)); same without Async


            }


            return users.OrderBy(x => x.Fullname).Skip(skip).Take(recordSize).ToList();
        }
        public async Task<int> SearchUsersCount(string searchTerm, string roleID)
        {

            var users = UserManager.Users.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleID))
            {
                var role = await RolesManager.FindByIdAsync(roleID);
                var userIDs = role.Users.Select(x => x.UserId).ToList();
                users = users.Where(x => userIDs.Contains(x.Id));

                // users = users.Where(a => a.Roles.Select(x => x.RoleId).Contains(roleID));


            }

            return users.Count();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            UsersActionViewModel model = new UsersActionViewModel();
            if (!string.IsNullOrEmpty(ID))
            {
                var user = await UserManager.FindByIdAsync(ID);
                model.ID = user.Id;
                model.FullName = user.Fullname;
                model.Email = user.Email;
                model.Username = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;

            }

            //model.Roles = accomodationPackagesService.GetAllAccomodationPackages();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<ActionResult> Action(UsersActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);
                user.Fullname = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                result = await UserManager.UpdateAsync(user);


            }
            else
            {
                var user = new HMSUser();
                user.Fullname = model.FullName;
                user.Email = model.Email;
                user.UserName = model.Username;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                result = await UserManager.CreateAsync(user);
            }
            jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return jsonResult;
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            UsersActionViewModel model = new UsersActionViewModel();

            var user = await UserManager.FindByIdAsync(ID);
            model.ID = ID;
            model.FullName = user.Fullname;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(UsersActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var user = await UserManager.FindByIdAsync(model.ID);
                result = await UserManager.DeleteAsync(user);

                jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Invalid User." };

            }

            return jsonResult;
        }

        [HttpGet]
        public async Task<ActionResult> UserRoles(string ID)
        {
            RoleActionViewModel model = new RoleActionViewModel();
            model.UserID = ID;
            var user = await UserManager.FindByIdAsync(ID);
            var userRoleIDs = user.Roles.Select(x => x.RoleId).ToList();
            model.UserRoles = RolesManager.Roles.Where(x => userRoleIDs.Contains(x.Id)).ToList();
            model.Roles = RolesManager.Roles.Where(x => !userRoleIDs.Contains(x.Id)).ToList();

            return PartialView("_UserRoles", model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"> Type of operation to perform e.g Assign Role or Delete Role</param>
        /// <param name="UserID"></param>
        /// <param name="roleID"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UserRoleOperation(string UserID, string roleID, bool isDelete = false)
        {
            JsonResult jsonResult = new JsonResult();
            var user = await UserManager.FindByIdAsync(UserID);
            var role = await RolesManager.FindByIdAsync(roleID);

            if (user != null && role != null)
            {
                IdentityResult result = null;
                if (!isDelete)
                {
                    result = await UserManager.AddToRoleAsync(UserID, role.Name);

                }
                else
                {
                    result = await UserManager.RemoveFromRoleAsync(UserID, role.Name);

                }
                jsonResult.Data = new { Success = result.Succeeded, Message = string.Join("", result.Errors) };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Invalid operation." };

            }
            return jsonResult;

        }

    }
}