using CLN.Application.Enums;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpareCategoryController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ISpareCategoryRepository _spareCategoryRepository;

        public SpareCategoryController(ISpareCategoryRepository spareCategoryRepository)
        {
            _spareCategoryRepository = spareCategoryRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveSpareCategory(SpareCategory_Request parameters)
        {
            int result = await _spareCategoryRepository.SaveSpareCategory(parameters);

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
        public async Task<ResponseModel> GetSpareCategoryList(BaseSearchEntity parameters)
        {
            IEnumerable<SpareCategory_Response> lstRoles = await _spareCategoryRepository.GetSpareCategoryList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetSpareCategoryById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _spareCategoryRepository.GetSpareCategoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }
    }
}
