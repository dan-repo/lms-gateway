using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using LmsGateway.Domain;
using LmsGateway.Domain.Users;

namespace LmsGateway.Core.Models
{
    public class UserModel : UrlModel
    {
        public UserModel()
        {
            Types = GetUserTypes();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required]
        public string Type { get; set; }

        public List<SelectListItem> Types { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        [Required]
        [UIHint("password")]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and it's confirmation did not match!")]
        public string ConfirmPassword { get; set; }

        public string Username { get; set; }

        [Required]
        public bool IAgree { get; set; }

        private List<SelectListItem> GetUserTypes()
        {
            string[] userTypes = Enum.GetNames(typeof(UserType));
            SelectListItem defaultItem = new SelectListItem() { Value = "", Text = "Select a user type" };

            List<SelectListItem> modelListItems = new List<SelectListItem>();
            modelListItems.Add(defaultItem);
            foreach (string userType in userTypes)
            {
                SelectListItem selectList = new SelectListItem() { Value = userType, Text = userType };
                modelListItems.Add(selectList);
            }

            return modelListItems;

        }



    }


}
