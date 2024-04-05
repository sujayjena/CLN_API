using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ProblemReportedModel
    {
    }
    public class ProblemReported_Request : BaseEntity
    {
        public string? ProblemReported { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ProblemReported_Response : BaseResponseEntity
    {
        public string? ProblemReported { get; set; }
        public bool? IsActive { get; set; }
    }
}
