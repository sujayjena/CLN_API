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
        private readonly IManageSalesRepository _manageSalesRepository;
        private readonly ICustomerRepository _customerRepository;
        private IFileManager _fileManager;

        public ManageSalesController(IManageSalesRepository manageSalesRepository, ICustomerRepository customerRepository, IFileManager fileManager)
        {
            _manageSalesRepository = manageSalesRepository;
            _customerRepository = customerRepository;
            _fileManager = fileManager;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Customer 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveManageSales(ManageSales_Consignee_Request parameters)
        {
            var vCustObj = new Customer_Request();

            // Consignee Address Detail
            int iConsigneeAddressId = 0;
            if (!string.IsNullOrWhiteSpace(parameters.Address1))
            {
                var AddressDetail = new Address_Request()
                {
                    Id = parameters.AddressId,
                    RefId = 0,
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

                int resultAddressDetail = await _customerRepository.SaveCustomerAddress(AddressDetail);
                if (resultAddressDetail > 0)
                {
                    iConsigneeAddressId = resultAddressDetail;
                }
            }

            // Customer Create
            if (parameters.IsBuyerSameAsConsignee == true)
            {
                // Buyer / Customer
                vCustObj.Id = parameters.Id;
                vCustObj.CompanyTypeId = parameters.ConsigneeTypeId;
                vCustObj.CompanyName = parameters.ConsigneeName;
                vCustObj.LandLineNumber = null;
                vCustObj.MobileNumber = parameters.MobileNumber;
                vCustObj.EmailId = null;
                vCustObj.Website = null;
                vCustObj.Remark = null;
                vCustObj.RefParty = null;
                vCustObj.CompanyAddressId = null;
                vCustObj.IsActive = parameters.IsActive;

                // Consignee
                vCustObj.ConsigneeTypeId = parameters.ConsigneeTypeId;
                vCustObj.ConsigneeName = parameters.ConsigneeName;
                vCustObj.ConsigneeMobileNumber = parameters.MobileNumber;
                vCustObj.ConsigneeAddressId = iConsigneeAddressId;
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
                    // Buyer Address Detail
                    if (!string.IsNullOrWhiteSpace(parameters.BuyerDetail.Address1))
                    {
                        var AddressDetail = new Address_Request()
                        {
                            Id = parameters.BuyerDetail.AddressId,
                            RefId = resultCustomerId,
                            Address1 = parameters.BuyerDetail.Address1,
                            RegionId = parameters.BuyerDetail.RegionId,
                            StateId = parameters.BuyerDetail.StateId,
                            DistrictId = parameters.BuyerDetail.DistrictId,
                            CityId = parameters.BuyerDetail.CityId,
                            PinCode = parameters.BuyerDetail.PinCode,
                            IsDeleted = false,
                            IsDefault = false,
                            IsActive = true,
                        };

                        int resultAddressDetail = await _customerRepository.SaveCustomerAddress(AddressDetail);
                        if (resultAddressDetail > 0)
                        {
                            // UpDate Customer > CompanyAddressId Address
                            var vCustomer_Request = new Customer_Request()
                            {
                                Id = resultCustomerId,
                                CompanyAddressId = resultAddressDetail,
                            };

                            int resultAddress = await _customerRepository.UpdateCustomerAddress(vCustomer_Request);
                        }
                    }

                    // Save/Update Accessory List
                    foreach (var item in parameters.AccessoryDetail)
                    {
                        var vManageSales_Accessory_Request = new ManageSales_Accessory_Request()
                        {
                            Id = item.Id,
                            CustomerId = resultCustomerId,
                            AccessoryId = item.AccessoryId,
                            IsActive = item.IsActive,
                            Quantity = item.Quantity,
                        };

                        int resultCustomerAccessory = await _manageSalesRepository.SaveCustomerAccessory(vManageSales_Accessory_Request);
                    }

                    _response.Message = "Record saved sucessfully";
                    return _response;
                }
            }
            else if (parameters.BuyerDetail.BuyerTypeId > 0 && !string.IsNullOrWhiteSpace(parameters.BuyerDetail.BuyerName))  // If Buyer exists
            {
                // Buyer / Customer
                vCustObj.Id = parameters.Id;
                vCustObj.CompanyTypeId = parameters.BuyerDetail.BuyerTypeId;
                vCustObj.CompanyName = parameters.BuyerDetail.BuyerName;
                vCustObj.LandLineNumber = null;
                vCustObj.MobileNumber = parameters.BuyerDetail.MobileNumber;
                vCustObj.EmailId = null;
                vCustObj.Website = null;
                vCustObj.Remark = null;
                vCustObj.RefParty = null;

                // Consignee
                vCustObj.ConsigneeTypeId = parameters.ConsigneeTypeId;
                vCustObj.ConsigneeName = parameters.ConsigneeName;
                vCustObj.ConsigneeMobileNumber = parameters.MobileNumber;
                vCustObj.ConsigneeAddressId = iConsigneeAddressId;
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
                    // Buyer Address Detail
                    if (!string.IsNullOrWhiteSpace(parameters.BuyerDetail.Address1))
                    {
                        var AddressDetail = new Address_Request()
                        {
                            Id = parameters.BuyerDetail.AddressId,
                            RefId = result,
                            Address1 = parameters.BuyerDetail.Address1,
                            RegionId = parameters.BuyerDetail.RegionId,
                            StateId = parameters.BuyerDetail.StateId,
                            DistrictId = parameters.BuyerDetail.DistrictId,
                            CityId = parameters.BuyerDetail.CityId,
                            PinCode = parameters.BuyerDetail.PinCode,
                            IsDeleted = false,
                            IsDefault = false,
                            IsActive = true,
                        };

                        int resultAddressDetail = await _customerRepository.SaveCustomerAddress(AddressDetail);
                        if (resultAddressDetail > 0)
                        {
                            // UpDate Customer > CompanyAddressId Address
                            var vCustomer_Request = new Customer_Request()
                            {
                                Id = result,
                                CompanyAddressId = resultAddressDetail,
                            };

                            int resultAddress = await _customerRepository.UpdateCustomerAddress(vCustomer_Request);
                        }
                    }

                    // Save/Update Accessory List
                    foreach (var item in parameters.AccessoryDetail)
                    {
                        var vManageSales_Accessory_Request = new ManageSales_Accessory_Request()
                        {
                            Id = item.Id,
                            CustomerId = result,
                            AccessoryId = item.AccessoryId,
                            IsActive = item.IsActive,
                            Quantity = item.Quantity,
                        };

                        int resultCustomerAccessory = await _manageSalesRepository.SaveCustomerAccessory(vManageSales_Accessory_Request);
                    }

                    _response.Message = "Record saved sucessfully";
                    return _response;
                }
            }

            _response.Message = "Record not saved sucessfully";
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
