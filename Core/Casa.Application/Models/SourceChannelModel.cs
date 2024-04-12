using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SourceChannelModel
    {
    }

    public class SourceChannel_Request : BaseEntity
    {
        public string? SourceChannel { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SourceChannel_Response : BaseResponseEntity
    {
        public string? SourceChannel { get; set; }
        public bool? IsActive { get; set; }
    }
}
