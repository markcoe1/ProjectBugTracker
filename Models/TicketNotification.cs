using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBugTracker.Models
{
    public class TicketNotification
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        public virtual Ticket Tickets { get; set; }
        public virtual ApplicationUser _ApplicationUsersUsers { get; set; }

    }
}