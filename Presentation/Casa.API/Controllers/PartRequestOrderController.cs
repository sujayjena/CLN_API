using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using CLN.Application.Helpers;
using System.Globalization;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartRequestOrderController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;
        private readonly IPartRequestOrderRepository _partRequestOrderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private IEmailHelper _emailHelper;
        private readonly IWebHostEnvironment _environment;

        public PartRequestOrderController(IFileManager fileManager, IPartRequestOrderRepository enggPartRequestOrderRepository, IUserRepository userRepository, IBranchRepository branchRepository, IEmailHelper emailHelper, IWebHostEnvironment environment)
        {
            _fileManager = fileManager;
            _partRequestOrderRepository = enggPartRequestOrderRepository;
            _userRepository = userRepository;
            _branchRepository = branchRepository;
            _emailHelper = emailHelper;
            _environment = environment;

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
                        Id=item.Id,
                        RequestId = result,
                        SpareCategoryId = item.SpareCategoryId,
                        ProductMakeId = item.ProductMakeId,
                        BMSMakeId = item.BMSMakeId,
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

                //Send Email
                if (parameters.Id == 0)
                {
                    var vEmailEngg = await SendSparePartRequest_EmailToEmployee(result);
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
                            Id = item.Id,
                            RequestId = item.RequestId,
                            RequestNumber = item.RequestNumber,
                            SpareCategoryId = item.SpareCategoryId,
                            SpareCategory = item.SpareCategory,
                            ProductMakeId = item.ProductMakeId,
                            ProductMake = item.ProductMake,
                            BMSMakeId = item.BMSMakeId,
                            BMSMake = item.BMSMake,
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
                            RGP = item.RGP,
                            StockAvailableQty = item.StockAvailableQty,
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportEnggPartRequest()
        {
            _response.IsSuccess = false;
            byte[] result;

            var request = new BaseSearchEntity();

            var parameters = new EnggPartRequest_Search()
            {
                EngineerId = 0,
                SearchText = string.Empty,
                StatusId = 0,
                SpareDetailsId=0
            };

            var objList = await _partRequestOrderRepository.GetEnggPartRequestList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("EnggPartRequest");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Order Number";
                    WorkSheet1.Cells[1, 2].Value = "Date";
                    WorkSheet1.Cells[1, 3].Value = "Engineer Name";
                    WorkSheet1.Cells[1, 4].Value = "Spare Category";
                    WorkSheet1.Cells[1, 5].Value = "Product Make";
                    WorkSheet1.Cells[1, 6].Value = "BMS Make";
                    WorkSheet1.Cells[1, 7].Value = "Part Code";
                    WorkSheet1.Cells[1, 8].Value = "Part Description";
                    WorkSheet1.Cells[1, 9].Value = "UOM";
                    WorkSheet1.Cells[1, 10].Value = "Available Qty";
                    WorkSheet1.Cells[1, 11].Value = "Order Qty";
                    WorkSheet1.Cells[1, 12].Value = "RGP";
                    WorkSheet1.Cells[1, 13].Value = "Remark";

                    recordIndex = 2;
                    foreach (var itemsReqList in objList)
                    {
                        // Accessory
                        var vSearchObj = new EnggPartRequestDetails_Search()
                        {
                            RequestId = itemsReqList.Id,
                        };

                        var objReqDetailsList = await _partRequestOrderRepository.GetEnggPartRequestDetailList(vSearchObj);
                        if (objReqDetailsList.Count() > 0)
                        {
                            foreach (var itemReqDetails in objReqDetailsList)
                            {
                                WorkSheet1.Cells[recordIndex, 1].Value = itemsReqList.RequestNumber;

                                WorkSheet1.Cells[recordIndex, 2].Value = itemsReqList.RequestDate;
                                WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                                WorkSheet1.Cells[recordIndex, 3].Value = itemsReqList.EngineerName;
                                WorkSheet1.Cells[recordIndex, 4].Value = itemReqDetails.SpareCategory;
                                WorkSheet1.Cells[recordIndex, 5].Value = itemReqDetails.ProductMake;
                                WorkSheet1.Cells[recordIndex, 6].Value = itemReqDetails.BMSMake;
                                WorkSheet1.Cells[recordIndex, 7].Value = itemReqDetails.UniqueCode;
                                WorkSheet1.Cells[recordIndex, 8].Value = itemReqDetails.SpareDesc;
                                WorkSheet1.Cells[recordIndex, 9].Value = itemReqDetails.UOMName;
                                WorkSheet1.Cells[recordIndex, 10].Value = itemReqDetails.AvailableQty;
                                WorkSheet1.Cells[recordIndex, 11].Value = itemReqDetails.RequiredQty;
                                WorkSheet1.Cells[recordIndex, 12].Value = itemReqDetails.RGP == true ? "OK" : "NOT OK";
                                WorkSheet1.Cells[recordIndex, 13].Value = itemReqDetails.Remarks;

                                recordIndex += 1;
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

        protected async Task<bool> SendSparePartRequest_EmailToEmployee(int RequestId)
        {
            bool result = false;
            string templateFilePath = "", emailTemplateContent = "", remarks = "", sSubjectDynamicContent = "", listContent = "";

            try
            {
                var dataObj = await _partRequestOrderRepository.GetEnggPartRequestById(RequestId);
                if (dataObj != null)
                {
                    string recipientEmail = "";
                    string vReportedToEmployeeEmailId = "";
                    string vStoreInchargeEmployeeEmailId = "";

                    string vloginUserName = "";
                    string vloginUserRole = "";

                    int vloginUserBrandId = 0;

                    var vloginUserId = SessionManager.LoggedInUserId;
                    var vUserDetail = await _userRepository.GetUserById(Convert.ToInt32(vloginUserId));
                    if (vUserDetail != null)
                    {
                        vloginUserName = vUserDetail.UserName;
                        vloginUserRole = vUserDetail.RoleName;

                        var vReportedToUserDetail = await _userRepository.GetUserById(Convert.ToInt32(vUserDetail.ReportingTo));
                        if (vReportedToUserDetail != null)
                        {
                            vReportedToEmployeeEmailId = vReportedToUserDetail.IsActive == true ? vReportedToUserDetail.EmailId : string.Empty;
                        }

                        var vUserBranchList = await _branchRepository.GetBranchMappingByEmployeeId(vUserDetail.Id, 0);
                        if (vUserBranchList.ToList().Count > 0)
                        {
                            vloginUserBrandId = vUserBranchList.ToList().Select(x => x.BranchId).FirstOrDefault() != null ? Convert.ToInt32(vUserBranchList.ToList().Select(x => x.BranchId).FirstOrDefault()) : 0;
                        }

                        var vBranchUser = await _branchRepository.GetBranchMappingByEmployeeId(0, vloginUserBrandId);
                        if (vBranchUser.ToList().Count > 0)
                        {
                            var searchUser = new BaseSearchEntity();
                            searchUser.IsActive = true;

                            var vUserList = await _userRepository.GetUserList(searchUser);
                            if (vUserList.ToList().Count > 0)
                            {
                                var vStoreIncharge = vUserList.Where(x => x.RoleName == "Store Incharge" && vBranchUser.Select(x => x.EmployeeId).Contains(x.Id));
                                if (vStoreIncharge.ToList().Count > 0)
                                {
                                    vStoreInchargeEmployeeEmailId = string.Join(",", new List<string>(vStoreIncharge.ToList().Select(x => x.EmailId)).ToArray());
                                }
                            }
                        }
                    }

                    if (vStoreInchargeEmployeeEmailId != "")
                    {
                        recipientEmail = vReportedToEmployeeEmailId + "," + vStoreInchargeEmployeeEmailId;
                    }
                    else
                    {
                        recipientEmail = vReportedToEmployeeEmailId;
                    }


                    templateFilePath = _environment.ContentRootPath + "\\EmailTemplates\\SparePartRequest_Employee_Template.html";
                    emailTemplateContent = System.IO.File.ReadAllText(templateFilePath);

                    if (emailTemplateContent.IndexOf("[RequestNumber]", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        emailTemplateContent = emailTemplateContent.Replace("[RequestNumber]", dataObj.RequestNumber);
                    }

                    if (emailTemplateContent.IndexOf("[PartRequestDetailsList]", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        listContent = string.Empty;

                        var vSearchObj = new EnggPartRequestDetails_Search()
                        {
                            RequestId = dataObj.Id,
                        };

                        var objDetailsList = await _partRequestOrderRepository.GetEnggPartRequestDetailList(vSearchObj);

                        int rowNo = 1;
                        foreach (EnggPartRequestDetails_Response items in objDetailsList)
                        {
                            listContent = $@"{listContent}
                            <tr style='border: 1px solid #dddddd; text-align: left; padding: 8px;'>
                                <td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>{rowNo}</td>
                                <td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>{items.SpareCategory}</td>
                                <td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>{items.UniqueCode}</td>
                                <td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>{items.SpareDesc}</td>
                                <td style='border: 1px solid #dddddd;text-align: center;padding: 8px;'>{items.RequiredQty}</td>
                            </tr>";

                            rowNo++;
                        }

                        emailTemplateContent = emailTemplateContent.Replace("[PartRequestDetailsList]", listContent);
                    }

                    if (emailTemplateContent.IndexOf("[EngineerName]", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        emailTemplateContent = emailTemplateContent.Replace("[EngineerName]", vloginUserName);
                    }

                    if (emailTemplateContent.IndexOf("[EngineerRole]", StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        emailTemplateContent = emailTemplateContent.Replace("[EngineerRole]", vloginUserRole);
                    }

                    sSubjectDynamicContent = "Spare Part Request | " + dataObj.RequestNumber;
                    result = await _emailHelper.SendEmail(module: "Spare Part Request", subject: sSubjectDynamicContent, sendTo: "Employee", content: emailTemplateContent, recipientEmail: recipientEmail, files: null, remarks: remarks);
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
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
                        Id = item.Id,
                        RequestId = result,
                        SpareCategoryId = item.SpareCategoryId,
                        ProductMakeId = item.ProductMakeId,
                        BMSMakeId = item.BMSMakeId,
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
                            Id = item.Id,
                            RequestId = item.RequestId,
                            RequestNumber = item.RequestNumber,
                            SpareCategoryId = item.SpareCategoryId,
                            SpareCategory = item.SpareCategory,
                            ProductMakeId = item.ProductMakeId,
                            ProductMake = item.ProductMake,
                            BMSMakeId = item.BMSMakeId,
                            BMSMake = item.BMSMake,
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
                            RGP = item.RGP,
                            StockAvailableQty = item.StockAvailableQty,
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

        #region TRC Part Request Import/Export

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadTRCPartRequestTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_TRCPartRequest.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportTrcPartRequest([FromQuery] ImportRequest request)
        {
            _response.IsSuccess = false;

            ExcelWorksheets currentSheet;
            ExcelWorksheet workSheet;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            int noOfCol, noOfRow;

            List<string[]> data = new List<string[]>();
            List<TRCPartRequest_ImportData> lstTRCPartRequest_ImportData = new List<TRCPartRequest_ImportData>();
            IEnumerable<TRCPartRequest_ImportDataValidation> lstTRCPartRequest_ImportDataValidation;

            if (request.FileUpload == null || request.FileUpload.Length == 0)
            {
                _response.Message = "Please upload an excel file";
                return _response;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                request.FileUpload.CopyTo(stream);
                using ExcelPackage package = new ExcelPackage(stream);
                currentSheet = package.Workbook.Worksheets;
                workSheet = currentSheet.First();
                noOfCol = workSheet.Dimension.End.Column;
                noOfRow = workSheet.Dimension.End.Row;

                if (!string.Equals(workSheet.Cells[1, 1].Value.ToString(), "EngineerName", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 2].Value.ToString(), "PartCode", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 3].Value.ToString(), "UOM", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 4].Value.ToString(), "TypeOfBMS", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 5].Value.ToString(), "Quantity", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 6].Value.ToString(), "RGP", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 7].Value.ToString(), "Remarks", StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Please upload a valid excel file";
                    return _response;
                }

                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 3].Value?.ToString()))
                    {
                        lstTRCPartRequest_ImportData.Add(new TRCPartRequest_ImportData()
                        {
                            EngineerName = workSheet.Cells[rowIterator, 1].Value?.ToString(),
                            PartCode = workSheet.Cells[rowIterator, 2].Value?.ToString(),
                            UOM = workSheet.Cells[rowIterator, 3].Value?.ToString(),
                            TypeOfBMS = workSheet.Cells[rowIterator, 4].Value?.ToString(),
                            Quantity = workSheet.Cells[rowIterator, 5].Value?.ToString(),
                            RGP = workSheet.Cells[rowIterator, 6].Value?.ToString(),
                            Remarks = workSheet.Cells[rowIterator, 7].Value?.ToString(),
                        });
                    }
                }
            }

            if (lstTRCPartRequest_ImportData.Count == 0)
            {
                _response.Message = "File does not contains any record(s)";
                return _response;
            }

            lstTRCPartRequest_ImportDataValidation = await _partRequestOrderRepository.ImportTRCPartRequestDetail(lstTRCPartRequest_ImportData);

            _response.IsSuccess = true;
            _response.Message = "Record imported successfully";

            #region Generate Excel file for Invalid Data

            if (lstTRCPartRequest_ImportDataValidation.ToList().Count > 0)
            {
                _response.Message = "Uploaded file contains invalid records, please check downloaded file for more details";
                _response.Data = GenerateInvalidImportDataFile(lstTRCPartRequest_ImportDataValidation);

            }

            #endregion

            return _response;
        }

        private byte[] GenerateInvalidImportDataFile(IEnumerable<TRCPartRequest_ImportDataValidation> lstTRCPartRequest_ImportDataValidation)
        {
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;

            using (MemoryStream msInvalidDataFile = new MemoryStream())
            {
                using (ExcelPackage excelInvalidData = new ExcelPackage())
                {
                    WorkSheet1 = excelInvalidData.Workbook.Worksheets.Add("Invalid_Records");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "EngineerName";
                    WorkSheet1.Cells[1, 2].Value = "PartCode";
                    WorkSheet1.Cells[1, 3].Value = "UOM";
                    WorkSheet1.Cells[1, 4].Value = "TypeOfBMS";
                    WorkSheet1.Cells[1, 5].Value = "Quantity";
                    WorkSheet1.Cells[1, 6].Value = "RGP";
                    WorkSheet1.Cells[1, 7].Value = "Remarks";
                    WorkSheet1.Cells[1, 8].Value = "ErrorMessage";

                    recordIndex = 2;

                    foreach (TRCPartRequest_ImportDataValidation record in lstTRCPartRequest_ImportDataValidation)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = record.EngineerName;
                        WorkSheet1.Cells[recordIndex, 2].Value = record.PartCode;
                        WorkSheet1.Cells[recordIndex, 3].Value = record.UOM;
                        WorkSheet1.Cells[recordIndex, 4].Value = record.TypeOfBMS;
                        WorkSheet1.Cells[recordIndex, 5].Value = record.Quantity;
                        WorkSheet1.Cells[recordIndex, 6].Value = record.RGP;
                        WorkSheet1.Cells[recordIndex, 7].Value = record.Remarks;
                        WorkSheet1.Cells[recordIndex, 8].Value = record.ValidationMessage;

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();

                    excelInvalidData.SaveAs(msInvalidDataFile);
                    msInvalidDataFile.Position = 0;
                    result = msInvalidDataFile.ToArray();
                }
            }

            return result;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportTrcPartRequest()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var parameters = new TRCPartRequest_Search()
            {
                EngineerId = 0,
                SearchText = string.Empty,
                StatusId = 0,
                SpareDetailsId = 0,
            };

            var objList = await _partRequestOrderRepository.GetTRCPartRequestList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("TRCPartRequest");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "TRC Req #";
                    WorkSheet1.Cells[1, 2].Value = "Order Date";
                    WorkSheet1.Cells[1, 3].Value = "Service Engg. Name";
                    WorkSheet1.Cells[1, 4].Value = "Spare Category";
                    WorkSheet1.Cells[1, 5].Value = "Product Make";
                    WorkSheet1.Cells[1, 6].Value = "BMS Make";
                    WorkSheet1.Cells[1, 7].Value = "Spare Part Code";
                    WorkSheet1.Cells[1, 8].Value = "Part Description";
                    WorkSheet1.Cells[1, 9].Value = "UOM";
                    WorkSheet1.Cells[1, 10].Value = "Type of BMS";
                    WorkSheet1.Cells[1, 11].Value = "Available Qty";
                    WorkSheet1.Cells[1, 12].Value = "Order Qty";
                    WorkSheet1.Cells[1, 13].Value = "RGP";
                    WorkSheet1.Cells[1, 14].Value = "Remark";

                    recordIndex = 2;
                    foreach (var itemsReqList in objList)
                    {
                        // Accessory
                        var vSearchObj = new TRCPartRequestDetails_Search()
                        {
                            RequestId = itemsReqList.Id,
                        };

                        var objReqDetailsList = await _partRequestOrderRepository.GetTRCPartRequestDetailList(vSearchObj);
                        if (objReqDetailsList.Count() > 0)
                        {
                            foreach (var itemReqDetails in objReqDetailsList)
                            {
                                WorkSheet1.Cells[recordIndex, 1].Value = itemsReqList.RequestNumber;

                                WorkSheet1.Cells[recordIndex, 2].Value = itemsReqList.RequestDate;
                                WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                                WorkSheet1.Cells[recordIndex, 3].Value = itemsReqList.EngineerName;
                                WorkSheet1.Cells[recordIndex, 4].Value = itemReqDetails.SpareCategory;
                                WorkSheet1.Cells[recordIndex, 5].Value = itemReqDetails.ProductMake;
                                WorkSheet1.Cells[recordIndex, 6].Value = itemReqDetails.BMSMake;
                                WorkSheet1.Cells[recordIndex, 7].Value = itemReqDetails.UniqueCode;
                                WorkSheet1.Cells[recordIndex, 8].Value = itemReqDetails.SpareDesc;
                                WorkSheet1.Cells[recordIndex, 9].Value = itemReqDetails.UOMName;
                                WorkSheet1.Cells[recordIndex, 10].Value = itemReqDetails.TypeOfBMS;
                                WorkSheet1.Cells[recordIndex, 11].Value = itemReqDetails.AvailableQty;
                                WorkSheet1.Cells[recordIndex, 12].Value = itemReqDetails.RequiredQty;
                                WorkSheet1.Cells[recordIndex, 13].Value = itemReqDetails.RGP == true ? "OK" : "NOT OK";
                                WorkSheet1.Cells[recordIndex, 14].Value = itemReqDetails.Remarks;

                                recordIndex += 1;
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
    }
}
