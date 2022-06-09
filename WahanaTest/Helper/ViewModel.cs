using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Penjualan.Helper
{
    public class ViewModel
    {
    }

    public class LoginViewModel
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class MenuViewModel
    {
        public Nullable<int> ID { get; set; }
        public Nullable<int> ParentModuleID { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string ModuleName { get; set; }
        public string PageIcon { get; set; }
        public string PageURL { get; set; }
        public string PageSlug { get; set; }
        public List<SubMenuViewModel> SubMenu { get; set; }
    }

    public class SubMenuViewModel
    {
        public Nullable<int> ID { get; set; }
        public Nullable<int> DisplayOrder { get; set; }
        public string ModuleName { get; set; }
        public string PageIcon { get; set; }
        public string PageURL { get; set; }
        public string PageSlug { get; set; }
    }

    public class UserViewModel
    {
        public Nullable<Guid> ID { get; set; }
        [Required(ErrorMessage ="This field is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string BirthDate { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<int> CountryID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string cPassword { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public Nullable<Guid> RoleID { get; set; }
        public string Address { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string GoogleLink { get; set; }
        public string LinkedInLink { get; set; }
        public string SkypeID { get; set; }
    }

    public class RoleViewModel
    {
        public Nullable<Guid> ID { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
    }

    public class ActivityViewModel
    {
        public Nullable<Guid> UserID { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public Nullable<DateTime> LogTime { get; set; }
        public string IPAddress { get; set; }
    }
}