using HMS.Data;
using HMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class AccomodationsService
    {
        public IEnumerable<Accomodation> GetAllAccomodations()
        {
            var context = new HMSContext();
            return context.accomodations.ToList();
        }
        public IEnumerable<Accomodation> GetAllAccomodationsByAccomodationPackageID(int accomodationPackageID)
        {
            var context = new HMSContext();
            return context.accomodations.Where(x=>x.AccomodationPackageID == accomodationPackageID).ToList();
        }
        public IEnumerable<Accomodation> SearchAccomodations(string searchTerm, int? AccomodationPackageID, int page, int recordSize)
        {
            var context = new HMSContext();
            var accomodations = context.accomodations.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (AccomodationPackageID.HasValue && AccomodationPackageID > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageID == AccomodationPackageID.Value);
            }
            var skip = (page - 1) * recordSize;

            return accomodations.OrderBy(x => x.AccomodationPackageID).Skip(skip).Take(recordSize).ToList();
        }
        public int SearchAccomodationsCount(string searchTerm, int? AccomodationPackageID)
        {
            var context = new HMSContext();
            var accomodations = context.accomodations.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            if (AccomodationPackageID.HasValue && AccomodationPackageID > 0)
            {
                accomodations = accomodations.Where(a => a.AccomodationPackageID == AccomodationPackageID.Value);
            }

            return accomodations.Count();
        }
        public Accomodation GetAccomodationById(int id)
        {
            using (var context = new HMSContext())
            {
                return context.accomodations.Find(id);

            }

        }
        public bool SaveAccomodation(Accomodation accomodation)
        {
            var context = new HMSContext();
            context.accomodations.Add(accomodation);
            return context.SaveChanges() > 0;
        }
        public bool UpdateAccomodation(Accomodation accomodation)
        {
            var context = new HMSContext();
            context.Entry(accomodation).State = System.Data.Entity.EntityState.Modified;
            return context.SaveChanges() > 0;
        }
        public bool DeleteAccomodation(Accomodation accomodation)
        {
            var context = new HMSContext();
            context.Entry(accomodation).State = System.Data.Entity.EntityState.Deleted;
            return context.SaveChanges() > 0;
        }
    }
}
