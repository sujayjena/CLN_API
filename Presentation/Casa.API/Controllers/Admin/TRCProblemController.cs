using CLN.Application.Enums;
using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using CLN.Persistence.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CLN.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TRCProblemController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ITRCProblemRepository _trcProblemRepository;
        private IFileManager _fileManager;

        public TRCProblemController(ITRCProblemRepository trcProblemRepository, IFileManager fileManager)
        {
            _trcProblemRepository = trcProblemRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
            _fileManager = fileManager;
        }

        #region WireHarness 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveWireHarness(WireHarness_Request parameters)
        {
            int result = await _trcProblemRepository.SaveWireHarness(parameters);

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
                _response.Message = "Record details saved successfully";
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetWireHarnessList(BaseSearchEntity parameters)
        {
            IEnumerable<WireHarness_Response> lstWireHarness = await _trcProblemRepository.GetWireHarnessList(parameters);
            _response.Data = lstWireHarness.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetWireHarnessById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _trcProblemRepository.GetWireHarnessById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region Connector 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveConnector(Connector_Request parameters)
        {
            int result = await _trcProblemRepository.SaveConnector(parameters);

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
                _response.Message = "Record details saved successfully";
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetConnectorList(BaseSearchEntity parameters)
        {
            IEnumerable<Connector_Response> lstConnector = await _trcProblemRepository.GetConnectorList(parameters);
            _response.Data = lstConnector.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetConnectorById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _trcProblemRepository.GetConnectorById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion

        #region OtherDamage 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveOtherDamage(OtherDamage_Request parameters)
        {
            int result = await _trcProblemRepository.SaveOtherDamage(parameters);

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
                _response.Message = "Record details saved successfully";
            }

            _response.Id = result;
            return _response;
        }


        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetOtherDamageList(BaseSearchEntity parameters)
        {
            IEnumerable<OtherDamage_Response> lstOtherDamage = await _trcProblemRepository.GetOtherDamageList(parameters);
            _response.Data = lstOtherDamage.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetOtherDamageById(long Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _trcProblemRepository.GetOtherDamageById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        #endregion
    }
}
