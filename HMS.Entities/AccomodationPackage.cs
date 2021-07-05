using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HMS.Entities
{
    public class AccomodationPackage
    {
        public int ID { get; set; }
        public int AccomodationTypeID { get; set; }
        public virtual AccomodationType AccomodationType { get; set; }
        public string Name { get; set; }
        public int NoOfRoom { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public decimal FeePerNight { get; set; }

        public virtual List<AccomodationPackagePicture> AccomodationPackagePictures { get; set; }
    }
}
