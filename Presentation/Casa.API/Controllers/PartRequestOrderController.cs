using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartRequestOrderController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IPartRequestOrderRepository _partRequestOrderRepository;

        public PartRequestOrderController(IPartRequestOrderRepository enggPartRequestOrderRepository)
        {
            _partRequestOrderRepository = enggPartRequestOrderRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Engg Part Request

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEnggPartRequest(EnggPartRequest_Request parameters)
        {
            int result = await _partRequestOrderRepository.SaveEnggPartRequest(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record saved sucessfully";
            }

            if (result > 0)
            {
                // Save/Update Part Request Order Details
                foreach (var item in parameters.PartDetail)
                {
                    var vEnggPartRequestDetails_Response = new EnggPartRequestDetails_Request()
                    {
                        RequestId = result,
                        SpareDetailsId = item.SpareDetailsId,
                        UOMId = item.UOMId,
                        TypeOfBMSId = item.TypeOfBMSId,
                        AvailableQty = item.AvailableQty,
                        RequiredQty = item.RequiredQty,
                        Remarks = item.Remarks,
                        RGP = item.RGP
                    };

                    int result_EnggPartRequestOrderDetails = await _partRequestOrderRepository.SaveEnggPartRequestDetail(vEnggPartRequestDetails_Response);
                }
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestList(EnggPartRequest_Search parameters)
        {
            var objList = await _partRequestOrderRepository.GetEnggPartRequestList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestById(int Id)
        {
            var vEnggPartRequest_Response = new EnggPartRequest_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _partRequestOrderRepository.GetEnggPartRequestById(Id);
                if (vResultObj != null)
                {
                    vEnggPartRequest_Response.Id = vResultObj.Id;
                    vEnggPartRequest_Response.RequestNumber = vResultObj.RequestNumber;
                    vEnggPartRequest_Response.RequestDate = vResultObj.RequestDate;

                    vEnggPartRequest_Response.EngineerId = vResultObj.EngineerId;
                    vEnggPartRequest_Response.EngineerName = vResultObj.EngineerName;

                    vEnggPartRequest_Response.Remarks = vResultObj.Remarks;
                    vEnggPartRequest_Response.StatusId = vResultObj.StatusId;
                    vEnggPartRequest_Response.StatusName = vResultObj.StatusName;

                    vEnggPartRequest_Response.IsActive = vResultObj.IsActive;

                    vEnggPartRequest_Response.CreatorName = vResultObj.CreatorName;
                    vEnggPartRequest_Response.CreatedBy = vResultObj.CreatedBy;
                    vEnggPartRequest_Response.CreatedDate = vResultObj.CreatedDate;
                    vEnggPartRequest_Response.ModifierName = vResultObj.ModifierName;
                    vEnggPartRequest_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vEnggPartRequest_Response.ModifiedDate = vResultObj.ModifiedDate;

                    // Accessory
                    var vSearchObj = new EnggPartRequestDetails_Search()
                    {
                        RequestId = vResultObj.Id,
                    };

                    var objDetailsList = await _partRequestOrderRepository.GetEnggPartRequestDetailList(vSearchObj);
                    foreach (var item in objDetailsList)
                    {
                        var vEnggPartRequestDetails_Response = new EnggPartRequestDetails_Response()
                        {
                            Id= item.Id,
                            RequestId = item.RequestId,
                            RequestNumber = item.RequestNumber,
                            SpareDetailsId = item.SpareDetailsId,
                            SpareDesc = item.SpareDesc,
                            UniqueCode = item.UniqueCode,
                            UOMId = item.UOMId,
                            UOMName = item.UOMName,
                            TypeOfBMSId = item.TypeOfBMSId,
                            TypeOfBMS = item.TypeOfBMS,
                            AvailableQty = item.AvailableQty,
                            RequiredQty = item.RequiredQty,
                            Remarks = item.Remarks,
                            RGP = item.RGP
                        };

                        vEnggPartRequest_Response.PartDetail.Add(vEnggPartRequestDetails_Response);
                    }
                }

                _response.Data = vEnggPartRequest_Response;
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEnggPartRequestDetail(EnggPartRequestDetails_Request parameters)
        {
            int result = await _partRequestOrderRepository.SaveEnggPartRequestDetail(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record saved sucessfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestDetailList(EnggPartRequestDetails_Search parameters)
        {
            var objList = await _partRequestOrderRepository.GetEnggPartRequestDetailList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestDetailById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _partRequestOrderRepository.GetEnggPartRequestDetailById(Id);

                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion


        #region TRC Part Request

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTRCPartRequest(TRCPartRequest_Request parameters)
        {
            int result = await _partRequestOrderRepository.SaveTRCPartRequest(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record saved sucessfully";
            }

            if (result > 0)
            {
                // Save/Update Part Request Order Details
                foreach (var item in parameters.PartDetail)
                {
                    var vTRCPartRequestDetails_Response = new TRCPartRequestDetails_Request()
                    {
                        RequestId = result,
                        SpareDetailsId = item.SpareDetailsId,
                        UOMId = item.UOMId,
                        TypeOfBMSId = item.TypeOfBMSId,
                        AvailableQty = item.AvailableQty,
                        RequiredQty = item.RequiredQty,
                        Remarks = item.Remarks,
                        RGP = item.RGP
                    };

                    int result_EnggPartRequestOrderDetails = await _partRequestOrderRepository.SaveTRCPartRequestDetail(vTRCPartRequestDetails_Response);
                }
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTRCPartRequestList(TRCPartRequest_Search parameters)
        {
            var objList = await _partRequestOrderRepository.GetTRCPartRequestList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTRCPartRequestById(int Id)
        {
            var vTRCPartRequest_Response = new TRCPartRequest_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _partRequestOrderRepository.GetTRCPartRequestById(Id);
                if (vResultObj != null)
                {
                    vTRCPartRequest_Response.Id = vResultObj.Id;
                    vTRCPartRequest_Response.RequestNumber = vResultObj.RequestNumber;
                    vTRCPartRequest_Response.RequestDate = vResultObj.RequestDate;

                    vTRCPartRequest_Response.EngineerId = vResultObj.EngineerId;
                    vTRCPartRequest_Response.EngineerName = vResultObj.EngineerName;

                    vTRCPartRequest_Response.Remarks = vResultObj.Remarks;
                    vTRCPartRequest_Response.StatusId = vResultObj.StatusId;
                    vTRCPartRequest_Response.StatusName = vResultObj.StatusName;

                    vTRCPartRequest_Response.IsActive = vResultObj.IsActive;

                    vTRCPartRequest_Response.CreatorName = vResultObj.CreatorName;
                    vTRCPartRequest_Response.CreatedBy = vResultObj.CreatedBy;
                    vTRCPartRequest_Response.CreatedDate = vResultObj.CreatedDate;
                    vTRCPartRequest_Response.ModifierName = vResultObj.ModifierName;
                    vTRCPartRequest_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vTRCPartRequest_Response.ModifiedDate = vResultObj.ModifiedDate;

                    // Accessory
                    var vSearchObj = new TRCPartRequestDetails_Search()
                    {
                        RequestId = vResultObj.Id,
                    };

                    var objDetailsList = await _partRequestOrderRepository.GetTRCPartRequestDetailList(vSearchObj);
                    foreach (var item in objDetailsList)
                    {
                        var vTRCPartRequestDetails_Response = new TRCPartRequestDetails_Response()
                        {
                            Id=item.Id,
                            RequestId = item.RequestId,
                            RequestNumber = item.RequestNumber,
                            SpareDetailsId = item.SpareDetailsId,
                            SpareDesc = item.SpareDesc,
                            UniqueCode = item.UniqueCode,
                            UOMId = item.UOMId,
                            UOMName = item.UOMName,
                            TypeOfBMSId = item.TypeOfBMSId,
                            TypeOfBMS = item.TypeOfBMS,
                            AvailableQty = item.AvailableQty,
                            RequiredQty = item.RequiredQty,
                            Remarks = item.Remarks,
                            RGP = item.RGP
                        };

                        vTRCPartRequest_Response.PartDetail.Add(vTRCPartRequestDetails_Response);
                    }
                }

                _response.Data = vTRCPartRequest_Response;
            }
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTRCPartRequestDetail(TRCPartRequestDetails_Request parameters)
        {
            int result = await _partRequestOrderRepository.SaveTRCPartRequestDetail(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record saved sucessfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTRCPartRequestDetailList(TRCPartRequestDetails_Search parameters)
        {
            var objList = await _partRequestOrderRepository.GetTRCPartRequestDetailList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTRCPartRequestDetailById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _partRequestOrderRepository.GetTRCPartRequestDetailById(Id);

                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
