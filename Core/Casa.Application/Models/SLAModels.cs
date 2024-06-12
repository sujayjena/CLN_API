using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SLAModels
    {
    }
    public class SLAPriority_Request : BaseEntity
    {
        public string? SLAPriority { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SLAPriority_Response : BaseResponseEntity
    {
        public string? SLAPriority { get; set; }
        public bool? IsActive { get; set; }
    }



    public class SlaMatrix_Search : BaseSearchEntity
    {
    }

    public class SlaMatrix_Request : BaseEntity
    {
        public int? TicketStatusFromId { get; set; }
        public int? TicketStatusToId { get; set; }
        public int? SeqNo { get; set; }
        public int? TicketPriorityId { get; set; }
        public string Days { get; set; }
        public string Hrs { get; set; }
        public string Min { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SlaMatrix_Response : BaseResponseEntity
    {
        public int? TicketStatusFromId { get; set; }
        public string TicketStatusFrom { get; set; }
        public int? TicketStatusToId { get; set; }
        public string TicketStatusTo { get; set; }
        public int? SeqNo { get; set; }
        public int? TicketPriorityId { get; set; }
        public string SLAPriority { get; set; }
        public string Days { get; set; }
        public string Hrs { get; set; }
        public string Min { get; set; }
        public bool? IsActive { get; set; }
    }
}
