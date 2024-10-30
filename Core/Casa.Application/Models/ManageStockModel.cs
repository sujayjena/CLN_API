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
        public int? SpareCategoryId { get; set; }
        public int? ProductMakeId { get; set; }
        public int? BMSMakeId { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? UOMId { get; set; }
        public int? TypeOfBMSId { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public int? RequestedQty { get; set; }

        [DefaultValue("")]
        public string? Remarks { get; set; }
        //public int? CompanyId { get; set; }
        //public int? BranchId { get; set; }
    }
    public class GeneratePartRequestSearch_Request : BaseSearchEntity
    {
        //public int? CompanyId { get; set; }

        //[DefaultValue("")]
        //public string? BranchId { get; set; }
    }

    public class GeneratePartRequest_Response : BaseResponseEntity
    {
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? BMSMakeId { get; set; }
        public string? BMSMake { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }
        public int? TypeOfBMSId { get; set; }
        public string? TypeOfBMS { get; set; }
        public int? MinQty { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public int? RequestedQty { get; set; }
        public string? Remarks { get; set; }
        //public int? CompanyId { get; set; }
        //public string? CompanyName { get; set; }
        //public int? BranchId { get; set; }
        //public string? BranchName { get; set; }
    }
    #endregion

    #region Generate Challan
    public class GenerateChallan_Request : BaseEntity
    {
        public GenerateChallan_Request()
        {
            GenerateChallanPartDetailList = new List<GenerateChallanPartDetails_Request>();
        }

        //public int? CompanyId { get; set; }

        //public int? BranchId { get; set; }

        public List<GenerateChallanPartDetails_Request> GenerateChallanPartDetailList { get; set; }
    }

    public class GenerateChallanSearch_Request : BaseSearchEntity
    {
        //public int? CompanyId { get; set; }

        //[DefaultValue("")]
        //public string? BranchId { get; set; }
    }

    public class GenerateChallanPartDetailsSearch_Request : BaseSearchEntity
    {
        public int? GenerateChallanId { get; set; }
    }

    public class GenerateChallan_Response : BaseResponseEntity
    {
        public int? Id { get; set; }
        public string? RequestId { get; set; }
        //public int? CompanyId { get; set; }
        //public string? CompanyName { get; set; }
        //public int? BranchId { get; set; }
        //public string? BranchName { get; set; }
    }

    public class GenerateChallanPartDetailsById_Response : BaseResponseEntity
    {
        public GenerateChallanPartDetailsById_Response()
        {
            GenerateChallanPartDetailList = new List<GenerateChallanPartDetails_Response>();
        }
        public int? Id { get; set; }
        public string? RequestId { get; set; }
        //public int? CompanyId { get; set; }
        //public string? CompanyName { get; set; }
        //public int? BranchId { get; set; }
        //public string? BranchName { get; set; }
        public List<GenerateChallanPartDetails_Response> GenerateChallanPartDetailList { get; set; }
    }

    public class GenerateChallanPartDetails_Request : BaseEntity
    {
        [JsonIgnore]
        public int? GenerateChallanId { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? UOMId { get; set; }
        public int? TypeOfBMSId { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public int? RequestedQty { get; set; }
    }

    public class GenerateChallanPartDetails_Response : BaseResponseEntity
    {
        public int? GenerateChallanId { get; set; }
        public string? RequestId { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }
        public int? TypeOfBMSId { get; set; }
        public string? TypeOfBMS { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public int? RequestedQty { get; set; }
    }
    #endregion

    #region Stock In
    public class StockIn_Request
    {
        public int? Id { get; set; }
        public int? GenerateChallanId { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? UOMId { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public int? RequestedQty { get; set; }
        public int? ReceivedQty { get; set; }
        public int? StatusId { get; set; }
    }
    public class StockInListSearch_Request : BaseSearchEntity
    {
        public int? GenerateChallanId { get; set; }
        public int? StatusId { get; set; }
    }

    public class StockIn_Response : BaseResponseEntity
    {
        public int? GenerateChallanId { get; set; }
        public string? RequestId { get; set; }
        public DateTime? GenerateChallanDate { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }

        public int? MinQty { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public int? RequestedQty { get; set; }
        public int? ReceivedQty { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
    }

    #endregion

    #region Stock Allocation

    public class StockAllocationList_Response : BaseResponseEntity
    {
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }
        public int? MinQty { get; set; }
        public int? BalanceQty { get; set; }
        public int? ReceivedQty { get; set; }
        public int? AvailableQty { get; set; }
        public bool? RGP { get; set; }

    }

    #region Stock Allocate To Engineer / TRC

    public class StockAllocated_Search : BaseSearchEntity
    {
        [DefaultValue("Engg")]
        public string? AllocatedType { get; set; }

        [DefaultValue(0)]
        public int? EngineerId { get; set; }

        [DefaultValue(0)]
        public int? StatusId { get; set; }
    }

    public class StockAllocated_Request : BaseEntity
    {
        public StockAllocated_Request()
        {
            PartList = new List<StockAllocatedPartDetails_Request>();
        }

        public int? RequestId { get; set; }

        public int? EngineerId { get; set; }

        [DefaultValue("Engg/TRC")]
        public string? AllocatedType { get; set; }

        public int? StatusId { get; set; }

        public bool? IsActive { get; set; }

        public List<StockAllocatedPartDetails_Request> PartList { get; set; }
    }

    public class StockAllocatedList_Response : BaseResponseEntity
    {
        public int? RequestId { get; set; }
        public string? RequestNumber { get; set; }
        public int? EngineerId { get; set; }
        public string? EngineerName { get; set; }
        public string? AllocatedType { get; set; }

        [DefaultValue(0)]
        public int? RequiredQty { get; set; }

        [DefaultValue(0)]
        public int? AllocatedQty { get; set; }

        [DefaultValue(0)]
        public int? ReceivedQty { get; set; }

        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public bool? IsActive { get; set; }
    }

    public class StockAllocatedDetails_Response : BaseResponseEntity
    {
        public StockAllocatedDetails_Response()
        {
            PartList = new List<StockAllocatedPartDetails_Response>();
        }

        public int? RequestId { get; set; }
        public string? RequestNumber { get; set; }
        public int? EngineerId { get; set; }
        public string? EngineerName { get; set; }
        public string? AllocatedType { get; set; }

        [DefaultValue(0)]
        public int? RequiredQty { get; set; }

        [DefaultValue(0)]
        public int? AllocatedQty { get; set; }

        [DefaultValue(0)]
        public int? ReceivedQty { get; set; }

        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public bool? IsActive { get; set; }

        public List<StockAllocatedPartDetails_Response> PartList { get; set; }
    }


    public class StockAllocatedPartDetails_Search : BaseSearchEntity
    {
        public int? StockAllocatedId { get; set; }
    }

    public class StockAllocatedPartDetails_Request : BaseEntity
    {
        public int? EngineerId { get; set; }
        public int? StockAllocatedId { get; set; }
        public int? SpareCategoryId { get; set; }
        public int? ProductMakeId { get; set; }
        public int? BMSMakeId { get; set; }
        public int? SpareId { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public int? AllocatedQty { get; set; }
        public int? ReceivedQty { get; set; }
        [DefaultValue(false)]
        public bool? RGP { get; set; }
    }

    public class StockAllocatedPartDetails_Response : BaseEntity
    {
        public int? StockAllocatedId { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? BMSMakeId { get; set; }
        public string? BMSMake { get; set; }
        public int? SpareId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public int? AvailableQty { get; set; }
        public int? RequiredQty { get; set; }
        public int? AllocatedQty { get; set; }
        public int? ReceivedQty { get; set; }
        [DefaultValue(false)]
        public bool? RGP { get; set; }
    }

    #endregion

    #endregion

    #region Stock Master

    public class StockMaster_Request
    {
        public int? EngineerId { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? Quantity { get; set; }
    }

    public class StockMaster_Response : BaseResponseEntity
    {
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareCategory { get; set; }
        public string? SpareDesc { get; set; }
        public string? UOMName { get; set; }
        public int? MinQty { get; set; }
        public int? AvailableQty { get; set; }
    }

    #endregion

    #region Engineer Stock Master

    public class EnggStockMasterListSearch_Request : BaseSearchEntity
    {
        public int? EngineerId { get; set; }
        public int? SpareDetailsId { get; set; }
    }

    public class EnggStockMaster_Response : BaseResponseEntity
    {
        public int? EngineerId { get; set; }
        public string? EngineerName { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }
        public int? MinQty { get; set; }
        public int? AvailableQty { get; set; }
        public bool? RGP { get; set; }
    }

    public class EnggStockMaster_Request : BaseEntity
    {
        public int? MinQty { get; set; }
    }

    #endregion

    #region Order Received Engineer

    public class OrderReceivedEngineer_Search : BaseSearchEntity
    {
        [DefaultValue(0)]
        public int? EngineerId { get; set; }
    }

    public class OrderReceivedEngineer_Response
    {
        public int? EngineerId { get; set; }
        public string EngineerName { get; set; }
        public int? RegionId { get; set; }
        public string RegionName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? AreaId { get; set; }
        public string AreaName { get; set; }
    }

    public class EngineerOrderListByEngineerId_Search : BaseSearchEntity
    {
        public int? EngineerId { get; set; }
    }

    public class EngineerOrderListByEngineerId_Response : BaseResponseEntity
    {
        public int? EngineerId { get; set; }
        public string RequestNumber { get; set; }
        public DateTime? RequestDate { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public string RequestType { get; set; }
    }
       

    #endregion
}
