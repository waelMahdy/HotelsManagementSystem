using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class AccomodationTypesListingViewModel
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public string SearchTerm { get; set; }
    }
    public class AccomodationTypesActionViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }

    }
}