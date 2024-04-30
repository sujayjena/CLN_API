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
                parameters.AddressDetail.RefId = result;
                parameters.AddressDetail.RefType = "Customer";

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
    }
}
