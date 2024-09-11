using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System.Data;
using Newtonsoft.Json;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;

        private readonly ICustomerRepository _customerRepository;
        private readonly IContactDetailRepository _contactDetailRepository;
        private readonly IAddressRepository _addressRepository;

        public CustomerController(
            IFileManager fileManager,
            ICustomerRepository customerRepository,
            IContactDetailRepository contactDetailRepository,
            IAddressRepository addressRepository
            )
        {
            _fileManager = fileManager;
            _customerRepository = customerRepository;
            _contactDetailRepository = contactDetailRepository;
            _addressRepository = addressRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Customer 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCustomer(Customer_Request parameters)
        {
            // Image Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.GSTImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.GSTImage_Base64, "\\Uploads\\Customer\\", parameters.GSTImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.GSTImageFileName = vUploadFile;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PanCardImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.PanCardImage_Base64, "\\Uploads\\Customer\\", parameters.PanCardOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.PanCardImageFileName = vUploadFile;
                }
            }

            int result = await _customerRepository.SaveCustomer(parameters);

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
                parameters.ContactDetail.RefType = "Customer";
                parameters.ContactDetail.IsDefault = true;

                parameters.AddressDetail.RefId = result;
                parameters.AddressDetail.RefType = "Customer";
                parameters.AddressDetail.IsDefault = true;

                // Contact Detail
                if (!string.IsNullOrWhiteSpace(parameters.ContactDetail.ContactName))
                {
                    // Image Upload
                    if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.ContactDetail.AadharCardImage_Base64))
                    {
                        var vUploadFile_AadharCardImage = _fileManager.UploadDocumentsBase64ToFile(parameters.ContactDetail.AadharCardImage_Base64, "\\Uploads\\Customer\\", parameters.ContactDetail.AadharCardOriginalFileName);

                        if (!string.IsNullOrWhiteSpace(vUploadFile_AadharCardImage))
                        {
                            parameters.ContactDetail.AadharCardImageFileName = vUploadFile_AadharCardImage;
                        }
                    }

                    if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.ContactDetail.PanCardImage_Base64))
                    {
                        var vUploadFile_PanCard = _fileManager.UploadDocumentsBase64ToFile(parameters.ContactDetail.PanCardImage_Base64, "\\Uploads\\Customer\\", parameters.ContactDetail.PanCardOriginalFileName);

                        if (!string.IsNullOrWhiteSpace(vUploadFile_PanCard))
                        {
                            parameters.ContactDetail.PanCardImageFileName = vUploadFile_PanCard;
                        }
                    }

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
        public async Task<ResponseModel> GetCustomerList(BaseSearchEntity parameters)
        {
            var objList = await _customerRepository.GetCustomerList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerById(int Id)
        {
            var vCustomerDetail_Response = new CustomerDetail_Response();

            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _customerRepository.GetCustomerById(Id);
                if (vResultObj != null)
                {
                    vCustomerDetail_Response.Id = Convert.ToInt32(vResultObj.Id);
                    vCustomerDetail_Response.CustomerTypeId = vResultObj.CustomerTypeId;
                    vCustomerDetail_Response.CustomerName = vResultObj.CustomerName;
                    vCustomerDetail_Response.CustomerCode = vResultObj.CustomerCode;
                    vCustomerDetail_Response.LandLineNumber = vResultObj.LandLineNumber;
                    vCustomerDetail_Response.MobileNumber = vResultObj.MobileNumber;
                    vCustomerDetail_Response.EmailId = vResultObj.EmailId;
                    vCustomerDetail_Response.Website = vResultObj.Website;
                    vCustomerDetail_Response.Remark = vResultObj.Remark;
                    vCustomerDetail_Response.CustomerRemark = vResultObj.CustomerRemark;
                    vCustomerDetail_Response.RefParty = vResultObj.RefParty;
                    vCustomerDetail_Response.GSTImage = vResultObj.GSTImage;
                    vCustomerDetail_Response.GSTImageOriginalFileName = vResultObj.GSTImageOriginalFileName;
                    vCustomerDetail_Response.GSTImageURL = vResultObj.GSTImageURL;
                    vCustomerDetail_Response.PanCardImage = vResultObj.PanCardImage;
                    vCustomerDetail_Response.PanCardOriginalFileName = vResultObj.PanCardOriginalFileName;
                    vCustomerDetail_Response.PanCardImageURL = vResultObj.PanCardImageURL;
                    vCustomerDetail_Response.IsActive = vResultObj.IsActive;


                    var vContactDetail_Search = new ContactDetail_Search()
                    {
                        RefId = Convert.ToInt32(vResultObj.Id),
                        RefType = "Customer"
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
                        RefType = "Customer"
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
        public async Task<ResponseModel> DownloadCustomerTemplate()
        {
            byte[]? formatFile = await Task.Run(() => _fileManager.GetFormatFileFromPath("Template_Customer.xlsx"));

            if (formatFile != null)
            {
                _response.Data = formatFile;
            }

            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> ImportCustomerCustomer([FromQuery] ImportRequest request)
        {
            byte[] result;
            _response.IsSuccess = false;

            int noOfColCustomer, noOfRowCustomer, noOfColCustomerContact, noOfRowCustomerContact, noOfColCustomerAddress, noOfRowCustomerAddress;
            bool tableHasNullCustomer = false, tableHasNullCustomerContact = false, tableHasNullCustomerAddress = false;

            List<Customer_ImportData> lstCustomerImportRequestModel = new List<Customer_ImportData>();
            List<Contact_ImportData> lstCustomerContactImportRequestModel = new List<Contact_ImportData>();
            List<Address_ImportData> lstCustomerAddressImportRequestModel = new List<Address_ImportData>();

            DataTable dtCustomerTable;
            DataTable dtCustomerContactTable;
            DataTable dtCustomerAddressTable;

            IEnumerable<Customer_ImportDataValidation> lstCustomer_ImportDataValidation_Result;
            IEnumerable<Contact_ImportDataValidation> lstCustomerContact_ImportDataValidation_Result;
            IEnumerable<Address_ImportDataValidation> lstCustomerAddress_ImportDataValidation_Result;

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
                noOfColCustomer = workSheet.Dimension.End.Column;
                noOfRowCustomer = workSheet.Dimension.End.Row;

                for (int rowIterator = 2; rowIterator <= noOfRowCustomer; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()))
                    {
                        Customer_ImportData record = new Customer_ImportData();
                        record.CustomerType = workSheet.Cells[rowIterator, 1].Value != null ? workSheet.Cells[rowIterator, 1].Value.ToString() : null;
                        record.CustomerName = workSheet.Cells[rowIterator, 2].Value != null ? workSheet.Cells[rowIterator, 2].Value.ToString() : null;
                        record.CustomerCode = workSheet.Cells[rowIterator, 3].Value != null ? workSheet.Cells[rowIterator, 3].Value.ToString() : null;
                        record.LandLineNumber = workSheet.Cells[rowIterator, 4].Value != null ? workSheet.Cells[rowIterator, 4].Value.ToString() : null;
                        record.MobileNumber = workSheet.Cells[rowIterator, 5].Value != null ? workSheet.Cells[rowIterator, 5].Value.ToString() : null;
                        record.Email = workSheet.Cells[rowIterator, 6].Value != null ? workSheet.Cells[rowIterator, 6].Value.ToString() : null;
                        record.Website = workSheet.Cells[rowIterator, 7].Value != null ? workSheet.Cells[rowIterator, 7].Value.ToString() : null;
                        record.SpecialRemark = workSheet.Cells[rowIterator, 8].Value != null ? workSheet.Cells[rowIterator, 8].Value.ToString() : null;
                        record.CustomerRemark =  workSheet.Cells[rowIterator, 9].Value != null ? workSheet.Cells[rowIterator, 9].Value.ToString() : null;
                        record.RefParty = workSheet.Cells[rowIterator, 10].Value != null ? workSheet.Cells[rowIterator, 10].Value.ToString() : null;
                        record.IsActive = workSheet.Cells[rowIterator, 11].Value != null ? workSheet.Cells[rowIterator, 11].Value.ToString() : null;

                        lstCustomerImportRequestModel.Add(record);
                    }
                }

                workSheet = currentSheet[1];
                noOfColCustomerContact = workSheet.Dimension.End.Column;
                noOfRowCustomerContact = workSheet.Dimension.End.Row;

                for (int rowIterator = 2; rowIterator <= noOfRowCustomerContact; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()))
                    {
                        Contact_ImportData record = new Contact_ImportData();
                        record.CustomerName = workSheet.Cells[rowIterator, 1].Value != null ? workSheet.Cells[rowIterator, 1].Value.ToString() : null;
                        record.VendorName = string.Empty;
                        record.ContactName = workSheet.Cells[rowIterator, 2].Value != null ? workSheet.Cells[rowIterator, 2].Value.ToString() : null;
                        record.MobileNumber = workSheet.Cells[rowIterator, 3].Value != null ? workSheet.Cells[rowIterator, 3].Value.ToString() : null;
                        record.Email = workSheet.Cells[rowIterator, 4].Value != null ? workSheet.Cells[rowIterator, 4].Value.ToString() : null;
                        record.IsActive = workSheet.Cells[rowIterator, 5].Value != null ? workSheet.Cells[rowIterator, 5].Value.ToString() : null;

                        lstCustomerContactImportRequestModel.Add(record);
                    }
                }

                workSheet = currentSheet[2];
                noOfColCustomerAddress = workSheet.Dimension.End.Column;
                noOfRowCustomerAddress = workSheet.Dimension.End.Row;

                for (int rowIterator = 2; rowIterator <= noOfRowCustomerAddress; rowIterator++)
                {
                    if (!string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 1].Value?.ToString()) && !string.IsNullOrWhiteSpace(workSheet.Cells[rowIterator, 2].Value?.ToString()))
                    {
                        Address_ImportData record = new Address_ImportData();
                        record.CustomerName = workSheet.Cells[rowIterator, 1].Value != null ? workSheet.Cells[rowIterator, 1].Value.ToString() : null;
                        record.VendorName = string.Empty;
                        record.Address = workSheet.Cells[rowIterator, 2].Value != null ? workSheet.Cells[rowIterator, 2].Value.ToString() : null;
                        record.Region = workSheet.Cells[rowIterator, 3].Value != null ? workSheet.Cells[rowIterator, 3].Value.ToString() : null;
                        record.State = workSheet.Cells[rowIterator, 4].Value != null ? workSheet.Cells[rowIterator, 4].Value.ToString() : null;
                        record.District = workSheet.Cells[rowIterator, 5].Value != null ? workSheet.Cells[rowIterator, 5].Value.ToString() : null;
                        record.City = workSheet.Cells[rowIterator, 6].Value != null ? workSheet.Cells[rowIterator, 6].Value.ToString() : null;
                        record.PinCode = workSheet.Cells[rowIterator, 7].Value != null ? workSheet.Cells[rowIterator, 7].Value.ToString() : null;
                        record.IsActive = workSheet.Cells[rowIterator, 8].Value != null ? workSheet.Cells[rowIterator, 8].Value.ToString() : null;


                        lstCustomerAddressImportRequestModel.Add(record);
                    }
                }

                if (lstCustomerImportRequestModel.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Uploaded customerfdata file does not contains any record";
                    return _response;
                };

                dtCustomerTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lstCustomerImportRequestModel), typeof(DataTable));
                dtCustomerContactTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lstCustomerContactImportRequestModel), typeof(DataTable));
                dtCustomerAddressTable = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(lstCustomerAddressImportRequestModel), typeof(DataTable));

                //Excel Column Mismatch check. If column name has been changed then it's value will be null;
                foreach (DataRow row in dtCustomerTable.Rows)
                {
                    foreach (DataColumn col in dtCustomerTable.Columns)
                    {
                        if (row[col] == DBNull.Value)
                        {
                            tableHasNullCustomer = true;
                            break;
                        }
                    }
                }

                //Excel Column Mismatch check. If column name has been changed then it's value will be null;
                foreach (DataRow row in dtCustomerContactTable.Rows)
                {
                    foreach (DataColumn col in dtCustomerContactTable.Columns)
                    {
                        if (row[col] == DBNull.Value)
                        {
                            tableHasNullCustomerContact = true;
                            break;
                        }
                    }
                }

                //Excel Column Mismatch check. If column name has been changed then it's value will be null;
                foreach (DataRow row in dtCustomerAddressTable.Rows)
                {
                    foreach (DataColumn col in dtCustomerAddressTable.Columns)
                    {
                        if (row[col] == DBNull.Value)
                        {
                            tableHasNullCustomerAddress = true;
                            break;
                        }
                    }
                }


                //if (tableHasNullCustomer || tableHasNullCustomerContact || tableHasNullCustomerAddress)
                //{
                //    _response.IsSuccess = false;
                //    _response.Message = "Please upload a valid excel file. Please Download Format file for reference.";
                //    return _response;
                //}

                // Import Data
                lstCustomer_ImportDataValidation_Result = await _customerRepository.ImportCustomer(lstCustomerImportRequestModel);
                lstCustomerContact_ImportDataValidation_Result = await _customerRepository.ImportCustomerContact(lstCustomerContactImportRequestModel);
                lstCustomerAddress_ImportDataValidation_Result = await _customerRepository.ImportCustomerAddress(lstCustomerAddressImportRequestModel);

                if (lstCustomer_ImportDataValidation_Result.ToList().Count == 0 && lstCustomerContact_ImportDataValidation_Result.ToList().Count == 0 && lstCustomerAddress_ImportDataValidation_Result.ToList().Count == 0)
                {
                    _response.IsSuccess = true;
                    _response.Message = "Record imported successfully";
                }
                else
                {
                    _response.IsSuccess = true;
                    _response.Message = "Invalid record exist.";
                }

                #region Generate Excel file for Invalid Data

                if (lstCustomer_ImportDataValidation_Result.ToList().Count > 0 || lstCustomerContact_ImportDataValidation_Result.ToList().Count > 0 || lstCustomerAddress_ImportDataValidation_Result.ToList().Count > 0)
                {
                    _response.IsSuccess = false;

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (ExcelPackage excelInvalidData = new ExcelPackage())
                        {
                            if (lstCustomer_ImportDataValidation_Result.ToList().Count > 0)
                            {
                                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                                int recordIndex;
                                ExcelWorksheet WorkSheet1 = excelInvalidData.Workbook.Worksheets.Add("Invalid_Customer_Records");
                                WorkSheet1.TabColor = System.Drawing.Color.Black;
                                WorkSheet1.DefaultRowHeight = 12;

                                //Header of table
                                WorkSheet1.Row(1).Height = 20;
                                WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                WorkSheet1.Row(1).Style.Font.Bold = true;

                                WorkSheet1.Cells[1, 1].Value = "CustomerType";
                                WorkSheet1.Cells[1, 2].Value = "CustomerName";
                                WorkSheet1.Cells[1, 3].Value = "CustomerCode";
                                WorkSheet1.Cells[1, 4].Value = "LandLineNumber";
                                WorkSheet1.Cells[1, 5].Value = "MobileNumber";
                                WorkSheet1.Cells[1, 6].Value = "Email";
                                WorkSheet1.Cells[1, 7].Value = "Website";
                                WorkSheet1.Cells[1, 8].Value = "SpecialRemark";
                                WorkSheet1.Cells[1, 9].Value = "CustomerRemark";
                                WorkSheet1.Cells[1, 10].Value = "RefParty";
                                WorkSheet1.Cells[1, 11].Value = "ErrorMessage";

                                recordIndex = 2;

                                foreach (Customer_ImportDataValidation record in lstCustomer_ImportDataValidation_Result)
                                {
                                    WorkSheet1.Cells[recordIndex, 1].Value = record.CustomerType;
                                    WorkSheet1.Cells[recordIndex, 2].Value = record.CustomerName;
                                    WorkSheet1.Cells[recordIndex, 3].Value = record.CustomerCode;
                                    WorkSheet1.Cells[recordIndex, 4].Value = record.LandLineNumber;
                                    WorkSheet1.Cells[recordIndex, 5].Value = record.MobileNumber;
                                    WorkSheet1.Cells[recordIndex, 6].Value = record.Email;
                                    WorkSheet1.Cells[recordIndex, 7].Value = record.Website;
                                    WorkSheet1.Cells[recordIndex, 8].Value = record.SpecialRemark;
                                    WorkSheet1.Cells[recordIndex, 9].Value = record.CustomerRemark;
                                    WorkSheet1.Cells[recordIndex, 10].Value = record.RefParty;
                                    WorkSheet1.Cells[recordIndex, 11].Value = record.ValidationMessage;

                                    recordIndex += 1;
                                }

                                WorkSheet1.Columns.AutoFit();
                            }

                            if (lstCustomerContact_ImportDataValidation_Result.ToList().Count > 0)
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

                                WorkSheet1.Cells[1, 1].Value = "CustomerName";
                                WorkSheet1.Cells[1, 2].Value = "ContactName";
                                WorkSheet1.Cells[1, 3].Value = "MobileNumber";
                                WorkSheet1.Cells[1, 4].Value = "Email";
                                WorkSheet1.Cells[1, 5].Value = "ErrorMessage";

                                recordIndex = 2;

                                foreach (Contact_ImportDataValidation record in lstCustomerContact_ImportDataValidation_Result)
                                {
                                    WorkSheet1.Cells[recordIndex, 1].Value = record.CustomerName;
                                    WorkSheet1.Cells[recordIndex, 2].Value = record.ContactName;
                                    WorkSheet1.Cells[recordIndex, 3].Value = record.MobileNumber;
                                    WorkSheet1.Cells[recordIndex, 4].Value = record.Email;
                                    WorkSheet1.Cells[recordIndex, 11].Value = record.ValidationMessage;

                                    recordIndex += 1;
                                }

                                WorkSheet1.Columns.AutoFit();
                            }

                            if (lstCustomerAddress_ImportDataValidation_Result.ToList().Count > 0)
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

                                WorkSheet1.Cells[1, 1].Value = "CustomerName";
                                WorkSheet1.Cells[1, 2].Value = "Address";
                                WorkSheet1.Cells[1, 3].Value = "Region";
                                WorkSheet1.Cells[1, 4].Value = "State";
                                WorkSheet1.Cells[1, 5].Value = "District";
                                WorkSheet1.Cells[1, 6].Value = "City";
                                WorkSheet1.Cells[1, 7].Value = "PinCode";
                                WorkSheet1.Cells[1, 8].Value = "ErrorMessage";

                                recordIndex = 2;

                                foreach (Address_ImportDataValidation record in lstCustomerAddress_ImportDataValidation_Result)
                                {
                                    WorkSheet1.Cells[recordIndex, 1].Value = record.CustomerName;
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
        public async Task<ResponseModel> ExportCustomerCustomer()
        {
            _response.IsSuccess = false;
            byte[] result;

            var request = new BaseSearchEntity();

            var lstCustomerListObj = await _customerRepository.GetCustomerList(request);

            using (MemoryStream msExportDataFile = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (ExcelPackage excelExportData = new ExcelPackage())
                {
                    int recordIndex;
                    ExcelWorksheet WorkSheet1 = excelExportData.Workbook.Worksheets.Add("Customer");
                    WorkSheet1.TabColor = System.Drawing.Color.Black;
                    WorkSheet1.DefaultRowHeight = 12;

                    //Header of table
                    WorkSheet1.Row(1).Height = 20;
                    WorkSheet1.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    WorkSheet1.Row(1).Style.Font.Bold = true;

                    WorkSheet1.Cells[1, 1].Value = "Customer Type";
                    WorkSheet1.Cells[1, 2].Value = "Customer Name";
                    WorkSheet1.Cells[1, 3].Value = "Customer Code";
                    WorkSheet1.Cells[1, 4].Value = "LandLine Number";
                    WorkSheet1.Cells[1, 5].Value = "Mobile Number";
                    WorkSheet1.Cells[1, 6].Value = "Email";
                    WorkSheet1.Cells[1, 7].Value = "Website";
                    WorkSheet1.Cells[1, 8].Value = "Special Remark";
                    WorkSheet1.Cells[1, 9].Value = "Customer Remark";
                    WorkSheet1.Cells[1, 10].Value = "Ref Party";
                    WorkSheet1.Cells[1, 11].Value = "IsActive";

                    recordIndex = 2;
                    foreach (var items in lstCustomerListObj)
                    {
                        WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerType;
                        WorkSheet1.Cells[recordIndex, 2].Value = items.CustomerName;
                        WorkSheet1.Cells[recordIndex, 3].Value = items.CustomerCode;
                        WorkSheet1.Cells[recordIndex, 4].Value = items.LandLineNumber;
                        WorkSheet1.Cells[recordIndex, 5].Value = items.MobileNumber;
                        WorkSheet1.Cells[recordIndex, 6].Value = items.EmailId;
                        WorkSheet1.Cells[recordIndex, 7].Value = items.Website;
                        WorkSheet1.Cells[recordIndex, 8].Value = items.Remark;
                        WorkSheet1.Cells[recordIndex, 9].Value = items.CustomerRemark;
                        WorkSheet1.Cells[recordIndex, 10].Value = items.RefParty;
                        WorkSheet1.Cells[recordIndex, 11].Value = items.IsActive == true ? "Active" : "Inactive";

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

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Contact Name";
                    WorkSheet1.Cells[1, 3].Value = "Mobile Number";
                    WorkSheet1.Cells[1, 4].Value = "Email";
                    WorkSheet1.Cells[1, 5].Value = "IsActive";

                    recordIndex = 2;
                    foreach (var items in lstCustomerListObj.ToList().Distinct())
                    {
                        if(items.Id == 13)
                        {
                            string fdf = "";
                        }

                        var vContactDetail_Search = new ContactDetail_Search()
                        {
                            RefId = Convert.ToInt32(items.Id),
                            RefType = "Customer"
                        };

                        var lstContactListObj = await _contactDetailRepository.GetContactDetailList(vContactDetail_Search);
                        foreach (var itemContact in lstContactListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
                            WorkSheet1.Cells[recordIndex, 2].Value = itemContact.ContactName;
                            WorkSheet1.Cells[recordIndex, 3].Value = itemContact.MobileNumber;
                            WorkSheet1.Cells[recordIndex, 4].Value = itemContact.EmailId;
                            WorkSheet1.Cells[recordIndex, 11].Value = itemContact.IsActive == true ? "Active" : "Inactive";

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

                    WorkSheet1.Cells[1, 1].Value = "Customer Name";
                    WorkSheet1.Cells[1, 2].Value = "Address1";
                    WorkSheet1.Cells[1, 3].Value = "RegionName";
                    WorkSheet1.Cells[1, 4].Value = "StateName";
                    WorkSheet1.Cells[1, 5].Value = "DistrictName";
                    WorkSheet1.Cells[1, 6].Value = "CityName";
                    WorkSheet1.Cells[1, 7].Value = "PinCode";
                    WorkSheet1.Cells[1, 8].Value = "IsActive";

                    recordIndex = 2;
                    foreach (var items in lstCustomerListObj)
                    {
                        var vAddress_Search = new Address_Search()
                        {
                            RefId = Convert.ToInt32(items.Id),
                            RefType = "Customer"
                        };

                        var lstAddressListObj = await _addressRepository.GetAddressList(vAddress_Search);
                        foreach (var itemAddress in lstAddressListObj)
                        {
                            WorkSheet1.Cells[recordIndex, 1].Value = items.CustomerName;
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

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerNameForSelectList(int CustomerTypeId = 0)
        {
            IEnumerable<SelectListResponse> lstResponse = await _customerRepository.GetCustomerNameForSelectList(CustomerTypeId);
            _response.Data = lstResponse.ToList();
            return _response;
        }

        #endregion

        #region Customer Contact Detail

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCustomerContactDetail(ContactDetail_Request parameters)
        {
            // Image Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.AadharCardImage_Base64))
            {
                var vUploadFile_AadharCardImage = _fileManager.UploadDocumentsBase64ToFile(parameters.AadharCardImage_Base64, "\\Uploads\\Customer\\", parameters.AadharCardOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile_AadharCardImage))
                {
                    parameters.AadharCardImageFileName = vUploadFile_AadharCardImage;
                }
            }

            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PanCardImage_Base64))
            {
                var vUploadFile_PanCard = _fileManager.UploadDocumentsBase64ToFile(parameters.PanCardImage_Base64, "\\Uploads\\Customer\\", parameters.PanCardOriginalFileName);

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
        public async Task<ResponseModel> GetCustomerContactDetailList(ContactDetail_Search parameters)
        {
            var objList = await _contactDetailRepository.GetContactDetailList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerContactDetailById(int Id)
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

        #region Customer Address

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCustomerAddressDetail(Address_Request parameters)
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
        public async Task<ResponseModel> GetCustomerAddressDetailList(Address_Search parameters)
        {
            var objList = await _addressRepository.GetAddressList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerAddressDetailById(int Id)
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
