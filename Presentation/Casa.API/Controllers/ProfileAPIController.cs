using CLN.Application.Constants;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Globalization;

namespace CLN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileAPIController : CommonController
    {
        private ResponseModel _response;
        private readonly IProfileRepository _profileRepository;

        public ProfileAPIController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Role API

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetRolesList(SearchRoleRequest request)
        {
            IEnumerable<RoleResponse> lstRoles = await _profileRepository.GetRolesList(request);
            _response.Data = lstRoles.ToList();
            _response.Total = request.pagination.Total;
            return _response;
        }

        #endregion
    }
}
