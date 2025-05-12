using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ErrorLogHistory_Request : BaseEntity
    {
        public string? ModuleName { get; set; }
        public string? JsonData { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
