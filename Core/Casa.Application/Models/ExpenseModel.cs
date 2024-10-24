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
    public class ExpenseModel
    {
    }

    public class Expense_Search : BaseSearchEntity
    {
        [DefaultValue(null)]
        public DateTime? FromDate { get; set; }

        [DefaultValue(null)]
        public DateTime? ToDate { get; set; }

        public int? EmployeeId { get; set; }

        [DefaultValue(0)]
        public int? StatusId { get; set; }

        [DefaultValue("")]
        public string? ExpenseId { get; set; }

        [DefaultValue(false)]
        public bool? IsDownloded { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }
    }
    public class ExportExpense_Request
    {
        [DefaultValue("")]
        public string? ExpenseId { get; set; }
    }

    public class Expense_Request : BaseEntity
    {
        public Expense_Request()
        {
            ExpenseDetails = new List<ExpenseDetails_Request>();
        }

        [DefaultValue(false)]
        public bool? IsMySelf { get; set; }

        public string? ExpenseNumber { get; set; }

        [DefaultValue(0)]
        public int? EmployeeId { get; set; }

        public int? TicketId { get; set; }

        public int? StatusId { get; set; }

        public bool? IsActive { get; set; }

        public List<ExpenseDetails_Request> ExpenseDetails { get; set; }
    }

    public class Expense_Response : BaseResponseEntity
    {
        public Expense_Response()
        {
            ExpenseDetails = new List<ExpenseDetails_Response>();
        }

        public bool? IsMySelf { get; set; }
        public string? ExpenseNumber { get; set; }
        public int? EmployeeId { get; set; }
        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? TicketStartDate { get; set; }
        public DateTime? TicketCloserDate { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public bool? IsExport { get; set; }
        public bool? IsDownloded { get; set; }
        public bool? IsActive { get; set; }

        public List<ExpenseDetails_Response> ExpenseDetails { get; set; }
    }

    public class Expense_ApproveNReject
    {
        public int? Id { get; set; }
        public int? ExpenseId { get; set; }
        public int? ExpenseDetailStatusId { get; set; }

        [DefaultValue("")]
        public string? Remarks { get; set; }
    }

    public class UpdateIsExport_Request
    {
        public string? Id { get; set; }
        public string? Module { get; set; }
    }

    public class UpdateDownloadedExpense_Request
    {
        [DefaultValue("")]
        public string? ExpenseId { get; set; }
    }

    public class ExpenseDetails_Search : BaseSearchEntity
    {
        public int? ExpenseId { get; set; }
        public int? ExpenseDetailStatusId { get; set; }
    }

    public class ExpenseDetails_Request : BaseEntity
    {
        public int? ExpenseId { get; set; }

        [DefaultValue(false)]
        public bool? IsSingleDayExpense { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int? ExpenseTypeId { get; set; }

        public int? VehicleTypeId { get; set; }

        public string? ExpenseDescription { get; set; }

        public decimal? ApprovedAmount { get; set; }
        public decimal? ExpenseAmount { get; set; }

        public string? ExpenseImageFileName { get; set; }

        public string? ExpenseImageOriginalFileName { get; set; }

        public string? ExpenseImageFile_Base64 { get; set; }

        public int? ExpenseDetailStatusId { get; set; }

        public int? CityGradeId { get; set; }
    }

    public class ExpenseDetails_Response : BaseResponseEntity
    {
        public ExpenseDetails_Response()
        {
            remarksList = new List<ExpenseDetailsRemarks_Response>();
        }
        public int? ExpenseId { get; set; }
        public string? ExpenseNumber { get; set; }
        public bool? IsSingleDayExpense { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? ExpenseTypeId { get; set; }
        public string? ExpenseType { get; set; }
        public int? VehicleTypeId { get; set; }
        public string? VehicleType { get; set; }
        public string? ExpenseDescription { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? ExpenseAmount { get; set; }
        public string? ExpenseImageFileName { get; set; }
        public string? ExpenseImageOriginalFileName { get; set; }
        public string? ExpenseImageFileURL { get; set; }
        public int? ExpenseDetailStatusId { get; set; }
        public string? ExpenseDeteillStatusName { get; set; }
        public string? Remarks { get; set; }
        public int? CityGradeId { get; set; }
        public string? CityGrade { get; set; }
        public List<ExpenseDetailsRemarks_Response> remarksList { get; set; }
    }

    public class ExpenseDetailsRemarks_Response
    {
        public int? Id { get; set; }
        public string? Remarks { get; set; }
        public string? ApproveOrReject { get; set; }
        public string? CreatorName { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }

    public class DailyTravelExpense_Request : BaseEntity
    {
        public string? ExpenseNumber { get; set; }
        public bool? IsTicetExpense { get; set; }
        public int? TicketId { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int? ExpenseTypeId { get; set; }
        public string? ExpenseDesc { get; set; }
        public int? VehicleTypeId { get; set; }
        public decimal? RatePerKm { get; set; }
        public decimal? TotalKm { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Remarks { get; set; }
        public int? StatusId { get; set; }

        [JsonIgnore]
        public string? ExpenseImageFileName { get; set; }
        public string? ExpenseImageOriginalFileName { get; set; }
        public string? ExpenseImageFile_Base64 { get; set; }

    }

    public class DailyTravelExpense_Search : BaseSearchEntity
    {
        [DefaultValue(null)]
        public DateTime? FromDate { get; set; }

        [DefaultValue(null)]
        public DateTime? ToDate { get; set; }

        public int? EmployeeId { get; set; }
        public int? StatusId { get; set; }

        [DefaultValue("")]
        public string? ExpenseId { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }

    }

    public class DailyTravelExpense_Response : BaseResponseEntity
    {
        public DailyTravelExpense_Response()
        {
            remarksList = new List<ExpenseDetailsRemarks_Response>();
        }

        public string? ExpenseNumber { get; set; }
        public bool? IsTicetExpense { get; set; }
        public int? TicketId { get; set; }
        public string? TicketNumber { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int? ExpenseTypeId { get; set; }
        public string? ExpenseType { get; set; }
        public string? ExpenseDesc { get; set; }
        public int? VehicleTypeId { get; set; }
        public string? VehicleType { get; set; }
        public decimal? RatePerKm { get; set; }
        public decimal? TotalKm { get; set; }
        public decimal? TotalAmount { get; set; }
        public string? Remarks { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public string? ExpenseImageFileName { get; set; }
        public string? ExpenseImageOriginalFileName { get; set; }
        public string? ExpenseImageFileURL { get; set; }
        public bool? IsExport { get; set; }
        public List<ExpenseDetailsRemarks_Response> remarksList { get; set; }
    }

    public class DailyTravelExpense_ApproveNReject
    {
        public int? Id { get; set; }
        public int? StatusId { get; set; }

        [DefaultValue("")]
        public string? Remarks { get; set; }
    }
}
