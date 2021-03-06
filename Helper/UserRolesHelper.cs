﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjectBugTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBugTracker.Helper
{
    public class UserRolesHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        private UserManager <ApplicationUser> manager =
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));

        public bool IsUserInRole (string userId, string roleName)
        {
            return manager.IsInRole(userId, roleName);
        }

        public IList<string> ListUserRoles (string userId)
        {
            return manager.GetRoles(userId);
        }

        public bool AddUserToRole(string userId, string roleName)
        {
            var result = manager.AddToRole(userId, roleName);
                return result.Succeeded;
        }

        public bool RemoveUserFromRole (string userId, string roleName)
        {
            var result = manager.RemoveFromRole(userId, roleName);
                return result.Succeeded;
        }

        public ICollection<ApplicationUser> UsersInRole(string roleName)
        {
            var roleId = db.Roles.SingleOrDefault(r => r.Name == roleName).Id;
            return db.Users.Where(u => u.Roles.Any(r => r.RoleId.Equals(roleId))).ToList();
        }

        public ICollection<ApplicationUser> UsersNotInRole(string roleName)
        {
            var roleId = db.Roles.SingleOrDefault(r => r.Name == roleName).Id;
            return db.Users.Where(u => !u.Roles.Any(r => r.RoleId.Equals(roleId))).ToList();
        }
    }
}