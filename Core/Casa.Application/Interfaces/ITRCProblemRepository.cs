using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ITRCProblemRepository
    {
        #region WireHarness 

        Task<int> SaveWireHarness(WireHarness_Request parameters);

        Task<IEnumerable<WireHarness_Response>> GetWireHarnessList(BaseSearchEntity parameters);

        Task<WireHarness_Response?> GetWireHarnessById(long Id);

        #endregion

        #region Connector 

        Task<int> SaveConnector(Connector_Request parameters);

        Task<IEnumerable<Connector_Response>> GetConnectorList(BaseSearchEntity parameters);

        Task<Connector_Response?> GetConnectorById(long Id);

        #endregion

        #region Other Damage 

        Task<int> SaveOtherDamage(OtherDamage_Request parameters);

        Task<IEnumerable<OtherDamage_Response>> GetOtherDamageList(BaseSearchEntity parameters);

        Task<OtherDamage_Response?> GetOtherDamageById(long Id);

        #endregion
    }
}
