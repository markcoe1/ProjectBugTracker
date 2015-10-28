using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectBugTracker.Models
{

    public class ChangeUserNameViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }
    }

    public class UserViewModel
    {
        [Display(Name = "Display name")]
        public string DisplayName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }

        [Display(Name = "User Roles")]
        public List<string> UserRoles { get; set; }

        [Display(Name = "Assigned Projects")]
        public List<Project> UserProjects { get; set; }

        [Display(Name = "Submitted Tickets")]
        public List<Ticket> TicketsOwned { get; set; }

        [Display(Name = "Assigned Tickets")]
        public List<Ticket> TicketsAssigned { get; set; }
    }
}