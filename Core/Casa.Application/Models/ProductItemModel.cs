using CLN.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ProductItemModel
    {
    }

    #region Product Category

    public class ProductCategory_Request : BaseEntity
    {
        public string? ProductCategory { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ProductCategory_Response : BaseResponseEntity
    {
        public string? ProductCategory { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Sub Category

    public class SubCategory_Request : BaseEntity
    {
        public string? SubCategory { get; set; }

        public bool? IsActive { get; set; }
    }

    public class SubCategory_Response : BaseResponseEntity
    {
        public string? SubCategory { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Segment

    public class Segment_Request : BaseEntity
    {
        public string? Segment { get; set; }

        public bool? IsActive { get; set; }
    }

    public class Segment_Response : BaseResponseEntity
    {
        public string? Segment { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Sub Segment

    public class SubSegment_Request : BaseEntity
    {
        public string? SubSegment { get; set; }

        public bool? IsActive { get; set; }
    }

    public class SubSegment_Response : BaseResponseEntity
    {
        public string? SubSegment { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Cell Chemistry
    public class CellChemistry_Request : BaseEntity
    {
        public string? CellChemistry { get; set; }

        public bool? IsActive { get; set; }
    }

    public class CellChemistry_Response : BaseResponseEntity
    {
        public string? CellChemistry { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Product Speces
    public class ProductSpeces_Request : BaseEntity
    {
        public string? ProductSpeces { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ProductSpeces_Response : BaseResponseEntity
    {
        public string? ProductSpeces { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Item Category
    public class ItemCategory_Request : BaseEntity
    {
        public string? ItemCategory { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ItemCategory_Response : BaseResponseEntity
    {
        public string? ItemCategory { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Item Description
    public class ItemDescription_Request : BaseEntity
    {
        public string? ItemDescription { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ItemDescription_Response : BaseResponseEntity
    {
        public string? ItemDescription { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Product Type
    public class ProductType_Request : BaseEntity
    {
        public string? ProductType { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ProductType_Response : BaseResponseEntity
    {
        public string? ProductType { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Product Make
    public class ProductMake_Request : BaseEntity
    {
        public string? ProductMake { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ProductMake_Response : BaseResponseEntity
    {
        public string? ProductMake { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Product Model
    public class ProductModel_Request : BaseEntity
    {
        public string? ProductModel { get; set; }

        public bool? IsActive { get; set; }
    }

    public class ProductModel_Response : BaseResponseEntity
    {
        public string? ProductModel { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region BMS Make
    public class BMSMake_Request : BaseEntity
    {
        public string? BMSMake { get; set; }

        public bool? IsActive { get; set; }
    }

    public class BMSMake_Response : BaseResponseEntity
    {
        public string? BMSMake { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Type of BMS
    public class TypeOfBMS_Request : BaseEntity
    {
        public string? TypeOfBMS { get; set; }

        public bool? IsActive { get; set; }
    }

    public class TypeOfBMS_Response : BaseResponseEntity
    {
        public string? TypeOfBMS { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion

    #region Battery Physical Status
    public class BatteryPhysicalStatus_Request : BaseEntity
    {
        public string? BatteryPhysicalStatus { get; set; }

        public bool? IsActive { get; set; }
    }

    public class BatteryPhysicalStatus_Response : BaseResponseEntity
    {
        public string? BatteryPhysicalStatus { get; set; }

        public bool? IsActive { get; set; }
    }

    #endregion
}
