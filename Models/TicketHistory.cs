using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBugTracker.Models
{
    public class TicketHistory
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Changed { get; set; }
        public string UserId { get; set; }

        public virtual Ticket Tickets { get; set; }
    }
}