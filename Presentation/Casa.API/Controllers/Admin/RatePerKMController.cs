using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Globalization;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatePerKMController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IRatePerKMRepository _ratePerKMRepository;

        public RatePerKMController(IRatePerKMRepository ratePerKMRepository)
        {
            _ratePerKMRepository = ratePerKMRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveRatePerKM(RatePerKM_Request parameters)
        {
            int result = await _ratePerKMRepository.SaveRatePerKM(parameters);

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
        public async Task<ResponseModel> GetRatePerKMList(RatePerKMSearch_Request parameters)
        {
            IEnumerable<RatePerKM_Response> lstRoles = await _ratePerKMRepository.GetRatePerKMList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetRatePerKMById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ratePerKMRepository.GetRatePerKMById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportRatePerKMData()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new RatePerKMSearch_Request();
            request.VehicleTypeId = 0;
            request.EmployeeLevelId = 0;

            IEnumerable<RatePerKM_Response> lstSizeObj = await _ratePerKMRepository.GetRatePerKMList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("RatePerKM");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Distance (KM)";
                    WorkSheet1.Cells[1, 2].Value = "Rate";
                    WorkSheet1.Cells[1, 3].Value = "Vehicle Type";
                    WorkSheet1.Cells[1, 4].Value = "Employee Grade";
                    WorkSheet1.Cells[1, 5].Value = "Status";

                    WorkSheet1.Cells[1, 6].Value = "CreatedDate";
                    WorkSheet1.Cells[1, 7].Value = "CreatedBy";


                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.Distance;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.Rate;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.VehicleType;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.EmployeeLevel;
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
                _response.Message = "Rate Per KM list Exported successfully";
            }

            return _response;
        }
    }
}
