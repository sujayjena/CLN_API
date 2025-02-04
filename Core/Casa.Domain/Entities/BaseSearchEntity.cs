using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class BaseSearchEntity : BasePaninationEntity
    {
        [DefaultValue("")]
        public string? SearchText { get; set; }

        [DefaultValue(null)]
        public bool? IsActive { get; set; }

        [DefaultValue("")]
        public string? SortBy { get; set; }

        [DefaultValue("")]
        public string? OrderBy { get; set; }

    }
}
