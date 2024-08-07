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
    public class ProblemReportedByEngRepository : GenericRepository, IProblemReportedByEngRepository
    {

        private IConfiguration _configuration;

        public ProblemReportedByEngRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveProblemReportedByEng(ProblemReportedByEng_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ProductCategoryId", parameters.ProductCategoryId);
            queryParameters.Add("@SegmentId", parameters.SegmentId);
            queryParameters.Add("@SubSegmentId", parameters.SubSegmentId);
            queryParameters.Add("@ProblemReportedByEng", parameters.ProblemReportedByEng);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveProblemReportedByEng", queryParameters);
        }

        public async Task<IEnumerable<ProblemReportedByEng_Response>> GetProblemReportedByEngList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ProblemReportedByEng_Response>("GetProblemReportedByEngList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ProblemReportedByEng_Response?> GetProblemReportedByEngById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<ProblemReportedByEng_Response>("GetProblemReportedByEngById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<ProblemReportedByEngDataValidationErrors>> ImportProblemReportedByEng(List<ImportedProblemReportedByEng> parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            string xmlData = ConvertListToXml(parameters);
            queryParameters.Add("@XmlData", xmlData);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);
            return await ListByStoredProcedure<ProblemReportedByEngDataValidationErrors>("ImportProblemReportedByEng", queryParameters);
        }
    }
}
