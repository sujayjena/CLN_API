﻿using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class AddressModel
    {
    }

    public class Address_Request : BaseEntity
    {
        public int RefId { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public int? RegionId { get; set; }

        public int? StateId { get; set; }

        public int? DistrictId { get; set; }

        public int? CityId { get; set; }

        public string PinCode { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }
    }

    public class Address_Response : BaseResponseEntity
    {
        public int RefId { get; set; }

        public string RefType { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public int? RegionId { get; set; }

        public string RegionName { get; set; }

        public int? StateId { get; set; }

        public string StateName { get; set; }

        public int? DistrictId { get; set; }

        public string DistrictName { get; set; }

        public int? CityId { get; set; }

        public string CityName { get; set; }

        public string PinCode { get; set; }

        public bool? IsDefault { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
