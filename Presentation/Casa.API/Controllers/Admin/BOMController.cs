using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class BOMController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;
        private readonly IBOMRepository _bomRepository;

        public BOMController(IFileManager fileManager, IBOMRepository bomRepository)
        {
            _fileManager = fileManager;
            _bomRepository = bomRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBOM(BOM_Request parameters)
        {
            // Image Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PartImage_Base64))
            {
                var vUploadFile_AadharCardImage = _fileManager.UploadDocumentsBase64ToFile(parameters.PartImage_Base64, "\\Uploads\\Customer\\QC\\", parameters.PartImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile_AadharCardImage))
                {
                    parameters.PartImage = vUploadFile_AadharCardImage;
                }
            }

            int result = await _bomRepository.SaveBOM(parameters);

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
        public async Task<ResponseModel> GetBOMList(BaseSearchEntity parameters)
        {
            IEnumerable<BOM_Response> lstRoles = await _bomRepository.GetBOMList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBOMById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _bomRepository.GetBOMById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadBOMTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_Bom.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportBOM([FromQuery] ImportRequest request)
        {
            _response.IsSuccess = false;

            ExcelWorksheets currentSheet;
            ExcelWorksheet workSheet;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            int noOfCol, noOfRow;

            List<string[]> data = new List<string[]>();
            List<BOM_ImportData> lstBOM_ImportData = new List<BOM_ImportData>();
            IEnumerable<BOM_ImportDataValidation> lstBOM_ImportDataValidation;

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

                if (!string.Equals(workSheet.Cells[1, 1].Value.ToString(), "BOM#", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 2].Value.ToString(), "ProductCategory", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 3].Value.ToString(), "Segment", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 4].Value.ToString(), "SubSegment", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 5].Value.ToString(), "ProductModel", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 6].Value.ToString(), "DrawingNumber", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 7].Value.ToString(), "Warranty", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 8].Value.ToString(), "Remarks", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 9].Value.ToString(), "IsActive", StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Please upload a valid excel file";
                    return _response;
                }

                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()))
                    {
                        lstBOM_ImportData.Add(new BOM_ImportData()
                        {
                            PartCode = workSheet.Cells[rowIterator, 1].Value?.ToString(),
                            ProductCategory = workSheet.Cells[rowIterator, 2].Value?.ToString(),
                            Segment = workSheet.Cells[rowIterator, 3].Value?.ToString(),
                            SubSegment = workSheet.Cells[rowIterator, 4].Value?.ToString(),
                            ProductModel = workSheet.Cells[rowIterator, 5].Value?.ToString(),
                            DrawingNumber = workSheet.Cells[rowIterator, 6].Value?.ToString(),
                            Warranty = workSheet.Cells[rowIterator, 7].Value?.ToString(),
                            Remarks = workSheet.Cells[rowIterator, 8].Value?.ToString(),
                            IsActive = workSheet.Cells[rowIterator, 9].Value?.ToString(),
                        });
                    }
                }
            }

            if (lstBOM_ImportData.Count == 0)
            {
                _response.Message = "File does not contains any record(s)";
                return _response;
            }

            lstBOM_ImportDataValidation = await _bomRepository.ImportBOM(lstBOM_ImportData);

            _response.IsSuccess = true;
            _response.Message = "Record imported successfully";

            #region Generate Excel file for Invalid Data

            if (lstBOM_ImportDataValidation.ToList().Count > 0)
            {
                _response.IsSuccess = false;
                _response.Message = "Uploaded file contains invalid records, please check downloaded file for more details";
                _response.Data = GenerateInvalidImportDataFile(lstBOM_ImportDataValidation);
            }

            #endregion

            return _response;
        }

        private byte[] GenerateInvalidImportDataFile(IEnumerable<BOM_ImportDataValidation> lstBOM_ImportDataValidation)
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

                    WorkSheet1.Cells[1, 1].Value = "BOM#";
                    WorkSheet1.Cells[1, 2].Value = "ProductCategory";
                    WorkSheet1.Cells[1, 3].Value = "Segment";
                    WorkSheet1.Cells[1, 4].Value = "SubSegment";
                    WorkSheet1.Cells[1, 5].Value = "ProductModel";
                    WorkSheet1.Cells[1, 6].Value = "DrawingNumber";
                    WorkSheet1.Cells[1, 7].Value = "Warranty";
                    WorkSheet1.Cells[1, 8].Value = "Remarks";
                    WorkSheet1.Cells[1, 9].Value = "IsActive";
                    WorkSheet1.Cells[1, 10].Value = "ErrorMessage";

                    recordIndex = 2;

                    foreach (BOM_ImportDataValidation record in lstBOM_ImportDataValidation)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = record.PartCode;
                        WorkSheet1.Cells[recordIndex, 2].Value = record.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 3].Value = record.Segment;
                        WorkSheet1.Cells[recordIndex, 4].Value = record.SubSegment;
                        WorkSheet1.Cells[recordIndex, 5].Value = record.ProductModel;
                        WorkSheet1.Cells[recordIndex, 6].Value = record.DrawingNumber;
                        WorkSheet1.Cells[recordIndex, 7].Value = record.Warranty;
                        WorkSheet1.Cells[recordIndex, 8].Value = record.Remarks;
                        WorkSheet1.Cells[recordIndex, 9].Value = record.IsActive;
                        WorkSheet1.Cells[recordIndex, 10].Value = record.ValidationMessage;

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
        public async Task<ResponseModel> ExportBOM()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new BaseSearchEntity();

            IEnumerable<BOM_Response> lstSizeObj = await _bomRepository.GetBOMList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("BOMDetails");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "BOM #";
                    WorkSheet1.Cells[1, 2].Value = "Product Category";
                    WorkSheet1.Cells[1, 3].Value = "Segment";
                    WorkSheet1.Cells[1, 4].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 5].Value = "Product Model";
                    WorkSheet1.Cells[1, 6].Value = "Drawing Number";
                    WorkSheet1.Cells[1, 7].Value = "Warranty";
                    WorkSheet1.Cells[1, 8].Value = "Remarks";
                    WorkSheet1.Cells[1, 9].Value = "Status";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.PartCode;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.ProductModel;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.DrawingNumber;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.Warranty;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.Remarks;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.IsActive == true ? "Active" : "Inactive";

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
    }
}
