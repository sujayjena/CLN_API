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
    public class SpareDetailsController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;
        private readonly ISpareDetailsRepository _spareDetailsRepository;

        public SpareDetailsController(IFileManager fileManager, ISpareDetailsRepository spareDetailsRepository)
        {
            _spareDetailsRepository = spareDetailsRepository;

            _response = new ResponseModel();
            _fileManager = fileManager;
            _response.IsSuccess = true;
        }

        #region SpareDetails 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSpareDetails(SpareDetails_Request parameters)
        {
            int result = await _spareDetailsRepository.SaveSpareDetails(parameters);

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
        public async Task<ResponseModel> GetSpareDetailsList(SpareDetails_Search parameters)
        {
            IEnumerable<SpareDetails_Response> lstSpareDetailss = await _spareDetailsRepository.GetSpareDetailsList(parameters);
            _response.Data = lstSpareDetailss.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSpareDetailsById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _spareDetailsRepository.GetSpareDetailsById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadSpareDetailsTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_SpareDetails.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportSpareDetails([FromQuery] ImportRequest request)
        {
            _response.IsSuccess = false;

            ExcelWorksheets currentSheet;
            ExcelWorksheet workSheet;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            int noOfCol, noOfRow;

            List<string[]> data = new List<string[]>();
            List<SpareDetails_ImportData> lstSpareDetails_ImportData = new List<SpareDetails_ImportData>();
            IEnumerable<SpareDetails_ImportDataValidation> lstSpareDetails_ImportDataValidation;

            if (request.FileUpload == null || request.FileUpload.Length == 0)
            {
                _response.Message = "Please upload an excel file to import Problem Reported By Cust data";
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

                if (!string.Equals(workSheet.Cells[1, 1].Value.ToString(), "PartCode", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 2].Value.ToString(), "Description", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 3].Value.ToString(), "UOM", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 4].Value.ToString(), "MinQty", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 5].Value.ToString(), "RGP", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 6].Value.ToString(), "IsActive", StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Please upload a valid excel file. Please Download Format file for reference";
                    return _response;
                }

                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    lstSpareDetails_ImportData.Add(new SpareDetails_ImportData()
                    {
                        PartCode = workSheet.Cells[rowIterator, 1].Value?.ToString(),
                        Description = workSheet.Cells[rowIterator, 2].Value?.ToString(),
                        UOM = workSheet.Cells[rowIterator, 3].Value?.ToString(),
                        MinQty = workSheet.Cells[rowIterator, 4].Value?.ToString(),
                        RGP = workSheet.Cells[rowIterator, 5].Value?.ToString(),
                        IsActive = workSheet.Cells[rowIterator, 6].Value?.ToString()
                    });
                }
            }

            if (lstSpareDetails_ImportData.Count == 0)
            {
                _response.Message = "File does not contains any record(s)";
                return _response;
            }

            lstSpareDetails_ImportDataValidation = await _spareDetailsRepository.ImportProblemReportedByCustsDetails(lstSpareDetails_ImportData);

            _response.IsSuccess = true;
            _response.Message = "Record imported successfully";

            #region Generate Excel file for Invalid Data

            if (lstSpareDetails_ImportDataValidation.ToList().Count > 0)
            {
                _response.Message = "Uploaded file contains invalid records, please check downloaded file for more details";
                _response.Data = GenerateInvalidImportDataFile(lstSpareDetails_ImportDataValidation);

            }

            #endregion

            return _response;
        }

        private byte[] GenerateInvalidImportDataFile(IEnumerable<SpareDetails_ImportDataValidation> lstSpareDetails_ImportDataValidation)
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

                    WorkSheet1.Cells[1, 1].Value = "PartCode";
                    WorkSheet1.Cells[1, 2].Value = "Description";
                    WorkSheet1.Cells[1, 3].Value = "UOM";
                    WorkSheet1.Cells[1, 4].Value = "MinQty";
                    WorkSheet1.Cells[1, 5].Value = "RGP";
                    WorkSheet1.Cells[1, 6].Value = "IsActive";
                    WorkSheet1.Cells[1, 7].Value = "ErrorMessage";

                    recordIndex = 2;

                    foreach (SpareDetails_ImportDataValidation record in lstSpareDetails_ImportDataValidation)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = record.PartCode;
                        WorkSheet1.Cells[recordIndex, 2].Value = record.Description;
                        WorkSheet1.Cells[recordIndex, 3].Value = record.UOM;
                        WorkSheet1.Cells[recordIndex, 4].Value = record.MinQty;
                        WorkSheet1.Cells[recordIndex, 5].Value = record.RGP;
                        WorkSheet1.Cells[recordIndex, 6].Value = record.IsActive;
                        WorkSheet1.Cells[recordIndex, 7].Value = record.ValidationMessage;

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

        #endregion
    }
}
