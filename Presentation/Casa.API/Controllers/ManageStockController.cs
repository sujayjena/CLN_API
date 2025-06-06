﻿using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Globalization;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageStockController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageStockRepository _manageStockRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IPartRequestOrderRepository _partRequestOrderRepository;

        public ManageStockController(IManageStockRepository manageStockRepository, INotificationRepository notificationRepository, IPartRequestOrderRepository partRequestOrderRepository)
        {
            _manageStockRepository = manageStockRepository;
            _notificationRepository = notificationRepository;
            _partRequestOrderRepository = partRequestOrderRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Generate Part Request
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveGeneratePartRequest(GeneratePartRequest parameters)
        {
            int result = 0;
            foreach (var item in parameters.generatePartList)
            {
                var vGeneratePartRequest_Request = new GeneratePartRequest_Request()
                {
                    Id = item.Id,
                    SpareCategoryId = item.SpareCategoryId,
                    ProductMakeId = item.ProductMakeId,
                    BMSMakeId = item.BMSMakeId,
                    SpareDetailsId = item.SpareDetailsId,
                    UOMId = item.UOMId,
                    TypeOfBMSId = item.TypeOfBMSId,
                    AvailableQty = item.AvailableQty,
                    RequiredQty = item.RequiredQty,
                    RequestedQty = item.RequestedQty,
                    Remarks = item.Remarks,
                };

                result = await _manageStockRepository.SaveGeneratePartRequest(vGeneratePartRequest_Request);
            }

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
                _response.Message = "Record saved successfully";
            }

            _response.Id = result;
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
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record saved successfully";
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
                        UOMId = item.UOMId,
                        TypeOfBMSId = item.TypeOfBMSId,
                        SpareDetailsId = item.SpareDetailsId,
                        AvailableQty = item.AvailableQty,
                        RequiredQty = item.RequiredQty,
                        RequestedQty = item.RequestedQty,
                        Remarks = item.Remarks
                    };

                    int result_GenerateChallanPartDetails = await _manageStockRepository.SaveGenerateChallanPartDetails(vGenerateChallanPartDetails_Request);
                }
            }

            _response.Id = result;
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportGenerateChallan(GenerateChallanSearch_Request parameters)
        {
            _response.IsSuccess = false;
            byte[] result;

            var objList = await _manageStockRepository.GetGenerateChallanList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("GenerateChallan");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Sr.No.";
                    WorkSheet1.Cells[1, 2].Value = "Challan Id";
                    WorkSheet1.Cells[1, 3].Value = "Spare Category";
                    WorkSheet1.Cells[1, 4].Value = "Product Make";
                    WorkSheet1.Cells[1, 5].Value = "Pare Part Code";
                    WorkSheet1.Cells[1, 6].Value = "Spare Part Description";
                    WorkSheet1.Cells[1, 7].Value = "UOM";
                    WorkSheet1.Cells[1, 8].Value = "Order Qty";

                    recordIndex = 2;

                    int i = 1;
                    foreach (var itemsReqList in objList)
                    {
                        //Generate challan part
                        var vSearchObj = new GenerateChallanPartDetailsSearch_Request()
                        {
                            GenerateChallanId = itemsReqList.Id,
                        };

                        var objReqDetailsList = await _manageStockRepository.GetGenerateChallanPartDetailsList(vSearchObj);
                        if (objReqDetailsList.Count() > 0)
                        {
                            foreach (var itemReqDetails in objReqDetailsList)
                            {
                                WorkSheet1.Cells[recordIndex, 1].Value = i.ToString();
                                WorkSheet1.Cells[recordIndex, 2].Value = itemsReqList.RequestId;
                                WorkSheet1.Cells[recordIndex, 3].Value = itemReqDetails.SpareCategory;
                                WorkSheet1.Cells[recordIndex, 4].Value = itemReqDetails.ProductMake;
                                WorkSheet1.Cells[recordIndex, 5].Value = itemReqDetails.UniqueCode;
                                WorkSheet1.Cells[recordIndex, 6].Value = itemReqDetails.SpareDesc;
                                WorkSheet1.Cells[recordIndex, 7].Value = itemReqDetails.UOMName;
                                WorkSheet1.Cells[recordIndex, 8].Value = itemReqDetails.RequiredQty;

                                recordIndex += 1;

                                i++;
                            }
                        }
                        else
                        {
                            recordIndex += 1;
                        }

                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }


            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
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
                _response.Message = "Record already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record saved successfully";
            }

            _response.Id = result;
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

        #endregion

        #region Stock Allocation > Stock Allocate To Engineer / TRC

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveStockAllocated_Engineer_N_TRC(StockAllocated_Request parameters)
        {
            //check if requestId = 0 then create new order for this engineer
            if (parameters.RequestId == 0)
            {
                var vEnggPartRequest = new EnggPartRequest_Request()
                {
                    Id = 0,
                    RequestDate = DateTime.Now,
                    EngineerId = parameters.EngineerId,
                    Remarks = "",
                    StatusId = 1,
                    IsActive = true
                };

                var vresult = await _partRequestOrderRepository.SaveEnggPartRequest(vEnggPartRequest);

                parameters.RequestId = vresult;

                if (vresult > 0)
                {
                    foreach (var items in parameters.PartList)
                    {
                        var vEnggPartRequestDetails = new EnggPartRequestDetails_Request()
                        {
                            Id = 0,
                            RequestId = vresult,
                            SpareCategoryId = items.SpareCategoryId,
                            ProductMakeId = items.ProductMakeId,
                            BMSMakeId = items.BMSMakeId,
                            SpareDetailsId = items.SpareId,
                            UOMId = 0,
                            TypeOfBMSId = 0,
                            AvailableQty = items.AvailableQty,
                            RequiredQty = items.RequiredQty,
                            Remarks = "",
                            RGP = items.RGP
                        };

                        int result_EnggPartRequestOrderDetails = await _partRequestOrderRepository.SaveEnggPartRequestDetail(vEnggPartRequestDetails);
                    }

                }
            }

            int result = await _manageStockRepository.SaveStockAllocated(parameters);

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
                _response.Message = "Record saved successfully";
            }

            if (result > 0)
            {
                // Save/Update Stock Allocated . Part Details
                foreach (var item in parameters.PartList)
                {
                    var vStockAllocatedPartDetails_Request = new StockAllocatedPartDetails_Request()
                    {
                        Id = item.Id,
                        EngineerId = parameters.EngineerId,
                        StockAllocatedId = result,
                        SpareCategoryId = item.SpareCategoryId,
                        ProductMakeId = item.ProductMakeId,
                        BMSMakeId = item.BMSMakeId,
                        SpareId = item.SpareId,
                        AvailableQty = item.AvailableQty,
                        RequiredQty = item.RequiredQty,
                        AllocatedQty = item.AllocatedQty,
                        ReceivedQty = item.ReceivedQty,
                        RGP = item.RGP
                    };

                    int result_StockAllocatedPartDetails = await _manageStockRepository.SaveStockAllocatedPartDetails(vStockAllocatedPartDetails_Request);
                }
            }

            //Notification
            if (result > 0)
            {
                if (parameters.AllocatedType == "Engg")
                {
                    var vStockAllocated = await _manageStockRepository.GetStockAllocatedById(result);
                    if (vStockAllocated != null)
                    {
                        string notifyMessage = String.Format(@"Part has been Allocate to you against Order Number - {0}.", vStockAllocated.RequestNumber);

                        var vNotifyObj = new Notification_Request()
                        {
                            Subject = "Part Allocated to Engineer",
                            SendTo = "Engineer",
                            //CustomerId = vWorkOrderObj.CustomerId,
                            //CustomerMessage = NotifyMessage,
                            EmployeeId = parameters.EngineerId,
                            EmployeeMessage = notifyMessage,
                            RefValue1 = vStockAllocated.RequestNumber,
                            ReadUnread = false
                        };

                        int resultNotification = await _notificationRepository.SaveNotification(vNotifyObj);
                    }
                }
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedList_Engineer_N_TRC(StockAllocated_Search parameters)
        {
            var objList = await _manageStockRepository.GetStockAllocatedList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedById_Engineer_N_TRC(int Id)
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

                    vStockAllocatedDetails_Response.RequiredQty = vResultObj.RequiredQty;
                    vStockAllocatedDetails_Response.AllocatedQty = vResultObj.AllocatedQty;
                    vStockAllocatedDetails_Response.ReceivedQty = vResultObj.ReceivedQty;

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

                            StockAllocatedId = item.StockAllocatedId,
                            SpareCategoryId = item.SpareCategoryId,
                            SpareCategory = item.SpareCategory,
                            ProductMakeId = item.ProductMakeId,
                            ProductMake = item.ProductMake,
                            BMSMakeId = item.BMSMakeId,
                            BMSMake = item.BMSMake,
                            SpareId = item.SpareId,
                            UniqueCode = item.UniqueCode,
                            SpareDesc = item.SpareDesc,

                            AvailableQty = item.AvailableQty,
                            RequiredQty = item.RequiredQty,
                            AllocatedQty = item.AllocatedQty,
                            ReceivedQty = item.ReceivedQty,
                            RGP = item.RGP,
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
        public async Task<ResponseModel> SaveStockAllocatedPartDetails_Engineer_N_TRC(StockAllocatedPartDetails_Request parameters)
        {
            int result = await _manageStockRepository.SaveStockAllocatedPartDetails(parameters);

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
                _response.Message = "Record saved successfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedPartDetailsList_Engineer_N_TRC(StockAllocatedPartDetails_Search parameters)
        {
            var objList = await _manageStockRepository.GetStockAllocatedPartDetailsList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockAllocatedPartDetailsById_Engineer_N_TRC(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetStockAllocatedPartDetailsById(Id);

                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Engineer Stock Master

        //[Route("[action]")]
        //[HttpPost]
        //public async Task<ResponseModel> GetAllocatedPartListToEngineer_PartReceived_Engineer_N_TRC(StockAllocated_Search parameters)
        //{
        //    var objList = await _manageStockRepository.GetStockAllocatedList(parameters);
        //    _response.Data = objList.ToList();
        //    _response.Total = parameters.Total;
        //    return _response;
        //}

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggStockMasterList(EnggStockMasterListSearch_Request parameters)
        {
            var objList = await _manageStockRepository.GetEnggStockMasterList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEnggStockMasterById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetEnggStockMasterById(Id);

                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> UpdateEnggStockMaster(EnggStockMaster_Request parameters)
        {
            int result = await _manageStockRepository.UpdateEnggStockMaster(parameters);

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
                _response.Message = "Record saved successfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportEnggStockMaster(EnggStockMasterListSearch_Request parameters)
        {
            _response.IsSuccess = false;
            byte[] result;

            var objList = await _manageStockRepository.GetEnggStockMasterList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("EnggStockMaster");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Sr.No.";
                    WorkSheet1.Cells[1, 2].Value = "Engineer Name";
                    WorkSheet1.Cells[1, 3].Value = "Spare Category";
                    WorkSheet1.Cells[1, 4].Value = "Product Make";
                    WorkSheet1.Cells[1, 5].Value = "Pare Part Code";
                    WorkSheet1.Cells[1, 6].Value = "Spare Description";
                    WorkSheet1.Cells[1, 7].Value = "UOM";
                    WorkSheet1.Cells[1, 8].Value = "Min Qty";
                    WorkSheet1.Cells[1, 9].Value = "Engg.Available Stock";
                    WorkSheet1.Cells[1, 10].Value = "RGP/NRGP";
                    WorkSheet1.Cells[1, 11].Value = "Created By";
                    WorkSheet1.Cells[1, 12].Value = "Created Date";

                    recordIndex = 2;

                    int i = 1;
                    foreach (var itemsReqList in objList)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = i.ToString();
                        WorkSheet1.Cells[recordIndex, 2].Value = itemsReqList.EngineerName;
                        WorkSheet1.Cells[recordIndex, 3].Value = itemsReqList.SpareCategory;
                        WorkSheet1.Cells[recordIndex, 4].Value = itemsReqList.ProductMake;
                        WorkSheet1.Cells[recordIndex, 5].Value = itemsReqList.UniqueCode;
                        WorkSheet1.Cells[recordIndex, 6].Value = itemsReqList.SpareDesc;
                        WorkSheet1.Cells[recordIndex, 7].Value = itemsReqList.UOMName;
                        WorkSheet1.Cells[recordIndex, 8].Value = itemsReqList.MinQty;
                        WorkSheet1.Cells[recordIndex, 9].Value = itemsReqList.AvailableQty;
                        WorkSheet1.Cells[recordIndex, 10].Value = itemsReqList.RGP == true ? "RGP" : "NRGP";
                        WorkSheet1.Cells[recordIndex, 11].Value = itemsReqList.CreatorName;
                        WorkSheet1.Cells[recordIndex, 12].Value = itemsReqList.CreatedDate.HasValue ? itemsReqList.CreatedDate.Value.ToString("dd/MM/yyyy") : string.Empty;

                        recordIndex += 1;

                        i++;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }


            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion

        #region Engineer Part Return

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetRequestIdListForPartReturnRequest(EnggPartsReturn_Search parameters)
        {
            var objList = await _manageStockRepository.GetRequestIdListForPartReturnRequest(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEngineerPartRequestDetailByRequestNumber(EnggPartsReturn_Search parameters)
        {
            var vEnggPartsReturnByRequestNumber_For_MobileObj = new EnggPartsReturnByRequestNumber_For_Mobile();


            if (string.IsNullOrWhiteSpace(parameters.SearchText))
            {
                _response.Message = "RequestNumber is required";
            }
            else
            {
                var objRequestIdList = _manageStockRepository.GetRequestIdListForPartReturnRequest(parameters).Result.FirstOrDefault();
                if (objRequestIdList != null)
                {
                    vEnggPartsReturnByRequestNumber_For_MobileObj.Id = objRequestIdList.Id;
                    vEnggPartsReturnByRequestNumber_For_MobileObj.Engineerid = objRequestIdList.Engineerid;
                    vEnggPartsReturnByRequestNumber_For_MobileObj.RequestNumber = parameters.SearchText;
                    vEnggPartsReturnByRequestNumber_For_MobileObj.Total_RequiredQty = objRequestIdList.Total_RequiredQty;
                    vEnggPartsReturnByRequestNumber_For_MobileObj.Total_AllocatedQty = objRequestIdList.Total_AllocatedQty;
                    vEnggPartsReturnByRequestNumber_For_MobileObj.Total_ReceivedQty = objRequestIdList.Total_ReceivedQty;
                    vEnggPartsReturnByRequestNumber_For_MobileObj.Total_AvailableQty = objRequestIdList.Total_AvailableQty;
                    vEnggPartsReturnByRequestNumber_For_MobileObj.Total_ReturnQuantity = objRequestIdList.Total_ReturnQuantity;

                    var vResultPartRequestDetailListObj = _manageStockRepository.GetEngineerPartRequestDetailByRequestNumber(parameters);

                    foreach (var item in vResultPartRequestDetailListObj.Result)
                    {
                        var vEnggSpareDetailsListByRequestNumber_RequestMobileobj = new EnggSpareDetailsList_ResponseMobile()
                        {
                            Id = Convert.ToInt32(item.Id),
                            SpareCategoryId = Convert.ToInt32(item.SpareCategoryId),
                            SpareCategory = Convert.ToString(item.SpareCategory),
                            ProductMakeId = Convert.ToInt32(item.ProductMakeId),
                            ProductMake = Convert.ToString(item.ProductMake),
                            BMSMakeId = Convert.ToInt32(item.BMSMakeId),
                            BMSMake = Convert.ToString(item.BMSMake),
                            SpareDetailsId = Convert.ToInt32(item.SpareDetailsId),
                            UniqueCode = Convert.ToString(item.UniqueCode),
                            SpareDesc = Convert.ToString(item.SpareDesc),

                            RequiredQty = Convert.ToDecimal(item.Total_RequiredQty),
                            AllocatedQty = Convert.ToDecimal(item.Total_AllocatedQty),
                            ReceivedQty = Convert.ToDecimal(item.Total_ReceivedQty),
                            AvailableQty = Convert.ToDecimal(item.Total_AvailableQty),
                            ReturnQuantity = Convert.ToDecimal(item.Total_ReturnQuantity),
                        };

                        vEnggPartsReturnByRequestNumber_For_MobileObj.SpareDetailsList.Add(vEnggSpareDetailsListByRequestNumber_RequestMobileobj);
                    }
                }

                _response.Data = vEnggPartsReturnByRequestNumber_For_MobileObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEngineerPartReturn(EnggPartsReturn_RequestWeb parameters)
        {
            int result = await _manageStockRepository.SaveEngineerPartReturn(parameters);

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
                _response.Message = "Record saved successfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveEngineerPartReturn_For_Mobile(EnggPartsReturn_RequestMobile parameters)
        {
            int result = await _manageStockRepository.SaveEngineerPartReturn_Mobile(parameters);

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
                _response.Message = "Record saved successfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEngineerPartReturnList(EnggPartsReturn_Search parameters)
        {
            var objList = await _manageStockRepository.GetEngineerPartReturnList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEngineerPartReturnById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageStockRepository.GetEngineerPartReturnById(Id);

                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ApproveOrRejectEngineerPartReturn(EnggPartsReturn_ApprovedRequest parameters)
        {
            int result = await _manageStockRepository.ApproveOrRejectEngineerPartReturn(parameters);

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
                _response.Message = "Record saved successfully";
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportEngineerPartReturn(EnggPartsReturn_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;

            var objList = await _manageStockRepository.GetEngineerPartReturnList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("EngineerPartReturn");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Sr.No.";
                    WorkSheet1.Cells[1, 2].Value = "Order Number";
                    WorkSheet1.Cells[1, 3].Value = "Engineer Name";
                    WorkSheet1.Cells[1, 4].Value = "Spare Category";
                    WorkSheet1.Cells[1, 5].Value = "Product Make";
                    WorkSheet1.Cells[1, 6].Value = "Spare Part Code";
                    WorkSheet1.Cells[1, 7].Value = "Spare Part Description";
                    WorkSheet1.Cells[1, 8].Value = "Total Qty";
                    WorkSheet1.Cells[1, 9].Value = "Return Qty";
                    WorkSheet1.Cells[1, 10].Value = "Pending Qty";
                    WorkSheet1.Cells[1, 11].Value = "RGP Closed Date";
                    WorkSheet1.Cells[1, 12].Value = "Created By";
                    WorkSheet1.Cells[1, 13].Value = "Created Date";

                    recordIndex = 2;

                    int i = 1;
                    foreach (var itemsReqList in objList)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = i.ToString();
                        WorkSheet1.Cells[recordIndex, 2].Value = itemsReqList.RequestNumber;
                        WorkSheet1.Cells[recordIndex, 3].Value = itemsReqList.EngineerName;
                        WorkSheet1.Cells[recordIndex, 4].Value = itemsReqList.SpareCategory;
                        WorkSheet1.Cells[recordIndex, 5].Value = itemsReqList.ProductMake;
                        WorkSheet1.Cells[recordIndex, 6].Value = itemsReqList.UniqueCode;
                        WorkSheet1.Cells[recordIndex, 7].Value = itemsReqList.SpareDesc;
                        WorkSheet1.Cells[recordIndex, 8].Value = itemsReqList.AvailableQty;
                        WorkSheet1.Cells[recordIndex, 9].Value = itemsReqList.ReturnQuantity;

                        decimal vPending = Convert.ToDecimal(itemsReqList.AvailableQty) - Convert.ToDecimal(itemsReqList.ReturnQuantity);

                        WorkSheet1.Cells[recordIndex, 10].Value = vPending < 0 ? 0 : vPending;
                        WorkSheet1.Cells[recordIndex, 11].Value = itemsReqList.RGPClosedDate.HasValue ? itemsReqList.RGPClosedDate.Value.ToString("dd/MM/yyyy hh:mm:ss:tt") : string.Empty;
                        WorkSheet1.Cells[recordIndex, 12].Value = itemsReqList.CreatorName;
                        WorkSheet1.Cells[recordIndex, 13].Value = itemsReqList.CreatedDate.HasValue ? itemsReqList.CreatedDate.Value.ToString("dd/MM/yyyy") : string.Empty;

                        recordIndex += 1;

                        i++;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }


            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion

        #region RGP Tracker

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEngineerPartReturnPendingListByRequestNumber(EnggPendingPartsReturn_Search parameters)
        {
            var objList = await _manageStockRepository.GetEngineerPartReturnPendingList_Engineer_N_TRC(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        #endregion

        #region Stock Master

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetStockMasterList(StockMaster_Search parameters)
        {
            var objList = await _manageStockRepository.GetStockMasterList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportStockMasterData(StockMaster_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new SpareDetails_Search();
            request.SpareCategoryId = 0;
            request.BMSMakeId = 0;
            request.ProductMakeId = 0;
            request.IsRGP = null;

            IEnumerable<StockMaster_Response> lstObj = await _manageStockRepository.GetStockMasterList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("StockAvailable");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Spare Category";
                    WorkSheet1.Cells[1, 2].Value = "Product Make";
                    WorkSheet1.Cells[1, 3].Value = "BMS Make";
                    WorkSheet1.Cells[1, 4].Value = "Spare Part Code";
                    WorkSheet1.Cells[1, 5].Value = "Spare Part Description";
                    WorkSheet1.Cells[1, 6].Value = "UOM";
                    WorkSheet1.Cells[1, 7].Value = "Min Qty.";
                    WorkSheet1.Cells[1, 8].Value = "Available Qty.";
                    WorkSheet1.Cells[1, 9].Value = "RGP/NRGP";

                    WorkSheet1.Cells[1, 10].Value = "CreatedDate";
                    WorkSheet1.Cells[1, 11].Value = "CreatedBy";


                    recordIndex = 2;

                    foreach (var items in lstObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.SpareCategory;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.ProductMake;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.BMSMake;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.UniqueCode;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.SpareDesc;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.UOMName;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.MinQty;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.AvailableQty;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.RGP == true ? "RGP" : "NRGP";

                        WorkSheet1.Cells[recordIndex, 10].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.CreatorName;

                        if (items.MinQty > items.AvailableQty)
                        {
                            WorkSheet1.Row(recordIndex).Style.Fill.PatternType = ExcelFillStyle.Solid;
                            WorkSheet1.Row(recordIndex).Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Pink);
                        }

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Record list Exported successfully";
            }

            return _response;
        }

        //[Route("[action]")]
        //[HttpPost]
        //public async Task<ResponseModel> GetStockMasterBySpareDetailsId(int SpareDetailsId)
        //{
        //    if (SpareDetailsId <= 0)
        //    {
        //        _response.Message = "Id is required";
        //    }
        //    else
        //    {
        //        var vResultObj = await _manageStockRepository.GetStockMasterBySpareDetailsId(SpareDetailsId);

        //        _response.Data = vResultObj;
        //    }
        //    return _response;
        //}

        #endregion

        #region Order Received Engineer

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetOrderReceivedEngineerList(OrderReceivedEngineer_Search parameters)
        {
            var objList = await _manageStockRepository.GetOrderReceivedEngineerList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetEngineerOrderListByEngineerId(EngineerOrderListByEngineerId_Search parameters)
        {
            var objList = await _manageStockRepository.GetEngineerOrderListByEngineerId(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        #endregion
    }
}