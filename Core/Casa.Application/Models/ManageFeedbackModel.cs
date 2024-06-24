using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ManageFeedbackModel
    {
    }
    public class ManageFeedback_Request : BaseEntity
    {
        public int? TicketId { get; set; }
        public int? Rating { get; set; }
        public string? FeedbackDetails { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ManageFeedback_Response : BaseResponseEntity
    {
        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }

        public int? CD_CustomerNameId { get; set; }
        public string? CD_CustomerName { get; set; }
        public string? CD_CallerName { get; set; }
        public string? CD_CustomerMobile { get; set; }
        public string? TicketSLADays { get; set; }

        public string? Rating { get; set; }
        public string? FeedbackDetails { get; set; }
        public bool? IsActive { get; set; }
    }
}
