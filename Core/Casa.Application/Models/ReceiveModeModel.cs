using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ReceiveModeModel
    {
    }

    public class ReceiveMode_Request : BaseEntity
    {
        public string? ReceiveMode { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ReceiveMode_Response : BaseResponseEntity
    {
        public string? ReceiveMode { get; set; }
        public bool? IsActive { get; set; }
    }
}
