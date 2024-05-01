using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                _response.Message = "Record is already exists";
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
                    vCustomerDetail_Response.LandLineNumber = vResultObj.LandLineNumber;
                    vCustomerDetail_Response.MobileNumber = vResultObj.MobileNumber;
                    vCustomerDetail_Response.EmailId = vResultObj.EmailId;
                    vCustomerDetail_Response.Website = vResultObj.Website;
                    vCustomerDetail_Response.Remark = vResultObj.Remark;
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
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
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
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details saved sucessfully";
            }
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
