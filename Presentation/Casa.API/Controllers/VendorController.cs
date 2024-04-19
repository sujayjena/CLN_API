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
        private readonly IVendorRepository _vendorRepository;
        private IFileManager _fileManager;

        public VendorController(IVendorRepository vendorRepository, IFileManager fileManager)
        {
            _vendorRepository = vendorRepository;
            _fileManager = fileManager;

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
                _response.Message = "Record is already exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                parameters.ContactDetail.RefId = result;
                parameters.AddressDetail.RefId = result;

                // Contact Detail
                if (!string.IsNullOrWhiteSpace(parameters.ContactDetail.ContactName))
                {
                    int resultContact = await _vendorRepository.SaveVendorContactDetail(parameters.ContactDetail);
                }

                // Address Detail
                if (!string.IsNullOrWhiteSpace(parameters.AddressDetail.Address1))
                {
                    int resultAddressDetail = await _vendorRepository.SaveVendorAddress(parameters.AddressDetail);
                }

                _response.Message = "Record details saved sucessfully";
            }
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
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _vendorRepository.GetVendorById(Id);
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

            int result = await _vendorRepository.SaveVendorContactDetail(parameters);

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
        public async Task<ResponseModel> GetVendorContactDetailList(VendorContactDetail_Search parameters)
        {
            var objList = await _vendorRepository.GetVendorContactDetailList(parameters);
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
                var vResultObj = await _vendorRepository.GetVendorContactDetailById(Id);
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
            int result = await _vendorRepository.SaveVendorAddress(parameters);

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
        public async Task<ResponseModel> GetVendorAddressDetailList(VendorAddress_Search parameters)
        {
            var objList = await _vendorRepository.GetVendorAddressList(parameters);
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
                var vResultObj = await _vendorRepository.GetVendorAddressById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
