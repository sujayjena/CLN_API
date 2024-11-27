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
    public class ManageReport_Search : BaseSearchEntity
    {
        [DefaultValue(null)]
        public DateTime? FromDate { get; set; }

        [DefaultValue(null)]
        public DateTime? ToDate { get; set; }

        [DefaultValue("")]
        public string? TicketType { get; set; }

        [DefaultValue("")]
        public string? BranchId { get; set; }
    }

    public class Ticket_TRC_Report_Response : BaseEntity
    {
        public string? TicketType { get; set; }
        public string? TRCLocation { get; set; }
        public DateTime? TicketDate { get; set; }
        public string? TicketNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public string? TRCNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? CallerName { get; set; }
        public string? CallerRegionName { get; set; }
        public string? CallerStateName { get; set; }
        public string? CallerDistrictName { get; set; }
        public string? CallerCityName { get; set; }
        public string? ProductCategory { get; set; }
        public string? Segment { get; set; }
        public string? SubSegment { get; set; }
        public string? ProductModel { get; set; }
        public string? TypeOfBMS { get; set; }
        public string? ProductSerialNumber { get; set; }
        public DateTime? DateofManufacturing { get; set; }
        public string? WarrantyStatus { get; set; }
        public string? ProbReportedByCust { get; set; }
        public string? ProblemObservedByEng { get; set; }
        public string? RectificationAction { get; set; }
        public string? TicketStatus { get; set; }
        public DateTime? ResolvedDate { get; set; }
        public DateTime? CSATDate { get; set; }
        public DateTime? ClosureDate { get; set; }
    }

    public class CustomerWiseReport_Response : BaseEntity
    {
        public string? CustomerName { get; set; }
        public string? ProductCategory { get; set; }
        public string? Segment { get; set; }
        public string? SubSegment { get; set; }
        public long? NoofIssue { get; set; }
        public long? OpenIssue { get; set; }
        public long? CloseIssue { get; set; }
    }

    public class CustomerSatisfactionReport_Response : BaseEntity
    {
        public string? TicketNumber { get; set; }
        public string? TRCNumber { get; set; }
        public string? ClosedBy { get; set; }
        public DateTime? ClosedDate { get; set; }
        public DateTime? CSATDate { get; set; }
        public string? OverallExperience { get; set; }
        public string? Satisfaction { get; set; }
        public string? CustomerService { get; set; }
        public string? Timeliness { get; set; }
        public string? Resolution { get; set; }
    }

    public class FTFReport_Response : BaseEntity
    {
        public DateTime? TicketDate { get; set; }
        public long? TotalRequest { get; set; }
        public long? ResolvedTickets { get; set; }
        public long? FTFRatePerct { get; set; }
        public long? CSATScore { get; set; }
    }

    public class LogisticSummaryReport_Response : BaseEntity
    {
        public string? TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public string? TRCNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? ReceiveMode { get; set; }
        public string? DocumentNo { get; set; }
        public string? RegionName { get; set; }
        public string? StateName { get; set; }
        public string? DistrictName { get; set; }
        public string? CityName { get; set; }
        public string? CustomerName { get; set; }
        public string? ProductCategory { get; set; }
        public string? Segment { get; set; }
        public string? SubSegment { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductSerialNumber { get; set; }
        public DateTime? DispatchedDate { get; set; }
        public string? DispatchStatus { get; set; }
        public string? DispatchMode { get; set; }
        public string? DispatchAddress { get; set; }
        public string? DispatchChallanNo { get; set; }
        public string? DispatchedDocketNo { get; set; }
        public string? CourierName { get; set; }
        public DateTime? CustomerReceivingDate { get; set; }
    }

    public class ExpenseReport_Response : BaseEntity
    {
        public string? TicketNumber { get; set; }
        public DateTime? TicketDate { get; set; }
        public string? TRCNumber { get; set; }
        public DateTime? TRCDate { get; set; }
        public string? TicketType { get; set; }
        public string? TRCLocation { get; set; }
        public string? ProductCategory { get; set; }
        public string? Segment { get; set; }
        public string? SubSegment { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductSerialNumber { get; set; }
        public decimal? TotalPartPrice { get; set; }
        public decimal? TotalExpense { get; set; }
        public decimal? TotalCost { get; set; }
    }
}
