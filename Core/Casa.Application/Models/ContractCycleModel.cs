using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ContractCycleModel
    {
    }
    public class ContractCycle_Request : BaseEntity
    {
        public string? ContractCycleName { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public string? ContractCycleFileName { get; set; }
        public string? ContractCycleFile_Base64 { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ContractCycle_Response : BaseResponseEntity
    {
        public string? ContractCycleName { get; set; }
        public int Months { get; set; }
        public int Days { get; set; }
        public string? ContractCycleFile { get; set; }
        public string? ContractCycleFileURL { get; set; }
        public bool? IsActive { get; set; }
    }
}
