﻿using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductItemController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IProductItemRepository _productItemRepository;

        public ProductItemController(IProductItemRepository productItemRepository)
        {
            _productItemRepository = productItemRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Product Category

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProductCategory(ProductCategory_Request parameters)
        {
            int result = await _productItemRepository.SaveProductCategory(parameters);

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
        public async Task<ResponseModel> GetProductCategoryList(BaseSearchEntity parameters)
        {
            IEnumerable<ProductCategory_Response> lstRoles = await _productItemRepository.GetProductCategoryList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProductCategoryById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetProductCategoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Sub Category

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSubCategory(SubCategory_Request parameters)
        {
            int result = await _productItemRepository.SaveSubCategory(parameters);

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
        public async Task<ResponseModel> GetSubCategoryList(BaseSearchEntity parameters)
        {
            IEnumerable<SubCategory_Response> lstRoles = await _productItemRepository.GetSubCategoryList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSubCategoryById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetSubCategoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Segment
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSegment(Segment_Request parameters)
        {
            int result = await _productItemRepository.SaveSegment(parameters);

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
        public async Task<ResponseModel> GetSegmentList(BaseSearchEntity parameters)
        {
            IEnumerable<Segment_Response> lstRoles = await _productItemRepository.GetSegmentList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSegmentById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetSegmentById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Sub Segment
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSubSegment(SubSegment_Request parameters)
        {
            int result = await _productItemRepository.SaveSubSegment(parameters);

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
        public async Task<ResponseModel> GetSubSegmentList(BaseSearchEntity parameters)
        {
            IEnumerable<SubSegment_Response> lstRoles = await _productItemRepository.GetSubSegmentList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSubSegmentById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetSubSegmentById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Cell Chemistry
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCellChemistry(CellChemistry_Request parameters)
        {
            int result = await _productItemRepository.SaveCellChemistry(parameters);

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
        public async Task<ResponseModel> GetCellChemistryList(BaseSearchEntity parameters)
        {
            IEnumerable<CellChemistry_Response> lstRoles = await _productItemRepository.GetCellChemistryList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCellChemistryById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetCellChemistryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Product Speces
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProductSpeces(ProductSpeces_Request parameters)
        {
            int result = await _productItemRepository.SaveProductSpeces(parameters);

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
        public async Task<ResponseModel> GetProductSpecesList(BaseSearchEntity parameters)
        {
            IEnumerable<ProductSpeces_Response> lstRoles = await _productItemRepository.GetProductSpecesList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProductSpecesById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetProductSpecesById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Item Category
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveItemCategory(ItemCategory_Request parameters)
        {
            int result = await _productItemRepository.SaveItemCategory(parameters);

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
        public async Task<ResponseModel> GetItemCategoryList(BaseSearchEntity parameters)
        {
            IEnumerable<ItemCategory_Response> lstRoles = await _productItemRepository.GetItemCategoryList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetItemCategoryById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetItemCategoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Item Description
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveItemDescription(ItemDescription_Request parameters)
        {
            int result = await _productItemRepository.SaveItemDescription(parameters);

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
        public async Task<ResponseModel> GetItemDescriptionList(BaseSearchEntity parameters)
        {
            IEnumerable<ItemDescription_Response> lstRoles = await _productItemRepository.GetItemDescriptionList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetItemDescriptionById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetItemDescriptionById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Product Type
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProductType(ProductType_Request parameters)
        {
            int result = await _productItemRepository.SaveProductType(parameters);

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
        public async Task<ResponseModel> GetProductTypeList(BaseSearchEntity parameters)
        {
            IEnumerable<ProductType_Response> lstRoles = await _productItemRepository.GetProductTypeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProductTypeById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetProductTypeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Product Make
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProductMake(ProductMake_Request parameters)
        {
            int result = await _productItemRepository.SaveProductMake(parameters);

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
        public async Task<ResponseModel> GetProductMakeList(BaseSearchEntity parameters)
        {
            IEnumerable<ProductMake_Response> lstRoles = await _productItemRepository.GetProductMakeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProductMakeById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetProductMakeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Product Model
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveProductModel(ProductModel_Request parameters)
        {
            int result = await _productItemRepository.SaveProductModel(parameters);

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
        public async Task<ResponseModel> GetProductModelList(BaseSearchEntity parameters)
        {
            IEnumerable<ProductModel_Response> lstRoles = await _productItemRepository.GetProductModelList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetProductModelById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetProductModelById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region BMS Make
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBMSMake(BMSMake_Request parameters)
        {
            int result = await _productItemRepository.SaveBMSMake(parameters);

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
        public async Task<ResponseModel> GetBMSMakeList(BaseSearchEntity parameters)
        {
            IEnumerable<BMSMake_Response> lstRoles = await _productItemRepository.GetBMSMakeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBMSMakeById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetBMSMakeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Type of BMS
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTypeOfBMS(TypeOfBMS_Request parameters)
        {
            int result = await _productItemRepository.SaveTypeOfBMS(parameters);

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
        public async Task<ResponseModel> GetTypeOfBMSList(BaseSearchEntity parameters)
        {
            IEnumerable<TypeOfBMS_Response> lstRoles = await _productItemRepository.GetTypeOfBMSList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTypeOfBMSById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetTypeOfBMSById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Battery Physical Status
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveBatteryPhysicalStatus(BatteryPhysicalStatus_Request parameters)
        {
            int result = await _productItemRepository.SaveBatteryPhysicalStatus(parameters);

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
        public async Task<ResponseModel> GetBatteryPhysicalStatusList(BaseSearchEntity parameters)
        {
            IEnumerable<BatteryPhysicalStatus_Response> lstRoles = await _productItemRepository.GetBatteryPhysicalStatusList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetBatteryPhysicalStatusById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _productItemRepository.GetBatteryPhysicalStatusById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
