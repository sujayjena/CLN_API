using CLN.Domain.Entities;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CLN.Application.Models
{
    public class ExpenseModel
    {
    }

    public class Expense_Search : BaseSearchEntity
    {
        public int? EmployeeId { get; set; }

        [DefaultValue("All")]
        public string? FilterType { get; set; }
    }

    public class Expense_Request : BaseEntity
    {
        public Expense_Request()
        {
            ExpenseDetails = new List<ExpenseDetails_Request>();
        }

        public string ExpenseNumber { get; set; }

        public int? TicketId { get; set; }

        public int? StatusId { get; set; }

        public bool? IsActive { get; set; }

        public List<ExpenseDetails_Request> ExpenseDetails { get; set; }
    }

    public class Expense_Response : BaseResponseEntity
    {
        public Expense_Response()
        {
            ExpenseDetails = new List<ExpenseDetails_Response>();
        }

        public string ExpenseNumber { get; set; }
        public int? TicketId { get; set; }
        public string TicketNumber { get; set; }
        public string CustomerName { get; set; }
        public DateTime? TicketStartDate { get; set; }
        public DateTime? TicketCloserDate { get; set; }
        public int? StatusId { get; set; }
        public string StatusName { get; set; }
        public bool? IsActive { get; set; }

        public List<ExpenseDetails_Response> ExpenseDetails { get; set; }
    }

    public class Expense_ApproveNReject
    {
        public int Id { get; set; }
        public int ExpenseId { get; set; }
        public int ExpenseDetailStatusId { get; set; }
    }


    public class ExpenseDetails_Search : BaseSearchEntity
    {
        public int? ExpenseId { get; set; }
        public int? ExpenseDetailStatusId { get; set; }
    }

    public class ExpenseDetails_Request : BaseEntity
    {
        public int? ExpenseId { get; set; }

        public DateTime? ExpenseDate { get; set; }

        public int? ExpenseTypeId { get; set; }

        public string ExpenseDescription { get; set; }

        public decimal? ExpenseAmount { get; set; }

        public string ExpenseImageFileName { get; set; }

        public string ExpenseImageOriginalFileName { get; set; }

        public string ExpenseImageFile_Base64 { get; set; }

        public int? ExpenseDetailStatusId { get; set; }
    }

    public class ExpenseDetails_Response : BaseResponseEntity
    {
        public int? ExpenseId { get; set; }
        public string ExpenseNumber { get; set; }
        public DateTime? ExpenseDate { get; set; }
        public int? ExpenseTypeId { get; set; }
        public string ExpenseType { get; set; }
        public string ExpenseDescription { get; set; }
        public decimal? ExpenseAmount { get; set; }
        public string ExpenseImageFileName { get; set; }
        public string ExpenseImageOriginalFileName { get; set; }
        public string ExpenseImageFileURL { get; set; }
        public int? ExpenseDetailStatusId { get; set; }
        public string ExpenseDeteillStatusName { get; set; }
    }
}
