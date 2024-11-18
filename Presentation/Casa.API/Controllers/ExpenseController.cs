using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.ComponentModel;
using System.Globalization;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;

        private readonly IExpenseRepository _expenseRepository;
        private readonly IManageTicketRepository _manageTicketRepository;

        public ExpenseController(IExpenseRepository expenseRepository, IManageTicketRepository manageTicketRepository, IFileManager fileManager)
        {
            _fileManager = fileManager;

            _expenseRepository = expenseRepository;
            _manageTicketRepository = manageTicketRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Expense

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveExpense(Expense_Request parameters)
        {
            //Save / Update
            int result = await _expenseRepository.SaveExpense(parameters);

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
                _response.Message = "Record details saved sucessfully";
            }

            //Add / Update Expense Details
            if (result > 0)
            {
                foreach (var item in parameters.ExpenseDetails)
                {
                    //Image Upload
                    if (!string.IsNullOrWhiteSpace(item.ExpenseImageFile_Base64))
                    {
                        var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(item.ExpenseImageFile_Base64, "\\Uploads\\Expense\\", item.ExpenseImageOriginalFileName);

                        if (!string.IsNullOrWhiteSpace(vUploadFile))
                        {
                            item.ExpenseImageFileName = vUploadFile;
                        }
                    }

                    var vExpenseDetails_Request = new ExpenseDetails_Request()
                    {
                        Id = item.Id,
                        ExpenseId = result,
                        IsSingleDayExpense = item.IsSingleDayExpense,
                        FromDate = item.FromDate,
                        ToDate = item.ToDate,
                        ExpenseTypeId = item.ExpenseTypeId,
                        VehicleTypeId = item.VehicleTypeId,
                        ExpenseDescription = item.ExpenseDescription,
                        ApprovedAmount = item.ApprovedAmount,
                        ExpenseAmount = item.ExpenseAmount,
                        ExpenseImageFileName = item.ExpenseImageFileName,
                        ExpenseImageOriginalFileName = item.ExpenseImageOriginalFileName,
                        ExpenseDetailStatusId = item.ExpenseDetailStatusId,
                        CityGradeId = item.CityGradeId,
                    };

                    int resultExpenseDetails = await _expenseRepository.SaveExpenseDetails(vExpenseDetails_Request);
                }
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseList(Expense_Search parameters)
        {
            var objList = await _expenseRepository.GetExpenseList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseById(int Id)
        {
            var vExpense_Response = new Expense_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _expenseRepository.GetExpenseById(Id);
                if (vResultObj != null)
                {
                    vExpense_Response.Id = vResultObj.Id;
                    vExpense_Response.IsMySelf = vResultObj.IsMySelf;
                    vExpense_Response.ExpenseNumber = vResultObj.ExpenseNumber;
                    vExpense_Response.EmployeeId = vResultObj.EmployeeId;
                    vExpense_Response.TicketId = vResultObj.TicketId;

                    vExpense_Response.TicketNumber = vResultObj.TicketNumber;
                    vExpense_Response.CustomerName = vResultObj.CustomerName;
                    vExpense_Response.TicketStartDate = vResultObj.TicketStartDate;
                    vExpense_Response.TicketCloserDate = vResultObj.TicketCloserDate;

                    vExpense_Response.StatusId = vResultObj.StatusId;
                    vExpense_Response.StatusName = vResultObj.StatusName;
                    vExpense_Response.IsActive = vResultObj.IsActive;

                    vExpense_Response.CreatedBy = vResultObj.CreatedBy;
                    vExpense_Response.CreatedDate = vResultObj.CreatedDate;
                    vExpense_Response.CreatorName = vResultObj.CreatorName;

                    vExpense_Response.ModifiedBy = vResultObj.ModifiedBy;
                    vExpense_Response.ModifiedDate = vResultObj.ModifiedDate;
                    vExpense_Response.ModifierName = vResultObj.ModifierName;

                    var vExpenseDetails_Search = new ExpenseDetails_Search()
                    {
                        ExpenseId = vResultObj.Id,
                        ExpenseDetailStatusId = 0,
                    };

                    var vResultExpenseListObj = await _expenseRepository.GetExpenseDetailsList(vExpenseDetails_Search);
                    foreach (var item in vResultExpenseListObj)
                    {
                        var vExpenseDetails_Response = new ExpenseDetails_Response()
                        {
                            Id = item.Id,
                            ExpenseId = vResultObj.Id,
                            ExpenseNumber = item.ExpenseNumber,

                            IsSingleDayExpense = item.IsSingleDayExpense,
                            FromDate = item.FromDate,
                            ToDate = item.ToDate,
                            ExpenseTypeId = item.ExpenseTypeId,
                            ExpenseType = item.ExpenseType,
                            VehicleTypeId = item.VehicleTypeId,
                            VehicleType = item.VehicleType,

                            ExpenseDescription = item.ExpenseDescription,
                            ApprovedAmount = item.ApprovedAmount,
                            ExpenseAmount = item.ExpenseAmount,
                            ExpenseImageFileName = item.ExpenseImageFileName,
                            ExpenseImageOriginalFileName = item.ExpenseImageOriginalFileName,
                            ExpenseImageFileURL = item.ExpenseImageFileURL,
                            ExpenseDetailStatusId = item.ExpenseDetailStatusId,
                            ExpenseDeteillStatusName = item.ExpenseDeteillStatusName,
                            CityGradeId = item.CityGradeId,
                            CityGrade = item.CityGrade,

                            CreatedBy = item.CreatedBy,
                            CreatorName = item.CreatorName,
                            CreatedDate = item.CreatedDate,

                            ModifiedBy = item.ModifiedBy,
                            ModifiedDate = item.ModifiedDate,
                            ModifierName = item.ModifierName,
                        };

                        vExpense_Response.ExpenseDetails.Add(vExpenseDetails_Response);
                    }
                }

                _response.Data = vExpense_Response;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportExpense(ExportExpense_Request request)
        {
            _response.IsSuccess = false;
            byte[] result;

            var vExpense_Search = new Expense_Search()
            {
                EmployeeId = 0,
                StatusId = 0,
                ExpenseId = request.ExpenseId,
            };

            var lstExpenseListObj = await _expenseRepository.GetExpenseList(vExpense_Search);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Expense");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Expense Number";
                    WorkSheet1.Cells[1, 2].Value = "Ticket Number";
                    WorkSheet1.Cells[1, 3].Value = "Customer Name";
                    WorkSheet1.Cells[1, 4].Value = "From Date";
                    WorkSheet1.Cells[1, 5].Value = "To Date";
                    WorkSheet1.Cells[1, 6].Value = "Expense Type";
                    WorkSheet1.Cells[1, 7].Value = "Vehicle Type";
                    WorkSheet1.Cells[1, 8].Value = "Description";
                    WorkSheet1.Cells[1, 9].Value = "Approved Amount";
                    WorkSheet1.Cells[1, 10].Value = "Expense Amount";
                    WorkSheet1.Cells[1, 11].Value = "Status";

                    recordIndex = 2;
                    foreach (var items in lstExpenseListObj)
                    {
                        var vExpenseDetails_Search = new ExpenseDetails_Search()
                        {
                            SearchText = string.Empty,
                            ExpenseId = items.Id,
                            ExpenseDetailStatusId = 0
                        };

                        var objExpenseDetailList = await _expenseRepository.GetExpenseDetailsList(vExpenseDetails_Search);

                        if (objExpenseDetailList.ToList().Count > 0)
                        {
                            foreach (var itemExpenseDetail in objExpenseDetailList)
                            {
                                WorkSheet1.Cells[recordIndex, 1].Value = items.ExpenseNumber;
                                WorkSheet1.Cells[recordIndex, 2].Value = items.TicketNumber;
                                WorkSheet1.Cells[recordIndex, 3].Value = items.CustomerName;

                                WorkSheet1.Cells[recordIndex, 4].Value = itemExpenseDetail.FromDate;
                                WorkSheet1.Cells[recordIndex, 4].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                                WorkSheet1.Cells[recordIndex, 5].Value = itemExpenseDetail.ToDate;
                                WorkSheet1.Cells[recordIndex, 5].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                                WorkSheet1.Cells[recordIndex, 6].Value = itemExpenseDetail.ExpenseType;
                                WorkSheet1.Cells[recordIndex, 7].Value = itemExpenseDetail.VehicleType;
                                WorkSheet1.Cells[recordIndex, 8].Value = itemExpenseDetail.ExpenseDescription;
                                WorkSheet1.Cells[recordIndex, 9].Value = itemExpenseDetail.ApprovedAmount;
                                WorkSheet1.Cells[recordIndex, 10].Value = itemExpenseDetail.ExpenseAmount;
                                WorkSheet1.Cells[recordIndex, 11].Value = items.StatusName;

                                recordIndex += 1;
                            }
                        }
                        else
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.ExpenseNumber;
                            WorkSheet1.Cells[recordIndex, 2].Value = items.TicketNumber;
                            WorkSheet1.Cells[recordIndex, 3].Value = items.CustomerName;

                            WorkSheet1.Cells[recordIndex, 11].Value = items.StatusName;

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
                //update IsExport
                if (request.ExpenseId != "")
                {
                    var vUpdateIsExport = new UpdateIsExport_Request()
                    {
                        Id = request.ExpenseId,
                        Module = "Expense"
                    };

                    int resultvUpdateIsExport = await _expenseRepository.UpdateIsExport(vUpdateIsExport);
                }

                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> UpdateDownloadedExpense(UpdateDownloadedExpense_Request parameters)
        {
            if (parameters.ExpenseId == "")
            {
                _response.Message = "Expense Id is required";
            }
            else
            {
                int resultExpenseDetails = await _expenseRepository.UpdateDownloadedExpense(parameters);

                if (resultExpenseDetails == (int)SaveOperationEnums.NoRecordExists)
                {
                    _response.Message = "No record exists";
                }
                else if (resultExpenseDetails == (int)SaveOperationEnums.ReocrdExists)
                {
                    _response.Message = "Record already exists";
                }
                else if (resultExpenseDetails == (int)SaveOperationEnums.NoResult)
                {
                    _response.Message = "Something went wrong, please try again";
                }
                else
                {
                    _response.Message = "Record details saved sucessfully";
                }
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseForPDF(ExpenseForPDF_Search_Request parameters)
        {
            var vExpenseForPDFList_Response = new ExpenseForPDFList_Response();

            if (parameters.ExpenseId == "")
            {
                _response.Message = "Expense Id is required";
            }
            else
            {
                var vResultObj = await _expenseRepository.GetExpenseForPDF(parameters);
                if (vResultObj != null)
                {
                    var vExpense = vResultObj.FirstOrDefault();
                    if (vExpense != null)
                    {
                        vExpenseForPDFList_Response.EmployeeName = vExpense.EmployeeName;
                        vExpenseForPDFList_Response.HODName = vExpense.HODName;
                        vExpenseForPDFList_Response.DateOfClaim = vExpense.DateOfClaim;
                        vExpenseForPDFList_Response.Department = vExpense.Department;
                        vExpenseForPDFList_Response.EmployeeID = vExpense.EmployeeID;
                    }

                    foreach (var item in vResultObj)
                    {
                        var vExpenseDetailsForPDF_Response = new ExpenseDetailsForPDF_Response()
                        {
                            Id = item.Id,
                            IsSingleDayExpense = item.IsSingleDayExpense,
                            FromDate = item.FromDate,
                            ToDate = item.ToDate,
                            ExpenseNumber = item.ExpenseNumber,
                            TicketNumber = item.TicketNumber,
                            ExpenseDescription = item.ExpenseDescription,
                            CustomerName = item.CustomerName,
                            CityGrade = item.CityGrade,
                            ExpenseType = item.ExpenseType,

                            ExpenseAmount = item.ExpenseAmount,
                            ApprovedAmount = item.ApprovedAmount,
                            VehicleType = item.VehicleType,
                        };

                        vExpenseForPDFList_Response.ExpenseDetails.Add(vExpenseDetailsForPDF_Response);
                    }
                }

                _response.Data = vExpenseForPDFList_Response;
            }
            return _response;
        }
        #endregion

        #region Expense Details

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveExpenseDetails(ExpenseDetails_Request parameters)
        {
            //Image Upload
            if (!string.IsNullOrWhiteSpace(parameters.ExpenseImageFile_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.ExpenseImageFile_Base64, "\\Uploads\\Expense\\", parameters.ExpenseImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.ExpenseImageFileName = vUploadFile;
                }
            }

            //Save / Update
            int result = await _expenseRepository.SaveExpenseDetails(parameters);

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
                _response.Message = "Record details saved sucessfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseDetailsList(ExpenseDetails_Search parameters)
        {
            var vExpenseDetails_Response = new ExpenseDetails_Response();

            var objList = await _expenseRepository.GetExpenseDetailsList(parameters);
            foreach (var item in objList)
            {
                var objExpenseDetailsRemarksList = await _expenseRepository.GetExpenseDetailsRemarksListById(item.Id);
                item.remarksList = objExpenseDetailsRemarksList.ToList();
            }

            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseDetailsById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _expenseRepository.GetExpenseDetailsById(Id);
                if (vResultObj != null)
                {
                    var objExpenseDetailsRemarksList = await _expenseRepository.GetExpenseDetailsRemarksListById(vResultObj.Id);
                    vResultObj.remarksList = objExpenseDetailsRemarksList.ToList();
                }

                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExpenseDetailsApproveNReject(Expense_ApproveNReject parameters)
        {
            if (parameters.Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                int resultExpenseDetails = await _expenseRepository.ExpenseDetailsApproveNReject(parameters);

                if (resultExpenseDetails == (int)SaveOperationEnums.NoRecordExists)
                {
                    _response.Message = "No record exists";
                }
                else if (resultExpenseDetails == (int)SaveOperationEnums.ReocrdExists)
                {
                    _response.Message = "Record already exists";
                }
                else if (resultExpenseDetails == (int)SaveOperationEnums.NoResult)
                {
                    _response.Message = "Something went wrong, please try again";
                }
                else
                {
                    _response.Message = "Record details saved sucessfully";
                }
            }

            return _response;
        }

        #endregion

        #region Daily Travel Expense

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveDailyTravelExpense(DailyTravelExpense_Request parameters)
        {
            //Image Upload
            if (!string.IsNullOrWhiteSpace(parameters.ExpenseImageFile_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.ExpenseImageFile_Base64, "\\Uploads\\Expense\\", parameters.ExpenseImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.ExpenseImageFileName = vUploadFile;
                }
            }

            //Save / Update
            int result = await _expenseRepository.SaveDailyTravelExpense(parameters);

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
                _response.Message = "Record details saved sucessfully";
            }

            _response.Id = result;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDailyTravelExpenseList(DailyTravelExpense_Search parameters)
        {
            var vDailyTravelExpense_Response = new DailyTravelExpense_Response();

            var objList = await _expenseRepository.GetDailyTravelExpenseList(parameters);
            foreach (var item in objList)
            {
                var objDailyTravelExpenseRemarksList = await _expenseRepository.GetDailyTravelExpenseRemarksListById(item.Id);
                item.remarksList = objDailyTravelExpenseRemarksList.ToList();
            }

            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDailyTravelExpenseById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _expenseRepository.GetDailyTravelExpenseById(Id);
                if (vResultObj != null)
                {
                    var objDailyTravelExpenseRemarksList = await _expenseRepository.GetDailyTravelExpenseRemarksListById(vResultObj.Id);
                    vResultObj.remarksList = objDailyTravelExpenseRemarksList.ToList();
                }

                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DailyTravelExpenseApproveNReject(DailyTravelExpense_ApproveNReject parameters)
        {
            if (parameters.Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                int resultDailyTravelExpense = await _expenseRepository.DailyTravelExpenseApproveNReject(parameters);

                if (resultDailyTravelExpense == (int)SaveOperationEnums.NoRecordExists)
                {
                    _response.Message = "No record exists";
                }
                else if (resultDailyTravelExpense == (int)SaveOperationEnums.ReocrdExists)
                {
                    _response.Message = "Record already exists";
                }
                else if (resultDailyTravelExpense == (int)SaveOperationEnums.NoResult)
                {
                    _response.Message = "Something went wrong, please try again";
                }
                else
                {
                    _response.Message = "Record details saved sucessfully";
                }
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportDailyTravelExpense(ExportExpense_Request request)
        {
            _response.IsSuccess = false;
            byte[] result;

            var vDailyTravelExpense_Search = new DailyTravelExpense_Search()
            {
                EmployeeId = 0,
                StatusId = 0,
                ExpenseId = request.ExpenseId,
            };

            var lstExpenseListObj = await _expenseRepository.GetDailyTravelExpenseList(vDailyTravelExpense_Search);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Expense");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Expense Number";
                    WorkSheet1.Cells[1, 2].Value = "Ticket Number";
                    WorkSheet1.Cells[1, 3].Value = "Expense Type";
                    WorkSheet1.Cells[1, 4].Value = "Vehicle Type";
                    WorkSheet1.Cells[1, 5].Value = "Rate Per KM";
                    WorkSheet1.Cells[1, 6].Value = "Total KM";
                    WorkSheet1.Cells[1, 7].Value = "Total Amount";
                    WorkSheet1.Cells[1, 8].Value = "Status";

                    recordIndex = 2;
                    foreach (var items in lstExpenseListObj)
                    {
                        foreach (var itemExpenseDetail in lstExpenseListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.ExpenseNumber;
                            WorkSheet1.Cells[recordIndex, 2].Value = items.TicketNumber;
                            WorkSheet1.Cells[recordIndex, 3].Value = itemExpenseDetail.ExpenseType;
                            WorkSheet1.Cells[recordIndex, 4].Value = itemExpenseDetail.VehicleType;
                            WorkSheet1.Cells[recordIndex, 5].Value = itemExpenseDetail.RatePerKm;
                            WorkSheet1.Cells[recordIndex, 6].Value = itemExpenseDetail.TotalKm;
                            WorkSheet1.Cells[recordIndex, 7].Value = itemExpenseDetail.TotalAmount;
                            WorkSheet1.Cells[recordIndex, 8].Value = items.StatusName;

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
                //update IsExport
                if (request.ExpenseId != "")
                {
                    var vUpdateIsExport = new UpdateIsExport_Request()
                    {
                        Id = request.ExpenseId,
                        Module = "Daily Travel Expense"
                    };

                    int resultvUpdateIsExport = await _expenseRepository.UpdateIsExport(vUpdateIsExport);
                }

                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetDailyTravelExpenseForPDF(ExpenseForPDF_Search_Request parameters)
        {
            var vDailyTravelExpenseForPDFList_Response = new DailyTravelExpenseForPDFList_Response();

            if (parameters.ExpenseId == "")
            {
                _response.Message = "Expense Id is required";
            }
            else
            {
                var vResultObj = await _expenseRepository.GetDailyTravelExpenseForPDF(parameters);
                if (vResultObj != null)
                {
                    var vExpense = vResultObj.FirstOrDefault();
                    if (vExpense != null)
                    {
                        vDailyTravelExpenseForPDFList_Response.EmployeeName = vExpense.EmployeeName;
                        vDailyTravelExpenseForPDFList_Response.HODName = vExpense.HODName;
                        vDailyTravelExpenseForPDFList_Response.DateOfClaim = vExpense.DateOfClaim;
                        vDailyTravelExpenseForPDFList_Response.Department = vExpense.Department;
                        vDailyTravelExpenseForPDFList_Response.EmployeeID = vExpense.EmployeeID;
                    }

                    foreach (var item in vResultObj)
                    {
                        var vDailyTravelExpenseDetailsForPDF_Response = new DailyTravelExpenseDetailsForPDF_Response()
                        {
                            Id = item.Id,
                            IsTicetExpense = item.IsTicetExpense,
                            ExpenseNumber = item.ExpenseNumber,
                            TicketNumber = item.TicketNumber,
                            CustomerName = item.CustomerName,
                            ExpenseDate = item.ExpenseDate,
                            ExpenseType = item.ExpenseType,
                            VehicleType = item.VehicleType,
                            ExpenseDesc = item.ExpenseDesc,
                            RatePerKm = item.RatePerKm,
                            TotalKm = item.TotalKm,
                            TotalAmount = item.TotalAmount,
                        };

                        vDailyTravelExpenseForPDFList_Response.ExpenseDetails.Add(vDailyTravelExpenseDetailsForPDF_Response);
                    }
                }

                _response.Data = vDailyTravelExpenseForPDFList_Response;
            }
            return _response;
        }
        #endregion
    }
}
