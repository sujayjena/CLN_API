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
        public int? RequestId { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? ReturnQuantity { get; set; }
        public int? StatusId { get; set; }
    }

    public class EnggPartsReturn_Response : BaseResponseEntity
    {
        public int? EngineerId { get; set; }
        public string EngineerName { get; set; }
        public int? RequestId { get; set; }
        public int? SpareDetailsId { get; set; }
        public string UniqueCode { get; set; }
        public string SpareDesc { get; set; }

        public int? AvailableQty { get; set; }
        public int? ReturnQuantity { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
    }

    public class EnggPartsReturn_RequestIdList 
    {
        public int? Id { get; set; }
        public string RequestNumber { get; set; }
        public int? EngineerId { get; set; }
        public int? SpareDetailsId { get; set; }
        public int RequiredQty { get; set; }
        public int AllocatedQty { get; set; }
        public int ReceivedQty { get; set; }
        public int AvailableQty { get; set; }
        public int? ReturnQuantity { get; set; }
        public int? ReturnQuantityPendingForApproved { get; set; }
        public int? ReturnQuantityApproved { get; set; }
        public int? ReturnQuantityRejected { get; set; }
    }

    public class EnggPartsReturn_ApprovedRequest  
    {
        public EnggPartsReturn_ApprovedRequest()
        {
            PartList=new List<EnggPartsReturn_ApprovedPartList>();
        }

        public int? EngineerId { get; set; }
        public int? RequestId { get; set; }
        
        public List<EnggPartsReturn_ApprovedPartList> PartList { get; set; }
    }

    public class EnggPartsReturn_ApprovedPartList 
    {
        public int? SpareDetailsId { get; set; }
        public int? StatusId { get; set; }
    }
}
