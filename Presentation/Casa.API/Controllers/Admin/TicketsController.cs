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
    public class TicketsController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ITicketsRepository _ticketsRepository;

        public TicketsController(ITicketsRepository ticketsRepository)
        {
            _ticketsRepository = ticketsRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Ticket Category

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTicketCategory(TicketCategory_Request parameters)
        {
            int result = await _ticketsRepository.SaveTicketCategory(parameters);

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
        public async Task<ResponseModel> GetTicketCategoryList(BaseSearchEntity parameters)
        {
            IEnumerable<TicketCategory_Response> lstRoles = await _ticketsRepository.GetTicketCategoryList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketCategoryById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ticketsRepository.GetTicketCategoryById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Ticket Status

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTicketStatus(TicketStatus_Request parameters)
        {
            int result = await _ticketsRepository.SaveTicketStatus(parameters);

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

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketStatusList(BaseSearchEntity parameters)
        {
            IEnumerable<TicketStatus_Response> lstRoles = await _ticketsRepository.GetTicketStatusList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketStatusById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ticketsRepository.GetTicketStatusById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Ticket Type

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveTicketType(TicketType_Request parameters)
        {
            int result = await _ticketsRepository.SaveTicketType(parameters);

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
        public async Task<ResponseModel> GetTicketTypeList(BaseSearchEntity parameters)
        {
            IEnumerable<TicketType_Response> lstRoles = await _ticketsRepository.GetTicketTypeList(parameters);
            _response.Data = lstRoles.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetTicketTypeById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _ticketsRepository.GetTicketTypeById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
