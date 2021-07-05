using HMS.Areas.Dashboard.ViewModels;
using HMS.Data;
using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new HMSContext();
            var Name = User.Identity.Name;
            var user = context.Users.Where(x => x.UserName == Name).FirstOrDefault();
            if (user != null)
            {
                TempData["currentUser"] = user.Fullname;
            }


            HomeViewModels model = new HomeViewModels();
            AccomodationTypesService typesService = new AccomodationTypesService();
            AccomodationPackagesService packagesService = new AccomodationPackagesService();

            model.AccomodationTypes = typesService.GetAllAccomodationTypes();
            model.AccomodationPackages = packagesService.GetAllAccomodationPackages();

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}