﻿using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageQCController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;
        private readonly IManageQCRepository _ManageQCRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IContactDetailRepository _contactDetailRepository;
        private readonly IAddressRepository _addressRepository;

        public ManageQCController(
            IFileManager fileManager,
            IManageQCRepository ManageQCRepository,
            ICustomerRepository customerRepository,
            IContactDetailRepository contactDetailRepository,
            IAddressRepository addressRepository)
        {
            _fileManager = fileManager;
            _ManageQCRepository = ManageQCRepository;
            _customerRepository = customerRepository;
            _contactDetailRepository = contactDetailRepository;
            _addressRepository = addressRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region QC 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveManageQC(ManageQC_Consignee_Request parameters)
        {
            var vCustObj = new Customer_Request();

            int iCustomerId = 0;

            // Customer Create
            vCustObj.Id = parameters.Id;
            vCustObj.CustomerTypeId = parameters.ConsigneeTypeId;
            vCustObj.CustomerName = parameters.ConsigneeName;
            vCustObj.CustomerCode = parameters.ConsigneeCode;
            vCustObj.MobileNumber = parameters.ConsigneeMobileNumber;
            vCustObj.IsActive = parameters.IsActive;

            int result = await _customerRepository.SaveCustomer(vCustObj);

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
                iCustomerId = result;
                _response.Message = "Record saved sucessfully";
            }

            if (iCustomerId > 0)
            {
                // Buyer Address Detail
                if (!string.IsNullOrWhiteSpace(parameters.ConsigneeAddress1))
                {
                    var AddressDetail = new Address_Request()
                    {
                        Id = Convert.ToInt32(parameters.ConsigneeAddressId),
                        RefId = iCustomerId,
                        RefType = "Customer",
                        Address1 = parameters.ConsigneeAddress1,
                        RegionId = parameters.ConsigneeRegionId,
                        StateId = parameters.ConsigneeStateId,
                        DistrictId = parameters.ConsigneeDistrictId,
                        CityId = parameters.ConsigneeCityId,
                        PinCode = parameters.ConsigneePinCode,
                        IsDeleted = false,
                        IsDefault = true,
                        IsActive = true,
                    };

                    int resultAddressDetail = await _addressRepository.SaveAddress(AddressDetail);
                }

                // Save/Update Accessory List
                foreach (var item in parameters.AccessoryList)
                {
                    var vCustomerAccessory_Request = new CustomerAccessory_Request()
                    {
                        Id = item.Id,
                        CustomerId = iCustomerId,
                        AccessoryName = item.AccessoryName,
                        IsActive = item.IsActive,
                        Quantity = item.Quantity,
                    };

                    int resultCustomerAccessory = await _ManageQCRepository.SaveManageQCAccessory(vCustomerAccessory_Request);
                }
            }

            _response.Id = result;
            return _response;

        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageQCList(BaseSearchEntity parameters)
        {
            var objList = await _ManageQCRepository.GetManageQCList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GettManageQCById(int Id)
        {
            var vManageQC_Consignee_Request = new ManageQC_Consignee_Response();


            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ManageQCRepository.GetManageQCById(Id);
                if (vResultObj != null)
                {
                    vManageQC_Consignee_Request.Id = vResultObj.Id;
                    vManageQC_Consignee_Request.ConsigneeTypeId = vResultObj.ConsigneeTypeId;
                    vManageQC_Consignee_Request.ConsigneeName = vResultObj.ConsigneeName;
                    vManageQC_Consignee_Request.ConsigneeCode = vResultObj.ConsigneeCode;
                    vManageQC_Consignee_Request.ConsigneeMobileNumber = vResultObj.ConsigneeMobileNumber;
                    vManageQC_Consignee_Request.ConsigneeAddressId = vResultObj.ConsigneeAddressId;
                    vManageQC_Consignee_Request.ConsigneeAddress1 = vResultObj.ConsigneeAddress1;
                    vManageQC_Consignee_Request.ConsigneeRegionId = vResultObj.ConsigneeRegionId;
                    vManageQC_Consignee_Request.ConsigneeRegionName = vResultObj.ConsigneeRegionName;
                    vManageQC_Consignee_Request.ConsigneeStateId = vResultObj.ConsigneeStateId;
                    vManageQC_Consignee_Request.ConsigneeStateName = vResultObj.ConsigneeStateName;
                    vManageQC_Consignee_Request.ConsigneeDistrictId = vResultObj.ConsigneeDistrictId;
                    vManageQC_Consignee_Request.ConsigneeDistrictName = vResultObj.ConsigneeDistrictName;
                    vManageQC_Consignee_Request.ConsigneeCityId = vResultObj.ConsigneeCityId;
                    vManageQC_Consignee_Request.ConsigneeCityName = vResultObj.ConsigneeCityName;
                    vManageQC_Consignee_Request.ConsigneePinCode = vResultObj.ConsigneePinCode;
                    vManageQC_Consignee_Request.IsActive = vResultObj.IsActive;

                    // Accessory
                    var vSearchObj = new CustomerAccessory_Search()
                    {
                        CustomerId = vResultObj.Id,
                    };

                    var objAccessoryList = await _ManageQCRepository.GetManageQCAccessoryList(vSearchObj);
                    foreach (var item in objAccessoryList)
                    {
                        vManageQC_Consignee_Request.AccessoryList.Add(item);
                    }
                }

                _response.Data = vManageQC_Consignee_Request;
            }
            return _response;
        }

        #endregion

        #region Cusromer BOM 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBOM(CustomerBOM_Request parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.PartCode))
            {
                _response.Message = "PartCode is required!";

                return _response;
            }

            // Image Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PartImage_Base64))
            {
                var vUploadFile_AadharCardImage = _fileManager.UploadDocumentsBase64ToFile(parameters.PartImage_Base64, "\\Uploads\\Customer\\QC\\", parameters.PartImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile_AadharCardImage))
                {
                    parameters.PartImage = vUploadFile_AadharCardImage;
                }
            }

            int result = await _ManageQCRepository.SaveCustomerBOM(parameters);

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
        public async Task<ResponseModel> GetBOMList(CustomerBOM_Search parameters)
        {
            var objList = await _ManageQCRepository.GetCustomerBOMList(parameters);
            _response.Data = objList.ToList();
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
                var vResultObj = await _ManageQCRepository.GetCustomerBOMById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Battery 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBattery(CustomerBattery_Request parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.ProductSerialNumber))
            {
                _response.Message = "SerialNumber is required!";

                return _response;
            }

            int result = await _ManageQCRepository.SaveCustomerBattery(parameters);

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
                _response.Message = "Product Serial Number already exists";
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
        public async Task<ResponseModel> GetBatteryList(CustomerBattery_Search parameters)
        {
            var objList = await _ManageQCRepository.GetCustomerBatteryList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBatteryById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ManageQCRepository.GetCustomerBatteryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadBatteryTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_QCProductSerialNumber.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportCustomerBattery([FromQuery] ImportRequest request)
        {
            _response.IsSuccess = false;

            ExcelWorksheets currentSheet;
            ExcelWorksheet workSheet;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            int noOfCol, noOfRow;

            List<string[]> data = new List<string[]>();
            List<CustomerBattery_ImportData> lstCustomerBattery_ImportData = new List<CustomerBattery_ImportData>();
            IEnumerable<CustomerBattery_ImportDataValidation> lstCustomerBattery_ImportDataValidation;

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

                if (!string.Equals(workSheet.Cells[1, 1].Value.ToString(), "CustomerId", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 2].Value.ToString(), "PartCode", StringComparison.OrdinalIgnoreCase) ||
                   //!string.Equals(workSheet.Cells[1, 3].Value.ToString(), "SerialNumber", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 3].Value.ToString(), "ProductSerialNumber", StringComparison.OrdinalIgnoreCase) ||
                   //!string.Equals(workSheet.Cells[1, 5].Value.ToString(), "InvoiceNumber", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 4].Value.ToString(), "ManufacturingDate", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 5].Value.ToString(), "WarrantyStartDate", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 6].Value.ToString(), "WarrantyEndDate", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 7].Value.ToString(), "WarrantyStatus", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 8].Value.ToString(), "IsActive", StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Please upload a valid excel file";
                    return _response;
                }

                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 3].Value?.ToString()))
                    {
                        lstCustomerBattery_ImportData.Add(new CustomerBattery_ImportData()
                        {
                            CustomerId = workSheet.Cells[rowIterator, 1].Value?.ToString(),
                            PartCode = workSheet.Cells[rowIterator, 2].Value?.ToString(),
                            //SerialNumber = workSheet.Cells[rowIterator, 3].Value?.ToString(),
                            ProductSerialNumber = workSheet.Cells[rowIterator, 3].Value?.ToString(),
                            //InvoiceNumber = workSheet.Cells[rowIterator, 5].Value?.ToString(),
                            WarrantyStartDate = !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 4].Value?.ToString()) ? DateTime.ParseExact(workSheet.Cells[rowIterator, 4].Value?.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat) : null,
                            WarrantyEndDate = !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 5].Value?.ToString()) ? DateTime.ParseExact(workSheet.Cells[rowIterator, 5].Value?.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat) : null,
                            ManufacturingDate = !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 6].Value?.ToString()) ? DateTime.ParseExact(workSheet.Cells[rowIterator, 6].Value?.ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat) : null,
                            WarrantyStatus = workSheet.Cells[rowIterator, 7].Value?.ToString(),
                            IsActive = workSheet.Cells[rowIterator, 8].Value?.ToString()
                        });
                    }
                }
            }

            if (lstCustomerBattery_ImportData.Count == 0)
            {
                _response.Message = "File does not contains any record(s)";
                return _response;
            }

            lstCustomerBattery_ImportDataValidation = await _ManageQCRepository.ImportBattery(lstCustomerBattery_ImportData);

            _response.IsSuccess = true;
            _response.Message = "Record imported successfully";

            #region Generate Excel file for Invalid Data

            if (lstCustomerBattery_ImportDataValidation.ToList().Count > 0)
            {
                _response.Message = "Uploaded file contains invalid records, please check downloaded file for more details";
                _response.Data = GenerateInvalidImportDataFile(lstCustomerBattery_ImportDataValidation);

            }

            #endregion

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportCustomerBattery()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new CustomerBattery_Search();

            IEnumerable<CustomerBattery_Response> lstSizeObj = await _ManageQCRepository.GetCustomerBatteryList(request);

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

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Part Code";
                    WorkSheet1.Cells[1, 3].Value = "Customer Code";
                    WorkSheet1.Cells[1, 4].Value = "Product Serial Number";
                    WorkSheet1.Cells[1, 5].Value = "Product Category";
                    WorkSheet1.Cells[1, 6].Value = "Segment";
                    WorkSheet1.Cells[1, 7].Value = "Sub Segment";
                    WorkSheet1.Cells[1, 8].Value = "Product Model";
                    WorkSheet1.Cells[1, 9].Value = "Drawing Number";
                    WorkSheet1.Cells[1, 10].Value = "Manufacturing Date";
                    WorkSheet1.Cells[1, 11].Value = "Warranty";
                    WorkSheet1.Cells[1, 12].Value = "Warranty Start Date";
                    WorkSheet1.Cells[1, 13].Value = "Warranty End Date";
                    WorkSheet1.Cells[1, 14].Value = "Warranty Status";
                    WorkSheet1.Cells[1, 15].Value = "IsActive";


                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.PartCode;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.CustomerCode;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.ProductSerialNumber;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.ProductCategory;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.Segment;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.SubSegment;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.ProductModel;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.DrawingNumber;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.ManufacturingDate;
                        WorkSheet1.Cells[recordIndex, 10].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.Warranty;
                        WorkSheet1.Cells[recordIndex, 12].Value = items.WarrantyStartDate;
                        WorkSheet1.Cells[recordIndex, 12].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 13].Value = items.WarrantyEndDate;
                        WorkSheet1.Cells[recordIndex, 13].Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                        WorkSheet1.Cells[recordIndex, 14].Value = items.WarrantyStatus;
                        WorkSheet1.Cells[recordIndex, 15].Value = items.IsActive == true ? "Active" : "Inactive";

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
                    WorkSheet1.Column(10).AutoFit();
                    WorkSheet1.Column(11).AutoFit();
                    WorkSheet1.Column(12).AutoFit();
                    WorkSheet1.Column(13).AutoFit();
                    WorkSheet1.Column(14).AutoFit();
                    WorkSheet1.Column(15).AutoFit();

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

        private byte[] GenerateInvalidImportDataFile(IEnumerable<CustomerBattery_ImportDataValidation> lstCustomerBattery_ImportDataValidation)
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

                    WorkSheet1.Cells[1, 1].Value = "CustomerId";
                    WorkSheet1.Cells[1, 2].Value = "PartCode";
                    WorkSheet1.Cells[1, 3].Value = "ProductSerialNumber";
                    WorkSheet1.Cells[1, 4].Value = "ManufacturingDate";
                    WorkSheet1.Cells[1, 5].Value = "WarrantyStartDate";
                    WorkSheet1.Cells[1, 6].Value = "WarrantyEndDate";
                    WorkSheet1.Cells[1, 7].Value = "WarrantyStatus";
                    WorkSheet1.Cells[1, 8].Value = "ErrorMessage";

                    recordIndex = 2;

                    foreach (CustomerBattery_ImportDataValidation record in lstCustomerBattery_ImportDataValidation)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = record.CustomerId;
                        WorkSheet1.Cells[recordIndex, 2].Value = record.PartCode;
                        WorkSheet1.Cells[recordIndex, 3].Value = record.ProductSerialNumber;
                        WorkSheet1.Cells[recordIndex, 4].Value = !string.IsNullOrWhiteSpace(record.ManufacturingDate) ? Convert.ToDateTime(record.ManufacturingDate).ToString("dd/MM/yyyy") : "";
                        WorkSheet1.Cells[recordIndex, 5].Value = !string.IsNullOrWhiteSpace(record.WarrantyStartDate) ? Convert.ToDateTime(record.WarrantyStartDate).ToString("dd/MM/yyyy") : "";
                        WorkSheet1.Cells[recordIndex, 6].Value = !string.IsNullOrWhiteSpace(record.WarrantyEndDate) ? Convert.ToDateTime(record.WarrantyEndDate).ToString("dd/MM/yyyy") : ""; ;
                        WorkSheet1.Cells[recordIndex, 7].Value = record.WarrantyStatus;
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

        #endregion

        #region Customer Charger 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCustomerCharger(CustomerCharger_Request parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.ChargerSerial))
            {
                _response.Message = "ChargerSerial is required!";

                return _response;
            }

            int result = await _ManageQCRepository.SaveCustomerCharger(parameters);

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
        public async Task<ResponseModel> GetCustomerChargerList(CustomerCharger_Search parameters)
        {
            var objList = await _ManageQCRepository.GetCustomerChargerList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerChargerById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ManageQCRepository.GetCustomerChargerById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Accessory 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveAccessory(CustomerAccessory_Request parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.AccessoryName))
            {
                _response.Message = "AccessoryName is required!";

                return _response;
            }

            int result = await _ManageQCRepository.SaveManageQCAccessory(parameters);

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
        public async Task<ResponseModel> GetAccessoryList(CustomerAccessory_Search parameters)
        {
            var objList = await _ManageQCRepository.GetManageQCAccessoryList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetAccessoryById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ManageQCRepository.GetManageQCAccessoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteManageQCAccessory(int Id)
        {
            int result = await _ManageQCRepository.DeleteManageQCAccessory(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadAccessoryTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_QCAccessory.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportCustomerAccessory([FromQuery] ImportRequest request)
        {
            _response.IsSuccess = false;

            ExcelWorksheets currentSheet;
            ExcelWorksheet workSheet;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            int noOfCol, noOfRow;

            List<string[]> data = new List<string[]>();
            List<CustomerAccessory_ImportData> lstCustomerAccessory_ImportData = new List<CustomerAccessory_ImportData>();
            IEnumerable<CustomerAccessory_ImportDataValidation> lstCustomerAccessory_ImportDataValidation;

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

                if (!string.Equals(workSheet.Cells[1, 1].Value.ToString(), "CustomerId", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 2].Value.ToString(), "PartCode", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 3].Value.ToString(), "AccessoryBOMNumber", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 4].Value.ToString(), "DrawingNumber", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 5].Value.ToString(), "AccessoryName", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 6].Value.ToString(), "Quantity", StringComparison.OrdinalIgnoreCase) ||
                   !string.Equals(workSheet.Cells[1, 7].Value.ToString(), "IsActive", StringComparison.OrdinalIgnoreCase))
                {
                    _response.IsSuccess = false;
                    _response.Message = "Please upload a valid excel file";
                    return _response;
                }

                for (int rowIterator = 2; rowIterator <= noOfRow; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()))
                    {
                        lstCustomerAccessory_ImportData.Add(new CustomerAccessory_ImportData()
                        {
                            CustomerId = workSheet.Cells[rowIterator, 1].Value?.ToString(),
                            PartCode = workSheet.Cells[rowIterator, 2].Value?.ToString(),
                            AccessoryBOMNumber = workSheet.Cells[rowIterator, 3].Value?.ToString(),
                            DrawingNumber = workSheet.Cells[rowIterator, 4].Value?.ToString(),
                            AccessoryName = workSheet.Cells[rowIterator, 5].Value?.ToString(),
                            Quantity = workSheet.Cells[rowIterator, 6].Value?.ToString(),
                            IsActive = workSheet.Cells[rowIterator, 7].Value?.ToString()
                        });
                    }
                }
            }

            if (lstCustomerAccessory_ImportData.Count == 0)
            {
                _response.Message = "File does not contains any record(s)";
                return _response;
            }

            lstCustomerAccessory_ImportDataValidation = await _ManageQCRepository.ImportAccessory(lstCustomerAccessory_ImportData);

            _response.IsSuccess = true;
            _response.Message = "Record imported successfully";

            #region Generate Excel file for Invalid Data

            if (lstCustomerAccessory_ImportDataValidation.ToList().Count > 0)
            {
                _response.Message = "Uploaded file contains invalid records, please check downloaded file for more details";
                _response.Data = GenerateInvalidImportDataFile(lstCustomerAccessory_ImportDataValidation);

            }

            #endregion

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportCustomerAccessory()
        {
            _response.IsSuccess = false;
            byte[] result;
            int recordIndex;
            ExcelWorksheet WorkSheet1;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var request = new CustomerAccessory_Search();

            IEnumerable<CustomerAccessory_Response> lstSizeObj = await _ManageQCRepository.GetManageQCAccessoryList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Template_QCAccessory");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Part Code";
                    WorkSheet1.Cells[1, 3].Value = "Accessory BOM Number";
                    WorkSheet1.Cells[1, 4].Value = "Drawing Number";
                    WorkSheet1.Cells[1, 5].Value = "Accessory Name";
                    WorkSheet1.Cells[1, 6].Value = "Quantity";
                    WorkSheet1.Cells[1, 7].Value = "IsActive";


                    recordIndex = 2;

                    foreach (var items in lstSizeObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.PartCode;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.AccessoryBOMNumber;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.DrawingNumber;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.AccessoryName;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.Quantity;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.IsActive == true ? "Active" : "Inactive";

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
                _response.Message = "Exported successfully";
            }

            return _response;
        }

        private byte[] GenerateInvalidImportDataFile(IEnumerable<CustomerAccessory_ImportDataValidation> lstCustomerAccessory_ImportDataValidation)
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

                    WorkSheet1.Cells[1, 1].Value = "CustomerId";
                    WorkSheet1.Cells[1, 2].Value = "PartCode";
                    WorkSheet1.Cells[1, 3].Value = "AccessoryBOMNumber";
                    WorkSheet1.Cells[1, 4].Value = "DrawingNumber";
                    WorkSheet1.Cells[1, 5].Value = "AccessoryName";
                    WorkSheet1.Cells[1, 6].Value = "Quantity";
                    WorkSheet1.Cells[1, 7].Value = "ErrorMessage";

                    recordIndex = 2;

                    foreach (CustomerAccessory_ImportDataValidation record in lstCustomerAccessory_ImportDataValidation)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = record.CustomerId;
                        WorkSheet1.Cells[recordIndex, 2].Value = record.PartCode;
                        WorkSheet1.Cells[recordIndex, 3].Value = record.AccessoryBOMNumber;
                        WorkSheet1.Cells[recordIndex, 4].Value = record.DrawingNumber;
                        WorkSheet1.Cells[recordIndex, 5].Value = record.AccessoryName;
                        WorkSheet1.Cells[recordIndex, 6].Value = record.Quantity;
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
