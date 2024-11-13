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
    }

    public class Ticket_TRC_Report_Response
    {
        public int? Id { get; set; }
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
}
