using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class AccomodationsViewModels
    {
        public IEnumerable<AccomodationPackage> accomodationPackages { get; set; }
        public AccomodationType accomodationType { get; set; }
        public IEnumerable<Accomodation> accomodations { get; set; }
        public int selectedAccomodationPackageID { get; internal set; }
    }
}