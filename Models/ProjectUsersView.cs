using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectBugTracker.Models
{
    public class UserProjectUsersView
    {

        public string ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

    }

    public class ProjectUsersView
    {

        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Developers")]
        public System.Web.Mvc.MultiSelectList Users { get; set; }

        public string[] SelectedUsers { get; set; }

    }
}
