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
        public async Task<ResponseModel> SaveEnggPartRequestOrder(EnggPartRequestOrder_Request parameters)
        {
            int result = await _partRequestOrderRepository.SaveEnggPartRequestOrder(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
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
                foreach (var item in parameters.EnggPartRequestOrderDetailList)
                {
                    var vEnggPartRequestOrderDetails_Request = new EnggPartRequestOrderDetails_Request()
                    {
                        Id = item.Id,
                        OrderId = result,
                        SpareDetailsId = item.SpareDetailsId,
                        TypeOfBMSId = item.TypeOfBMSId,
                        AvailableQty = item.AvailableQty,
                        OrderQty = item.OrderQty,
                        Remarks = item.Remarks,
                        //StatusId = item.StatusId,
                    };

                    int result_EnggPartRequestOrderDetails = await _partRequestOrderRepository.SaveEnggPartRequestOrderDetails(vEnggPartRequestOrderDetails_Request);
                }
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestOrderList(EnggPartRequestOrderSearch_Request parameters)
        {
            var objList = await _partRequestOrderRepository.GetEnggPartRequestOrderList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestOrderById(int Id)
        {
            var vEnggPartRequestOrderDetailsById_Response = new EnggPartRequestOrderDetailsById_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _partRequestOrderRepository.GetEnggPartRequestOrderById(Id);
                if (vResultObj != null)
                {
                    vEnggPartRequestOrderDetailsById_Response.Id = vResultObj.Id;
                    vEnggPartRequestOrderDetailsById_Response.OrderNumber = vResultObj.OrderNumber;
                    vEnggPartRequestOrderDetailsById_Response.OrderDate = vResultObj.OrderDate;
                    vEnggPartRequestOrderDetailsById_Response.EngineerId = vResultObj.EngineerId;
                    vEnggPartRequestOrderDetailsById_Response.EngineerName = vResultObj.EngineerName;
                    vEnggPartRequestOrderDetailsById_Response.Remarks = vResultObj.Remarks;
                    vEnggPartRequestOrderDetailsById_Response.StatusId = vResultObj.StatusId;
                    vEnggPartRequestOrderDetailsById_Response.StatusName = vResultObj.StatusName;
                    vEnggPartRequestOrderDetailsById_Response.CompanyId = vResultObj.CompanyId;
                    vEnggPartRequestOrderDetailsById_Response.CompanyName = vResultObj.CompanyName;
                    vEnggPartRequestOrderDetailsById_Response.BranchId = vResultObj.BranchId;
                    vEnggPartRequestOrderDetailsById_Response.BranchName = vResultObj.BranchName;
                    vEnggPartRequestOrderDetailsById_Response.CreatorName = vResultObj.CreatorName;
                    vEnggPartRequestOrderDetailsById_Response.CreatedBy = vResultObj.CreatedBy;
                    vEnggPartRequestOrderDetailsById_Response.CreatedDate = vResultObj.CreatedDate;
                    vEnggPartRequestOrderDetailsById_Response.ModifierName = vResultObj.ModifierName;
                    vEnggPartRequestOrderDetailsById_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vEnggPartRequestOrderDetailsById_Response.ModifiedDate = vResultObj.ModifiedDate;

                    // Accessory
                    var vSearchObj = new EnggPartRequestOrderDetailsSearch_Request()
                    {
                        OrderId = vResultObj.Id,
                    };

                    var objOrderDetailsList = await _partRequestOrderRepository.GetEnggPartRequestOrderDetailsList(vSearchObj);
                    foreach (var item in objOrderDetailsList)
                    {
                        vEnggPartRequestOrderDetailsById_Response.EnggPartRequestOrderDetailList.Add(item);
                    }
                }

                _response.Data = vEnggPartRequestOrderDetailsById_Response;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEnggPartRequestOrderDetails(EnggPartRequestOrderDetails_Request parameters)
        {
            int result = await _partRequestOrderRepository.SaveEnggPartRequestOrderDetails(parameters);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.ReocrdExists)
            {
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record saved sucessfully";
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestOrderDetailsList(EnggPartRequestOrderDetailsSearch_Request parameters)
        {
            var objList = await _partRequestOrderRepository.GetEnggPartRequestOrderDetailsList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestOrderDetailsById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _partRequestOrderRepository.GetEnggPartRequestOrderDetailsById(Id);
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
                _response.Message = "Record is already exists";
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
                        CategoryId = item.CategoryId,
                        SpareId = item.SpareId,
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

                    vTRCPartRequest_Response.CompanyId = vResultObj.CompanyId;
                    vTRCPartRequest_Response.CompanyName = vResultObj.CompanyName;
                    vTRCPartRequest_Response.BranchId = vResultObj.BranchId;
                    vTRCPartRequest_Response.BranchName = vResultObj.BranchName;

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
                            RequestId = item.Id,
                            CategoryId = item.CategoryId,
                            CategoryName = item.CategoryName,
                            SpareId = item.SpareId,
                            SpareDesc = item.SpareDesc,
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
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record saved sucessfully";
            }

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
            var vTRCPartRequest_Response = new TRCPartRequest_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _partRequestOrderRepository.GetTRCPartRequestDetailById(Id);

                _response.Data = vTRCPartRequest_Response;
            }
            return _response;
        }

        #endregion

    }
}
