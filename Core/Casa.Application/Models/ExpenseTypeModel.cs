using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ExpenseTypeModel
    {
    }
    public class ExpenseType_Request : BaseEntity
    {
        public string? ExpenseType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ExpenseType_Response : BaseResponseEntity
    {
        public string? ExpenseType { get; set; }
        public bool? IsActive { get; set; }
    }
}
