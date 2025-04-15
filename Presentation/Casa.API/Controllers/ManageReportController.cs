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
                    WorkSheet1.Cells[1, 4].Value = "Ticket Created By";
                    WorkSheet1.Cells[1, 5].Value = "Ticket #";
                    WorkSheet1.Cells[1, 6].Value = "TRC Date";
                    WorkSheet1.Cells[1, 7].Value = "TRC #";
                    WorkSheet1.Cells[1, 8].Value = "Customer Name";
                    WorkSheet1.Cells[1, 9].Value = "Caller Name";
                    WorkSheet1.Cells[1, 10].Value = "Caller Contact No.";
                    WorkSheet1.Cells[1, 11].Value = "Region";
                    WorkSheet1.Cells[1, 12].Value = "State";
                    WorkSheet1.Cells[1, 13].Value = "District";
                    WorkSheet1.Cells[1, 14].Value = "City";
                    WorkSheet1.Cells[1, 15].Value = "Product Category";
                    WorkSheet1.Cells[1, 16].Value = "Segment";
                    WorkSheet1.Cells[1, 17].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 18].Value = "Model";
                    WorkSheet1.Cells[1, 19].Value = "BMS Smart/Basic";
                    WorkSheet1.Cells[1, 20].Value = "Old";
                    WorkSheet1.Cells[1, 21].Value = "Product Serial No.";
                    WorkSheet1.Cells[1, 22].Value = "Manufacturing Date";
                    WorkSheet1.Cells[1, 23].Value = "Warranty Status";
                    WorkSheet1.Cells[1, 24].Value = "Problem Reported By Customer";
                    WorkSheet1.Cells[1, 25].Value = "Problem Observed By Engineer";
                    WorkSheet1.Cells[1, 26].Value = "Problem Observed By Service Engineer";
                    WorkSheet1.Cells[1, 27].Value = "Problem Observed By TRC Engineer";
                    WorkSheet1.Cells[1, 28].Value = "Rectification Action";
                    WorkSheet1.Cells[1, 29].Value = "Status";
                    WorkSheet1.Cells[1, 30].Value = "Resolved Date";
                    WorkSheet1.Cells[1, 31].Value = "CSAT Date";
                    WorkSheet1.Cells[1, 32].Value = "CSAT Status";
                    WorkSheet1.Cells[1, 33].Value = "CSAT Average";
                    WorkSheet1.Cells[1, 34].Value = "Closure Date";
                    WorkSheet1.Cells[1, 35].Value = "Ticket Aging";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketType;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TRCLocation;
                        WorkSheet1.Cells[recordIndex, 3].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.TicketDate;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.TicketCreatedBy;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.TicketNumber;
                        WorkSheet1.Cells[recordIndex, 6].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.TRCDate;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.TRCNumber;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.CallerName;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.CallerMobileNo;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.CallerRegionName;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.CallerStateName;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.CallerDistrictName;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.CallerCityName;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 16].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.ProductModel;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.TypeOfBMS;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.IsOldProduct == true ? "Yes" : "No";
                        WorkSheet1.Cells[recordIndex, 21].Value = items.ProductSerialNumber;
                        WorkSheet1.Cells[recordIndex, 22].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 22].Value = items.DateofManufacturing;
                        WorkSheet1.Cells[recordIndex, 23].Value = items.WarrantyStatus;
                        WorkSheet1.Cells[recordIndex, 24].Value = items.ProbReportedByCust;
                        WorkSheet1.Cells[recordIndex, 25].Value = items.ProblemObservedByEng;
                        WorkSheet1.Cells[recordIndex, 26].Value = items.ProblemObservedByServiceEng;
                        WorkSheet1.Cells[recordIndex, 27].Value = items.ProblemObservedByTRCEng;
                        WorkSheet1.Cells[recordIndex, 28].Value = items.RectificationAction;
                        WorkSheet1.Cells[recordIndex, 29].Value = items.TicketStatus;
                        WorkSheet1.Cells[recordIndex, 30].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 30].Value = items.ResolvedDate;
                        WorkSheet1.Cells[recordIndex, 31].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 31].Value = items.CSATDate;
                        WorkSheet1.Cells[recordIndex, 32].Value = items.CSATStatus;
                        WorkSheet1.Cells[recordIndex, 33].Value = items.CSATAverage;
                        WorkSheet1.Cells[recordIndex, 34].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 34].Value = items.ClosureDate;
                        WorkSheet1.Cells[recordIndex, 35].Value = items.TicketAging;

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

                    WorkSheet1.Cells[1, 1].Value = "Ticket Number";
                    WorkSheet1.Cells[1, 2].Value = "Ticket Date";
                    WorkSheet1.Cells[1, 3].Value = "TRC Number";
                    WorkSheet1.Cells[1, 4].Value = "TRC Date";
                    WorkSheet1.Cells[1, 5].Value = "Receive Date";
                    WorkSheet1.Cells[1, 6].Value = "Reverse Pickup Mode";
                    WorkSheet1.Cells[1, 7].Value = "Docket Details";
                    WorkSheet1.Cells[1, 8].Value = "Region";
                    WorkSheet1.Cells[1, 9].Value = "State";
                    WorkSheet1.Cells[1, 10].Value = "District";
                    WorkSheet1.Cells[1, 11].Value = "City";
                    WorkSheet1.Cells[1, 12].Value = "Customer";
                    WorkSheet1.Cells[1, 13].Value = "Product Category";
                    WorkSheet1.Cells[1, 14].Value = "Segment";
                    WorkSheet1.Cells[1, 15].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 16].Value = "Model";
                    WorkSheet1.Cells[1, 17].Value = "Product Sr No";
                    WorkSheet1.Cells[1, 18].Value = "Dispatched Date";
                    WorkSheet1.Cells[1, 19].Value = "Dispatch Status";
                    WorkSheet1.Cells[1, 20].Value = "Dispatch Mode";
                    WorkSheet1.Cells[1, 21].Value = "Dispatch Address";
                    WorkSheet1.Cells[1, 22].Value = "Dispatched Challan No.";
                    WorkSheet1.Cells[1, 23].Value = "Dispatched Docket No.";
                    WorkSheet1.Cells[1, 24].Value = "Courier Name";
                    WorkSheet1.Cells[1, 25].Value = "Customer Receiving Date";
                  
                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketNumber;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TicketDate;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.TRCNumber;
                        WorkSheet1.Cells[recordIndex, 4].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.TRCDate;
                        WorkSheet1.Cells[recordIndex, 5].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.ReceivedDate;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.ReceiveMode;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.DocumentNo;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.RegionName;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.StateName;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.DistrictName;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.CityName;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 16].Value = items.ProductModel;
                        WorkSheet1.Cells[recordIndex, 17].Value = items.ProductSerialNumber;
                        WorkSheet1.Cells[recordIndex, 18].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 18].Value = items.DispatchedDate;
                        WorkSheet1.Cells[recordIndex, 19].Value = items.DispatchStatus;
                        WorkSheet1.Cells[recordIndex, 20].Value = items.DispatchMode;
                        WorkSheet1.Cells[recordIndex, 21].Value = items.DispatchAddress;
                        WorkSheet1.Cells[recordIndex, 22].Value = items.DispatchChallanNo;
                        WorkSheet1.Cells[recordIndex, 23].Value = items.DispatchedDocketNo;
                        WorkSheet1.Cells[recordIndex, 24].Value = items.CourierName;
                        WorkSheet1.Cells[recordIndex, 25].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 25].Value = items.CustomerReceivingDate;

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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportExpenseReport(ManageReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<ExpenseReport_Response> lstSizeObj = await _manageReportRepository.GetExpenseReport(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("ExpenseReport");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Ticket Number";
                    WorkSheet1.Cells[1, 2].Value = "TRC Number";
                    WorkSheet1.Cells[1, 3].Value = "Ticket/TRC";
                    WorkSheet1.Cells[1, 4].Value = "TRC location";
                    WorkSheet1.Cells[1, 5].Value = "Product Category";
                    WorkSheet1.Cells[1, 6].Value = "Segment";
                    WorkSheet1.Cells[1, 7].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 8].Value = "Model";
                    WorkSheet1.Cells[1, 9].Value = "Product Serial No.";
                    WorkSheet1.Cells[1, 10].Value = "Total Part Price";
                    WorkSheet1.Cells[1, 11].Value = "Expense Amount";
                    WorkSheet1.Cells[1, 12].Value = "Total Cost";
                   
                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketNumber;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TRCNumber;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.TicketType;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.TRCLocation;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.ProductModel;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.ProductSerialNumber;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.TotalPartPrice;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.TotalExpense;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.TotalCost;
                      
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
        public async Task<ResponseModel> GetTicketActivityReport(ManageReport_Search parameters)
        {
            IEnumerable<TicketActivityReport_Response> lstRoles = await _manageReportRepository.GetTicketActivityReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportTicketActivityReport(ManageReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<TicketActivityReport_Response> lstSizeObj = await _manageReportRepository.GetTicketActivityReport(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("TicketActivityReport");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Ticket Number";
                    WorkSheet1.Cells[1, 2].Value = "Ticket Date";
                    WorkSheet1.Cells[1, 3].Value = "Ticket Time";
                    WorkSheet1.Cells[1, 4].Value = "TRC Number";
                    WorkSheet1.Cells[1, 5].Value = "TRC Date";
                    WorkSheet1.Cells[1, 6].Value = "Ticket Priority";
                    WorkSheet1.Cells[1, 7].Value = "Ticket SLA Days";
                    WorkSheet1.Cells[1, 8].Value = "Ticket SLA Hours";
                    WorkSheet1.Cells[1, 9].Value = "Ticket SLA Min";
                    WorkSheet1.Cells[1, 10].Value = "SLA Status";
                    WorkSheet1.Cells[1, 11].Value = "Ticket Aging";
                    WorkSheet1.Cells[1, 12].Value = "Logging Source";
                    WorkSheet1.Cells[1, 13].Value = "Caller Type";
                    WorkSheet1.Cells[1, 14].Value = "Caller Name";
                    WorkSheet1.Cells[1, 15].Value = "Caller Mobile";
                    WorkSheet1.Cells[1, 16].Value = "Caller EmailId";
                    WorkSheet1.Cells[1, 17].Value = "Caller Address1";
                    WorkSheet1.Cells[1, 18].Value = "Caller Region Name";
                    WorkSheet1.Cells[1, 19].Value = "Caller State Name";
                    WorkSheet1.Cells[1, 20].Value = "Caller District Name";
                    WorkSheet1.Cells[1, 21].Value = "Caller City Name";
                    WorkSheet1.Cells[1, 22].Value = "Caller PinCode";
                    WorkSheet1.Cells[1, 23].Value = "Caller Remarks";
                    WorkSheet1.Cells[1, 24].Value = "IsSiteAddressSameAsCaller";
                    WorkSheet1.Cells[1, 25].Value = "Complaint Type";
                    WorkSheet1.Cells[1, 26].Value = "IsOldProduct";
                    WorkSheet1.Cells[1, 27].Value = "Product Serial Number";
                    WorkSheet1.Cells[1, 28].Value = "Customer Type";
                    WorkSheet1.Cells[1, 29].Value = "Customer Name";
                    WorkSheet1.Cells[1, 30].Value = "Customer Mobile";
                    WorkSheet1.Cells[1, 31].Value = "Customer Email";
                    WorkSheet1.Cells[1, 32].Value = "Customer Address1";
                    WorkSheet1.Cells[1, 33].Value = "Customer Region Name";
                    WorkSheet1.Cells[1, 34].Value = "Customer State Name";
                    WorkSheet1.Cells[1, 35].Value = "Customer District Name";
                    WorkSheet1.Cells[1, 36].Value = "Customer City Name";
                    WorkSheet1.Cells[1, 37].Value = "Customer PinCode";
                    WorkSheet1.Cells[1, 38].Value = "Site Customer Name";
                    WorkSheet1.Cells[1, 39].Value = "Site Contact Name";
                    WorkSheet1.Cells[1, 40].Value = "Site Contact Mobile";
                    WorkSheet1.Cells[1, 41].Value = "Site Customer Address1";
                    WorkSheet1.Cells[1, 42].Value = "Site Customer Region Name";
                    WorkSheet1.Cells[1, 43].Value = "Site Customer State Name";
                    WorkSheet1.Cells[1, 44].Value = "Site Customer District Name";
                    WorkSheet1.Cells[1, 45].Value = "Site Customer City Name";
                    WorkSheet1.Cells[1, 46].Value = "Site Customer PinCode";
                    WorkSheet1.Cells[1, 47].Value = "Battery BOM Number";
                    WorkSheet1.Cells[1, 48].Value = "Product Category";
                    WorkSheet1.Cells[1, 49].Value = "Segment";
                    WorkSheet1.Cells[1, 50].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 51].Value = "Product Model";
                    WorkSheet1.Cells[1, 52].Value = "Cell Chemistry";
                    WorkSheet1.Cells[1, 53].Value = "Date of Manufacturing";
                    WorkSheet1.Cells[1, 54].Value = "Problem Reported By Cust";
                    WorkSheet1.Cells[1, 55].Value = "Problem Description";
                    WorkSheet1.Cells[1, 56].Value = "Warranty Start Date";
                    WorkSheet1.Cells[1, 57].Value = "Warranty End Date";
                    WorkSheet1.Cells[1, 58].Value = "Warranty Status";
                    WorkSheet1.Cells[1, 59].Value = "Warranty Type";
                    WorkSheet1.Cells[1, 60].Value = "IsTrackingDeviceRequired";
                    WorkSheet1.Cells[1, 61].Value = "Tracking Device Name";
                    WorkSheet1.Cells[1, 62].Value = "Make Name";
                    WorkSheet1.Cells[1, 63].Value = "Device ID";
                    WorkSheet1.Cells[1, 64].Value = "IMEI No";
                    WorkSheet1.Cells[1, 65].Value = "SIM No";
                    WorkSheet1.Cells[1, 66].Value = "SIM Provider Name";
                    WorkSheet1.Cells[1, 67].Value = "Platform Name";
                    WorkSheet1.Cells[1, 68].Value = "Technical Support Engg";
                    WorkSheet1.Cells[1, 69].Value = "Visual";
                    WorkSheet1.Cells[1, 70].Value = "Charging Value";
                    WorkSheet1.Cells[1, 71].Value = "Discharging Value";
                    WorkSheet1.Cells[1, 72].Value = "Temperature";
                    WorkSheet1.Cells[1, 73].Value = "Voltage";
                    WorkSheet1.Cells[1, 74].Value = "Cell Diffrence";
                    WorkSheet1.Cells[1, 75].Value = "Protections";
                    WorkSheet1.Cells[1, 76].Value = "Cycle Count";
                    WorkSheet1.Cells[1, 77].Value = "Problem Observed By Eng";
                    WorkSheet1.Cells[1, 78].Value = "Problem Observed Desc";
                    WorkSheet1.Cells[1, 79].Value = "Gravity";
                    WorkSheet1.Cells[1, 80].Value = "IP Voltage AC";
                    WorkSheet1.Cells[1, 81].Value = "IP Voltage DC";
                    WorkSheet1.Cells[1, 82].Value = "Output AC";
                    WorkSheet1.Cells[1, 83].Value = "Protection";
                    WorkSheet1.Cells[1, 84].Value = "Fan Status";
                    WorkSheet1.Cells[1, 85].Value = "Any Physical Damage";
                    WorkSheet1.Cells[1, 86].Value = "Other";
                    WorkSheet1.Cells[1, 87].Value = "IsWarrantyVoid";
                    WorkSheet1.Cells[1, 88].Value = "Reason for Void";
                    WorkSheet1.Cells[1, 89].Value = "Type Of BMS";
                    WorkSheet1.Cells[1, 90].Value = "Abnormal Noise";
                    WorkSheet1.Cells[1, 91].Value = "Connector Damage";
                    WorkSheet1.Cells[1, 92].Value = "Any Brunt";
                    WorkSheet1.Cells[1, 93].Value = "Physical Damage";
                    WorkSheet1.Cells[1, 94].Value = "Problem Remark";
                    WorkSheet1.Cells[1, 95].Value = "IP CurrentAC(A)";
                    WorkSheet1.Cells[1, 96].Value = "Output Current DC(A)";
                    WorkSheet1.Cells[1, 97].Value = "Output Voltage DC(V)";
                    WorkSheet1.Cells[1, 98].Value = "Type";
                    WorkSheet1.Cells[1, 99].Value = "Heating";
                    WorkSheet1.Cells[1, 100].Value = "Output Voltage AC(V)";
                    WorkSheet1.Cells[1, 101].Value = "Output Current AC(A)";
                    WorkSheet1.Cells[1, 102].Value = "IP Current DC(A)";
                    WorkSheet1.Cells[1, 103].Value = "Specific Gravity C2";
                    WorkSheet1.Cells[1, 104].Value = "Specific Gravity C3";
                    WorkSheet1.Cells[1, 105].Value = "Specific Gravity C4";
                    WorkSheet1.Cells[1, 106].Value = "Specific Gravity C5";
                    WorkSheet1.Cells[1, 107].Value = "Specific Gravity C6";
                    WorkSheet1.Cells[1, 108].Value = "Checking Visual";
                    WorkSheet1.Cells[1, 109].Value = "Checking Terminal Voltage";
                    WorkSheet1.Cells[1, 110].Value = "Checking Communication With Battery";
                    WorkSheet1.Cells[1, 111].Value = "Checking Terminal Wire";
                    WorkSheet1.Cells[1, 112].Value = "Checking Life Cycle";
                    WorkSheet1.Cells[1, 113].Value = "Checking String Voltage Variation";
                    WorkSheet1.Cells[1, 114].Value = "Checking Battery Parameters Setting";
                    WorkSheet1.Cells[1, 115].Value = "Checking Spare";
                    WorkSheet1.Cells[1, 116].Value = "Checking BMS Status";
                    WorkSheet1.Cells[1, 117].Value = "Checking BMS Type";
                    WorkSheet1.Cells[1, 118].Value = "Checking Battery Temp";
                    WorkSheet1.Cells[1, 119].Value = "Checking BMS Serial Number";
                    WorkSheet1.Cells[1, 120].Value = "Checking Problem Observed";
                    WorkSheet1.Cells[1, 121].Value = "Checking Problem Observed By Eng";
                    WorkSheet1.Cells[1, 122].Value = "Checking IsWarrantyVoid";
                    WorkSheet1.Cells[1, 123].Value = "Checking Reason For Void";
                    WorkSheet1.Cells[1, 124].Value = "Checking Battery Repaired OnSite";
                    WorkSheet1.Cells[1, 125].Value = "Checking Battery Repaired To Plant";
                    WorkSheet1.Cells[1, 126].Value = "Solution Provider";
                    WorkSheet1.Cells[1, 127].Value = "Allocate To Service Engg";
                    WorkSheet1.Cells[1, 128].Value = "Remarks";
                    WorkSheet1.Cells[1, 129].Value = "Branch Name";
                    WorkSheet1.Cells[1, 130].Value = "Rectification Action";
                    WorkSheet1.Cells[1, 131].Value = "Resolution Summary";
                    WorkSheet1.Cells[1, 132].Value = "OV IsCustomerAvailable";
                    WorkSheet1.Cells[1, 133].Value = "OV Engineer Name";
                    WorkSheet1.Cells[1, 134].Value = "OV Engineer Number";
                    WorkSheet1.Cells[1, 135].Value = "OV Customer Name";
                    WorkSheet1.Cells[1, 136].Value = "OV Customer Name Secondary";
                    WorkSheet1.Cells[1, 137].Value = "OV Customer Mobile Number";
                    WorkSheet1.Cells[1, 138].Value = "OV RequestOTP";
                    WorkSheet1.Cells[1, 139].Value = "OV Signature";
                    WorkSheet1.Cells[1, 140].Value = "IsReopen";
                    WorkSheet1.Cells[1, 141].Value = "RO Technical Support Engg";
                    WorkSheet1.Cells[1, 142].Value = "RO Visual";
                    WorkSheet1.Cells[1, 143].Value = "RO Charging Value";
                    WorkSheet1.Cells[1, 144].Value = "RO Discharging Value";
                    WorkSheet1.Cells[1, 145].Value = "RO Temperature";
                    WorkSheet1.Cells[1, 146].Value = "RO Voltage";
                    WorkSheet1.Cells[1, 147].Value = "RO Cell Diffrence";
                    WorkSheet1.Cells[1, 148].Value = "RO Protections";
                    WorkSheet1.Cells[1, 149].Value = "RO Cycle Count";
                    WorkSheet1.Cells[1, 150].Value = "RO Problem Observed By Eng";
                    WorkSheet1.Cells[1, 151].Value = "RO Problem Observed Desc";
                    WorkSheet1.Cells[1, 152].Value = "RO Gravity";
                    WorkSheet1.Cells[1, 153].Value = "RO IP Voltage AC";
                    WorkSheet1.Cells[1, 154].Value = "RO IP Voltage DC";
                    WorkSheet1.Cells[1, 155].Value = "RO Output AC";
                    WorkSheet1.Cells[1, 156].Value = "RO Protection";
                    WorkSheet1.Cells[1, 157].Value = "RO Fan Status";
                    WorkSheet1.Cells[1, 158].Value = "RO Any Physical Damage";
                    WorkSheet1.Cells[1, 159].Value = "RO Other";
                    WorkSheet1.Cells[1, 160].Value = "RO IsWarrantyVoid";
                    WorkSheet1.Cells[1, 161].Value = "RO Reason for Void";
                    WorkSheet1.Cells[1, 162].Value = "RO Type Of BMS";
                    WorkSheet1.Cells[1, 163].Value = "RO Abnormal Noise";
                    WorkSheet1.Cells[1, 164].Value = "RO Connector Damage";
                    WorkSheet1.Cells[1, 165].Value = "RO Any Brunt";
                    WorkSheet1.Cells[1, 166].Value = "RO Physical Damage";
                    WorkSheet1.Cells[1, 167].Value = "RO Problem Remark";
                    WorkSheet1.Cells[1, 168].Value = "RO IP CurrentAC(A)";
                    WorkSheet1.Cells[1, 169].Value = "RO Output Current DC(A)";
                    WorkSheet1.Cells[1, 170].Value = "RO Output Voltage DC(V)";
                    WorkSheet1.Cells[1, 171].Value = "RO Type";
                    WorkSheet1.Cells[1, 172].Value = "RO Heating";
                    WorkSheet1.Cells[1, 173].Value = "RO Output Voltage AC(V)";
                    WorkSheet1.Cells[1, 174].Value = "RO Output Current AC(A)";
                    WorkSheet1.Cells[1, 175].Value = "RO IP Current DC(A)";
                    WorkSheet1.Cells[1, 176].Value = "RO Specific Gravity C2";
                    WorkSheet1.Cells[1, 177].Value = "RO Specific Gravity C3";
                    WorkSheet1.Cells[1, 178].Value = "RO Specific Gravity C4";
                    WorkSheet1.Cells[1, 179].Value = "RO Specific Gravity C5";
                    WorkSheet1.Cells[1, 180].Value = "RO Specific Gravity C6";
                    WorkSheet1.Cells[1, 181].Value = "RO Checking Visual";
                    WorkSheet1.Cells[1, 182].Value = "RO Checking Terminal Voltage";
                    WorkSheet1.Cells[1, 183].Value = "RO Checking Communication With Battery";
                    WorkSheet1.Cells[1, 184].Value = "RO Checking Terminal Wire";
                    WorkSheet1.Cells[1, 185].Value = "RO Checking Life Cycle";
                    WorkSheet1.Cells[1, 186].Value = "RO Checking String Voltage Variation";
                    WorkSheet1.Cells[1, 187].Value = "RO Checking Battery Parameters Setting";
                    WorkSheet1.Cells[1, 188].Value = "RO Checking Spare";
                    WorkSheet1.Cells[1, 189].Value = "RO Checking BMS Status";
                    WorkSheet1.Cells[1, 190].Value = "RO Checking BMS Type";
                    WorkSheet1.Cells[1, 191].Value = "RO Checking Battery Temp";
                    WorkSheet1.Cells[1, 192].Value = "RO Checking BMS Serial Number";
                    WorkSheet1.Cells[1, 193].Value = "RO Checking Problem Observed";
                    WorkSheet1.Cells[1, 194].Value = "RO Checking Problem Observed By Eng";
                    WorkSheet1.Cells[1, 195].Value = "RO Checking IsWarrantyVoid";
                    WorkSheet1.Cells[1, 196].Value = "RO Checking Reason For Void";
                    WorkSheet1.Cells[1, 197].Value = "RO Checking Battery Repaired OnSite";
                    WorkSheet1.Cells[1, 198].Value = "RO Checking Battery Repaired To Plant";
                    WorkSheet1.Cells[1, 199].Value = "RO Solution Provider";
                    WorkSheet1.Cells[1, 200].Value = "RO Allocate To Service Engg";
                    WorkSheet1.Cells[1, 201].Value = "RO Remarks";
                    WorkSheet1.Cells[1, 202].Value = "RO Branch Name";
                    WorkSheet1.Cells[1, 203].Value = "RO Rectification Action";
                    WorkSheet1.Cells[1, 204].Value = "RO Resolution Summary";
                    WorkSheet1.Cells[1, 205].Value = "RO OV IsCustomerAvailable";
                    WorkSheet1.Cells[1, 206].Value = "RO OV Engineer Name";
                    WorkSheet1.Cells[1, 207].Value = "RO OV Engineer Number";
                    WorkSheet1.Cells[1, 208].Value = "RO OV Customer Name";
                    WorkSheet1.Cells[1, 209].Value = "RO OV Customer Name Secondary";
                    WorkSheet1.Cells[1, 210].Value = "RO OV Customer Mobile Number";
                    WorkSheet1.Cells[1, 211].Value = "RO OV RequestOTP";
                    WorkSheet1.Cells[1, 212].Value = "RO OV Signature";
                    WorkSheet1.Cells[1, 213].Value = "Ticket Status";
                    WorkSheet1.Cells[1, 214].Value = "Ticket Status Sequence No";
                    WorkSheet1.Cells[1, 215].Value = "TRC Engineer";
                    WorkSheet1.Cells[1, 216].Value = "IsResolvedWithoutOTP";
                    WorkSheet1.Cells[1, 217].Value = "IsClosedWithoutOTP";
                    WorkSheet1.Cells[1, 218].Value = "IsActive";
                    WorkSheet1.Cells[1, 219].Value = "Created Date";
                    WorkSheet1.Cells[1, 220].Value = "Creator Name";
                    WorkSheet1.Cells[1, 221].Value = "Modified Date";
                    WorkSheet1.Cells[1, 222].Value = "Modifier Name";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.TicketNumber;
                        WorkSheet1.Cells[recordIndex, 2].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.TicketDate;
                        WorkSheet1.Cells[recordIndex, 3].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortTimePattern;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.TicketTime; 
                        WorkSheet1.Cells[recordIndex, 4].Value = items.TRCNumber;
                        WorkSheet1.Cells[recordIndex, 5].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.TRCDate; 
                        WorkSheet1.Cells[recordIndex, 6].Value = items.TicketPriority; 
                        WorkSheet1.Cells[recordIndex, 7].Value = items.TicketSLADays; 
                        WorkSheet1.Cells[recordIndex, 8].Value = items.TicketSLAHours; 
                        WorkSheet1.Cells[recordIndex, 9].Value = items.TicketSLAMin; 
                        WorkSheet1.Cells[recordIndex, 10].Value = items.SLAStatus; 
                        WorkSheet1.Cells[recordIndex, 11].Value = items.TicketAging; 
                        WorkSheet1.Cells[recordIndex, 12].Value = items.CD_LoggingSourceChannel; 
                        WorkSheet1.Cells[recordIndex, 13].Value = items.CD_CallerType; 
                        WorkSheet1.Cells[recordIndex, 14].Value = items.CD_CallerName; 
                        WorkSheet1.Cells[recordIndex, 15].Value = items.CD_CallerMobile; 
                        WorkSheet1.Cells[recordIndex, 16].Value = items.CD_CallerEmailId; 
                        WorkSheet1.Cells[recordIndex, 17].Value = items.CD_CallerAddress1; 
                        WorkSheet1.Cells[recordIndex, 18].Value = items.CD_CallerRegionName; 
                        WorkSheet1.Cells[recordIndex, 19].Value = items.CD_CallerStateName; 
                        WorkSheet1.Cells[recordIndex, 20].Value = items.CD_CallerDistrictName; 
                        WorkSheet1.Cells[recordIndex, 21].Value = items.CD_CallerCityName; 
                        WorkSheet1.Cells[recordIndex, 22].Value = items.CD_CallerPinCode; 
                        WorkSheet1.Cells[recordIndex, 23].Value = items.CD_CallerRemarks; 
                        WorkSheet1.Cells[recordIndex, 24].Value = items.CD_IsSiteAddressSameAsCaller; 
                        WorkSheet1.Cells[recordIndex, 25].Value = items.CD_ComplaintType; 
                        WorkSheet1.Cells[recordIndex, 26].Value = items.CD_IsOldProduct; 
                        WorkSheet1.Cells[recordIndex, 27].Value = items.CD_ProductSerialNumber; 
                        WorkSheet1.Cells[recordIndex, 28].Value = items.CustomerType; 
                        WorkSheet1.Cells[recordIndex, 29].Value = items.CD_CustomerName; 
                        WorkSheet1.Cells[recordIndex, 30].Value = items.CD_CustomerMobile; 
                        WorkSheet1.Cells[recordIndex, 31].Value = items.CD_CustomerEmail; 
                        WorkSheet1.Cells[recordIndex, 32].Value = items.CD_CustomerAddress1; 
                        WorkSheet1.Cells[recordIndex, 33].Value = items.CD_CustomerRegionName; 
                        WorkSheet1.Cells[recordIndex, 34].Value = items.CD_CustomerStateName; 
                        WorkSheet1.Cells[recordIndex, 35].Value = items.CD_CustomerDistrictName; 
                        WorkSheet1.Cells[recordIndex, 36].Value = items.CD_CustomerCityName; 
                        WorkSheet1.Cells[recordIndex, 37].Value = items.CD_CustomerPinCode; 
                        WorkSheet1.Cells[recordIndex, 38].Value = items.CD_SiteCustomerName; 
                        WorkSheet1.Cells[recordIndex, 39].Value = items.CD_SiteContactName; 
                        WorkSheet1.Cells[recordIndex, 40].Value = items.CD_SitContactMobile; 
                        WorkSheet1.Cells[recordIndex, 41].Value = items.CD_SiteCustomerAddress1; 
                        WorkSheet1.Cells[recordIndex, 42].Value = items.CD_SiteCustomerRegionName; 
                        WorkSheet1.Cells[recordIndex, 43].Value = items.CD_SiteCustomerStateName; 
                        WorkSheet1.Cells[recordIndex, 44].Value = items.CD_SiteCustomerDistrictName; 
                        WorkSheet1.Cells[recordIndex, 45].Value = items.CD_SiteCustomerCityName; 
                        WorkSheet1.Cells[recordIndex, 46].Value = items.CD_SiteCustomerPinCode; 
                        WorkSheet1.Cells[recordIndex, 47].Value = items.BD_BatteryBOMNumber; 
                        WorkSheet1.Cells[recordIndex, 48].Value = items.BD_ProductCategory; 
                        WorkSheet1.Cells[recordIndex, 49].Value = items.BD_Segment; 
                        WorkSheet1.Cells[recordIndex, 50].Value = items.BD_SubSegment; 
                        WorkSheet1.Cells[recordIndex, 51].Value = items.BD_ProductModel; 
                        WorkSheet1.Cells[recordIndex, 52].Value = items.BD_CellChemistry;
                        WorkSheet1.Cells[recordIndex, 53].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 53].Value = items.BD_DateofManufacturing; 
                        WorkSheet1.Cells[recordIndex, 54].Value = items.BD_ProbReportedByCust; 
                        WorkSheet1.Cells[recordIndex, 55].Value = items.BD_ProblemDescription;
                        WorkSheet1.Cells[recordIndex, 56].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 56].Value = items.BD_WarrantyStartDate;
                        WorkSheet1.Cells[recordIndex, 57].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 57].Value = items.BD_WarrantyEndDate; 
                        WorkSheet1.Cells[recordIndex, 58].Value = items.BD_WarrantyStatus; 
                        WorkSheet1.Cells[recordIndex, 59].Value = items.BD_WarrantyType; 
                        WorkSheet1.Cells[recordIndex, 60].Value = items.BD_IsTrackingDeviceRequired; 
                        WorkSheet1.Cells[recordIndex, 61].Value = items.BD_TrackingDeviceName; 
                        WorkSheet1.Cells[recordIndex, 62].Value = items.BD_MakeName; 
                        WorkSheet1.Cells[recordIndex, 63].Value = items.BD_DeviceID; 
                        WorkSheet1.Cells[recordIndex, 64].Value = items.BD_IMEINo; 
                        WorkSheet1.Cells[recordIndex, 65].Value = items.BD_SIMNo; 
                        WorkSheet1.Cells[recordIndex, 66].Value = items.BD_SIMProviderName; 
                        WorkSheet1.Cells[recordIndex, 67].Value = items.BD_PlatformName; 
                        WorkSheet1.Cells[recordIndex, 68].Value = items.BD_TechnicalSupportEngg; 
                        WorkSheet1.Cells[recordIndex, 69].Value = items.TSAD_Visual == 1 ? "OK" : "NOT OK"; 
                        WorkSheet1.Cells[recordIndex, 70].Value = items.TSAD_CurrentChargingValue; 
                        WorkSheet1.Cells[recordIndex, 71].Value = items.TSAD_CurrentDischargingValue; 
                        WorkSheet1.Cells[recordIndex, 72].Value = items.TSAD_BatteryTemperature; 
                        WorkSheet1.Cells[recordIndex, 73].Value = items.TSAD_BatterVoltage; 
                        WorkSheet1.Cells[recordIndex, 74].Value = items.TSAD_CellDiffrence; 
                        WorkSheet1.Cells[recordIndex, 75].Value = items.TSAD_Protections; 
                        WorkSheet1.Cells[recordIndex, 76].Value = items.TSAD_CycleCount; 
                        WorkSheet1.Cells[recordIndex, 77].Value = items.TSAD_ProblemObservedByEng; 
                        WorkSheet1.Cells[recordIndex, 78].Value = items.TSAD_ProblemObservedDesc; 
                        WorkSheet1.Cells[recordIndex, 79].Value = items.TSAD_Gravity; 
                        WorkSheet1.Cells[recordIndex, 80].Value = items.TSAD_IP_VoltageAC; 
                        WorkSheet1.Cells[recordIndex, 81].Value = items.TSAD_IP_VoltageDC; 
                        WorkSheet1.Cells[recordIndex, 82].Value = items.TSAD_OutputAC; 
                        WorkSheet1.Cells[recordIndex, 83].Value = items.TSAD_Protection; 
                        WorkSheet1.Cells[recordIndex, 84].Value = items.TSAD_FanStatus == 1 ? "Working" : "Not Working";
                        WorkSheet1.Cells[recordIndex, 85].Value = items.TSPD_AnyPhysicalDamage; 
                        WorkSheet1.Cells[recordIndex, 86].Value = items.TSPD_Other; 
                        WorkSheet1.Cells[recordIndex, 87].Value = items.TSPD_IsWarrantyVoid; 
                        WorkSheet1.Cells[recordIndex, 88].Value = items.TSPD_ReasonforVoid; 
                        WorkSheet1.Cells[recordIndex, 89].Value = items.TSPD_TypeOfBMS; 
                        WorkSheet1.Cells[recordIndex, 90].Value = items.TS_AbnormalNoise == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 91].Value = items.TS_ConnectorDamage == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 92].Value = items.TS_AnyBrunt == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 93].Value = items.TS_PhysicalDamage == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 94].Value = items.TS_ProblemRemark; 
                        WorkSheet1.Cells[recordIndex, 95].Value = items.TS_IPCurrentAC_A; 
                        WorkSheet1.Cells[recordIndex, 96].Value = items.TS_OutputCurrentDC_A; 
                        WorkSheet1.Cells[recordIndex, 97].Value = items.TS_OutputVoltageDC_V; 
                        WorkSheet1.Cells[recordIndex, 98].Value = items.TS_Type == 1 ? "YES" : "NO";
                        WorkSheet1.Cells[recordIndex, 99].Value = items.TS_Heating == 1 ? "YES" : "NO";
                        WorkSheet1.Cells[recordIndex, 100].Value = items.TS_OutputVoltageAC_V; 
                        WorkSheet1.Cells[recordIndex, 101].Value = items.TS_OutputCurrentAC_A; 
                        WorkSheet1.Cells[recordIndex, 102].Value = items.TS_IPCurrentDC_A; 
                        WorkSheet1.Cells[recordIndex, 103].Value = items.TS_SpecificGravityC2; 
                        WorkSheet1.Cells[recordIndex, 104].Value = items.TS_SpecificGravityC3; 
                        WorkSheet1.Cells[recordIndex, 105].Value = items.TS_SpecificGravityC4; 
                        WorkSheet1.Cells[recordIndex, 106].Value = items.TS_SpecificGravityC5; 
                        WorkSheet1.Cells[recordIndex, 107].Value = items.TS_SpecificGravityC6; 
                        WorkSheet1.Cells[recordIndex, 108].Value = items.CP_Visual == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 109].Value = items.CP_TerminalVoltage;                  
                        WorkSheet1.Cells[recordIndex, 110].Value = items.CP_CommunicationWithBattery;                  
                        WorkSheet1.Cells[recordIndex, 111].Value = items.CP_TerminalWire;                                       		  
                        WorkSheet1.Cells[recordIndex, 112].Value = items.CP_LifeCycle;                  
                        WorkSheet1.Cells[recordIndex, 113].Value = items.CP_StringVoltageVariation;                  
                        WorkSheet1.Cells[recordIndex, 114].Value = items.CP_BatteryParametersSetting;                                        		  
                        WorkSheet1.Cells[recordIndex, 115].Value = items.CP_Spare;          
                        WorkSheet1.Cells[recordIndex, 116].Value = items.CP_BMSStatus;                                
                        WorkSheet1.Cells[recordIndex, 117].Value = items.CP_BMSType;          
                        WorkSheet1.Cells[recordIndex, 118].Value = items.CP_BatteryTemp;          
                        WorkSheet1.Cells[recordIndex, 119].Value = items.CP_BMSSerialNumber;          
                        WorkSheet1.Cells[recordIndex, 120].Value = items.CP_ProblemObserved;      
                        WorkSheet1.Cells[recordIndex, 121].Value = items.CP_ProblemObservedByEng; 
                        WorkSheet1.Cells[recordIndex, 122].Value = items.CP_IsWarrantyVoid; 
                        WorkSheet1.Cells[recordIndex, 123].Value = items.CP_ReasonForVoid; 
                        WorkSheet1.Cells[recordIndex, 124].Value = items.CC_BatteryRepairedOnSite;                  
                        WorkSheet1.Cells[recordIndex, 125].Value = items.CC_BatteryRepairedToPlant; 
                        WorkSheet1.Cells[recordIndex, 126].Value = items.TSSP_SolutionProvider;                            
                        WorkSheet1.Cells[recordIndex, 127].Value = items.TSSP_AllocateToServiceEngg;               
                        WorkSheet1.Cells[recordIndex, 128].Value = items.TSSP_Remarks;           
                        WorkSheet1.Cells[recordIndex, 129].Value = items.TSSP_BranchName;         
                        WorkSheet1.Cells[recordIndex, 130].Value = items.TSSP_RectificationAction;  
                        WorkSheet1.Cells[recordIndex, 131].Value = items.TSSP_ResolutionSummary;
                        WorkSheet1.Cells[recordIndex, 132].Value = items.OV_IsCustomerAvailable;                  
                        WorkSheet1.Cells[recordIndex, 133].Value = items.OV_EngineerName;                  
                        WorkSheet1.Cells[recordIndex, 134].Value = items.OV_EngineerNumber;                  
                        WorkSheet1.Cells[recordIndex, 135].Value = items.OV_CustomerName;                  
                        WorkSheet1.Cells[recordIndex, 136].Value = items.OV_CustomerNameSecondary;                  
                        WorkSheet1.Cells[recordIndex, 137].Value = items.OV_CustomerMobileNumber;                  
                        WorkSheet1.Cells[recordIndex, 138].Value = items.OV_RequestOTP;                  
                        WorkSheet1.Cells[recordIndex, 139].Value = items.OV_Signature;
                        WorkSheet1.Cells[recordIndex, 140].Value = items.IsReopen;
                        WorkSheet1.Cells[recordIndex, 141].Value = items.RO_BD_TechnicalSupportEngg; 
                        WorkSheet1.Cells[recordIndex, 142].Value = items.RO_TSAD_Visual; 
                        WorkSheet1.Cells[recordIndex, 143].Value = items.RO_TSAD_CurrentChargingValue; 
                        WorkSheet1.Cells[recordIndex, 144].Value = items.RO_TSAD_CurrentDischargingValue; 
                        WorkSheet1.Cells[recordIndex, 145].Value = items.RO_TSAD_BatteryTemperature; 
                        WorkSheet1.Cells[recordIndex, 146].Value = items.RO_TSAD_BatterVoltage; 
                        WorkSheet1.Cells[recordIndex, 147].Value = items.RO_TSAD_CellDiffrence; 
                        WorkSheet1.Cells[recordIndex, 148].Value = items.RO_TSAD_Protections; 
                        WorkSheet1.Cells[recordIndex, 149].Value = items.RO_TSAD_CycleCount; 
                        WorkSheet1.Cells[recordIndex, 150].Value = items.RO_TSAD_ProblemObservedByEng; 
                        WorkSheet1.Cells[recordIndex, 151].Value = items.RO_TSAD_ProblemObservedDesc; 
                        WorkSheet1.Cells[recordIndex, 152].Value = items.RO_TSAD_Gravity; 
                        WorkSheet1.Cells[recordIndex, 153].Value = items.RO_TSAD_IP_VoltageAC; 
                        WorkSheet1.Cells[recordIndex, 154].Value = items.RO_TSAD_IP_VoltageDC; 
                        WorkSheet1.Cells[recordIndex, 155].Value = items.RO_TSAD_OutputAC; 
                        WorkSheet1.Cells[recordIndex, 156].Value = items.RO_TSAD_Protection; 
                        WorkSheet1.Cells[recordIndex, 157].Value = items.RO_TSAD_FanStatus == 1 ? "Working" : "Not Working";
                        WorkSheet1.Cells[recordIndex, 158].Value = items.RO_TSPD_AnyPhysicalDamage; 
                        WorkSheet1.Cells[recordIndex, 159].Value = items.RO_TSPD_Other; 
                        WorkSheet1.Cells[recordIndex, 160].Value = items.RO_TSPD_IsWarrantyVoid; 
                        WorkSheet1.Cells[recordIndex, 161].Value = items.RO_TSPD_ReasonforVoid; 
                        WorkSheet1.Cells[recordIndex, 162].Value = items.RO_TSPD_TypeOfBMS; 
                        WorkSheet1.Cells[recordIndex, 163].Value = items.RO_TS_AbnormalNoise == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 164].Value = items.RO_TS_ConnectorDamage == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 165].Value = items.RO_TS_AnyBrunt == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 166].Value = items.RO_TS_PhysicalDamage == 1 ? "OK" : "NOT OK";
                        WorkSheet1.Cells[recordIndex, 167].Value = items.RO_TS_ProblemRemark; 
                        WorkSheet1.Cells[recordIndex, 168].Value = items.RO_TS_IPCurrentAC_A; 
                        WorkSheet1.Cells[recordIndex, 169].Value = items.RO_TS_OutputCurrentDC_A; 
                        WorkSheet1.Cells[recordIndex, 170].Value = items.RO_TS_OutputVoltageDC_V; 
                        WorkSheet1.Cells[recordIndex, 171].Value = items.RO_TS_Type == 1 ? "YES" : "NO";
                        WorkSheet1.Cells[recordIndex, 172].Value = items.RO_TS_Heating == 1 ? "YES" : "NO";
                        WorkSheet1.Cells[recordIndex, 173].Value = items.RO_TS_OutputVoltageAC_V; 
                        WorkSheet1.Cells[recordIndex, 174].Value = items.RO_TS_OutputCurrentAC_A; 
                        WorkSheet1.Cells[recordIndex, 175].Value = items.RO_TS_IPCurrentDC_A; 
                        WorkSheet1.Cells[recordIndex, 176].Value = items.RO_TS_SpecificGravityC2; 
                        WorkSheet1.Cells[recordIndex, 177].Value = items.RO_TS_SpecificGravityC3; 
                        WorkSheet1.Cells[recordIndex, 178].Value = items.RO_TS_SpecificGravityC4; 
                        WorkSheet1.Cells[recordIndex, 179].Value = items.RO_TS_SpecificGravityC5; 
                        WorkSheet1.Cells[recordIndex, 180].Value = items.RO_TS_SpecificGravityC6; 
                        WorkSheet1.Cells[recordIndex, 181].Value = items.RO_CP_Visual;                                   	  
                        WorkSheet1.Cells[recordIndex, 182].Value = items.RO_CP_TerminalVoltage;                  
                        WorkSheet1.Cells[recordIndex, 183].Value = items.RO_CP_CommunicationWithBattery;                  
                        WorkSheet1.Cells[recordIndex, 184].Value = items.RO_CP_TerminalWire;                                  
                        WorkSheet1.Cells[recordIndex, 185].Value = items.RO_CP_LifeCycle;                  
                        WorkSheet1.Cells[recordIndex, 186].Value = items.RO_CP_StringVoltageVariation;                  
                        WorkSheet1.Cells[recordIndex, 187].Value = items.RO_CP_BatteryParametersSetting;                      
                        WorkSheet1.Cells[recordIndex, 188].Value = items.RO_CP_Spare;          
                        WorkSheet1.Cells[recordIndex, 189].Value = items.RO_CP_BMSStatus;                                
                        WorkSheet1.Cells[recordIndex, 190].Value = items.RO_CP_BMSType;          
                        WorkSheet1.Cells[recordIndex, 191].Value = items.RO_CP_BatteryTemp;          
                        WorkSheet1.Cells[recordIndex, 192].Value = items.RO_CP_BMSSerialNumber;          
                        WorkSheet1.Cells[recordIndex, 193].Value = items.RO_CP_ProblemObserved;      
                        WorkSheet1.Cells[recordIndex, 194].Value = items.RO_CP_ProblemObservedByEng; 
                        WorkSheet1.Cells[recordIndex, 195].Value = items.RO_CP_IsWarrantyVoid; 
                        WorkSheet1.Cells[recordIndex, 196].Value = items.RO_CP_ReasonForVoid; 
                        WorkSheet1.Cells[recordIndex, 197].Value = items.RO_CC_BatteryRepairedOnSite;                  
                        WorkSheet1.Cells[recordIndex, 198].Value = items.RO_CC_BatteryRepairedToPlant; 
                        WorkSheet1.Cells[recordIndex, 199].Value = items.RO_TSSP_SolutionProvider;                            
                        WorkSheet1.Cells[recordIndex, 200].Value = items.RO_TSSP_AllocateToServiceEngg;               
                        WorkSheet1.Cells[recordIndex, 201].Value = items.RO_TSSP_Remarks;           
                        WorkSheet1.Cells[recordIndex, 202].Value = items.RO_TSSP_BranchName;         
                        WorkSheet1.Cells[recordIndex, 203].Value = items.RO_TSSP_RectificationAction;  
                        WorkSheet1.Cells[recordIndex, 204].Value = items.RO_TSSP_ResolutionSummary;
                        WorkSheet1.Cells[recordIndex, 205].Value = items.RO_OV_IsCustomerAvailable;                  
                        WorkSheet1.Cells[recordIndex, 206].Value = items.RO_OV_EngineerName;                  
                        WorkSheet1.Cells[recordIndex, 207].Value = items.RO_OV_EngineerNumber;                  
                        WorkSheet1.Cells[recordIndex, 208].Value = items.RO_OV_CustomerName;                  
                        WorkSheet1.Cells[recordIndex, 209].Value = items.RO_OV_CustomerNameSecondary;                  
                        WorkSheet1.Cells[recordIndex, 210].Value = items.RO_OV_CustomerMobileNumber;                  
                        WorkSheet1.Cells[recordIndex, 211].Value = items.RO_OV_RequestOTP;                  
                        WorkSheet1.Cells[recordIndex, 212].Value = items.RO_OV_Signature;

                        WorkSheet1.Cells[recordIndex, 213].Value = items.TicketStatus; 
                        WorkSheet1.Cells[recordIndex, 214].Value = items.TicketStatusSequenceNo; 
                        WorkSheet1.Cells[recordIndex, 215].Value = items.TRC_Engineer; 
                        WorkSheet1.Cells[recordIndex, 216].Value = items.IsResolvedWithoutOTP; 
                        WorkSheet1.Cells[recordIndex, 217].Value = items.IsClosedWithoutOTP; 
                        WorkSheet1.Cells[recordIndex, 218].Value = items.IsActive;
                        WorkSheet1.Cells[recordIndex, 219].Value = items.CreatedDate; 
                        WorkSheet1.Cells[recordIndex, 220].Value = items.CreatorName;
                        WorkSheet1.Cells[recordIndex, 221].Value = items.ModifiedDate; 
                        WorkSheet1.Cells[recordIndex, 222].Value = items.ModifierName; 

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
        public async Task<ResponseModel> GetInMaterialConsumptionReport(ManageReport_Search parameters)
        {
            IEnumerable<InMaterialConsumptionReport_Response> lstRoles = await _manageReportRepository.GetInMaterialConsumptionReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportInMaterialConsumptionReport(ManageReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<InMaterialConsumptionReport_Response> lstSizeObj = await _manageReportRepository.GetInMaterialConsumptionReport(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("InMaterialConsumptionReport");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Spare Part Code";
                    WorkSheet1.Cells[1, 2].Value = "Spare Part Description";
                    WorkSheet1.Cells[1, 3].Value = "UOM";
                    WorkSheet1.Cells[1, 4].Value = "Min Qty.";
                    WorkSheet1.Cells[1, 5].Value = "Available Qty.";
                    WorkSheet1.Cells[1, 6].Value = "Status";
                    WorkSheet1.Cells[1, 7].Value = "Created Date";
                    WorkSheet1.Cells[1, 8].Value = "Created By";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.UniqueCode;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.SpareDesc;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.UOMName;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.MinQty;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.AvailableQty;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.StatusName;
                        WorkSheet1.Cells[recordIndex, 7].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.CreatorName;
                      
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
        public async Task<ResponseModel> GetOutMaterialConsumptionReport(OutMaterialConsumptioneReport_Search parameters)
        {
            IEnumerable<OutMaterialConsumptionReport_Response> lstRoles = await _manageReportRepository.GetOutMaterialConsumptionReport(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportOutMaterialConsumptionReport(OutMaterialConsumptioneReport_Search parameters)
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            IEnumerable<OutMaterialConsumptionReport_Response> lstSizeObj = await _manageReportRepository.GetOutMaterialConsumptionReport(parameters);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("OutMaterialConsumptionReport");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Spare Part Code";
                    WorkSheet1.Cells[1, 2].Value = "Spare Part Description";
                    WorkSheet1.Cells[1, 3].Value = "UOM";
                    WorkSheet1.Cells[1, 4].Value = "Stock Min.Qty.";
                    WorkSheet1.Cells[1, 5].Value = "Stock Available Qty";
                    WorkSheet1.Cells[1, 6].Value = "Order Number";
                    WorkSheet1.Cells[1, 7].Value = "Engineer Name";
                    WorkSheet1.Cells[1, 8].Value = "Engg Stock Min Qty.";
                    WorkSheet1.Cells[1, 9].Value = "Engg Available Qty.";
                    WorkSheet1.Cells[1, 10].Value = "Engg Requestedd Qty.";
                    WorkSheet1.Cells[1, 11].Value = "Engg Allocated Qty.";
                    WorkSheet1.Cells[1, 12].Value = "Status";
                    WorkSheet1.Cells[1, 13].Value = "Created Date";
                    WorkSheet1.Cells[1, 14].Value = "Created By";

                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.UniqueCode;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.SpareDesc;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.UOMName;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.StockMinQty;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.StockAvailableQty;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.OrderNumber;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.EngineerName;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.EnggStockMinQty;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.EnggAvailableQty;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.EnggRequesteddQty;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.EnggAvailableQty;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.StatusName;
                        WorkSheet1.Cells[recordIndex, 13].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.CreatedDate;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.CreatorName;

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
