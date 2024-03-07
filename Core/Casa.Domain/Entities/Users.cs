using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Domain.Entities
{
    public class Users : BaseEntity
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Passwords { get; set; }
    }
}

