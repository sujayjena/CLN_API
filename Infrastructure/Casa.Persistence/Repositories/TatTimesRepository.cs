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
    public class TatTimesRepository : GenericRepository, ITatTimesRepository
    {
        private IConfiguration _configuration;

        public TatTimesRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveTatTimes(TatTimes_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@HolidayName", parameters.HolidayName);
            queryParameters.Add("@HolidayDate", parameters.HolidayDate);
            queryParameters.Add("@HolidayDay", parameters.HolidayDay);
            queryParameters.Add("@StartTimeHour", parameters.StartTimeHour);
            queryParameters.Add("@StartTimeMinutes", parameters.StartTimeMinutes);
            queryParameters.Add("@SlaTimeHour", parameters.SlaTimeHour);
            queryParameters.Add("@SlaTimeMinutes", parameters.SlaTimeMinutes);
            queryParameters.Add("@EndTimeHour", parameters.EndTimeHour);
            queryParameters.Add("@EndTimeMinutes", parameters.EndTimeMinutes);
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveTatTimes", queryParameters);
        }

        public async Task<IEnumerable<TatTimes_Response>> GetTatTimesList(TatTimes_Search parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<TatTimes_Response>("GetTatTimesList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<TatTimes_Response?> GetTatTimesById(int Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();

            queryParameters.Add("@Id", Id);

            return (await ListByStoredProcedure<TatTimes_Response>("GetTatTimesById", queryParameters)).FirstOrDefault();
        }
    }
}
