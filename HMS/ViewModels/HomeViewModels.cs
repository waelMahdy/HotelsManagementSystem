using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.ViewModels
{
    public class HomeViewModels
    {
        public IEnumerable<AccomodationType> AccomodationTypes { get; set; }
        public IEnumerable<AccomodationPackage> AccomodationPackages { get; set; }

    }
}