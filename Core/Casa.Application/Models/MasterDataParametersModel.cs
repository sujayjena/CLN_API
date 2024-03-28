using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SelectListResponse
    {
        public long Value { get; set; }
        public string Text { get; set; }
    }
    public class ReportingToEmpListParameters
    {
        public long RoleId { get; set; }
        public long? RegionId { get; set; }
    }
}
