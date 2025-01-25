using CLN.Application.Helpers;
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
    public class SpareDetailsRepository : GenericRepository, ISpareDetailsRepository
    {
        private IConfiguration _configuration;

        public SpareDetailsRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveSpareDetails(SpareDetails_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@UniqueCode", parameters.UniqueCode);
            queryParameters.Add("@SpareCategoryId", parameters.SpareCategoryId);
            queryParameters.Add("@SpareDesc", parameters.SpareDesc);
            queryParameters.Add("@UOMId", parameters.UOMId);
            queryParameters.Add("@MinQty", parameters.MinQty);
            queryParameters.Add("@AvailableQty", parameters.AvailableQty);
            queryParameters.Add("@TentativeCost", parameters.TentativeCost);
            queryParameters.Add("@ProductMakeId", parameters.ProductMakeId);
            queryParameters.Add("@BMSMakeId", parameters.BMSMakeId);
            queryParameters.Add("@RGP", parameters.RGP);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveSpareDetails", queryParameters);
        }

        public async Task<IEnumerable<SpareDetails_Response>> GetSpareDetailsList(SpareDetails_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SpareCategoryId", parameters.SpareCategoryId);
            queryParameters.Add("@BMSMakeId", parameters.BMSMakeId);
            queryParameters.Add("@ProductMakeId", parameters.ProductMakeId);
            queryParameters.Add("@IsRGP", parameters.IsRGP);
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<SpareDetails_Response>("GetSpareDetailsList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<SpareDetails_Response?> GetSpareDetailsById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<SpareDetails_Response>("GetSpareDetailsById", queryParameters)).FirstOrDefault();
        }

        public async Task<IEnumerable<SpareDetails_ImportDataValidation>> ImportProblemReportedByCustsDetails(List<SpareDetails_ImportData> parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            string xmlData = ConvertListToXml(parameters);
            queryParameters.Add("@XmlData", xmlData);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);
            return await ListByStoredProcedure<SpareDetails_ImportDataValidation>("ImportSpareDetails", queryParameters);
        }
    }
}
