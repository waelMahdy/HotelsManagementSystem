using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static HMS.ViewModels.PageViewModels;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationsController : Controller
    {
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        AccomodationsService accomodationsService = new AccomodationsService();

        // GET: Dashboard/Accomodations
        public ActionResult Index(string searchTerm, int? AccomodationPackageID, int? page)
        {

            int recordSize = 5;
            page = page ?? 1;
            AccomodationsListingViewModel model = new AccomodationsListingViewModel();
            model.SearchTerm = searchTerm;
            model.AccomodationPackageID = AccomodationPackageID;
            model.Accomodations = accomodationsService.SearchAccomodations(searchTerm, AccomodationPackageID, page.Value, recordSize);
            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();
            var totalRecords = accomodationsService.SearchAccomodationsCount(searchTerm, AccomodationPackageID);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationsActionViewModel model = new AccomodationsActionViewModel();
            if (ID.HasValue)
            {
                var accomodation = accomodationsService.GetAccomodationById(ID.Value);
                model.AccomodationPackageID = accomodation.AccomodationPackageID;
                model.ID = accomodation.ID;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
            }

            model.AccomodationPackages = accomodationPackagesService.GetAllAccomodationPackages();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public ActionResult Action(AccomodationsActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            if (model.ID > 0)
            {
                var accomodation = accomodationsService.GetAccomodationById(model.ID);
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;        
               result = accomodationsService.UpdateAccomodation(accomodation);
            }
            else
            {
                Accomodation accomodation = new Accomodation();
                accomodation.AccomodationPackageID = model.AccomodationPackageID;

                accomodation.Name = model.Name;
                accomodation.Description = model.Description;
                result = accomodationsService.SaveAccomodation(accomodation);
            }

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perfrom action on Accomodation." };
            }
            return jsonResult;
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationsActionViewModel model = new AccomodationsActionViewModel();

            var accomodation = accomodationsService.GetAccomodationById(ID);
            model.ID = accomodation.ID;
            model.Name = accomodation.Name;

            return PartialView("_Delete", model);
        }
        [HttpPost]
        public ActionResult Delete(AccomodationsActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            var accomodation = accomodationsService.GetAccomodationById(model.ID);

            result = accomodationsService.DeleteAccomodation(accomodation);
            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perfrom action on Accomodation." };
            }
            return jsonResult;
        }
    }
}