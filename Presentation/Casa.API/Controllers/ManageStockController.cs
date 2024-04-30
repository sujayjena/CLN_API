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
    public class ManageStockController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageStockRepository _manageStockRepository;

        public ManageStockController(IManageStockRepository manageStockRepository)
        {
            _manageStockRepository = manageStockRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Generate Part Request
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveGeneratePartRequest(GeneratePartRequest_Request parameters)
        {
            int result = await _manageStockRepository.SaveGeneratePartRequest(parameters);

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
        public async Task<ResponseModel> GetGeneratePartRequestList(GeneratePartRequestSearch_Request parameters)
        {
            var objList = await _manageStockRepository.GetGeneratePartRequestList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetGeneratePartRequestById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetGeneratePartRequestById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion

        #region Generate Challan
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveGenerateChallan(GenerateChallan_Request parameters)
        {
            int result = await _manageStockRepository.SaveGenerateChallan(parameters);

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
                // Save Generate Challan Part Details
                foreach (var item in parameters.GenerateChallanPartDetailList)
                {
                    var vGenerateChallanPartDetails_Request = new GenerateChallanPartDetails_Request()
                    {
                        Id = item.Id,
                        GenerateChallanId = result,
                        SpareDetailsId = item.SpareDetailsId,
                        OrderQty = item.OrderQty,
                    };

                    int result_GenerateChallanPartDetails = await _manageStockRepository.SaveGenerateChallanPartDetails(vGenerateChallanPartDetails_Request);
                }
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetGenerateChallanList(GenerateChallanSearch_Request parameters)
        {
            var objList = await _manageStockRepository.GetGenerateChallanList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetGenerateChallanById(int Id)
        {
            var vGenerateChallanPartDetailsById_Response = new GenerateChallanPartDetailsById_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetGenerateChallanById(Id);
                if (vResultObj != null)
                {
                    vGenerateChallanPartDetailsById_Response.Id = vResultObj.Id;
                    vGenerateChallanPartDetailsById_Response.RequestId = vResultObj.RequestId;
                    vGenerateChallanPartDetailsById_Response.CompanyId = vResultObj.CompanyId;
                    vGenerateChallanPartDetailsById_Response.CompanyName = vResultObj.CompanyName;
                    vGenerateChallanPartDetailsById_Response.BranchId = vResultObj.BranchId;
                    vGenerateChallanPartDetailsById_Response.BranchName = vResultObj.BranchName;
                    vGenerateChallanPartDetailsById_Response.CreatorName = vResultObj.CreatorName;
                    vGenerateChallanPartDetailsById_Response.CreatedBy = vResultObj.CreatedBy;
                    vGenerateChallanPartDetailsById_Response.CreatedDate = vResultObj.CreatedDate;
                    vGenerateChallanPartDetailsById_Response.ModifierName = vResultObj.ModifierName;
                    vGenerateChallanPartDetailsById_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vGenerateChallanPartDetailsById_Response.ModifiedDate = vResultObj.ModifiedDate;

                    // Accessory
                    var vSearchObj = new GenerateChallanPartDetailsSearch_Request()
                    {
                        GenerateChallanId = vResultObj.Id,
                    };

                    var objOrderDetailsList = await _manageStockRepository.GetGenerateChallanPartDetailsList(vSearchObj);
                    foreach (var item in objOrderDetailsList)
                    {
                        vGenerateChallanPartDetailsById_Response.GenerateChallanPartDetailList.Add(item);
                    }
                }

                _response.Data = vGenerateChallanPartDetailsById_Response;
            }
            return _response;
        }

        #endregion

        #region Stock In

        [Route("[action]")]
        [HttpPost]

        public async Task<ResponseModel> GetRequestIdListForSelectList(RequestIdListParameters parameters)
        {
            IEnumerable<SelectListResponse> lstResponse = await _manageStockRepository.GetRequestIdListForSelectList(parameters);
            _response.Data = lstResponse.ToList();
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveStockIn(StockIn_Request parameters)
        {
            int result = await _manageStockRepository.SaveStockIn(parameters);

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
        public async Task<ResponseModel> GetStockInList(StockInListSearch_Request parameters)
        {
            var objList = await _manageStockRepository.GetStockInList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockInById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetStockInById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion

        #region Stock Allocation
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocationList(BaseSearchEntity parameters)
        {
            var objList = await _manageStockRepository.GetStockAllocationList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveStockAllocatedEngg(StockAllocatedEngg_Request parameters)
        {
            int result = await _manageStockRepository.SaveStockAllocatedEngg(parameters);

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
                // Save/Update Stock Allocated Engg. Part Details
                foreach (var item in parameters.StockAllocatedEnggPartDetailList)
                {
                    var vStockAllocatedEnggPartDetails_Request = new StockAllocatedEnggPartDetails_Request()
                    {
                        Id = item.Id,
                        StockAllocatedEnggId = result,
                        SpareDetailsId = item.SpareDetailsId,
                        AvailableQty = item.AvailableQty,
                        OrderQty = item.OrderQty,
                        AllocatedQty = item.AllocatedQty,
                    };

                    int result_StockAllocatedEnggPartDetails = await _manageStockRepository.SaveStockAllocatedEnggPartDetails(vStockAllocatedEnggPartDetails_Request);
                }
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedEnggList(StockAllocatedEnggSearch_Request parameters)
        {
            var objList = await _manageStockRepository.GetStockAllocatedEnggList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedEnggById(int Id)
        {
            var vStockAllocatedEnggPartDetailsById_Response = new StockAllocatedEnggPartDetailsById_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetStockAllocatedEnggById(Id);
                if (vResultObj != null)
                {
                    vStockAllocatedEnggPartDetailsById_Response.Id = vResultObj.Id;
                    vStockAllocatedEnggPartDetailsById_Response.OrderNumber = vResultObj.OrderNumber;
                    vStockAllocatedEnggPartDetailsById_Response.EngineerId = vResultObj.EngineerId;
                    vStockAllocatedEnggPartDetailsById_Response.EngineerName = vResultObj.EngineerName;
                    vStockAllocatedEnggPartDetailsById_Response.CompanyId = vResultObj.CompanyId;
                    vStockAllocatedEnggPartDetailsById_Response.CompanyName = vResultObj.CompanyName;
                    vStockAllocatedEnggPartDetailsById_Response.BranchId = vResultObj.BranchId;
                    vStockAllocatedEnggPartDetailsById_Response.BranchName = vResultObj.BranchName;
                    vStockAllocatedEnggPartDetailsById_Response.CreatorName = vResultObj.CreatorName;
                    vStockAllocatedEnggPartDetailsById_Response.CreatedBy = vResultObj.CreatedBy;
                    vStockAllocatedEnggPartDetailsById_Response.CreatedDate = vResultObj.CreatedDate;
                    vStockAllocatedEnggPartDetailsById_Response.ModifierName = vResultObj.ModifierName;
                    vStockAllocatedEnggPartDetailsById_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vStockAllocatedEnggPartDetailsById_Response.ModifiedDate = vResultObj.ModifiedDate;

                    // Accessory
                    var vSearchObj = new StockAllocatedEnggPartDetailsSearch_Request()
                    {
                        StockAllocatedEnggId = vResultObj.Id,
                    };

                    var objOrderDetailsList = await _manageStockRepository.GetStockAllocatedEnggPartDetailsList(vSearchObj);
                    foreach (var item in objOrderDetailsList)
                    {
                        vStockAllocatedEnggPartDetailsById_Response.StockAllocatedEnggPartDetailList.Add(item);
                    }
                }

                _response.Data = vStockAllocatedEnggPartDetailsById_Response;
            }
            return _response;
        }

        #endregion

        #region Stock Master
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockMasterBySpareDetailsId(int SpareDetailsId)
        {
            if (SpareDetailsId <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetStockMasterBySpareDetailsId(SpareDetailsId);

                _response.Data = vResultObj;
            }
            return _response;
        }
        #endregion
    }
}