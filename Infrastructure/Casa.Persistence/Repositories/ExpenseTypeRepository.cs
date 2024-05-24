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
    public class ExpenseTypeRepository : GenericRepository, IExpenseTypeRepository
    {

        private IConfiguration _configuration;

        public ExpenseTypeRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> SaveExpenseType(ExpenseType_Request parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", parameters.Id);
            queryParameters.Add("@ExpenseType", parameters.ExpenseType.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            return await SaveByStoredProcedure<int>("SaveExpenseType", queryParameters);
        }

        public async Task<IEnumerable<ExpenseType_Response>> GetExpenseTypeList(BaseSearchEntity parameters)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
            queryParameters.Add("@IsActive", parameters.IsActive);
            queryParameters.Add("@PageNo", parameters.PageNo);
            queryParameters.Add("@PageSize", parameters.PageSize);
            queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
            queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

            var result = await ListByStoredProcedure<ExpenseType_Response>("GetExpenseTypeList", queryParameters);
            parameters.Total = queryParameters.Get<int>("Total");

            return result;
        }

        public async Task<ExpenseType_Response?> GetExpenseTypeById(long Id)
        {
            DynamicParameters queryParameters = new DynamicParameters();
            queryParameters.Add("@Id", Id);
            return (await ListByStoredProcedure<ExpenseType_Response>("GetExpenseTypeById", queryParameters)).FirstOrDefault();
        }



        //public async Task<int> SaveExpenseMatrix(ExpenseMatrix_Request parameters)
        //{
        //    //DynamicParameters queryParameters = new DynamicParameters();
        //    //queryParameters.Add("@Id", parameters.Id);
        //    //queryParameters.Add("@ExpenseMatrix", parameters.ExpenseMatrix.SanitizeValue());
        //    //queryParameters.Add("@IsActive", parameters.IsActive);
        //    //queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

        //    return await SaveByStoredProcedure<int>("SaveExpenseMatrix", queryParameters);
        //}

        //public async Task<IEnumerable<ExpenseMatrix_Response>> GetExpenseMatrixList(BaseSearchEntity parameters)
        //{
        //    DynamicParameters queryParameters = new DynamicParameters();
        //    queryParameters.Add("@SearchText", parameters.SearchText.SanitizeValue());
        //    queryParameters.Add("@IsActive", parameters.IsActive);
        //    queryParameters.Add("@PageNo", parameters.PageNo);
        //    queryParameters.Add("@PageSize", parameters.PageSize);
        //    queryParameters.Add("@Total", parameters.Total, null, System.Data.ParameterDirection.Output);
        //    queryParameters.Add("@UserId", SessionManager.LoggedInUserId);

        //    var result = await ListByStoredProcedure<ExpenseMatrix_Response>("GetExpenseMatrixList", queryParameters);
        //    parameters.Total = queryParameters.Get<int>("Total");

        //    return result;
        //}

        //public async Task<ExpenseMatrix_Response?> GetExpenseMatrixById(long Id)
        //{
        //    DynamicParameters queryParameters = new DynamicParameters();
        //    queryParameters.Add("@Id", Id);
        //    return (await ListByStoredProcedure<ExpenseMatrix_Response>("GetExpenseMatrixById", queryParameters)).FirstOrDefault();
        //}
    }
}
