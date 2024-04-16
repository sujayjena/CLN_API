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
        private readonly ICustomerRepository _customerRepository;
        private IFileManager _fileManager;

        public CustomerController(ICustomerRepository customerRepository, IFileManager fileManager)
        {
            _customerRepository = customerRepository;
            _fileManager = fileManager;

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
                parameters.ContactDetail.CustomerId = result;
                parameters.AddressDetail.RefId = result;

                // Contact Detail
                if (!string.IsNullOrWhiteSpace(parameters.ContactDetail.CustomerName))
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

                    int resultContact = await _customerRepository.SaveCustomerContactDetail(parameters.ContactDetail);
                }

                // Address Detail
                if (!string.IsNullOrWhiteSpace(parameters.AddressDetail.Address1))
                {
                    int resultAddressDetail = await _customerRepository.SaveCustomerAddress(parameters.AddressDetail);
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
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _customerRepository.GetCustomerById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Customer Contact Detail

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCustomerContactDetail(CustomerContactDetail_Request parameters)
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

            int result = await _customerRepository.SaveCustomerContactDetail(parameters);

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
        public async Task<ResponseModel> GetCustomerContactDetailList(CustomerContactDetail_Search parameters)
        {
            var objList = await _customerRepository.GetCustomerContactDetailList(parameters);
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
                var vResultObj = await _customerRepository.GetCustomerContactDetailById(Id);
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
            int result = await _customerRepository.SaveCustomerAddress(parameters);

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
        public async Task<ResponseModel> GetCustomerAddressDetailList(CustomerAddress_Search parameters)
        {
            var objList = await _customerRepository.GetCustomerAddressList(parameters);
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
                var vResultObj = await _customerRepository.GetCustomerAddressById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
