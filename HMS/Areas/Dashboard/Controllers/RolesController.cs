using HMS.Areas.Dashboard.ViewModels;
using HMS.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class RolesController : Controller
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
        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager, HMSRolesManager rolesManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RolesManager = rolesManager;
        }



        // GET: Dashboard/Accomodations
        public ActionResult Index(string searchTerm, int? page)
        {

            int recordSize = 5;
            page = page ?? 1;
            RolesListingModel model = new RolesListingModel();
            model.SearchTerm = searchTerm;
            model.Roles = SearchRoles(searchTerm, page.Value, recordSize);
            var totalRecords = SearchRolesCount(searchTerm);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int page, int recordSize)
        {
            var roles = RolesManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));

            }
            var skip = (page - 1) * recordSize;

            return roles.OrderBy(a=>a.Name).Skip(skip).Take(recordSize).ToList();
        }
        public int SearchRolesCount(string searchTerm)
        {

            var roles = RolesManager.Roles.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
               roles=roles.Where(a=>a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
       

            return roles.Count();
        }

        [HttpGet]
        public async Task<ActionResult> Action(string ID)
        {
            RoleActionModel model = new RoleActionModel();
            if (!string.IsNullOrEmpty(ID))
            {
                var role = await RolesManager.FindByIdAsync(ID);
                model.ID=role.Id ;
                model.Name = role.Name;

            }


            return PartialView("_Action", model);
        }

        [HttpPost]
        public async Task<ActionResult> Action(RoleActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {

                var role = await RolesManager.FindByIdAsync(model.ID);
                role.Name = model.Name;

                result = await RolesManager.UpdateAsync(role);

            }
            else
            {
                var role = new IdentityRole();
                role.Name = model.Name;
            
                result = await RolesManager.CreateAsync(role);
            }
            jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };

            return jsonResult;
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string ID)
        {
            RoleActionModel model = new RoleActionModel();

            var role = await RolesManager.FindByIdAsync(ID);
            model.ID = role.Id;
             model.Name = role.Name;

            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(RoleActionModel model)
        {
            JsonResult jsonResult = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.ID))
            {
                var role = await RolesManager.FindByIdAsync(model.ID);
                result = await RolesManager.DeleteAsync(role);
                jsonResult.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Invalid role." };

            }

            return jsonResult;
        }

    }
}