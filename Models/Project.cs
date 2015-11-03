using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBugTracker.Models
{
    public class Project
    {
        public Project()
        {
            this.Tickets = new HashSet<Ticket>();
            this._ApplicationUsers = new HashSet<ApplicationUser>();
            this.TicketComment = new HashSet<TicketComment>();
        
        }
         
        
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ApplicationUser> _ApplicationUsers { get; set; }
        public virtual ICollection<TicketComment> TicketComment { get; set; }
    }
}
