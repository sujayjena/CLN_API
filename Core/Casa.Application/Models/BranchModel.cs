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
    public class BranchModel
    {
    }

    public class Branch_Request : BaseEntity
    {
        public Branch_Request()
        {
            RegionList = new List<BranchRegion_Request>();
            StateList = new List<BranchState_Request>();
        }
        public string? BranchName { get; set; }
        public int? CompanyId { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string? DepartmentHead { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public int? RegionId { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? CityId { get; set; }
        public int? Pincode { get; set; }
        public int? NoofUserAdd { get; set; }
        public bool? IsActive { get; set; }

        public List<BranchRegion_Request> RegionList { get; set; }
        public List<BranchState_Request> StateList { get; set; }
    }

    public class Branch_Response : BaseResponseEntity
    {
        public string? BranchName { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public string? DepartmentHead { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public int? RegionId { get; set; }
        public string? RegionName { get; set; }
        public int? StateId { get; set; }
        public string? StateName { get; set; }
        public int? DistrictId { get; set; }
        public string? DistrictName { get; set; }
        public int? CityId { get; set; }
        public string? CityName { get; set; }
        public int? Pincode { get; set; }
        public int? NoofUserAdd { get; set; }
        public bool? IsActive { get; set; }

        public List<BranchRegion_Response> RegionList { get; set; }
        public List<BranchState_Response> StateList { get; set; }
    }

    public class BranchMapping_Request : BaseEntity
    {
        [JsonIgnore]
        public string? Action { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
        public int? BranchId { get; set; }
    }
    public class BranchSearch_Request : BaseSearchEntity
    {
        public int? CompanyId { get; set; }
    }

    public class BranchMapping_Response : BaseEntity
    {
        public int? UserId { get; set; }
        public int? EmployeeId { get; set; }
        public int? BranchId { get; set; }
        public string? BranchName { get; set; }
    }

    public class BranchState_Request
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string? Action { get; set; }

        [JsonIgnore]
        public int? BranchId { get; set; }
        public int? StateId { get; set; }
    }

    public class BranchState_Response
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
    }

    public class BranchRegion_Request
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string? Action { get; set; }

        [JsonIgnore]
        public int? BranchId { get; set; }
        public int? RegionId { get; set; }
    }

    public class BranchRegion_Response
    {
        public int Id { get; set; }
        public int? BranchId { get; set; }
        public int? RegionId { get; set; }
        public string RegionName { get; set; }
    }
}
