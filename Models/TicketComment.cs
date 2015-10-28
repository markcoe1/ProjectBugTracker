using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBugTracker.Models
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public System.DateTimeOffset Created { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }

        public virtual ApplicationUser _ApplicationUsers { get; set; }
        public virtual Ticket Tickets { get; set; }
    }
}