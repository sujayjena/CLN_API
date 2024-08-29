using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IAttendanceRepository _attendanceRepository;
        private IFileManager _fileManager;

        public AttendanceController(IAttendanceRepository attendanceRepository, IFileManager fileManager)
        {
            _attendanceRepository = attendanceRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveAttendance(Attendance_Request parameters)
        {
            int result = await _attendanceRepository.SaveAttendance(parameters);

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
        public async Task<ResponseModel> GetAttendanceList(AttendanceSearch parameters)
        {
            var objList = await _attendanceRepository.GetAttendanceList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetAttendanceById(int userId)
        {
            if (userId <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _attendanceRepository.GetAttendanceById(userId);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportAttendance()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var parameters = new AttendanceSearch()
            {
                CompanyId = 0,
                BranchId = "",
                EmployeeId = 0,
                EmployeeName = ""
            };
            IEnumerable<Attendance_Response> lstSizeObj = await _attendanceRepository.GetAttendanceList(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Attendance");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Employee Name";
                    WorkSheet1.Cells[1, 2].Value = "Punch InOut";
                    WorkSheet1.Cells[1, 3].Value = "Punch Type";
                    WorkSheet1.Cells[1, 4].Value = "Latitude";
                    WorkSheet1.Cells[1, 5].Value = "Longitude";
                    WorkSheet1.Cells[1, 6].Value = "Battery Status";
                    WorkSheet1.Cells[1, 7].Value = "Address";
                    WorkSheet1.Cells[1, 8].Value = "Remark";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.EmployeeName;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.PunchInOut;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.PunchType;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.Latitude;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.Longitude;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.BatteryStatus;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.Address;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.Remark;

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
