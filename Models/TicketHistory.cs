﻿using System;
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
        public System.DateTimeOffset Changed { get; set; }
        public string UserId { get; set; }
        public string Property { get; set; }
        public bool NotificationSeen { get; set; }

        public virtual Ticket Tickets { get; set; }
        public virtual ApplicationUser User { get; set; }

        public TicketHistory()
        {
            NotificationSeen = false;
        }
    }
}