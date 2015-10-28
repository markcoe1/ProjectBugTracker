using ProjectBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectBugTracker.Helper;

namespace ProjectBugTracker.Controllers
{
    public class ProjectManagerController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();
        private ProjectsHelper helper = new ProjectsHelper();

        //Index
        [Authorize(Roles = "Admin, ProjectManager")]
        public ActionResult Index()
        {
            List<Project> model = new List<Project>();
            if (User.IsInRole("Admin"))
            {
                //get a list of all projects in the DB
                model = db.Project.ToList();
          
            }

            else
            {
                //get a list of assigned projects in the DB
                model = db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name).Project.ToList();
              
            }
            //send the model to the view
            return View(model);
        }

        // GET: ProjectManagers/AssignProjectManager
        [Authorize(Roles = "Admin")]
        public ActionResult AssignProjectManager(int id)
        {
            var project = db.Project.Find(id);
            var model = new ProjectUsersView { ProjectId = id, ProjectName = project.Name };
            var userProjectList = helper.ListProjectManagersNotOnProject(id);

            model.Users = new MultiSelectList(userProjectList.OrderBy(m => m.DisplayName), "id", "DisplayName", null);

            return View(model);
        }

        //Post: ProjectUsers/AssignProjectManager
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AssignProjectManager(ProjectUsersView model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    foreach (string userId in model.SelectedUsers)
                    {
                        helper.AssignUserToProject(userId, model.ProjectId);
                    }
                }
                return RedirectToAction("Index", "ProjectManager");
            }
            return View(model);
        }
        // GET: ProjectUsers/UnassignProjectManager
        [Authorize(Roles = "Admin")]
        public ActionResult UnassignProjectManager(int id)
        {
            var project = db.Project.Find(id);
            var model = new ProjectUsersView { ProjectId = id, ProjectName = project.Name };
            var userProjectList = helper.ListProjectManagers (id);

            model.Users = new MultiSelectList(userProjectList.OrderBy(m => m.DisplayName), "id", "DisplayName", null);

            return View(model);
        }
        //POST: ProjectUsers/UnassignProjectManager
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UnassignProjectManager(ProjectUsersView model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    foreach (string userId in model.SelectedUsers)
                    {
                        helper.RemoveUserFromProject(userId, model.ProjectId);
                    }
                    return RedirectToAction("Index", "ProjectManager");
                }
            }
            return View(model);
        }

        // GET: ProjectUsers/AssignUsers
        [Authorize(Roles = "Admin")]
        public ActionResult AssignUsers(int id)
        {
            var project = db.Project.Find(id);
            var model = new ProjectUsersView { ProjectId = id, ProjectName = project.Name};
            var userProjectList = helper.ListDevsNotOnProject(id);

            model.Users = new MultiSelectList(userProjectList.OrderBy(m => m.DisplayName), "id", "DisplayName", null);

            return View(model);
        }
        //Post: ProjectUsers/AssignUsers
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult AssignUsers(ProjectUsersView model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    foreach (string userId in model.SelectedUsers)
                    {
                        helper.AssignUserToProject(userId, model.ProjectId);
                    }
                }
                return RedirectToAction("Index", "ProjectManager");
            }
            return View(model);
        }
        // GET: ProjectUsers/UnassignUsers
        [Authorize(Roles = "Admin")]
        public ActionResult UnassignUsers (int id)
        {
            var project = db.Project.Find(id);
            var model = new ProjectUsersView { ProjectId = id, ProjectName = project.Name };
            var userProjectList = helper.ListProjectDevelopers(id);

            model.Users = new MultiSelectList(userProjectList.OrderBy(m => m.DisplayName), "id", "DisplayName", null);

            return View(model);
        }
    //POST: ProjectUsers/UnassignUsers
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UnassignUsers (ProjectUsersView model)
        {
            if (ModelState.IsValid)
            {
                if (model.SelectedUsers != null)
                {
                    foreach (string userId in model.SelectedUsers)
                    {
                        helper.RemoveUserFromProject(userId, model.ProjectId);
                    }
                    return RedirectToAction("Index", "ProjectManager");
                }
            }
            return View(model);
        } 
    }
}