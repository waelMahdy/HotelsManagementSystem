using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HMS.ViewModels.PageViewModels;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationsListingViewModel
    {
        public IEnumerable<Accomodation> Accomodations { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public int? AccomodationPackageID { get; set; }
        public Pager Pager { get; set; }
    }
    public class AccomodationsActionViewModel
    {
        public int ID { get; set; }
        public int AccomodationPackageID { get; set; }
        public virtual AccomodationPackage AccomodationPackage { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
    }
    public class AccomodationsPackageDetailsViewModel
    {
    
        public  AccomodationPackage AccomodationPackage { get; set; }
   
    }
    public class ShowAllAccomodationsViewModel
    {
        public AccomodationPackage AccomodationPackage { get; set; }
        public IEnumerable<Accomodation> Accomodations { get; set; }

    }

    public class CheckAccomodationAvailabilityViewModel
    {
        public DateTime FromDate { get; set; }
        public int Duration { get; set; }
        public int noOfAdults { get; set; }
        public int noOfChildren { get; set; }
        public string Name { get; set; }
        public string email { get; set; }
        public string notes { get; set; }

    }
}