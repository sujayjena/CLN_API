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
