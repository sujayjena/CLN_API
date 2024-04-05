using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class TicketsModel
    {
    }
    public class TicketCategory_Request : BaseEntity
    {
        public string? TicketCategory { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TicketCategory_Response : BaseResponseEntity
    {
        public string? TicketCategory { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TicketStatus_Request : BaseEntity
    {
        public string? TicketStatus { get; set; }

        public bool? IsActive { get; set; }
    }

    public class TicketStatus_Response : BaseResponseEntity
    {
        public string? TicketStatus { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TicketType_Request : BaseEntity
    {
        public string? TicketType { get; set; }

        public bool? IsActive { get; set; }
    }

    public class TicketType_Response : BaseResponseEntity
    {
        public string? TicketType { get; set; }
        public bool? IsActive { get; set; }
    }
}
