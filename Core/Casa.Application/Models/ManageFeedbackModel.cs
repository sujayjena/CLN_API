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
        public int TicketId { get; set; }
        public int Rating { get; set; }
        public string FeedbackDetails { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ManageFeedback_Response : BaseResponseEntity
    {
        public string? TicketId { get; set; }
        public string? CustomerName { get; set; }
        public string? ContactName { get; set; }
        public string? MobielNo { get; set; }
        public string? Rating { get; set; }
        public string? FeedbackDetails { get; set; }
        public bool? IsActive { get; set; }
    }
}
