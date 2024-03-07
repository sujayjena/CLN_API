using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Domain.Entities
{
    public class Common : BaseEntity
    {
        public string FisrtName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
    }
}
