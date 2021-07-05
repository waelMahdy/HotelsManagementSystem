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
    public class AccomodationPackagesController : Controller
    {
        AccomodationPackagesService accomodationPackagesService = new AccomodationPackagesService();
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        DashboardService dashboardService = new DashboardService();
        // GET: Dashboard/AccomodationPackages
        public ActionResult Index(string searchTerm, int? AccomodationTypeID,int? page)
        {
            int recordSize = 5;
            page = page ?? 1;
            AccomodationPackagesListingViewModel model = new AccomodationPackagesListingViewModel();
            model.SearchTerm = searchTerm;
            model.AccomodationTypeID = AccomodationTypeID;
            model.AccomodationPackages = accomodationPackagesService.SearchAccomodationPackage(searchTerm, AccomodationTypeID,page.Value,recordSize);
            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();
            var totalRecords= accomodationPackagesService.SearchAccomodationPackagesCount(searchTerm, AccomodationTypeID);
            model.Pager = new Pager(totalRecords, page,recordSize);
        
            return View(model);
        }

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationPackagesActionViewModel model = new AccomodationPackagesActionViewModel();
            if (ID.HasValue)
            {
                var accomodationPackage= accomodationPackagesService.GetAccomodationPackageById(ID.Value);
                model.AccomodationTypeID = accomodationPackage.AccomodationTypeID;
                model.ID = accomodationPackage.ID;
                model.Name = accomodationPackage.Name;
                model.FeePerNight = accomodationPackage.FeePerNight;
                model.NoOfRoom = accomodationPackage.NoOfRoom;
                model.accomodationPackagePictures = accomodationPackagesService.GetPicturesByAccomodatoinPackageID(accomodationPackage.ID);
            }

            model.AccomodationTypes = accomodationTypesService.GetAllAccomodationTypes();

            return PartialView("_Action", model);
        }

        [HttpPost]
        public JsonResult Action(AccomodationPackagesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            List<int> pictureIDs =!string.IsNullOrEmpty(model.pictureIDs) ? model.pictureIDs.Split(',').Select(x => int.Parse(x)).ToList(): new List<int>();
            var pictures = dashboardService.GetPicturesByIDs(pictureIDs);

            if (model.ID > 0)
            {
                var accomodationpackage = accomodationPackagesService.GetAccomodationPackageById(model.ID);
                accomodationpackage.Name = model.Name;
                accomodationpackage.NoOfRoom = model.NoOfRoom;
                accomodationpackage.FeePerNight = model.FeePerNight;
                accomodationpackage.AccomodationTypeID = model.AccomodationTypeID;
                accomodationpackage.AccomodationPackagePictures.Clear();
                accomodationpackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { AccomodationPackageID = accomodationpackage.ID, PictureID = x.ID }));
                result = accomodationPackagesService.UpdateAccomodationPackage(accomodationpackage);
            }
            else
            {
                AccomodationPackage accomodationPackge = new AccomodationPackage();
                accomodationPackge.AccomodationTypeID = model.AccomodationTypeID;

                accomodationPackge.Name = model.Name;
                accomodationPackge.FeePerNight = model.FeePerNight;
                accomodationPackge.NoOfRoom = model.NoOfRoom;
                accomodationPackge.AccomodationPackagePictures = new List<AccomodationPackagePicture>();
                accomodationPackge.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { PictureID = x.ID }));

                result = accomodationPackagesService.SaveAccomodationPackage(accomodationPackge);
            }

            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perfrom action on Accomodation Package." };
            }
            return jsonResult;
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationPackagesActionViewModel model = new AccomodationPackagesActionViewModel();

            var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(ID);
            model.ID = accomodationPackage.ID;
            model.Name = accomodationPackage.Name;

            return PartialView("_Delete", model);
        }
        public ActionResult Delete(AccomodationPackagesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            var accomodationPackage = accomodationPackagesService.GetAccomodationPackageById(model.ID);

            result = accomodationPackagesService.DeleteAccomodationPackage(accomodationPackage);
            if (result)
            {
                jsonResult.Data = new { Success = true };
            }
            else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perfrom action on Accomodation Type." };
            }
            return jsonResult;
        }

    }
}