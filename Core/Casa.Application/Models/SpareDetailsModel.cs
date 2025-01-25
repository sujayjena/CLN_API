using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class SpareDetailsModel
    {
    }

    public class SpareDetails_Search : BaseSearchEntity
    {
        public int? SpareCategoryId { get; set; }
        public int? BMSMakeId { get; set; }
        public int? ProductMakeId { get; set; }

        [DefaultValue(null)]
        public bool? IsRGP { get; set; }
    }

    public class SpareDetails_Request : BaseEntity
    {
        public string? UniqueCode { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }

        [DefaultValue(0)]
        public decimal? MinQty { get; set; }

        [DefaultValue(0)]
        public decimal? AvailableQty { get; set; }

        [DefaultValue(0)]
        public decimal? TentativeCost { get; set; }

        public int? ProductMakeId { get; set; }
        public int? BMSMakeId { get; set; }

        [DefaultValue(false)]
        public bool? RGP { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SpareDetails_Response : BaseResponseEntity
    {
        public string? UniqueCode { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareCategory { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        public string? UOMName { get; set; }

        [DefaultValue(0)]
        public decimal? MinQty { get; set; }

        [DefaultValue(0)]
        public decimal AvailableQty { get; set; }

        public decimal? TentativeCost { get; set; }

        public int? ProductMakeId { get; set; }
        public string? ProductMake { get; set; }
        public int? BMSMakeId { get; set; }
        public string? BMSMake { get; set; }


        [DefaultValue(false)]
        public bool? RGP { get; set; }
        public bool? IsActive { get; set; }
    }


    #region Import and Download

    public class SpareDetails_ImportRequest
    {
        public IFormFile FileUpload { get; set; }
    }

    public class SpareDetails_ImportData
    {
        public string? SpareCategory { get; set; }
        public string? ProductMake { get; set; }
        public string? SparePartCode { get; set; }
        public string? SparePartDescription { get; set; }
        public string? UOM { get; set; }
        public string? MinQty { get; set; }
        public string? AvailableQty { get; set; }
        public string? TentativeCost { get; set; }
        public string? RGP { get; set; }
        public string? IsActive { get; set; }
    }

    public class SpareDetails_ImportDataValidation
    {
        public string? SpareCategory { get; set; }
        public string? ProductMake { get; set; }
        public string? SparePartCode { get; set; }
        public string SparePartDescription { get; set; }
        public string UOM { get; set; }
        public string MinQty { get; set; }
        public string AvailableQty { get; set; }
        public string? TentativeCost { get; set; }
        public string RGP { get; set; }
        public string IsActive { get; set; }
        public string ValidationMessage { get; set; }
    }

    #endregion
}
