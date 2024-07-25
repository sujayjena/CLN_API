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
    public class CompanyController : CustomBaseController
    {
        private ResponseModel _response;
        private readonly ICompanyRepository _companyRepository;
        private IFileManager _fileManager;
        private IEmailHelper _emailHelper;
        //private readonly IWebHostEnvironment _environment;

        private readonly IConfigRefRepository _configRefRepository;

        //public CompanyController(ICompanyRepository companyRepository, IFileManager fileManager, IEmailHelper emailHelper, IWebHostEnvironment environment, IConfigRefRepository configRefRepository)
        //{
        //    _companyRepository = companyRepository;
        //    _fileManager = fileManager;
        //    _emailHelper = emailHelper;
        //    _environment = environment;

        //    _configRefRepository = configRefRepository;

        //    _response = new ResponseModel();
        //    _response.IsSuccess = true;
        //}

        public CompanyController(ICompanyRepository companyRepository, IFileManager fileManager, IEmailHelper emailHelper, IConfigRefRepository configRefRepository)
        {
            _companyRepository = companyRepository;
            _fileManager = fileManager;
            _emailHelper = emailHelper;

            _configRefRepository = configRefRepository;

            _response = new ResponseModel();
            _response.IsSuccess = true;
        }

        #region Company 

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> SaveCompany(Company_Request parameters)
        {
            // Company Logo Upload
            if (parameters! != null && !string.IsNullOrWhiteSpace(parameters.LogoImage_Base64))
            {
                var vUploadFile = _fileManager.UploadDocumentsBase64ToFile(parameters.LogoImage_Base64, "\\Uploads\\Company\\", parameters.LogoImageOriginalFileName);

                if (!string.IsNullOrWhiteSpace(vUploadFile))
                {
                    parameters.LogoImageFileName = vUploadFile;
                }
            }

            int result = await _companyRepository.SaveCompany(parameters);

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
        public async Task<ResponseModel> GetCompanyList(CompanySearch_Request parameters)
        {
            IEnumerable<Company_Response> lstCompanys = await _companyRepository.GetCompanyList(parameters);
            _response.Data = lstCompanys.ToList();
            _response.Total = parameters.Total;
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> GetCompanyById(int Id)
        {
            if (Id <= 0)
            {
                _response.Message = "Id is required";
            }
            else
            {
                var vResultObj = await _companyRepository.GetCompanyById(Id);
                _response.Data = vResultObj;
            }
            return _response;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<ResponseModel> CheckCompanyAMC(CompanyAMC_Search parameters)
        {
            var vCompanySearch_Request = new CompanySearch_Request()
            {
                CompanyId = parameters.CompanyId,
            };

            var lstCompanys = await _companyRepository.GetCompanyList(vCompanySearch_Request);

            //foreach (var companyItem in lstCompanys.ToList())
            //{
            //    string sCompanyName = companyItem.CompanyName;
            //    int iCompanyId = companyItem.Id;
            //    int iTotalAmcRemainingDays = Convert.ToInt32(companyItem.TotalAmcRemainingDays);

            //    if (iTotalAmcRemainingDays == 0 && companyItem.AmcLastEmailDate.HasValue && DateTime.Now <= companyItem.AmcLastEmailDate)
            //    {
            //        var vEmailCustomer = await SendAMCEmailToCustomer11(companyItem);
            //    }
            //    else if (iTotalAmcRemainingDays > 0 && iTotalAmcRemainingDays > 45 && iTotalAmcRemainingDays <= 60)
            //    {
            //        var vEmailCustomer = await SendAMCEmailToCustomer11(companyItem);
            //        var vEmailServiceProvider = await SendAMCEmailToServiceProvider(companyItem);
            //    }
            //    else if (iTotalAmcRemainingDays > 0 && iTotalAmcRemainingDays > 30 && iTotalAmcRemainingDays <= 45)
            //    {
            //        var vEmailCustomer = await SendAMCEmailToCustomer11(companyItem);
            //        var vEmailServiceProvider = await SendAMCEmailToServiceProvider(companyItem);
            //    }
            //    else if (iTotalAmcRemainingDays > 0 && iTotalAmcRemainingDays > 15 && iTotalAmcRemainingDays <= 30)
            //    {
            //        var vEmailCustomer = await SendAMCEmailToCustomer11(companyItem);
            //        var vEmailServiceProvider = await SendAMCEmailToServiceProvider(companyItem);
            //    }
            //    else if (iTotalAmcRemainingDays > 0 && iTotalAmcRemainingDays > 0 && iTotalAmcRemainingDays <= 15)
            //    {
            //        var vEmailCustomer =await SendAMCEmailToCustomer11(companyItem);
            //        var vEmailServiceProvider = await SendAMCEmailToServiceProvider(companyItem);
            //    }
            //}

            return _response;
        }

        //public async Task<bool> SendAMCEmailToCustomer11(Company_Response company_Response)
        //{
        //    bool result = false;
        //    string templateFilePath = "", emailTemplateContent = "";

        //    try
        //    {
        //        var vConfigRef_Search = new ConfigRef_Search()
        //        {
        //            Ref_Type = "Email",
        //            Ref_Param = "AMCEmailToCustomer"
        //        };

        //        var vConfigRefObj = _configRefRepository.GetConfigRefList(vConfigRef_Search).Result.ToList().FirstOrDefault();
        //        if (vConfigRefObj != null)
        //        {
        //            //templateFilePath = _environment.ContentRootPath + "\\EmailTemplates\\QuotationTemplate.html";
        //            //emailTemplateContent = System.IO.File.ReadAllText(templateFilePath);

        //            //if (emailTemplateContent.IndexOf("[Date]", StringComparison.OrdinalIgnoreCase) > 0)
        //            //{
        //            //    emailTemplateContent = emailTemplateContent.Replace("[Date]", Convert.ToDateTime(company_Response.AmcEndDate).ToString("dd/MM/yyyy"));
        //            //}

        //            if (emailTemplateContent.IndexOf("[SenderCompanyLogo]", StringComparison.OrdinalIgnoreCase) > 0)
        //            {
        //                emailTemplateContent = emailTemplateContent.Replace("[SenderCompanyLogo]", company_Response.CompanyLogoImageURL);
        //            }

        //            result = await _emailHelper.SendEmail(vConfigRefObj.Ref_Value1, emailTemplateContent, vConfigRefObj.Ref_Value2, files: null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = false;
        //    }

        //    return result;
       // }

        //public async Task<bool> SendAMCEmailToServiceProvider(Company_Response company_Response)
        //{
        //    bool result = false;
        //    string templateFilePath = "", emailTemplateContent="";

        //    try
        //    {
        //        var vConfigRefSP_Search = new ConfigRef_Search()
        //        {
        //            Ref_Type = "Email",
        //            Ref_Param = "AMCEmailToServiceProvider"
        //        };

        //        var vConfigRefSPObj = _configRefRepository.GetConfigRefList(vConfigRefSP_Search).Result.ToList().FirstOrDefault();
        //        if (vConfigRefSPObj != null)
        //        {
        //            //templateFilePath = _environment.ContentRootPath + "\\EmailTemplates\\QuotationTemplate.html";
        //            //emailTemplateContent = System.IO.File.ReadAllText(templateFilePath);

        //            //if (emailTemplateContent.IndexOf("[Date]", StringComparison.OrdinalIgnoreCase) > 0)
        //            //{
        //            //    emailTemplateContent = emailTemplateContent.Replace("[Date]", Convert.ToDateTime(company_Response.AmcEndDate).ToString("dd/MM/yyyy"));
        //            //}

        //            //if (emailTemplateContent.IndexOf("[SenderCompanyLogo]", StringComparison.OrdinalIgnoreCase) > 0)
        //            //{
        //            //    emailTemplateContent = emailTemplateContent.Replace("[SenderCompanyLogo]", company_Response.CompanyLogoImageURL);
        //            //}

        //            result = await _emailHelper.SendEmail(vConfigRefSPObj.Ref_Value1, emailTemplateContent, vConfigRefSPObj.Ref_Value2, files: null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result = false;
        //    }

        //    return result;
        //}

        #endregion
    }
}
