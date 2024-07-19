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
    public class ExpenseTypeController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        private readonly IFileManager _fileManager;

        public ExpenseTypeController(IExpenseTypeRepository expenseTypeRepository, IFileManager fileManager)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Expense Type
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveExpenseType(ExpenseType_Request parameters)
        {
            int result = await _expenseTypeRepository.SaveExpenseType(parameters);

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
        public async Task<ResponseModel> GetExpenseTypeList(BaseSearchEntity parameters)
        {
            IEnumerable<ExpenseType_Response> lstRoles = await _expenseTypeRepository.GetExpenseTypeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseTypeById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _expenseTypeRepository.GetExpenseTypeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Expense Matrix

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveExpenseMatrix(ExpenseMatrix_Request parameters)
        {
            int result = await _expenseTypeRepository.SaveExpenseMatrix(parameters);

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
        public async Task<ResponseModel> GetExpenseMatrixList(ExpenseMatrixSearch_Request parameters)
        {
            IEnumerable<ExpenseMatrix_Response> lstRoles = await _expenseTypeRepository.GetExpenseMatrixList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseMatrixById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _expenseTypeRepository.GetExpenseMatrixById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadExpenseMatrixTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_ExpenseMatrix.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportExpenseMatrix([FromQuery] ImportRequest request)
        {
            _response.IsSuccess = false;
            List<string[]> data = new List<string[]>();
            ExcelWorksheets currentSheet;
            ExcelWorksheet workSheet;
            List<ImportedExpenseMatrix> lstImportedExpenseMatrix = new List<ImportedExpenseMatrix>();
            IEnumerable<ExpenseMatrixDataValidationErrors> lstExpenseMatrixFailedToImport;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            int noOfCol, noOfRow;

            if (request.FileUpload == null || request.FileUpload.Length == 0)
            {
                _response.Message = "Please upload an excel file to import Expense Matrix data";
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

                if (!string.Equals(workSheet.Cells[1, 1].Value.ToString(), "EmployeeLevel", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 2].Value.ToString(), "ExpenseType", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 3].Value.ToString(), "CityGrade", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 4].Value.ToString(), "Amount", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 5].Value.ToString(), "IsActive", StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Please upload a valid excel file. Please Download Format file for reference";
                    return _response;
                }

                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    lstImportedExpenseMatrix.Add(new ImportedExpenseMatrix()
                    {
                        EmployeeLevel = workSheet.Cells[rowIterator, 1].Value?.ToString(),
                        ExpenseType = workSheet.Cells[rowIterator, 2].Value?.ToString(),
                        CityGrade = workSheet.Cells[rowIterator, 3].Value?.ToString(),
                        Amount = workSheet.Cells[rowIterator, 4].Value?.ToString(),
                        IsActive = workSheet.Cells[rowIterator, 5].Value?.ToString()
                    });
                }
            }

            if (lstImportedExpenseMatrix.Count == 0)
            {
                _response.Message = "File does not contains any record(s)";
                return _response;
            }

            lstExpenseMatrixFailedToImport = await _expenseTypeRepository.ImportExpenseMatrix(lstImportedExpenseMatrix);

            _response.IsSuccess = true;
            _response.Message = "Expense Matrix list imported successfully";

            #region Generate Excel file for Invalid Data
            if (lstExpenseMatrixFailedToImport.ToList().Count > 0)
            {
                _response.Message = "Uploaded file contains invalid records, please check downloaded file for more details";
                _response.Data = GenerateInvalidExpenseMatrixDataFile(lstExpenseMatrixFailedToImport);

            }
            #endregion

            return _response;
        }

        private byte[] GenerateInvalidExpenseMatrixDataFile(IEnumerable<ExpenseMatrixDataValidationErrors> lstExpenseMatrixFailedToImport)
        {
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;

            using (MemoryStream msInvalidDataFile = new MemoryStream())
            {
                using (ExcelPackage excelInvalidData = new ExcelPackage())
                {
                    WorkSheet1 = excelInvalidData.Workbook.Worksheets.Add("Invalid_ExpenseMatrix_Records");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "EmployeeLevel";
                    WorkSheet1.Cells[1, 2].Value = "ExpenseType";
                    WorkSheet1.Cells[1, 3].Value = "CityGrade";
                    WorkSheet1.Cells[1, 4].Value = "Amount";
                    WorkSheet1.Cells[1, 5].Value = "IsActive";
                    WorkSheet1.Cells[1, 6].Value = "ValidationMessage";

                    recordIndex = 2;

                    foreach (ExpenseMatrixDataValidationErrors record in lstExpenseMatrixFailedToImport)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = record.EmployeeLevel;
                        WorkSheet1.Cells[recordIndex, 2].Value = record.ExpenseType;
                        WorkSheet1.Cells[recordIndex, 3].Value = record.CityGrade;
                        WorkSheet1.Cells[recordIndex, 4].Value = record.Amount;
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
        public async Task<ResponseModel> ExportExpenseMatrixData()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new ExpenseMatrixSearch_Request();
            request.EmployeeLevelId = 0;
            request.ExpenseTypeId = 0;
            request.CityGradeId = 0;

            IEnumerable<ExpenseMatrix_Response> lstSizeObj = await _expenseTypeRepository.GetExpenseMatrixList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("ExpenseMatrix");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "EmployeeLevel";
                    WorkSheet1.Cells[1, 2].Value = "ExpenseType";
                    WorkSheet1.Cells[1, 3].Value = "CityGrade";
                    WorkSheet1.Cells[1, 4].Value = "Amount";
                    WorkSheet1.Cells[1, 5].Value = "Status";

                    WorkSheet1.Cells[1, 6].Value = "CreatedDate";
                    WorkSheet1.Cells[1, 7].Value = "CreatedBy";


                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.EmployeeLevel;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.ExpenseType;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.CityGrade;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.Amount;
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
                _response.Message = "Expense Matrix list Exported successfully";
            }

            return _response;
        }
        #endregion
    }
}
