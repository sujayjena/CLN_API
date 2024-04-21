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
    public class ManageSalesController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;
        private readonly IManageSalesRepository _manageSalesRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IContactDetailRepository _contactDetailRepository;
        private readonly IAddressRepository _addressRepository;

        public ManageSalesController(
            IFileManager fileManager,
            IManageSalesRepository manageSalesRepository,
            ICustomerRepository customerRepository,
            IContactDetailRepository contactDetailRepository,
            IAddressRepository addressRepository)
        {
            _fileManager = fileManager;
            _manageSalesRepository = manageSalesRepository;
            _customerRepository = customerRepository;
            _contactDetailRepository = contactDetailRepository;
            _addressRepository = addressRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Customer 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveManageSales(ManageSales_Consignee_Request parameters)
        {
            var vCustObj = new Customer_Request();

            int iCustomerId = 0;
            int iConsigneeAddressId = 0;

            // Customer Create
            if (parameters.IsBuyerSameAsConsignee == true)
            {
                // Buyer / Customer
                vCustObj.Id = parameters.Id;
                vCustObj.CompanyTypeId = parameters.ConsigneeTypeId;
                vCustObj.CompanyName = parameters.ConsigneeName;
                vCustObj.MobileNumber = parameters.ConsigneeMobileNumber;
                vCustObj.IsActive = parameters.IsActive;

                // Consignee
                vCustObj.ConsigneeTypeId = parameters.ConsigneeTypeId;
                vCustObj.ConsigneeName = parameters.ConsigneeName;
                vCustObj.ConsigneeMobileNumber = parameters.ConsigneeMobileNumber;
                vCustObj.ConsigneeAddressId = parameters.ConsigneeAddressId;
                vCustObj.IsBuyerSameAsConsignee = parameters.IsBuyerSameAsConsignee;

                int resultCustomerId = await _customerRepository.SaveCustomer(vCustObj);

                if (resultCustomerId == (int)SaveOperationEnums.NoRecordExists)
                {
                    _response.Message = "No record exists";
                }
                else if (resultCustomerId == (int)SaveOperationEnums.ReocrdExists)
                {
                    _response.Message = "Record is already exists";
                }
                else if (resultCustomerId == (int)SaveOperationEnums.NoResult)
                {
                    _response.Message = "Something went wrong, please try again";
                }
                else
                {
                    iCustomerId = resultCustomerId;
                    _response.Message = "Record saved sucessfully";
                }
            }
            else if (parameters.BuyerDetail.BuyerTypeId > 0 && !string.IsNullOrWhiteSpace(parameters.BuyerDetail.BuyerName))  // If Buyer exists
            {
                // Buyer / Customer
                vCustObj.Id = parameters.Id;
                vCustObj.CompanyTypeId = parameters.BuyerDetail.BuyerTypeId;
                vCustObj.CompanyName = parameters.BuyerDetail.BuyerName;
                vCustObj.MobileNumber = parameters.BuyerDetail.MobileNumber;

                // Consignee
                vCustObj.ConsigneeTypeId = parameters.ConsigneeTypeId;
                vCustObj.ConsigneeName = parameters.ConsigneeName;
                vCustObj.ConsigneeMobileNumber = parameters.ConsigneeMobileNumber;
                vCustObj.ConsigneeAddressId = parameters.ConsigneeAddressId;
                vCustObj.IsBuyerSameAsConsignee = parameters.IsBuyerSameAsConsignee;

                vCustObj.IsActive = parameters.IsActive;

                int result = await _customerRepository.SaveCustomer(vCustObj);

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
                    iCustomerId = result;
                    _response.Message = "Record saved sucessfully";
                }
            }

            if (iCustomerId > 0)
            {
                // Consignee Address Detail
                if (!string.IsNullOrWhiteSpace(parameters.Address1))
                {
                    var AddressDetail = new Address_Request()
                    {
                        Id = parameters.AddressId,
                        RefId = 0,
                        RefType = "Customer",
                        Address1 = parameters.Address1,
                        RegionId = parameters.RegionId,
                        StateId = parameters.StateId,
                        DistrictId = parameters.DistrictId,
                        CityId = parameters.CityId,
                        PinCode = parameters.PinCode,
                        IsDeleted = false,
                        IsDefault = false,
                        IsActive = true,
                    };

                    int resultAddressDetail = await _addressRepository.SaveAddress(AddressDetail);
                    if (resultAddressDetail > 0 && parameters.Id == 0)
                    {
                        var vCustAddressObj = new Customer_Request()
                        {
                            Id = iCustomerId,
                            ConsigneeAddressId = resultAddressDetail,
                        };

                        int resultUpdateCustomerConsigneeAddress = await _customerRepository.UpdateCustomerConsigneeAddress(vCustAddressObj);
                    }
                }

                // Buyer Address Detail
                if (!string.IsNullOrWhiteSpace(parameters.BuyerDetail.Address1))
                {
                    var AddressDetail = new Address_Request()
                    {
                        Id = parameters.BuyerDetail.AddressId,
                        RefId = iCustomerId,
                        RefType = "Customer",
                        Address1 = parameters.BuyerDetail.Address1,
                        RegionId = parameters.BuyerDetail.RegionId,
                        StateId = parameters.BuyerDetail.StateId,
                        DistrictId = parameters.BuyerDetail.DistrictId,
                        CityId = parameters.BuyerDetail.CityId,
                        PinCode = parameters.BuyerDetail.PinCode,
                        IsDeleted = false,
                        IsDefault = true,
                        IsActive = true,
                    };

                    int resultAddressDetail = await _addressRepository.SaveAddress(AddressDetail);
                }

                // Save/Update Accessory List
                foreach (var item in parameters.AccessoryDetail)
                {
                    var vManageSales_Accessory_Request = new ManageSales_Accessory_Request()
                    {
                        Id = item.Id,
                        CustomerId = iCustomerId,
                        AccessoryId = item.AccessoryId,
                        IsActive = item.IsActive,
                        Quantity = item.Quantity,
                    };

                    int resultCustomerAccessory = await _manageSalesRepository.SaveManageSalesAccessory(vManageSales_Accessory_Request);
                }
            }

            return _response;

        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageSalesList(BaseSearchEntity parameters)
        {
            var objList = await _manageSalesRepository.GetManageSalesList(parameters);
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
                var vResultObj = await _manageSalesRepository.GetManageSalesById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageSalesAccessoryList(BaseSearchEntity parameters)
        {
            var objList = await _manageSalesRepository.GetManageSalesAccessoryList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageSalesAccessoryById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageSalesRepository.GetManageSalesAccessoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
