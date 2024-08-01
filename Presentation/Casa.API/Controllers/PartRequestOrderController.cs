using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using CLN.Application.Helpers;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartRequestOrderController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;
        private readonly IPartRequestOrderRepository _partRequestOrderRepository;

        public PartRequestOrderController(IFileManager fileManager, IPartRequestOrderRepository enggPartRequestOrderRepository)
        {
            _fileManager = fileManager;
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
                            Id = item.Id,
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
                            Id = item.Id,
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

        #region TRC Part Request Import/Export

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadBOMTemplate()
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

            var parameters = new TRCPartRequestDetails_Search()
            {
                RequestId = 0
            };

            var objList = await _partRequestOrderRepository.GetTRCPartRequestDetailList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("QCProductSerialNumber");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Engineer Name";
                    WorkSheet1.Cells[1, 2].Value = "Request Number";
                    WorkSheet1.Cells[1, 3].Value = "Part Code";
                    WorkSheet1.Cells[1, 4].Value = "Part Description";
                    WorkSheet1.Cells[1, 5].Value = "UOM";
                    WorkSheet1.Cells[1, 6].Value = "TypeOfBMS";
                    WorkSheet1.Cells[1, 7].Value = "Quantity";
                    WorkSheet1.Cells[1, 8].Value = "RGP";
                    WorkSheet1.Cells[1, 9].Value = "Remarks";

                    recordIndex = 2;

                    foreach (var items in objList.OrderBy(x => x.EngineerName).ToList())
                    {


                        WorkSheet1.Cells[recordIndex, 1].Value = items.EngineerName;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.RequestNumber;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.UniqueCode;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.SpareDesc;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.UOMName;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.TypeOfBMS;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.RequiredQty;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.RGP;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.Remarks;

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
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        #endregion
    }
}
