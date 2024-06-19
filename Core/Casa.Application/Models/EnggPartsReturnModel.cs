using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class EnggPartsReturnModel
    {
    }

    public class EnggPartsReturn_Search : BaseSearchEntity
    {
        [DefaultValue("Engg")]
        public string? RequestType { get; set; }
        public int? EngineerId { get; set; }
        public int? StatusId { get; set; }
    }

    public class EnggPartsReturn_RequestWeb : BaseEntity
    {
        public int? EngineerId { get; set; }
        public string? RequestNumber { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? ReturnQuantity { get; set; }
        public int? StatusId { get; set; }
    }

    public class EnggPartsReturn_ResponseWeb : BaseResponseEntity
    {
        public int? EngineerId { get; set; }
        public string EngineerName { get; set; }
        public string? RequestNumber { get; set; }
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }

        public int? AvailableQty { get; set; }
        public int? ReturnQuantity { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
    }


    public class EnggPartsReturn_RequestMobile : BaseEntity
    {
        public EnggPartsReturn_RequestMobile()
        {
            SpareDetailsList = new List<EnggSpareDetailsList_RequestMobile>();
        }

        public int? EngineerId { get; set; }
        public string? RequestNumber { get; set; }

        public List<EnggSpareDetailsList_RequestMobile> SpareDetailsList { get; set; }
    }

    public class EnggSpareDetailsList_RequestMobile : BaseEntity
    {
        public int? SpareDetailsId { get; set; }
        public int? ReturnQuantity { get; set; }
        public int? StatusId { get; set; }
    }



    public class EnggPartsReturn_For_Mobile_RequestIdList : BaseResponseEntity
    {
        public int? Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public string RequestType { get; set; }
        public int? Engineerid { get; set; }
        public string RequestNumber { get; set; }
        public int Total_RequiredQty { get; set; }
        public int Total_AllocatedQty { get; set; }
        public int Total_ReceivedQty { get; set; }
        public int? Total_AvailableQty { get; set; }
        public int? Total_ReturnQuantity { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
    }




    public class EnggSpareDetailsListByRequestNumber_ResponseMobile : BaseEntity
    {
        public int? Id { get; set; }
        public int? Engineerid { get; set; }
        public string? RequestNumber { get; set; }

        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }

        public int? Total_RequiredQty { get; set; }
        public int? Total_AllocatedQty { get; set; }
        public int? Total_ReceivedQty { get; set; }
        public int? Total_AvailableQty { get; set; }
        public int? Total_ReturnQuantity { get; set; }
    }

    public class EnggPartsReturnByRequestNumber_For_Mobile
    {
        public EnggPartsReturnByRequestNumber_For_Mobile()
        {
            SpareDetailsList = new List<EnggSpareDetailsList_ResponseMobile>();
        }

        public int? Id { get; set; }
        public int? Engineerid { get; set; }
        public string? RequestNumber { get; set; }

        public int? Total_RequiredQty { get; set; }
        public int? Total_AllocatedQty { get; set; }
        public int? Total_ReceivedQty { get; set; }
        public int? Total_AvailableQty { get; set; }
        public int? Total_ReturnQuantity { get; set; }

        public List<EnggSpareDetailsList_ResponseMobile> SpareDetailsList { get; set; }
    }

    public class EnggSpareDetailsList_ResponseMobile : BaseEntity
    {
        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }

        public int? RequiredQty { get; set; }
        public int? AllocatedQty { get; set; }
        public int? ReceivedQty { get; set; }
        public int? AvailableQty { get; set; }
        public int? ReturnQuantity { get; set; }
    }


    public class EnggPendingPartsReturn_Search : BaseSearchEntity
    {
        [DefaultValue("Engg")]
        public string? RequestType { get; set; }
        public string? RequestNumber { get; set; }
    }

    public class EnggPendingSpareDetailsList_ResponseWeb : BaseEntity
    {
        public int? Id { get; set; }
        public int? Engineerid { get; set; }
        public string? RequestNumber { get; set; }

        public int? SpareDetailsId { get; set; }
        public string? UniqueCode { get; set; }
        public string? SpareDesc { get; set; }
        public int? UOMId { get; set; }
        public string UOMName { get; set; }
        public int? TypeOfBMSId { get; set; }
        public string TypeOfBMS { get; set; }

        public int? RequiredQty { get; set; }
        public int? AllocatedQty { get; set; }
        public int? ReceivedQty { get; set; }
        public int? AvailableQty { get; set; }
        public int? ReturnQuantity { get; set; }
    }


    public class EnggPartsReturn_ApprovedRequest
    {
        public EnggPartsReturn_ApprovedRequest()
        {
            PartList = new List<EnggPartsReturn_ApprovedPartList>();
        }

        public int? EngineerId { get; set; }
        public string? RequestNumber { get; set; }

        public List<EnggPartsReturn_ApprovedPartList> PartList { get; set; }
    }

    public class EnggPartsReturn_ApprovedPartList
    {
        public int? Id { get; set; }
        public int? SpareDetailsId { get; set; }
        public int? StatusId { get; set; }
    }

    // RGP Tracker
    public class EnggPartReturnPendingList_Response : BaseResponseEntity
    {
        public string? RequestNumber { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
    }

}
