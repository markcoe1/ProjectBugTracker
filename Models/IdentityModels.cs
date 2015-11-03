using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }

        public ApplicationUser()
        {
            this.Project = new HashSet<Project>();
            //this.TicketAttachments = new HashSet<TicketAttachment>();
            //this.TicketComments = new HashSet<TicketComment>();
            //this.TicketHistorys = new HashSet<TicketHistory>();
            //this.TicketNotifications = new HashSet<TicketNotification>();
            //this.Ticket = new HashSet<Ticket>();
            this.TicketsOwned = new HashSet<Ticket>();
            this.TicketsAssigned = new HashSet<Ticket>();
        }


        public virtual ICollection<Project> Project { get; set; }
        [InverseProperty("OwnerUser")]
        public virtual ICollection<Ticket> TicketsOwned { get; set; }
        [InverseProperty("AssignedUser")]
        public virtual ICollection<Ticket> TicketsAssigned { get; set; }
        //public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        //public virtual ICollection<TicketComment> TicketComments { get; set; }
        //public virtual ICollection<TicketHistory> TicketHistorys { get; set; }
        //public virtual ICollection<TicketNotification> TicketNotifications { get; set; }
        //public virtual ICollection<Ticket> Ticket{ get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    
    }






    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("azureDb", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public DbSet<Project> Project { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<TicketComment> TicketComment { get; set; }
        public DbSet<TicketHistory> TicketHistory { get; set; }
        public DbSet<TicketPriority> TicketPriority { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketStatus> TicketStatus { get; set; }
        public DbSet<TicketType> TicketType { get; set; }

        
    
    }
}