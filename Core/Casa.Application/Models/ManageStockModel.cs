using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ManageStockModel
    {
    }

    #region Generate Part Request
    public class GeneratePartRequest_Request : BaseEntity
    {
        public int? SpareDetailsId { get; set; }
        public int? TypeOfBMSId { get; set; }
        public int? AvailableQty { get; set; }
        public int? OrderQty { get; set; }
        public int? RequiredQty { get; set; }

        [DefaultValue("")]
        public string? Remarks { get; set; }
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
    }
    public class GeneratePartRequestSearch_Request : BaseSearchEntity
    {
        public int CompanyId { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }
    }

    public class GeneratePartRequest_Response : BaseResponseEntity
    {
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareCategory { get; set; }
        public string? SpareDesc { get; set; }
        public string? UOMName { get; set; }
        public int? MinQty { get; set; }
        public int? TypeOfBMSId { get; set; }
        public string? TypeOfBMS { get; set; }
        public int? AvailableQty { get; set; }
        public int? OrderQty { get; set; }
        public int? RequiredQty { get; set; }
        public string? Remarks { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
    }
    #endregion

    #region Generate Challan
    public class GenerateChallan_Request : BaseEntity
    {
        public GenerateChallan_Request()
        {
            GenerateChallanPartDetailList = new List<GenerateChallanPartDetails_Request>();
        }

        public int CompanyId { get; set; }

        public int BranchId { get; set; }

        public List<GenerateChallanPartDetails_Request> GenerateChallanPartDetailList { get; set; }
    }

    public class GenerateChallanSearch_Request : BaseSearchEntity
    {
        public int CompanyId { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }
    }

    public class GenerateChallanPartDetailsSearch_Request : BaseSearchEntity
    {
        public int GenerateChallanId { get; set; }
    }

    public class GenerateChallan_Response : BaseResponseEntity
    {
        public int Id { get; set; }
        public string? RequestId { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
    }

    public class GenerateChallanPartDetailsById_Response : BaseResponseEntity
    {
        public GenerateChallanPartDetailsById_Response()
        {
            GenerateChallanPartDetailList = new List<GenerateChallanPartDetails_Response>();
        }
        public int Id { get; set; }
        public string? RequestId { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
        public List<GenerateChallanPartDetails_Response> GenerateChallanPartDetailList { get; set; }
    }

    public class GenerateChallanPartDetails_Request : BaseEntity
    {
        [JsonIgnore]
        public int? GenerateChallanId { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? OrderQty { get; set; }
    }

    public class GenerateChallanPartDetails_Response : BaseResponseEntity
    {
        public int Id { get; set; }
        public int GenerateChallanId { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareCategory { get; set; }
        public string? SpareDesc { get; set; }
        public string? UOMName { get; set; }
        public int? OrderQty { get; set; }
    }
    #endregion
}
