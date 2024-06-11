using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class EnggPartsReturnModel
    {
    }

    public class EnggPartsReturn_Search : BaseSearchEntity
    {
        public int? EngineerId { get; set; }
        public int? StatusId { get; set; }
    }

    public class EnggPartsReturn_Request : BaseEntity
    {
        public int? EngineerId { get; set; }

        public int? SpareDetailsId { get; set; }

        public int? ReturnQuantity { get; set; }

        public int? StatusId { get; set; }
    }

    public class EnggPartsReturn_Response : BaseResponseEntity
    {
        public int? EngineerId { get; set; }
        public string EngineerName { get; set; }
        public int? SpareDetailsId { get; set; }
        public string UniqueCode { get; set; }
        public string SpareDesc { get; set; }

        public int? AvailableQty { get; set; }
        public int? ReturnQuantity { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
    }
}
