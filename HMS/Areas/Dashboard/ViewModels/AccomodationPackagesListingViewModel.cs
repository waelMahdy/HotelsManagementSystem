using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HMS.ViewModels.PageViewModels;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationPackagesListingViewModel
    {
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get;  set; }
        public int? AccomodationTypeID { get; set; }
        public Pager Pager { get; set; }
    }
    public class AccomodationPackagesActionViewModel
    {
        public int ID { get; set; }
        public int AccomodationTypeID { get; set; }
        public virtual AccomodationType AccomodationType { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }
        public decimal FeePerNight { get; set; }
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public string pictureIDs { get; set; }
        public  List<AccomodationPackagePicture> accomodationPackagePictures { get; set; }
    }
} 
