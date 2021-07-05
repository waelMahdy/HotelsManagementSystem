using HMS.Areas.Dashboard.ViewModels;
using HMS.Entities;
using HMS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMS.Areas.Dashboard.Controllers
{
    public class AccomodationTypesController : Controller
    {
        AccomodationTypesService accomodationTypesService = new AccomodationTypesService();
        public ActionResult Index(string searchTerm)
        {
            AccomodationTypesListingViewModel model = new AccomodationTypesListingViewModel();
            model.SearchTerm = searchTerm;

            model.AccomodationTypes = accomodationTypesService.SearchAccomodationTypes(searchTerm);
            return View(model);
        }
    

        [HttpGet]
        public ActionResult Action(int? ID)
        {
            AccomodationTypesActionViewModel model = new AccomodationTypesActionViewModel();
            if (ID.HasValue)
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypeById(ID.Value);
                model.ID = accomodationType.ID;
                model.Name = accomodationType.Name;
                model.Desciption = accomodationType.Desciption;
            }
       

            return PartialView("_Action",model);
        }

        [HttpPost]
        public ActionResult Action(AccomodationTypesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;
            if(model.ID > 0)
            {
                var accomodationType = accomodationTypesService.GetAccomodationTypeById(model.ID);
                accomodationType.Name = model.Name;
                accomodationType.Desciption = model.Desciption;
                result = accomodationTypesService.UpdateAccomodationType(accomodationType);
            }
            else
            {
                AccomodationType accomodationType = new AccomodationType();
                accomodationType.Name = model.Name;
                accomodationType.Desciption = model.Desciption;
                result = accomodationTypesService.SaveAccomodationType(accomodationType);
            }
      
            if (result)
            {
                jsonResult.Data = new { Success = true };
            }else
            {
                jsonResult.Data = new { Success = false, Message = "Unable to perfrom action on Accomodation Type." };
            }
            return jsonResult;
        }
        [HttpGet]
        public ActionResult Delete(int ID)
        {
            AccomodationTypesActionViewModel model = new AccomodationTypesActionViewModel();
        
             var accomodationType = accomodationTypesService.GetAccomodationTypeById(ID);
                model.ID = accomodationType.ID;
                model.Name = accomodationType.Name;

            return PartialView("_Delete", model);
        }
        public ActionResult Delete(AccomodationTypesActionViewModel model)
        {
            JsonResult jsonResult = new JsonResult();
            var result = false;

            var accomodationType = accomodationTypesService.GetAccomodationTypeById(model.ID);

            result = accomodationTypesService.DeleteAccomodationType(accomodationType);
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