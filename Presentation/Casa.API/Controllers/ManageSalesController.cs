using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

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

        #region Sales 

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
                vCustObj.MobileNumber = parameters.BuyerDetail.BuyerMobileNumber;

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
                if (!string.IsNullOrWhiteSpace(parameters.ConsigneeAddress1))
                {
                    var AddressDetail = new Address_Request()
                    {
                        Id = Convert.ToInt32(parameters.ConsigneeAddressId),
                        RefId = 0,
                        RefType = "Customer",
                        Address1 = parameters.ConsigneeAddress1,
                        RegionId = parameters.ConsigneeRegionId,
                        StateId = parameters.ConsigneeStateId,
                        DistrictId = parameters.ConsigneeDistrictId,
                        CityId = parameters.ConsigneeCityId,
                        PinCode = parameters.ConsigneePinCode,
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
                if (!string.IsNullOrWhiteSpace(parameters.BuyerDetail.BuyerAddress1))
                {
                    var AddressDetail = new Address_Request()
                    {
                        Id = Convert.ToInt32(parameters.BuyerDetail.BuyerAddressId),
                        RefId = iCustomerId,
                        RefType = "Customer",
                        Address1 = parameters.BuyerDetail.BuyerAddress1,
                        RegionId = parameters.BuyerDetail.BuyerRegionId,
                        StateId = parameters.BuyerDetail.BuyerStateId,
                        DistrictId = parameters.BuyerDetail.BuyerDistrictId,
                        CityId = parameters.BuyerDetail.BuyerCityId,
                        PinCode = parameters.BuyerDetail.BuyerPinCode,
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

                    int resultCustomerAccessory = await _manageSalesRepository.SaveManageSalesAccessory(vCustomerAccessory_Request);
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
        public async Task<ResponseModel> GettManageSalesById(int Id)
        {
            var vManageSales_Consignee_Request = new ManageSales_Consignee_Request();


            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _manageSalesRepository.GetManageSalesById(Id);
                if (vResultObj != null)
                {
                    vManageSales_Consignee_Request.Id = vResultObj.Id;
                    vManageSales_Consignee_Request.ConsigneeTypeId = vResultObj.ConsigneeTypeId;
                    vManageSales_Consignee_Request.ConsigneeName = vResultObj.ConsigneeName;
                    vManageSales_Consignee_Request.ConsigneeMobileNumber = vResultObj.ConsigneeMobileNumber;
                    vManageSales_Consignee_Request.IsBuyerSameAsConsignee = vResultObj.IsBuyerSameAsConsignee;
                    vManageSales_Consignee_Request.ConsigneeAddressId = vResultObj.ConsigneeAddressId;
                    vManageSales_Consignee_Request.ConsigneeAddress1 = vResultObj.ConsigneeAddress1;
                    vManageSales_Consignee_Request.ConsigneeRegionId = vResultObj.ConsigneeRegionId;
                    vManageSales_Consignee_Request.ConsigneeRegionName = vResultObj.ConsigneeRegionName;
                    vManageSales_Consignee_Request.ConsigneeStateId = vResultObj.ConsigneeStateId;
                    vManageSales_Consignee_Request.ConsigneeStateName = vResultObj.ConsigneeStateName;
                    vManageSales_Consignee_Request.ConsigneeDistrictId = vResultObj.ConsigneeDistrictId;
                    vManageSales_Consignee_Request.ConsigneeDistrictName = vResultObj.ConsigneeDistrictName;
                    vManageSales_Consignee_Request.ConsigneeCityId = vResultObj.ConsigneeCityId;
                    vManageSales_Consignee_Request.ConsigneeCityName = vResultObj.ConsigneeCityName;
                    vManageSales_Consignee_Request.ConsigneePinCode = vResultObj.ConsigneePinCode;
                    vManageSales_Consignee_Request.IsActive = vResultObj.IsActive;

                    var vBuyerObj = new ManageSales_Buyer_Request()
                    {
                        BuyerTypeId = vResultObj.BuyerTypeId,
                        BuyerType = vResultObj.BuyerType,
                        BuyerName = vResultObj.BuyerName,
                        BuyerMobileNumber = vResultObj.BuyerMobileNumber,
                        BuyerAddressId = vResultObj.BuyerAddressId,
                        BuyerAddress1 = vResultObj.BuyerAddress1,
                        BuyerRegionId = vResultObj.BuyerRegionId,
                        BuyerRegionName = vResultObj.BuyerRegionName,
                        BuyerStateId = vResultObj.BuyerStateId,
                        BuyerStateName = vResultObj.BuyerStateName,
                        BuyerDistrictId = vResultObj.BuyerDistrictId,
                        BuyerDistrictName = vResultObj.BuyerDistrictName,
                        BuyerCityId = vResultObj.BuyerCityId,
                        BuyerCityName = vResultObj.BuyerCityName,
                        BuyerPinCode = vResultObj.BuyerPinCode
                    };

                    vManageSales_Consignee_Request.BuyerDetail = vBuyerObj;

                    // Accessory
                    var vSearchObj = new CustomerAccessory_Search()
                    {
                        CustomerId = vResultObj.Id,
                    };

                    var objAccessoryList = await _manageSalesRepository.GetManageSalesAccessoryList(vSearchObj);
                    foreach (var item in objAccessoryList)
                    {
                        vManageSales_Consignee_Request.AccessoryList.Add(item);
                    }
                }

                _response.Data = vManageSales_Consignee_Request;
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

            int result = await _manageSalesRepository.SaveManageSalesAccessory(parameters);

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
        public async Task<ResponseModel> GetAccessoryList(CustomerAccessory_Search parameters)
        {
            var objList = await _manageSalesRepository.GetManageSalesAccessoryList(parameters);
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
                var vResultObj = await _manageSalesRepository.GetManageSalesAccessoryById(Id);
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
            if (string.IsNullOrWhiteSpace(parameters.SerialNumber))
            {
                _response.Message = "SerialNumber is required!";

                return _response;
            }

            int result = await _manageSalesRepository.SaveCustomerBattery(parameters);

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
        public async Task<ResponseModel> GetBatteryList(CustomerBattery_Search parameters)
        {
            var objList = await _manageSalesRepository.GetCustomerBatteryList(parameters);
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
                var vResultObj = await _manageSalesRepository.GetCustomerBatteryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
