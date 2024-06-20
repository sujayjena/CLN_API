using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{

    public class Notification_Search : BaseSearchEntity
    {
        public DateTime? NotifyDate { get; set; }
    }

    public class Notification_Request : BaseEntity
    {
        public string Subject { get; set; }
        public string SendTo { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerMessage { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeMessage { get; set; }
        public string RefValue1 { get; set; }
        public string RefValue2 { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? ReadUnread { get; set; }
    }

    public class Notification_Response
    {
        public long Id { get; set; }
        public int? CustomerEmployeeId { get; set; }

        public string Subject { get; set; }
        public string SendTo { get; set; }
        public string Message { get; set; }
        public string RefValue1 { get; set; }
        public string RefValue2 { get; set; }
        public bool? ReadUnread { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
