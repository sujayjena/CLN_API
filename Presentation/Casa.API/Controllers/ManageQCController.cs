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
    public class ManageQCController : CustomBaseController
    {
        private ResponseModel _response;
        private IFileManager _fileManager;
        private readonly IManageQCRepository _ManageQCRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IContactDetailRepository _contactDetailRepository;
        private readonly IAddressRepository _addressRepository;

        public ManageQCController(
            IFileManager fileManager,
            IManageQCRepository ManageQCRepository,
            ICustomerRepository customerRepository,
            IContactDetailRepository contactDetailRepository,
            IAddressRepository addressRepository)
        {
            _fileManager = fileManager;
            _ManageQCRepository = ManageQCRepository;
            _customerRepository = customerRepository;
            _contactDetailRepository = contactDetailRepository;
            _addressRepository = addressRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region QC 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveManageQC(ManageQC_Consignee_Request parameters)
        {
            var vCustObj = new Customer_Request();

            int iCustomerId = 0;

            // Customer Create
            vCustObj.Id = parameters.Id;
            vCustObj.CustomerTypeId = parameters.ConsigneeTypeId;
            vCustObj.CustomerName = parameters.ConsigneeName;
            vCustObj.CustomerCode = parameters.ConsigneeCode;
            vCustObj.MobileNumber = parameters.ConsigneeMobileNumber;
            vCustObj.IsActive = parameters.IsActive;

            int result = await _customerRepository.SaveCustomer(vCustObj);

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
                iCustomerId = result;
                _response.Message = "Record saved sucessfully";
            }

            if (iCustomerId > 0)
            {
                // Buyer Address Detail
                if (!string.IsNullOrWhiteSpace(parameters.ConsigneeAddress1))
                {
                    var AddressDetail = new Address_Request()
                    {
                        Id = Convert.ToInt32(parameters.ConsigneeAddressId),
                        RefId = iCustomerId,
                        RefType = "Customer",
                        Address1 = parameters.ConsigneeAddress1,
                        RegionId = parameters.ConsigneeRegionId,
                        StateId = parameters.ConsigneeStateId,
                        DistrictId = parameters.ConsigneeDistrictId,
                        CityId = parameters.ConsigneeCityId,
                        PinCode = parameters.ConsigneePinCode,
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

                    int resultCustomerAccessory = await _ManageQCRepository.SaveManageQCAccessory(vCustomerAccessory_Request);
                }
            }

            _response.Id = result;
            return _response;

        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetManageQCList(BaseSearchEntity parameters)
        {
            var objList = await _ManageQCRepository.GetManageQCList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GettManageQCById(int Id)
        {
            var vManageQC_Consignee_Request = new ManageQC_Consignee_Response();


            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ManageQCRepository.GetManageQCById(Id);
                if (vResultObj != null)
                {
                    vManageQC_Consignee_Request.Id = vResultObj.Id;
                    vManageQC_Consignee_Request.ConsigneeTypeId = vResultObj.ConsigneeTypeId;
                    vManageQC_Consignee_Request.ConsigneeName = vResultObj.ConsigneeName;
                    vManageQC_Consignee_Request.ConsigneeCode = vResultObj.ConsigneeCode;
                    vManageQC_Consignee_Request.ConsigneeMobileNumber = vResultObj.ConsigneeMobileNumber;
                    vManageQC_Consignee_Request.ConsigneeAddressId = vResultObj.ConsigneeAddressId;
                    vManageQC_Consignee_Request.ConsigneeAddress1 = vResultObj.ConsigneeAddress1;
                    vManageQC_Consignee_Request.ConsigneeRegionId = vResultObj.ConsigneeRegionId;
                    vManageQC_Consignee_Request.ConsigneeRegionName = vResultObj.ConsigneeRegionName;
                    vManageQC_Consignee_Request.ConsigneeStateId = vResultObj.ConsigneeStateId;
                    vManageQC_Consignee_Request.ConsigneeStateName = vResultObj.ConsigneeStateName;
                    vManageQC_Consignee_Request.ConsigneeDistrictId = vResultObj.ConsigneeDistrictId;
                    vManageQC_Consignee_Request.ConsigneeDistrictName = vResultObj.ConsigneeDistrictName;
                    vManageQC_Consignee_Request.ConsigneeCityId = vResultObj.ConsigneeCityId;
                    vManageQC_Consignee_Request.ConsigneeCityName = vResultObj.ConsigneeCityName;
                    vManageQC_Consignee_Request.ConsigneePinCode = vResultObj.ConsigneePinCode;
                    vManageQC_Consignee_Request.IsActive = vResultObj.IsActive;

                    // Accessory
                    var vSearchObj = new CustomerAccessory_Search()
                    {
                        CustomerId = vResultObj.Id,
                    };

                    var objAccessoryList = await _ManageQCRepository.GetManageQCAccessoryList(vSearchObj);
                    foreach (var item in objAccessoryList)
                    {
                        vManageQC_Consignee_Request.AccessoryList.Add(item);
                    }
                }

                _response.Data = vManageQC_Consignee_Request;
            }
            return _response;
        }

        #endregion

        #region Cusromer BOM 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBOM(CustomerBOM_Request parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.PartCode))
            {
                _response.Message = "PartCode is required!";

                return _response;
            }

            // Image Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.PartImage_Base64))
            {
                var vUploadFile_AadharCardImage = _fileManager.UploadDocumentsBase64ToFile(parameters.PartImage_Base64, "\\Uploads\\Customer\\QC\\", parameters.PartImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile_AadharCardImage))
                {
                    parameters.PartImage = vUploadFile_AadharCardImage;
                }
            }

            int result = await _ManageQCRepository.SaveCustomerBOM(parameters);

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
        public async Task<ResponseModel> GetBOMList(CustomerBOM_Search parameters)
        {
            var objList = await _ManageQCRepository.GetCustomerBOMList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBOMById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ManageQCRepository.GetCustomerBOMById(Id);
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
            if (string.IsNullOrWhiteSpace(parameters.ProductSerialNumber))
            {
                _response.Message = "SerialNumber is required!";

                return _response;
            }

            int result = await _ManageQCRepository.SaveCustomerBattery(parameters);

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
            else if (result == -3)
            {
                _response.Message = "Product Serial Number already exists";
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
        public async Task<ResponseModel> GetBatteryList(CustomerBattery_Search parameters)
        {
            var objList = await _ManageQCRepository.GetCustomerBatteryList(parameters);
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
                var vResultObj = await _ManageQCRepository.GetCustomerBatteryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Customer Charger 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCustomerCharger(CustomerCharger_Request parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.ChargerSerial))
            {
                _response.Message = "ChargerSerial is required!";

                return _response;
            }

            int result = await _ManageQCRepository.SaveCustomerCharger(parameters);

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
        public async Task<ResponseModel> GetCustomerChargerList(CustomerCharger_Search parameters)
        {
            var objList = await _ManageQCRepository.GetCustomerChargerList(parameters);
            _response.Data = objList.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCustomerChargerById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ManageQCRepository.GetCustomerChargerById(Id);
                _response.Data = vResultObj;
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

            int result = await _ManageQCRepository.SaveManageQCAccessory(parameters);

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
        public async Task<ResponseModel> GetAccessoryList(CustomerAccessory_Search parameters)
        {
            var objList = await _ManageQCRepository.GetManageQCAccessoryList(parameters);
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
                var vResultObj = await _ManageQCRepository.GetManageQCAccessoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> DeleteManageQCAccessory(int Id)
        {
            int result = await _ManageQCRepository.DeleteManageQCAccessory(Id);

            if (result == (int)SaveOperationEnums.NoRecordExists)
            {
                _response.Message = "No record exists";
            }
            else if (result == (int)SaveOperationEnums.NoResult)
            {
                _response.Message = "Something went wrong, please try again";
            }
            else
            {
                _response.Message = "Record details deleted sucessfully";
            }
            return _response;
        }
        #endregion
    }
}
