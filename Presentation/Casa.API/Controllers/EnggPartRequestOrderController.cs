using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnggPartRequestOrderController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IEnggPartRequestOrderRepository _enggPartRequestOrderRepository;

        public EnggPartRequestOrderController(IEnggPartRequestOrderRepository enggPartRequestOrderRepository)
        {
            _enggPartRequestOrderRepository = enggPartRequestOrderRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEnggPartRequestOrder(EnggPartRequestOrder_Request parameters)
        {
            int result = await _enggPartRequestOrderRepository.SaveEnggPartRequestOrder(parameters);

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

                    int result_EnggPartRequestOrderDetails = await _enggPartRequestOrderRepository.SaveEnggPartRequestOrderDetails(vEnggPartRequestOrderDetails_Request);
                }
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggPartRequestOrderList(EnggPartRequestOrderSearch_Request parameters)
        {
            var objList = await _enggPartRequestOrderRepository.GetEnggPartRequestOrderList(parameters);
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
                var vResultObj = await _enggPartRequestOrderRepository.GetEnggPartRequestOrderById(Id);
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

                    var objOrderDetailsList = await _enggPartRequestOrderRepository.GetEnggPartRequestOrderDetailsList(vSearchObj);
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
            int result = await _enggPartRequestOrderRepository.SaveEnggPartRequestOrderDetails(parameters);

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
            var objList = await _enggPartRequestOrderRepository.GetEnggPartRequestOrderDetailsList(parameters);
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
                var vResultObj = await _enggPartRequestOrderRepository.GetEnggPartRequestOrderDetailsById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
