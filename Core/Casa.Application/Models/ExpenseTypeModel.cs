using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ExpenseTypeModel
    {
    }

    #region Expense Type
    public class ExpenseType_Request : BaseEntity
    {
        public string? ExpenseType { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ExpenseType_Response : BaseResponseEntity
    {
        public string? ExpenseType { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion

    #region Expense Matrix
    public class ExpenseMatrix_Request : BaseEntity
    {
        public int? EmployeeLevelId { get; set; }
        public int? ExpenseTypeId { get; set; }
        public int? CityGradeId { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ExpenseMatrixSearch_Request : BaseSearchEntity
    {
        public int? EmployeeLevelId { get; set; }
        public int? ExpenseTypeId { get; set; }
        public int? CityGradeId { get; set; }
    }

    public class ExpenseMatrix_Response : BaseResponseEntity
    {
        public int? EmployeeLevelId { get; set; }
        public string EmployeeLevel { get; set; }

        public int? ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }

        public int? CityGradeId { get; set; }
        public string CityGrade { get; set; }

        public decimal? Amount { get; set; }
        public bool? IsActive { get; set; }
    }
    #endregion
}
