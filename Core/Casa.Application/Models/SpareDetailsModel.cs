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
    }

    public class SpareDetails_Request : BaseEntity
    {
        public string? UniqueCode { get; set; }
        public int? SpareCategoryId { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        [DefaultValue(0)]
        public int? MinQty { get; set; }
        [DefaultValue(0)]
        public int AvailableQty { get; set; }
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
        public int? MinQty { get; set; }
        [DefaultValue(0)]
        public int AvailableQty { get; set; }
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
        public string? PartCode { get; set; }
        public string? Description { get; set; }
        public string? UOM { get; set; }
        public string? MinQty { get; set; }
        public string? AvailableQty { get; set; }
        public string? RGP { get; set; }
        public string? IsActive { get; set; }
    }

    public class SpareDetails_ImportDataValidation
    {
        public string? SpareCategory { get; set; }
        public string? PartCode { get; set; }
        public string Description { get; set; }
        public string UOM { get; set; }
        public string MinQty { get; set; }
        public string AvailableQty { get; set; }
        public string IsActive { get; set; }
        public string RGP { get; set; }
        public string ValidationMessage { get; set; }
    }

    #endregion
}
