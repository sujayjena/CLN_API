using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;
using CLN.Application.Helpers;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProblemReportedByEngController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IProblemReportedByEngRepository _problemReportedByEngRepository;
        private IFileManager _fileManager;

        public ProblemReportedByEngController(IProblemReportedByEngRepository problemReportedByEngRepository, IFileManager fileManager)
        {
            _problemReportedByEngRepository = problemReportedByEngRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
            _fileManager = fileManager;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProblemReportedByEng(ProblemReportedByEng_Request parameters)
        {
            int result = await _problemReportedByEngRepository.SaveProblemReportedByEng(parameters);

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
                _response.Message = "Record details saved successfully";
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProblemReportedByEngList(BaseSearchEntity parameters)
        {
            IEnumerable<ProblemReportedByEng_Response> lstRoles = await _problemReportedByEngRepository.GetProblemReportedByEngList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProblemReportedByEngById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _problemReportedByEngRepository.GetProblemReportedByEngById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadProblemReportedByEngTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_ProblemReportedByEng.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportProblemReportedByEng([FromQuery] ImportRequest request)
        {
            _response.IsSuccess = false;
            List<string[]> data = new List<string[]>();
            ExcelWorksheets currentSheet;
            ExcelWorksheet workSheet;
            List<ImportedProblemReportedByEng> lstImportedProblemReportedByEng = new List<ImportedProblemReportedByEng>();
            IEnumerable<ProblemReportedByEngDataValidationErrors> lstProblemReportedByEngFailedToImport;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            int noOfCol, noOfRow;

            if (request.FileUpload == null || request.FileUpload.Length == 0)
            {
                _response.Message = "Please upload an excel file to import Problem Reported By Eng data";
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

                if (!string.Equals(workSheet.Cells[1, 1].Value.ToString(), "ProductCategory", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 2].Value.ToString(), "Segment", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 3].Value.ToString(), "SubSegment", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 4].Value.ToString(), "ProblemReportedByEng", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 5].Value.ToString(), "IsActive", StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Please upload a valid excel file. Please Download Format file for reference";
                    return _response;
                }

                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    lstImportedProblemReportedByEng.Add(new ImportedProblemReportedByEng()
                    {
                        ProductCategory = workSheet.Cells[rowIterator, 1].Value?.ToString(),
                        Segment = workSheet.Cells[rowIterator, 2].Value?.ToString(),
                        SubSegment = workSheet.Cells[rowIterator, 3].Value?.ToString(),
                        ProblemReportedByEng = workSheet.Cells[rowIterator, 4].Value?.ToString(),
                        IsActive = workSheet.Cells[rowIterator, 5].Value?.ToString()
                    });
                }
            }

            if (lstImportedProblemReportedByEng.Count == 0)
            {
                _response.Message = "File does not contains any record(s)";
                return _response;
            }

            lstProblemReportedByEngFailedToImport = await _problemReportedByEngRepository.ImportProblemReportedByEng(lstImportedProblemReportedByEng);

            _response.IsSuccess = true;
            _response.Message = "Problem Reported By Engg list imported successfully";

            #region Generate Excel file for Invalid Data
            if (lstProblemReportedByEngFailedToImport.ToList().Count > 0)
            {
                _response.Message = "Uploaded file contains invalid records, please check downloaded file for more details";
                _response.Data = GenerateInvalidProblemReportedByEngDataFile(lstProblemReportedByEngFailedToImport);

            }
            #endregion

            return _response;
        }

        private byte[] GenerateInvalidProblemReportedByEngDataFile(IEnumerable<ProblemReportedByEngDataValidationErrors> lstProblemReportedByEngFailedToImport)
        {
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;

            using (MemoryStream msInvalidDataFile = new MemoryStream())
            {
                using (ExcelPackage excelInvalidData = new ExcelPackage())
                {
                    WorkSheet1 = excelInvalidData.Workbook.Worksheets.Add("Invalid_ProblemReportedByEng_Records");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "ProductCategory";
                    WorkSheet1.Cells[1, 2].Value = "Segment";
                    WorkSheet1.Cells[1, 3].Value = "SubSegment";
                    WorkSheet1.Cells[1, 4].Value = "ProblemReportedByEng";
                    WorkSheet1.Cells[1, 5].Value = "IsActive";
                    WorkSheet1.Cells[1, 6].Value = "ValidationMessage";

                    recordIndex = 2;

                    foreach (ProblemReportedByEngDataValidationErrors record in lstProblemReportedByEngFailedToImport)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = record.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 2].Value = record.Segment;
                        WorkSheet1.Cells[recordIndex, 3].Value = record.SubSegment;
                        WorkSheet1.Cells[recordIndex, 4].Value = record.ProblemReportedByEng;
                        WorkSheet1.Cells[recordIndex, 5].Value = record.IsActive;
                        WorkSheet1.Cells[recordIndex, 6].Value = record.ValidationMessage;

                        recordIndex += 1;
                    }

                    WorkSheet1.Column(1).AutoFit();
                    WorkSheet1.Column(2).AutoFit();
                    WorkSheet1.Column(3).AutoFit();
                    WorkSheet1.Column(4).AutoFit();
                    WorkSheet1.Column(5).AutoFit();
                    WorkSheet1.Column(6).AutoFit();

                    excelInvalidData.SaveAs(msInvalidDataFile);
                    msInvalidDataFile.Position = 0;
                    result = msInvalidDataFile.ToArray();
                }
            }

            return result;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportProblemReportedByEngData()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new BaseSearchEntity();

            IEnumerable<ProblemReportedByEng_Response> lstSizeObj = await _problemReportedByEngRepository.GetProblemReportedByEngList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("ProblemReportedByEng");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "ProductCategory";
                    WorkSheet1.Cells[1, 2].Value = "Segment";
                    WorkSheet1.Cells[1, 3].Value = "SubSegment";
                    WorkSheet1.Cells[1, 4].Value = "ProblemReportedByEng";
                    WorkSheet1.Cells[1, 5].Value = "Status";

                    WorkSheet1.Cells[1, 6].Value = "CreatedDate";
                    WorkSheet1.Cells[1, 7].Value = "CreatedBy";


                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.ProblemReportedByEng;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.IsActive == true ? "Active" : "Inactive";

                        WorkSheet1.Cells[recordIndex, 6].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.CreatorName;

                        recordIndex += 1;
                    }

                    WorkSheet1.Column(1).AutoFit();
                    WorkSheet1.Column(2).AutoFit();
                    WorkSheet1.Column(3).AutoFit();
                    WorkSheet1.Column(4).AutoFit();
                    WorkSheet1.Column(5).AutoFit();
                    WorkSheet1.Column(6).AutoFit();
                    WorkSheet1.Column(7).AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Problem Reported By Engg list Exported successfully";
            }

            return _response;
        }
    }
}
