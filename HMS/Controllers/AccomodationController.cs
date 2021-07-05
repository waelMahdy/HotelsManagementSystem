using HMS.Areas.Dashboard.ViewModels;
using HMS.Services;
using HMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Controllers
{
    public class AccomodationController : Controller
    {
        AccomodationPackagesService PackagesService = new AccomodationPackagesService();
        AccomodationTypesService TypesService = new AccomodationTypesService();
        AccomodationsService AccomodationsService = new AccomodationsService();
     
        public ActionResult Index(int accomodationTypeID,int? accomodationPackageID)
        {
            AccomodationsViewModels model = new AccomodationsViewModels();
            model.accomodationType = TypesService.GetAccomodationTypeById(accomodationTypeID);
            model.accomodationPackages = PackagesService.GetAllAccomodationPackagesByAccomodationType(accomodationTypeID);
            model.selectedAccomodationPackageID=accomodationPackageID.HasValue ? accomodationPackageID.Value:model.accomodationPackages.First().ID;
            model.accomodations = AccomodationsService.GetAllAccomodationsByAccomodationPackageID(model.selectedAccomodationPackageID);
           
            return View(model);
        }
        public ActionResult Details(int AccomodationPackageID)
        {

            AccomodationsPackageDetailsViewModel model = new AccomodationsPackageDetailsViewModel();
            model.AccomodationPackage = PackagesService.GetAccomodationPackageById(AccomodationPackageID);
            return View(model);
        }
        public ActionResult ShowAllAccomodation(int AccomodationPackageID)
        {

            ShowAllAccomodationsViewModel model = new ShowAllAccomodationsViewModel();
            model.Accomodations = AccomodationsService.GetAllAccomodationsByAccomodationPackageID(AccomodationPackageID);
            model.AccomodationPackage = PackagesService.GetAccomodationPackageById(AccomodationPackageID);
            return View(model);
        }
        public ActionResult CheckAvailability(CheckAccomodationAvailabilityViewModel model)
        {

            return View();
        }
    }
}
