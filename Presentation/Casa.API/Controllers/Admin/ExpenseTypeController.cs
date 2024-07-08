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
    public class ExpenseTypeController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly IExpenseTypeRepository _expenseTypeRepository;

        public ExpenseTypeController(IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseTypeRepository = expenseTypeRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Expense Type
        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveExpenseType(ExpenseType_Request parameters)
        {
            int result = await _expenseTypeRepository.SaveExpenseType(parameters);

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
        public async Task<ResponseModel> GetExpenseTypeList(BaseSearchEntity parameters)
        {
            IEnumerable<ExpenseType_Response> lstRoles = await _expenseTypeRepository.GetExpenseTypeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseTypeById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _expenseTypeRepository.GetExpenseTypeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Expense Matrix

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveExpenseMatrix(ExpenseMatrix_Request parameters)
        {
            int result = await _expenseTypeRepository.SaveExpenseMatrix(parameters);

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
        public async Task<ResponseModel> GetExpenseMatrixList(ExpenseMatrixSearch_Request parameters)
        {
            IEnumerable<ExpenseMatrix_Response> lstRoles = await _expenseTypeRepository.GetExpenseMatrixList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetExpenseMatrixById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _expenseTypeRepository.GetExpenseMatrixById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
