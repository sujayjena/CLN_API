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
    public class TatTimes_Search : BaseSearchEntity
    {
    }

    public class TatTimes_Request : BaseEntity
    {
        public string HolidayName { get; set; }

        public DateTime? HolidayDate { get; set; }

        public int? DaysId { get; set; }

        public int? StartTimeHour { get; set; }

        public int? StartTimeMinutes { get; set; }

        public int? SlaTimeHour { get; set; }

        public int? SlaTimeMinutes { get; set; }

        public int? EndTimeHour { get; set; }

        public int? EndTimeMinutes { get; set; }

        public bool? IsHoliday { get; set; }

        public bool? IsActive { get; set; }
    }

    public class TatTimes_Response : BaseResponseEntity
    {
        public string HolidayName { get; set; }

        public DateTime? HolidayDate { get; set; }

        public int? DaysId { get; set; }

        public string Days { get; set; }

        public int? StartTimeHour { get; set; }

        public int? StartTimeMinutes { get; set; }

        public int? SlaTimeHour { get; set; }

        public int? SlaTimeMinutes { get; set; }

        public int? EndTimeHour { get; set; }

        public int? EndTimeMinutes { get; set; }

        public bool? IsHoliday { get; set; }

        public bool? IsActive { get; set; }
    }

    public class TatTimes_ImportData
    {
        public string HolidayName { get; set; }
        public DateTime? HolidayDate { get; set; }
        public string? Days { get; set; }
    }

    public class TatTimes_ImportDataValidation
    {
        public string HolidayName { get; set; }
        public string? HolidayDate { get; set; }
        public string? Days { get; set; }
        public string ValidationMessage { get; set; }
    }
}
