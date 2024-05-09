using CLN.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface IManageTRCRepository
    {
        Task<int> SaveManageTRC(ManageTRC_Request parameters);

        Task<IEnumerable<ManageTRCList_Response>> GetManageTRCList(ManageTRC_Search parameters);

        Task<ManageTRCDetail_Response?> GetManageTRCById(int Id);


        Task<int> SaveManageTRCPartDetail(ManageTRCPartDetails_Request parameters);

        Task<IEnumerable<ManageTRCPartDetails_Response>> GetManageTRCPartDetailById(int Id);
    }
}
