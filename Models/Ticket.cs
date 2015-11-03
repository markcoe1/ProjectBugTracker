using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectBugTracker.Models
{
    public class Ticket
    {

                public Ticket()
        {
            this.TicketAttachments = new HashSet<TicketAttachment>();
            this.TicketComments = new HashSet<TicketComment>();
                    this.TicketHistorys = new HashSet<TicketHistory>();

        }

            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        
            public int ProjectId { get; set; }
            public System.DateTimeOffset Created { get; set; }
            public System.DateTimeOffset? Updated { get; set; }
            public string UpdateReason { get; set; }

            public int TicketTypeId { get; set; }
            public int TicketPriorityId { get; set; }
            public int TicketStatusId { get; set; }
            public string OwnerUserId { get; set; }
            public string AssignedUserId { get; set; }

            public virtual ApplicationUser OwnerUser { get; set; }
            public virtual ApplicationUser AssignedUser { get; set; }
            public virtual TicketPriority TicketPrioritys { get; set; }
            public virtual TicketStatus TicketStatuses { get; set; }
            public virtual TicketType TicketType { get; set; }
            public virtual Project Projects { get; set; }

            public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
            public virtual ICollection<TicketComment> TicketComments { get; set; }
            public virtual ICollection<TicketHistory> TicketHistorys { get; set; }

    }
}