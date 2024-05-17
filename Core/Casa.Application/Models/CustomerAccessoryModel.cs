using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{

    public class CustomerAccessory_Search : BaseSearchEntity
    {
        public long CustomerId { get; set; }
    }

    public class CustomerAccessory_Request : BaseEntity
    {
        public int? CustomerId { get; set; }

        public string AccessoryName { get; set; }

        public int? Quantity { get; set; }

        public bool? IsActive { get; set; }
    }

    public class CustomerAccessory_Response : BaseResponseEntity
    {
        public int? CustomerId { get; set; }

        public string AccessoryName { get; set; }

        public int? Quantity { get; set; }

        public bool? IsActive { get; set; }
    }
}
