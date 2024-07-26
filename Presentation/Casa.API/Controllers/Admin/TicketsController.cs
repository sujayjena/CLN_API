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
    public class TicketsController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsController(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Ticket Category

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTicketCategory(TicketCategory_Request parameters)
        {
            int result = await _ticketsRepository.SaveTicketCategory(parameters);

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
            else if (result == -3)
            {
                _response.Message = "Sequence number already exists";
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
        public async Task<ResponseModel> GetTicketCategoryList(BaseSearchEntity parameters)
        {
            IEnumerable<TicketCategory_Response> lstRoles = await _ticketsRepository.GetTicketCategoryList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketCategoryById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ticketsRepository.GetTicketCategoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Ticket Status

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTicketStatus(TicketStatus_Request parameters)
        {
            int result = await _ticketsRepository.SaveTicketStatus(parameters);

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
            else if (result == -3)
            {
                _response.Message = "Sequence number already exists";
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
        public async Task<ResponseModel> GetTicketStatusList(BaseSearchEntity parameters)
        {
            IEnumerable<TicketStatus_Response> lstRoles = await _ticketsRepository.GetTicketStatusList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketStatusById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ticketsRepository.GetTicketStatusById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportTicketStatusData()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new BaseSearchEntity();

            IEnumerable<TicketStatus_Response> lstSizeObj = await _ticketsRepository.GetTicketStatusList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("TicketStatus");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Ticket Status";
                    WorkSheet1.Cells[1, 2].Value = "Sequence";
                    WorkSheet1.Cells[1, 3].Value = "Status";

                    WorkSheet1.Cells[1, 4].Value = "CreatedDate";
                    WorkSheet1.Cells[1, 5].Value = "CreatedBy";


                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketStatus;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.SequenceNo;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.IsActive == true ? "Active" : "Inactive";

                        WorkSheet1.Cells[recordIndex, 4].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.CreatorName;

                        recordIndex += 1;
                    }

                    WorkSheet1.Column(1).AutoFit();
                    WorkSheet1.Column(2).AutoFit();
                    WorkSheet1.Column(3).AutoFit();
                    WorkSheet1.Column(4).AutoFit();
                    WorkSheet1.Column(5).AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Ticket Status list Exported successfully";
            }

            return _response;
        }

        #endregion

        #region Ticket Type

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTicketType(TicketType_Request parameters)
        {
            int result = await _ticketsRepository.SaveTicketType(parameters);

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
        public async Task<ResponseModel> GetTicketTypeList(BaseSearchEntity parameters)
        {
            IEnumerable<TicketType_Response> lstRoles = await _ticketsRepository.GetTicketTypeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketTypeById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ticketsRepository.GetTicketTypeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Ticket Status Matrix

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTicketStatusMatrix(TicketStatusMatrix_Request parameters)
        {
            int result = await _ticketsRepository.SaveTicketStatusMatrix(parameters);

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
            else if (result == -3)
            {
                _response.Message = "Sequence number already exists";
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
        public async Task<ResponseModel> GetTicketStatusMatrixList(BaseSearchEntity parameters)
        {
            IEnumerable<TicketStatusMatrix_Response> lstRoles = await _ticketsRepository.GetTicketStatusMatrixList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketStatusMatrixById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ticketsRepository.GetTicketStatusMatrixById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportTicketStatusMatrixData()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new BaseSearchEntity();

            IEnumerable<TicketStatusMatrix_Response> lstSizeObj = await _ticketsRepository.GetTicketStatusMatrixList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("TicketStatusMatrix");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Ticket Status";
                    WorkSheet1.Cells[1, 2].Value = "Ticket Priority";
                    WorkSheet1.Cells[1, 3].Value = "Sequence";
                    WorkSheet1.Cells[1, 4].Value = "SLA Days";
                    WorkSheet1.Cells[1, 5].Value = "SLA Hrs";
                    WorkSheet1.Cells[1, 6].Value = "SLA Min";
                    WorkSheet1.Cells[1, 7].Value = "Status";

                    WorkSheet1.Cells[1, 8].Value = "CreatedDate";
                    WorkSheet1.Cells[1, 9].Value = "CreatedBy";


                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketStatus;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TicketCategory;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.SequenceNo;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.SLADays;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.SLAHours;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.SLAMin;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.IsActive == true ? "Active" : "Inactive";

                        WorkSheet1.Cells[recordIndex, 8].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.CreatorName;

                        recordIndex += 1;
                    }

                    WorkSheet1.Column(1).AutoFit();
                    WorkSheet1.Column(2).AutoFit();
                    WorkSheet1.Column(3).AutoFit();
                    WorkSheet1.Column(4).AutoFit();
                    WorkSheet1.Column(5).AutoFit();
                    WorkSheet1.Column(6).AutoFit();
                    WorkSheet1.Column(7).AutoFit();
                    WorkSheet1.Column(8).AutoFit();
                    WorkSheet1.Column(9).AutoFit();

                    excelExportData.SaveAs(msExportDataFile);
                    msExportDataFile.Position = 0;
                    result = msExportDataFile.ToArray();
                }
            }

            if (result != null)
            {
                _response.Data = result;
                _response.IsSuccess = true;
                _response.Message = "Ticket Status Matrix list Exported successfully";
            }

            return _response;
        }

        #endregion
    }
}
