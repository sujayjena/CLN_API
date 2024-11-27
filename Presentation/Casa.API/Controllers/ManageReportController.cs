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
    public class ManageReportController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IManageReportRepository _manageReportRepository;

        public ManageReportController(IManageReportRepository manageReportRepository)
        {
            _manageReportRepository = manageReportRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicket_TRC_Report(ManageReport_Search parameters)
        {
            IEnumerable<Ticket_TRC_Report_Response> lstRoles = await _manageReportRepository.GetTicket_TRC_Report(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportTicket_TRC_Report(ManageReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<Ticket_TRC_Report_Response> lstSizeObj = await _manageReportRepository.GetTicket_TRC_Report(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Ticket_TRC_Report");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Ticket/TRC";
                    WorkSheet1.Cells[1, 2].Value = "TRC Location";
                    WorkSheet1.Cells[1, 3].Value = "Ticket Date";
                    WorkSheet1.Cells[1, 4].Value = "Ticket #";
                    WorkSheet1.Cells[1, 5].Value = "TRC Date";
                    WorkSheet1.Cells[1, 6].Value = "TRC #";
                    WorkSheet1.Cells[1, 7].Value = "Customer Name";
                    WorkSheet1.Cells[1, 8].Value = "Caller Name";
                    WorkSheet1.Cells[1, 9].Value = "Region";
                    WorkSheet1.Cells[1, 10].Value = "State";
                    WorkSheet1.Cells[1, 11].Value = "District";
                    WorkSheet1.Cells[1, 12].Value = "City";
                    WorkSheet1.Cells[1, 13].Value = "Product Category";
                    WorkSheet1.Cells[1, 14].Value = "Segment";
                    WorkSheet1.Cells[1, 15].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 16].Value = "Model";
                    WorkSheet1.Cells[1, 17].Value = "BMS Smart/Basic";
                    WorkSheet1.Cells[1, 18].Value = "Product Serial No.";
                    WorkSheet1.Cells[1, 19].Value = "Manufacturing Date";
                    WorkSheet1.Cells[1, 20].Value = "Warranty Status";
                    WorkSheet1.Cells[1, 21].Value = "Problem Reported By Customer";
                    WorkSheet1.Cells[1, 22].Value = "Problem Observed By Service Engineer";
                    WorkSheet1.Cells[1, 23].Value = "Rectification Action";
                    WorkSheet1.Cells[1, 24].Value = "Status";
                    WorkSheet1.Cells[1, 25].Value = "Resolved Date";
                    WorkSheet1.Cells[1, 26].Value = "CSAT Date";
                    WorkSheet1.Cells[1, 27].Value = "Closure Date";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketType;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TRCLocation;
                        WorkSheet1.Cells[recordIndex, 3].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.TicketDate;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.TicketNumber;
                        WorkSheet1.Cells[recordIndex, 5].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.TRCDate;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.TRCNumber;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.CallerName;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.CallerRegionName;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.CallerStateName;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.CallerDistrictName;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.CallerCityName;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 16].Value = items.ProductModel;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.TypeOfBMS;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.ProductSerialNumber;
                        WorkSheet1.Cells[recordIndex, 19].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.DateofManufacturing;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.WarrantyStatus;
                        WorkSheet1.Cells[recordIndex, 21].Value = items.ProbReportedByCust;
                        WorkSheet1.Cells[recordIndex, 22].Value = items.ProblemObservedByEng;
                        WorkSheet1.Cells[recordIndex, 23].Value = items.RectificationAction;
                        WorkSheet1.Cells[recordIndex, 24].Value = items.TicketStatus;
                        WorkSheet1.Cells[recordIndex, 25].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 25].Value = items.ResolvedDate;
                        WorkSheet1.Cells[recordIndex, 26].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 26].Value = items.CSATDate;
                        WorkSheet1.Cells[recordIndex, 27].Value = items.ClosureDate;

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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerWiseReport(ManageReport_Search parameters)
        {
            IEnumerable<CustomerWiseReport_Response> lstRoles = await _manageReportRepository.GetCustomerWiseReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportCustomerWiseReport(ManageReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<CustomerWiseReport_Response> lstSizeObj = await _manageReportRepository.GetCustomerWiseReport(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("CustomerWiseReport");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Product Category";
                    WorkSheet1.Cells[1, 3].Value = "Segment";
                    WorkSheet1.Cells[1, 4].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 5].Value = "No of issue";
                    WorkSheet1.Cells[1, 6].Value = "Open issue";
                    WorkSheet1.Cells[1, 7].Value = "Close issue";
                   
                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.NoofIssue;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.OpenIssue;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.CloseIssue;

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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerSatisfactionReport(ManageReport_Search parameters)
        {
            IEnumerable<CustomerSatisfactionReport_Response> lstRoles = await _manageReportRepository.GetCustomerSatisfactionReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportCustomerSatisfactionReport(ManageReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<CustomerSatisfactionReport_Response> lstSizeObj = await _manageReportRepository.GetCustomerSatisfactionReport(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("CustomerSatisfactionReport");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Ticket Number";
                    WorkSheet1.Cells[1, 2].Value = "TRC Number";
                    WorkSheet1.Cells[1, 3].Value = "Closed By";
                    WorkSheet1.Cells[1, 4].Value = "Ticket/TRC Closer Date";
                    WorkSheet1.Cells[1, 5].Value = "CSAT Date";
                    WorkSheet1.Cells[1, 6].Value = "Overall Experience";
                    WorkSheet1.Cells[1, 7].Value = "Satisfaction";
                    WorkSheet1.Cells[1, 8].Value = "Customer Service";
                    WorkSheet1.Cells[1, 9].Value = "Timeliness";
                    WorkSheet1.Cells[1, 10].Value = "Resolution";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketNumber;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TRCNumber;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.ClosedBy;
                        WorkSheet1.Cells[recordIndex, 4].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.ClosedDate;
                        WorkSheet1.Cells[recordIndex, 5].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.CSATDate;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.OverallExperience;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.Satisfaction;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.CustomerService;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.Timeliness;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.Resolution;

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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetFTFReport(ManageReport_Search parameters)
        {
            IEnumerable<FTFReport_Response> lstRoles = await _manageReportRepository.GetFTFReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportFTFReport(ManageReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<FTFReport_Response> lstSizeObj = await _manageReportRepository.GetFTFReport(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("FTFReport");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Date";
                    WorkSheet1.Cells[1, 2].Value = "Total Requests";
                    WorkSheet1.Cells[1, 3].Value = "Resolved on First Attempt";
                    WorkSheet1.Cells[1, 4].Value = "FTF Rate (%)";
                    WorkSheet1.Cells[1, 5].Value = "Customer Satisfaction (CSAT) Score";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketDate;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TotalRequest;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.ResolvedTickets;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.FTFRatePerct;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.CSATScore;
                        
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetLogisticSummaryReport(ManageReport_Search parameters)
        {
            IEnumerable<LogisticSummaryReport_Response> lstRoles = await _manageReportRepository.GetLogisticSummaryReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportLogisticSummaryReport(ManageReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<LogisticSummaryReport_Response> lstSizeObj = await _manageReportRepository.GetLogisticSummaryReport(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("LogisticSummaryReport");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Complaint Number";
                    WorkSheet1.Cells[1, 2].Value = "Receive Date";
                    WorkSheet1.Cells[1, 3].Value = "Receive Mode";
                    WorkSheet1.Cells[1, 4].Value = "Document No [Inward]";
                    WorkSheet1.Cells[1, 5].Value = "Region";
                    WorkSheet1.Cells[1, 6].Value = "State";
                    WorkSheet1.Cells[1, 7].Value = "District";
                    WorkSheet1.Cells[1, 8].Value = "City";
                    WorkSheet1.Cells[1, 9].Value = "Customer";
                    WorkSheet1.Cells[1, 10].Value = "Product Category";
                    WorkSheet1.Cells[1, 11].Value = "Segment";
                    WorkSheet1.Cells[1, 12].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 13].Value = "Model";
                    WorkSheet1.Cells[1, 14].Value = "Product Sr No";
                    WorkSheet1.Cells[1, 15].Value = "Dispatched Date";
                    WorkSheet1.Cells[1, 16].Value = "Dispatch Status";
                    WorkSheet1.Cells[1, 17].Value = "Dispatch Mode";
                    WorkSheet1.Cells[1, 18].Value = "Dispatch Address";
                    WorkSheet1.Cells[1, 19].Value = "Dispatched Challan No.";
                    WorkSheet1.Cells[1, 20].Value = "Dispatched Docket No.";
                    WorkSheet1.Cells[1, 21].Value = "Courier Name";
                    WorkSheet1.Cells[1, 22].Value = "Customer Receiving Date";
                  

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketNumber;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.ReceivedDate;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.ReceiveMode;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.DocumentNo;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.RegionName;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.StateName;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.DistrictName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.CityName;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.ProductModel;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.ProductSerialNumber;
                        WorkSheet1.Cells[recordIndex, 15].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.DispatchedDate;
                        WorkSheet1.Cells[recordIndex, 16].Value = items.DispatchStatus;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.DispatchMode;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.DispatchAddress;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.DispatchChallanNo;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.DispatchedDocketNo;
                        WorkSheet1.Cells[recordIndex, 21].Value = items.CourierName;
                        WorkSheet1.Cells[recordIndex, 22].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 22].Value = items.CustomerReceivingDate;

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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseReport(ManageReport_Search parameters)
        {
            IEnumerable<ExpenseReport_Response> lstRoles = await _manageReportRepository.GetExpenseReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }
    }
}
