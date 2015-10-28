using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace ProjectBugTracker.Helper
{
    public class ProjectsHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();


        public ICollection<Project> ListProject()
        {
            return db.Project.ToList();
        }


        public bool IsUserOnProject(string userId, int ProjectId)
        {
            var proj = db.Project.Find(ProjectId);
            return proj._ApplicationUsers.Any(u => u.Id.Equals(userId));
        }

        public void AssignUserToProject(string userId, int ProjectId)
        {
            if (!IsUserOnProject(userId, ProjectId))
            {
                var proj = db.Project.Find(ProjectId);
                var user = db.Users.Find(userId);
                proj._ApplicationUsers.Add(user);
                db.Entry(proj).State = EntityState.Modified;
                db.SaveChanges();

            }
        }

        public bool RemoveUserFromProject(string userId, int ProjectId)
        {
            if (IsUserOnProject(userId, ProjectId))
            {
                var proj = db.Project.Find(ProjectId);
                var user = db.Users.Find(userId);
                if (proj._ApplicationUsers.Remove(user))
                {
                    db.Entry(proj).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public ICollection<Project> ListUserProjects(string userId)
        {
            return db.Users.Find(userId).Project.ToList();
        }

        public ICollection<ApplicationUser> ListProjectDevelopers(int projectId)
        {
            var roleId = db.Roles.SingleOrDefault(r => r.Name == "Developer").Id;
            var users = db.Project.Find(projectId)._ApplicationUsers;
            var devs = users.Where(user => user.Roles.Any(role => role.RoleId == roleId));
            return devs.ToList();
        }

        public ICollection<ApplicationUser> ListDevsNotOnProject(int projectId)
        {
            // get Developer role id
            var roleId = db.Roles.SingleOrDefault(r => r.Name == "Developer").Id;
            //get all users NOT on the project
            var users = db.Users.Where(u => !u.Project.Any(p => p.Id == projectId));
            // restrict user list to developers only
            var devs = users.Where(user => user.Roles.Any(role => role.RoleId == roleId));
            // return the list of devs
            return devs.ToList();

            //    var roleId = db.Roles.SingleOrDefault(r => r.Name == "Developer").Id;    
            //return db.Users.Where(u => !u.Project.Any(p => p.Id == projectId)).Where(user => user.Roles.Any(role => role.RoleId == roleId)).ToList();
        }

        public ICollection<Project> ListProjectsNotForUser(string userId)
    {
        return db.Project.Where(p => !p._ApplicationUsers.Any(u => u.Id == userId)).ToList();    
        }
        public ICollection<ApplicationUser> ListProjectManagersNotOnProject(int projectId)
        {
            // get Developer role id
            var roleId = db.Roles.SingleOrDefault(r => r.Name == "ProjectManager").Id;
            //get all users NOT on the project
            var users = db.Users.Where(u => !u.Project.Any(p => p.Id == projectId));
            // restrict user list to developers only
            var devs = users.Where(user => user.Roles.Any(role => role.RoleId == roleId));
            // return the list of devs
            return devs.ToList();
        }
        public ICollection<ApplicationUser> ListProjectManagers(int projectId)
        {
            var roleId = db.Roles.SingleOrDefault(r => r.Name == "ProjectManager").Id;
            var users = db.Project.Find(projectId)._ApplicationUsers;
            var devs = users.Where(user => user.Roles.Any(role => role.RoleId == roleId));
            return devs.ToList();
        }
    }
}