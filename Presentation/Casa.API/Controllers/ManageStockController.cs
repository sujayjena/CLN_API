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

        #region Stock Allocate To Engineer / TRC

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveStockAllocated(StockAllocated_Request parameters)
        {
            int result = await _manageStockRepository.SaveStockAllocated(parameters);

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
                // Save/Update Stock Allocated . Part Details
                foreach (var item in parameters.PartList)
                {
                    var vStockAllocatedPartDetails_Request = new StockAllocatedPartDetails_Request()
                    {
                        Id = item.Id,
                        StockAllocatedId = result,
                        SpareId = item.SpareId,
                        AvailableQty = item.AvailableQty,
                        RequiredQty = item.RequiredQty,
                        AllocatedQty = item.AllocatedQty,
                    };

                    int result_StockAllocatedPartDetails = await _manageStockRepository.SaveStockAllocatedPartDetails(vStockAllocatedPartDetails_Request);
                }
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedList(StockAllocated_Search parameters)
        {
            var objList = await _manageStockRepository.GetStockAllocatedList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedById(int Id)
        {
            var vStockAllocatedDetails_Response = new StockAllocatedDetails_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetStockAllocatedById(Id);
                if (vResultObj != null)
                {
                    vStockAllocatedDetails_Response.Id = vResultObj.Id;
                    vStockAllocatedDetails_Response.RequestId = vResultObj.RequestId;
                    vStockAllocatedDetails_Response.RequestNumber = vResultObj.RequestNumber;
                    vStockAllocatedDetails_Response.EngineerId = vResultObj.EngineerId;
                    vStockAllocatedDetails_Response.EngineerName = vResultObj.EngineerName;
                    vStockAllocatedDetails_Response.AllocatedType = vResultObj.AllocatedType;
                    vStockAllocatedDetails_Response.StatusId = vResultObj.StatusId;
                    vStockAllocatedDetails_Response.StatusName = vResultObj.StatusName;
                    vStockAllocatedDetails_Response.IsActive = vResultObj.IsActive;

                    vStockAllocatedDetails_Response.CreatorName = vResultObj.CreatorName;
                    vStockAllocatedDetails_Response.CreatedBy = vResultObj.CreatedBy;
                    vStockAllocatedDetails_Response.CreatedDate = vResultObj.CreatedDate;
                    vStockAllocatedDetails_Response.ModifierName = vResultObj.ModifierName;
                    vStockAllocatedDetails_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vStockAllocatedDetails_Response.ModifiedDate = vResultObj.ModifiedDate;

                    // Accessory
                    var vSearchObj = new StockAllocatedPartDetails_Search()
                    {
                        StockAllocatedId = vResultObj.Id,
                    };

                    var objOrderDetailsList = await _manageStockRepository.GetStockAllocatedPartDetailsList(vSearchObj);
                    foreach (var item in objOrderDetailsList)
                    {
                        var vStockAllocatedPartDetails_Response = new StockAllocatedPartDetails_Response()
                        {
                            Id = item.Id,

                            StockAllocatedId = item.Id,
                            SpareId = item.SpareId,
                            AvailableQty = item.AvailableQty,
                            RequiredQty = item.RequiredQty,
                            AllocatedQty = item.AllocatedQty
                        };

                        vStockAllocatedDetails_Response.PartList.Add(vStockAllocatedPartDetails_Response);
                    }
                }

                _response.Data = vStockAllocatedDetails_Response;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveStockAllocatedPartDetails(StockAllocatedPartDetails_Request parameters)
        {
            int result = await _manageStockRepository.SaveStockAllocatedPartDetails(parameters);

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
        public async Task<ResponseModel> GetStockAllocatedPartDetailsList(StockAllocatedPartDetails_Search parameters)
        {
            var objList = await _manageStockRepository.GetStockAllocatedPartDetailsList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedPartDetailsById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetStockAllocatedById(Id);

                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion


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