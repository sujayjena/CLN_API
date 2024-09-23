using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Data;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;

        private readonly IVendorRepository _vendorRepository;
        private readonly IContactDetailRepository _contactDetailRepository;
        private readonly IAddressRepository _addressRepository;


        public VendorController(
            IFileManager fileManager,
            IVendorRepository vendorRepository,
            IContactDetailRepository contactDetailRepository,
            IAddressRepository addressRepository)
        {
            _fileManager = fileManager;
            _vendorRepository = vendorRepository;
            _contactDetailRepository = contactDetailRepository;
            _addressRepository = addressRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Vendor 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveVendor(Vendor_Request parameters)
        {
            // Image Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PanCardImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.PanCardImage_Base64, "\\Uploads\\Vendor\\", parameters.PanCardOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.PanCardImageFileName = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.GSTImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.GSTImage_Base64, "\\Uploads\\Vendor\\", parameters.GSTImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.GSTImageFileName = vUploadFile;
                }
            }

            int result = await _vendorRepository.SaveVendor(parameters);

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
                parameters.ContactDetail.RefId = result;
                parameters.ContactDetail.RefType = "Vendor";
                parameters.ContactDetail.IsDefault = true;

                parameters.AddressDetail.RefId = result;
                parameters.AddressDetail.RefType = "Vendor";
                parameters.AddressDetail.IsDefault = true;

                // Contact Detail
                if (!string.IsNullOrWhiteSpace(parameters.ContactDetail.ContactName))
                {
                    int resultContact = await _contactDetailRepository.SaveContactDetail(parameters.ContactDetail);
                }

                // Address Detail
                if (!string.IsNullOrWhiteSpace(parameters.AddressDetail.Address1))
                {
                    int resultAddressDetail = await _addressRepository.SaveAddress(parameters.AddressDetail);
                }

                _response.Message = "Record details saved sucessfully";
            }

            _response.Id = result;
            return _response;

        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetVendorList(BaseSearchEntity parameters)
        {
            var objList = await _vendorRepository.GetVendorList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetVendorById(int Id)
        {
            var vCustomerDetail_Response = new Vendor_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _vendorRepository.GetVendorById(Id);
                if (vResultObj != null)
                {
                    vCustomerDetail_Response.Id = Convert.ToInt32(vResultObj.Id);
                    vCustomerDetail_Response.VendorTypeId = vResultObj.VendorTypeId;
                    vCustomerDetail_Response.VendorType = vResultObj.VendorType;
                    vCustomerDetail_Response.VendorName = vResultObj.VendorName;
                    vCustomerDetail_Response.LandLineNumber = vResultObj.LandLineNumber;
                    vCustomerDetail_Response.MobileNumber = vResultObj.MobileNumber;
                    vCustomerDetail_Response.EmailId = vResultObj.EmailId;
                    vCustomerDetail_Response.SpecialRemark = vResultObj.SpecialRemark;
                    vCustomerDetail_Response.PanCardNo = vResultObj.PanCardNo;
                    vCustomerDetail_Response.PanCardImage = vResultObj.PanCardImage;
                    vCustomerDetail_Response.PanCardOriginalFileName = vResultObj.PanCardOriginalFileName;
                    vCustomerDetail_Response.PanCardImageURL = vResultObj.PanCardImageURL;
                    vCustomerDetail_Response.GSTNo = vResultObj.GSTNo;
                    vCustomerDetail_Response.GSTImage = vResultObj.GSTImage;
                    vCustomerDetail_Response.GSTImageOriginalFileName = vResultObj.GSTImageOriginalFileName;
                    vCustomerDetail_Response.GSTImageURL = vResultObj.GSTImageURL;
                    vCustomerDetail_Response.IsActive = vResultObj.IsActive;

                    var vContactDetail_Search = new ContactDetail_Search()
                    {
                        RefId = Convert.ToInt32(vResultObj.Id),
                        RefType = "Vendor"
                    };

                    // Contact Detail
                    var vResultContactListObj = await _contactDetailRepository.GetContactDetailList(vContactDetail_Search);
                    var vContactObj = vResultContactListObj.ToList().Where(x => x.IsDefault == true).FirstOrDefault();
                    if (vContactObj != null)
                    {
                        vCustomerDetail_Response.ContactDetail.Id = vContactObj.Id;
                        vCustomerDetail_Response.ContactDetail.RefId = Convert.ToInt32(vContactObj.RefId);
                        vCustomerDetail_Response.ContactDetail.RefType = vContactObj.RefType;
                        vCustomerDetail_Response.ContactDetail.ContactName = vContactObj.ContactName;
                        vCustomerDetail_Response.ContactDetail.MobileNumber = vContactObj.MobileNumber;
                        vCustomerDetail_Response.ContactDetail.EmailId = vContactObj.EmailId;
                        vCustomerDetail_Response.ContactDetail.AadharCardImageFileName = vContactObj.AadharCardImageFileName;
                        vCustomerDetail_Response.ContactDetail.AadharCardOriginalFileName = vContactObj.AadharCardOriginalFileName;
                        vCustomerDetail_Response.ContactDetail.AadharCardImageURL = vContactObj.AadharCardImageURL;
                        vCustomerDetail_Response.ContactDetail.PanCardImageFileName = vContactObj.PanCardImageFileName;
                        vCustomerDetail_Response.ContactDetail.PanCardOriginalFileName = vContactObj.PanCardOriginalFileName;
                        vCustomerDetail_Response.ContactDetail.PanCardImageURL = vContactObj.PanCardImageURL;
                        vCustomerDetail_Response.ContactDetail.IsDefault = vContactObj.IsDefault;
                        vCustomerDetail_Response.ContactDetail.IsActive = vContactObj.IsActive;
                    }

                    var vAddress_Search = new Address_Search()
                    {
                        RefId = Convert.ToInt32(vResultObj.Id),
                        RefType = "Vendor"
                    };

                    // Address Detail
                    var vResultAddressListObj = await _addressRepository.GetAddressList(vAddress_Search);
                    var vAddressObj = vResultAddressListObj.ToList().Where(x => x.IsDefault == true).FirstOrDefault();
                    if (vAddressObj != null)
                    {
                        vCustomerDetail_Response.AddressDetail.Id = vAddressObj.Id;
                        vCustomerDetail_Response.AddressDetail.RefId = Convert.ToInt32(vAddressObj.RefId);
                        vCustomerDetail_Response.AddressDetail.RefType = vAddressObj.RefType;
                        vCustomerDetail_Response.AddressDetail.Address1 = vAddressObj.Address1;
                        vCustomerDetail_Response.AddressDetail.RegionId = vAddressObj.RegionId;
                        vCustomerDetail_Response.AddressDetail.RegionName = vAddressObj.RegionName;
                        vCustomerDetail_Response.AddressDetail.StateId = vAddressObj.StateId;
                        vCustomerDetail_Response.AddressDetail.StateName = vAddressObj.StateName;
                        vCustomerDetail_Response.AddressDetail.DistrictId = vAddressObj.DistrictId;
                        vCustomerDetail_Response.AddressDetail.DistrictName = vAddressObj.DistrictName;
                        vCustomerDetail_Response.AddressDetail.CityId = vAddressObj.CityId;
                        vCustomerDetail_Response.AddressDetail.CityName = vAddressObj.CityName;
                        vCustomerDetail_Response.AddressDetail.PinCode = vAddressObj.PinCode;
                        vCustomerDetail_Response.AddressDetail.IsDefault = vAddressObj.IsDefault;
                        vCustomerDetail_Response.AddressDetail.IsActive = vAddressObj.IsActive;
                    }
                }

                _response.Data = vCustomerDetail_Response;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DownloadVendorTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_Vendor.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportVendorVendor([FromQuery] ImportRequest request)
        {
            byte[] result;
            _response.IsSuccess = false;

            int noOfColVendor, noOfRowVendor, noOfColVendorContact, noOfRowVendorContact, noOfColVendorAddress, noOfRowVendorAddress;
            bool tableHasNullVendor = false, tableHasNullVendorContact = false, tableHasNullVendorAddress = false;

            List<Vendor_ImportData> lstVendorImportRequestModel = new List<Vendor_ImportData>();
            List<Contact_ImportData> lstVendorContactImportRequestModel = new List<Contact_ImportData>();
            List<Address_ImportData> lstVendorAddressImportRequestModel = new List<Address_ImportData>();

            DataTable dtVendorTable;
            DataTable dtVendorContactTable;
            DataTable dtVendorAddressTable;

            IEnumerable<Vendor_ImportDataValidation> lstVendor_ImportDataValidation_Result;
            IEnumerable<Contact_ImportDataValidation> lstVendorContact_ImportDataValidation_Result;
            IEnumerable<Address_ImportDataValidation> lstVendorAddress_ImportDataValidation_Result;

            ExcelWorksheets currentSheet;
            ExcelWorksheet workSheet;
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

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

                workSheet = currentSheet[0];
                noOfColVendor = workSheet.Dimension.End.Column;
                noOfRowVendor = workSheet.Dimension.End.Row;

                for (int rowIterator = 2; rowIterator <= noOfRowVendor; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()))
                    {
                        Vendor_ImportData record = new Vendor_ImportData();
                        record.VendorType = workSheet.Cells[rowIterator, 1].Value.ToString();
                        record.VendorName = workSheet.Cells[rowIterator, 2].Value.ToString();
                        record.LandLineNumber = workSheet.Cells[rowIterator, 3].Value.ToString();
                        record.MobileNumber = workSheet.Cells[rowIterator, 4].Value.ToString();
                        record.Email = workSheet.Cells[rowIterator, 5].Value.ToString();
                        record.PanCardNo = workSheet.Cells[rowIterator, 6].Value.ToString();
                        record.GSTNo = workSheet.Cells[rowIterator, 7].Value.ToString();
                        record.SpecialRemark = workSheet.Cells[rowIterator, 8].Value.ToString();
                        record.IsActive = workSheet.Cells[rowIterator, 9].Value.ToString();

                        lstVendorImportRequestModel.Add(record);
                    }
                }

                workSheet = currentSheet[1];
                noOfColVendorContact = workSheet.Dimension.End.Column;
                noOfRowVendorContact = workSheet.Dimension.End.Row;

                for (int rowIterator = 2; rowIterator <= noOfRowVendorContact; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()))
                    {
                        Contact_ImportData record = new Contact_ImportData();
                        record.CustomerName = string.Empty;
                        record.VendorName = workSheet.Cells[rowIterator, 1].Value.ToString();
                        record.ContactName = workSheet.Cells[rowIterator, 2].Value.ToString();
                        record.MobileNumber = workSheet.Cells[rowIterator, 3].Value.ToString();
                        record.Email = workSheet.Cells[rowIterator, 4].Value.ToString();
                        record.IsActive = workSheet.Cells[rowIterator, 5].Value.ToString();

                        lstVendorContactImportRequestModel.Add(record);
                    }
                }

                workSheet = currentSheet[2];
                noOfColVendorAddress = workSheet.Dimension.End.Column;
                noOfRowVendorAddress = workSheet.Dimension.End.Row;

                for (int rowIterator = 2; rowIterator <= noOfRowVendorAddress; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()))
                    {
                        Address_ImportData record = new Address_ImportData();
                        record.CustomerName = string.Empty;
                        record.VendorName = workSheet.Cells[rowIterator, 1].Value.ToString();
                        record.Address = workSheet.Cells[rowIterator, 2].Value.ToString();
                        record.Region = workSheet.Cells[rowIterator, 3].Value.ToString();
                        record.State = workSheet.Cells[rowIterator, 4].Value.ToString();
                        record.District = workSheet.Cells[rowIterator, 5].Value.ToString();
                        record.City = workSheet.Cells[rowIterator, 6].Value.ToString();
                        record.PinCode = workSheet.Cells[rowIterator, 7].Value.ToString();
                        record.IsActive = workSheet.Cells[rowIterator, 8].Value.ToString();

                        lstVendorAddressImportRequestModel.Add(record);
                    }
                }

                if (lstVendorImportRequestModel.Count == 0 || lstVendorContactImportRequestModel.Count == 0 || lstVendorAddressImportRequestModel.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Uploaded Vendorfdata file does not contains any record";
                    return _response;
                };

                dtVendorTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lstVendorImportRequestModel), typeof(DataTable));
                dtVendorContactTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lstVendorContactImportRequestModel), typeof(DataTable));
                dtVendorAddressTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lstVendorAddressImportRequestModel), typeof(DataTable));

                //Excel Column Mismatch check. If column name has been changed then it's value will be null;
                foreach (DataRow row in dtVendorTable.Rows)
                {
                    foreach (DataColumn col in dtVendorTable.Columns)
                    {
                        if (row[col] == DBNull.Value)
                        {
                            tableHasNullVendor = true;
                            break;
                        }
                    }
                }

                //Excel Column Mismatch check. If column name has been changed then it's value will be null;
                foreach (DataRow row in dtVendorContactTable.Rows)
                {
                    foreach (DataColumn col in dtVendorContactTable.Columns)
                    {
                        if (row[col] == DBNull.Value)
                        {
                            tableHasNullVendorContact = true;
                            break;
                        }
                    }
                }

                //Excel Column Mismatch check. If column name has been changed then it's value will be null;
                foreach (DataRow row in dtVendorAddressTable.Rows)
                {
                    foreach (DataColumn col in dtVendorAddressTable.Columns)
                    {
                        if (row[col] == DBNull.Value)
                        {
                            tableHasNullVendorAddress = true;
                            break;
                        }
                    }
                }


                if (tableHasNullVendor || tableHasNullVendorContact || tableHasNullVendorAddress)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Please upload a valid excel file. Please Download Format file for reference.";
                    return _response;
                }

                // Import Data
                lstVendor_ImportDataValidation_Result = await _vendorRepository.ImportVendor(lstVendorImportRequestModel);
                lstVendorContact_ImportDataValidation_Result = await _vendorRepository.ImportVendorContact(lstVendorContactImportRequestModel);
                lstVendorAddress_ImportDataValidation_Result = await _vendorRepository.ImportVendorAddress(lstVendorAddressImportRequestModel);


                _response.IsSuccess = true;
                _response.Message = "Record imported successfully";

                #region Generate Excel file for Invalid Data

                if (lstVendor_ImportDataValidation_Result.ToList().Count > 0 || lstVendorContact_ImportDataValidation_Result.ToList().Count > 0 || lstVendorAddress_ImportDataValidation_Result.ToList().Count > 0)
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (ExcelPackage excelInvalidData = new ExcelPackage())
                        {
                            if (lstVendor_ImportDataValidation_Result.ToList().Count > 0)
                            {
                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                int recordIndex;
                                ExcelWorksheet WorkSheet1 = excelInvalidData.Workbook.Worksheets.Add("Invalid_Vendor_Records");
                                WorkSheet1.TabColor = System.Drawing.Color.Black;
                                WorkSheet1.DefaultRowHeight = 12;

                                //Header of table
                                WorkSheet1.Row(1).Height = 20;
                                WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                WorkSheet1.Row(1).Style.Font.Bold = true;

                                WorkSheet1.Cells[1, 1].Value = "VendorType";
                                WorkSheet1.Cells[1, 2].Value = "VendorName";
                                WorkSheet1.Cells[1, 3].Value = "LandLineNumber";
                                WorkSheet1.Cells[1, 4].Value = "MobileNumber";
                                WorkSheet1.Cells[1, 5].Value = "Email";
                                WorkSheet1.Cells[1, 6].Value = "PanCardNo";
                                WorkSheet1.Cells[1, 7].Value = "GSTNo";
                                WorkSheet1.Cells[1, 8].Value = "SpecialRemark";
                                WorkSheet1.Cells[1, 9].Value = "ErrorMessage";

                                recordIndex = 2;

                                foreach (Vendor_ImportDataValidation record in lstVendor_ImportDataValidation_Result)
                                {
                                    WorkSheet1.Cells[recordIndex, 1].Value = record.VendorType;
                                    WorkSheet1.Cells[recordIndex, 2].Value = record.VendorName;
                                    WorkSheet1.Cells[recordIndex, 3].Value = record.LandLineNumber;
                                    WorkSheet1.Cells[recordIndex, 4].Value = record.MobileNumber;
                                    WorkSheet1.Cells[recordIndex, 5].Value = record.Email;
                                    WorkSheet1.Cells[recordIndex, 6].Value = record.PanCardNo;
                                    WorkSheet1.Cells[recordIndex, 7].Value = record.GSTNo;
                                    WorkSheet1.Cells[recordIndex, 8].Value = record.SpecialRemark;
                                    WorkSheet1.Cells[recordIndex, 9].Value = record.ValidationMessage;

                                    recordIndex += 1;
                                }

                                WorkSheet1.Columns.AutoFit();
                            }

                            if (lstVendorContact_ImportDataValidation_Result.ToList().Count > 0)
                            {
                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                int recordIndex;
                                ExcelWorksheet WorkSheet1 = excelInvalidData.Workbook.Worksheets.Add("Invalid_Contact_Records");
                                WorkSheet1.TabColor = System.Drawing.Color.Black;
                                WorkSheet1.DefaultRowHeight = 12;

                                //Header of table
                                WorkSheet1.Row(1).Height = 20;
                                WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                WorkSheet1.Row(1).Style.Font.Bold = true;

                                WorkSheet1.Cells[1, 1].Value = "VendorName";
                                WorkSheet1.Cells[1, 2].Value = "ContactName";
                                WorkSheet1.Cells[1, 3].Value = "MobileNumber";
                                WorkSheet1.Cells[1, 4].Value = "Email";
                                WorkSheet1.Cells[1, 5].Value = "ErrorMessage";

                                recordIndex = 2;

                                foreach (Contact_ImportDataValidation record in lstVendorContact_ImportDataValidation_Result)
                                {
                                    WorkSheet1.Cells[recordIndex, 1].Value = record.VendorName;
                                    WorkSheet1.Cells[recordIndex, 2].Value = record.ContactName;
                                    WorkSheet1.Cells[recordIndex, 3].Value = record.MobileNumber;
                                    WorkSheet1.Cells[recordIndex, 4].Value = record.Email;
                                    WorkSheet1.Cells[recordIndex, 5].Value = record.ValidationMessage;

                                    recordIndex += 1;
                                }

                                WorkSheet1.Columns.AutoFit();
                            }

                            if (lstVendorAddress_ImportDataValidation_Result.ToList().Count > 0)
                            {
                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                int recordIndex;
                                ExcelWorksheet WorkSheet1 = excelInvalidData.Workbook.Worksheets.Add("Invalid_Address_Records");
                                WorkSheet1.TabColor = System.Drawing.Color.Black;
                                WorkSheet1.DefaultRowHeight = 12;

                                //Header of table
                                WorkSheet1.Row(1).Height = 20;
                                WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                WorkSheet1.Row(1).Style.Font.Bold = true;

                                WorkSheet1.Cells[1, 1].Value = "VendorName";
                                WorkSheet1.Cells[1, 2].Value = "Address";
                                WorkSheet1.Cells[1, 3].Value = "Region";
                                WorkSheet1.Cells[1, 4].Value = "State";
                                WorkSheet1.Cells[1, 5].Value = "District";
                                WorkSheet1.Cells[1, 6].Value = "City";
                                WorkSheet1.Cells[1, 7].Value = "PinCode";
                                WorkSheet1.Cells[1, 8].Value = "ErrorMessage";

                                recordIndex = 2;

                                foreach (Address_ImportDataValidation record in lstVendorAddress_ImportDataValidation_Result)
                                {
                                    WorkSheet1.Cells[recordIndex, 1].Value = record.VendorName;
                                    WorkSheet1.Cells[recordIndex, 2].Value = record.Address;
                                    WorkSheet1.Cells[recordIndex, 3].Value = record.Region;
                                    WorkSheet1.Cells[recordIndex, 4].Value = record.State;
                                    WorkSheet1.Cells[recordIndex, 5].Value = record.District;
                                    WorkSheet1.Cells[recordIndex, 6].Value = record.City;
                                    WorkSheet1.Cells[recordIndex, 7].Value = record.PinCode;
                                    WorkSheet1.Cells[recordIndex, 8].Value = record.ValidationMessage;

                                    recordIndex += 1;
                                }

                                WorkSheet1.Columns.AutoFit();
                            }

                            excelInvalidData.SaveAs(memoryStream);
                            memoryStream.Position = 0;
                            result = memoryStream.ToArray();

                            _response.Data = result;
                        }
                    }
                }

                #endregion

                return _response;
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ExportVendorVendor()
        {
            _response.IsSuccess = false;
            byte[] result;

            var request = new BaseSearchEntity();

            var lstVendorListObj = await _vendorRepository.GetVendorList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Vendor");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Vendor Type";
                    WorkSheet1.Cells[1, 2].Value = "Vendor Name";
                    WorkSheet1.Cells[1, 3].Value = "Vendor Code";
                    WorkSheet1.Cells[1, 4].Value = "LandLine Number";
                    WorkSheet1.Cells[1, 5].Value = "Mobile Number";
                    WorkSheet1.Cells[1, 6].Value = "Email";
                    WorkSheet1.Cells[1, 7].Value = "Website";
                    WorkSheet1.Cells[1, 8].Value = "Special Remark";
                    WorkSheet1.Cells[1, 9].Value = "Vendor Remark";
                    WorkSheet1.Cells[1, 10].Value = "Ref Party";
                    //WorkSheet1.Cells[1, 11].Value = "IsActive";

                    recordIndex = 2;
                    foreach (var items in lstVendorListObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.VendorType;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.VendorName;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.LandLineNumber;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.MobileNumber;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.EmailId;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.PanCardNo;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.GSTNo;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.SpecialRemark;
                        //WorkSheet1.Cells[recordIndex, 10].Value = items.IsActive == true ? "Active" : "Inactive";

                        recordIndex += 1;
                    }

                    WorkSheet1.Columns.AutoFit();


                    // Contact
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Contact");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Vendor Name";
                    WorkSheet1.Cells[1, 2].Value = "Contact Name";
                    WorkSheet1.Cells[1, 3].Value = "Mobile Number";
                    WorkSheet1.Cells[1, 4].Value = "Email";
                    WorkSheet1.Cells[1, 5].Value = "IsActive";

                    recordIndex = 2;
                    foreach (var items in lstVendorListObj.ToList().Distinct())
                    {
                        if (items.Id == 13)
                        {
                            string fdf = "";
                        }

                        var vContactDetail_Search = new ContactDetail_Search()
                        {
                            RefId = Convert.ToInt32(items.Id),
                            RefType = "Vendor"
                        };

                        var lstContactListObj = await _contactDetailRepository.GetContactDetailList(vContactDetail_Search);
                        foreach (var itemContact in lstContactListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.VendorName;
                            WorkSheet1.Cells[recordIndex, 2].Value = itemContact.ContactName;
                            WorkSheet1.Cells[recordIndex, 3].Value = itemContact.MobileNumber;
                            WorkSheet1.Cells[recordIndex, 4].Value = itemContact.EmailId;
                            WorkSheet1.Cells[recordIndex, 5].Value = itemContact.IsActive == true ? "Active" : "Inactive";

                            recordIndex += 1;
                        }
                    }
                    WorkSheet1.Columns.AutoFit();


                    // Address
                    WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Address");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Vendor Name";
                    WorkSheet1.Cells[1, 2].Value = "Address1";
                    WorkSheet1.Cells[1, 3].Value = "RegionName";
                    WorkSheet1.Cells[1, 4].Value = "StateName";
                    WorkSheet1.Cells[1, 5].Value = "DistrictName";
                    WorkSheet1.Cells[1, 6].Value = "CityName";
                    WorkSheet1.Cells[1, 7].Value = "PinCode";
                    WorkSheet1.Cells[1, 8].Value = "IsActive";

                    recordIndex = 2;
                    foreach (var items in lstVendorListObj)
                    {
                        var vAddress_Search = new Address_Search()
                        {
                            RefId = Convert.ToInt32(items.Id),
                            RefType = "Vendor"
                        };

                        var lstAddressListObj = await _addressRepository.GetAddressList(vAddress_Search);
                        foreach (var itemAddress in lstAddressListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.VendorName;
                            WorkSheet1.Cells[recordIndex, 2].Value = itemAddress.Address1;
                            WorkSheet1.Cells[recordIndex, 3].Value = itemAddress.RegionName;
                            WorkSheet1.Cells[recordIndex, 4].Value = itemAddress.StateName;
                            WorkSheet1.Cells[recordIndex, 5].Value = itemAddress.DistrictName;
                            WorkSheet1.Cells[recordIndex, 6].Value = itemAddress.CityName;
                            WorkSheet1.Cells[recordIndex, 7].Value = itemAddress.PinCode;
                            WorkSheet1.Cells[recordIndex, 8].Value = items.IsActive == true ? "Active" : "Inactive";

                            recordIndex += 1;
                        }
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

        #endregion

        #region Vendor Detail

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveVendorDetail(VendorDetail_Request parameters)
        {
            int result = await _vendorRepository.SaveVendorDetail(parameters);

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
        public async Task<ResponseModel> GetVendorDetailList(VendorDetail_Search parameters)
        {
            var objList = await _vendorRepository.GetVendorDetailList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetVendorDetailById(int Id)
        {
            var vCustomerDetail_Response = new Vendor_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _vendorRepository.GetVendorDetailById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Vendor Contact Detail

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveVendorContactDetail(ContactDetail_Request parameters)
        {
            // Image Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.AadharCardImage_Base64))
            {
                var vUploadFile_AadharCardImage = _fileManager.UploadDocumentsBase64ToFile(parameters.AadharCardImage_Base64, "\\Uploads\\Vendor\\", parameters.AadharCardOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile_AadharCardImage))
                {
                    parameters.AadharCardImageFileName = vUploadFile_AadharCardImage;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PanCardImage_Base64))
            {
                var vUploadFile_PanCard = _fileManager.UploadDocumentsBase64ToFile(parameters.PanCardImage_Base64, "\\Uploads\\Vendor\\", parameters.PanCardOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile_PanCard))
                {
                    parameters.PanCardImageFileName = vUploadFile_PanCard;
                }
            }

            int result = await _contactDetailRepository.SaveContactDetail(parameters);

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
        public async Task<ResponseModel> GetVendorContactDetailList(ContactDetail_Search parameters)
        {
            var objList = await _contactDetailRepository.GetContactDetailList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetVendorContactDetailById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _contactDetailRepository.GetContactDetailById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Vendor Address

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveVendorAddressDetail(Address_Request parameters)
        {
            int result = await _addressRepository.SaveAddress(parameters);

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
        public async Task<ResponseModel> GetVendorAddressDetailList(Address_Search parameters)
        {
            var objList = await _addressRepository.GetAddressList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetVendorAddressDetailById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _addressRepository.GetAddressById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
