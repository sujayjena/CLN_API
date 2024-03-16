using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class BaseSearchEntity : BasePaninationEntity
    {
        public string SearchText { get; set; }

        public bool? IsActive { get; set; }
    }
}
