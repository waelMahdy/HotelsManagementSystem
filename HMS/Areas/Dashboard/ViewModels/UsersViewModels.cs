using HMS.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static HMS.ViewModels.PageViewModels;

namespace HMS.Areas.Dashboard.ViewModels
{
    public class UsersViewModels
    {
 
    }
    public class UsersListingViewModel
    {
        public IEnumerable<HMSUser> Users { get; set; }
        public string RoleID { get; set; }
        public string SearchTerm { get; set; }
        public string FullName { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }

        public Pager Pager { get; set; }
    }
    public class UsersActionViewModel
    {
        public string ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class RoleActionViewModel
    {
        //public int ID { get; set; }
        //public string RoleID { get; set; }
        //public IdentityRole Role { get; set; }
        //public string Name { get; set; }

        public string UserID { get; set; }
        public IEnumerable<IdentityRole> UserRoles { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
    }
}