using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomodationPackagesService
    {
        public IEnumerable<AccomodationPackage> GetAllAccomodationPackages()
        {
            var context = new HMSContext();
            return context.accomodationPackages.ToList();
        }
        public IEnumerable<AccomodationPackage> GetAllAccomodationPackagesByAccomodationType(int accomodationTypeID)
        {
            var context = new HMSContext();
            return context.accomodationPackages.Where(x=>x.AccomodationTypeID == accomodationTypeID ).ToList();
        }
        public IEnumerable<AccomodationPackage> SearchAccomodationPackage(string searchTerm,int? AccomodationTypeID,int page, int recordSize)
        {
            var context = new HMSContext();
            var accomodationPackes = context.accomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackes = accomodationPackes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (AccomodationTypeID.HasValue && AccomodationTypeID > 0)
            {
                accomodationPackes = accomodationPackes.Where(a => a.AccomodationTypeID == AccomodationTypeID.Value);
            }
            var skip = (page -1) * recordSize;

            return accomodationPackes.OrderBy(x=>x.AccomodationTypeID).Skip(skip).Take(recordSize).ToList();
        }
        public int SearchAccomodationPackagesCount(string searchTerm, int? AccomodationTypeID)
        {
            var context = new HMSContext();
            var accomodationPackes = context.accomodationPackages.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodationPackes = accomodationPackes.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (AccomodationTypeID.HasValue && AccomodationTypeID > 0)
            {
                accomodationPackes = accomodationPackes.Where(a => a.AccomodationTypeID == AccomodationTypeID.Value);
            }

            return accomodationPackes.Count();
        }
        public AccomodationPackage GetAccomodationPackageById(int id)
        {
            var context = new HMSContext();
            
             return context.accomodationPackages.Find(id);

        }
        public bool SaveAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();
            context.accomodationPackages.Add(accomodationPackage);
            return context.SaveChanges() > 0;
        }
        public bool UpdateAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();
            var existingAccomodationPackage = context.accomodationPackages.Find(accomodationPackage.ID);
            context.AccomodationPackagePictures.RemoveRange(existingAccomodationPackage.AccomodationPackagePictures);
            context.Entry(existingAccomodationPackage).CurrentValues.SetValues(accomodationPackage);
            context.AccomodationPackagePictures.AddRange(accomodationPackage.AccomodationPackagePictures);
            return context.SaveChanges() > 0;
        }
        public bool DeleteAccomodationPackage(AccomodationPackage accomodationPackage)
        {
            var context = new HMSContext();
            var existingAccomodationPackage = context.accomodationPackages.Find(accomodationPackage.ID);
            context.AccomodationPackagePictures.RemoveRange(existingAccomodationPackage.AccomodationPackagePictures);
            context.Entry(existingAccomodationPackage).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
        public List<AccomodationPackagePicture> GetPicturesByAccomodatoinPackageID(int accomodationPackageID)
        {
            var context = new HMSContext();
          
                return context.accomodationPackages.Find(accomodationPackageID).AccomodationPackagePictures.ToList();

         

        }

    }
}
