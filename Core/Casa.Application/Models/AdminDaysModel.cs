using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class AdminDays_Search : BaseSearchEntity
    {
    }

    public class AdminDays_Request : BaseEntity
    {
        public string Days { get; set; }

        public bool? IsActive { get; set; }
    }

    public class AdminDays_Response : BaseResponseEntity
    {
        public string Days { get; set; }

        public bool? IsActive { get; set; }
    }
}
