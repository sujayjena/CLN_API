using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class RectificationActionModel
    {
    }
    public class RectificationAction_Request : BaseEntity
    {
        public string? RectificationAction { get; set; }
        public bool? IsActive { get; set; }
    }

    public class RectificationAction_Response : BaseResponseEntity
    {
        public string? RectificationAction { get; set; }
        public bool? IsActive { get; set; }
    }
}
