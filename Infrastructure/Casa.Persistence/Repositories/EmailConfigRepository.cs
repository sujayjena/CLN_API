﻿using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Application.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence.Repositories
{
    public class EmailConfigRepository : GenericRepository, IEmailConfigRepository
    {
        private IConfiguration _configuration;

        public EmailConfigRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveEmailConfig(EmailConfig_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@SmtpServerName", parameters.SmtpServerName);
            queryParameters.Add("@SmtpServer", parameters.SmtpServer);
            queryParameters.Add("@SmtpUsername", parameters.SmtpUsername);
            queryParameters.Add("@SmtpPassword", parameters.SmtpPassword);
            queryParameters.Add("@SmtpUseDefaultCredentials", parameters.SmtpUseDefaultCredentials);
            queryParameters.Add("@SmtpEnableSSL", parameters.SmtpEnableSSL);
            queryParameters.Add("@SmtpTimeout", parameters.SmtpTimeout);
            queryParameters.Add("@FromAddress", parameters.FromAddress);
            queryParameters.Add("@EmailSenderName", parameters.EmailSenderName);
            queryParameters.Add("@EmailSenderCompanyLogo", parameters.EmailSenderCompanyLogo);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveEmailConfig", queryParameters);
        }

        public async Task<IEnumerable<EmailConfig_Response>> GetEmailConfigList(EmailConfig_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<EmailConfig_Response>("GetEmailConfigList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<EmailConfig_Response?> GetEmailConfigById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<EmailConfig_Response>("GetEmailConfigById", queryParameters)).FirstOrDefault();
        }
    }
}