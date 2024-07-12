using CLN.Application.Models;
using CLN.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Application.Interfaces
{
    public interface ITerritoryRepository
    {
        #region Region 

        Task<int> SaveRegion(Region_Request parameters);

        Task<IEnumerable<Region_Response>> GetRegionList(BaseSearchEntity parameters);

        Task<Region_Response?> GetRegionById(long Id);

        #endregion

        #region State 

        Task<int> SaveState(State_Request parameters);

        Task<IEnumerable<State_Response>> GetStateList(BaseSearchEntity parameters);

        Task<State_Response?> GetStateById(long Id);

        Task<IEnumerable<StateDataValidationErrors>> ImportState(List<ImportedState> parameters);

        #endregion

        #region District 

        Task<int> SaveDistrict(District_Request parameters);

        Task<IEnumerable<District_Response>> GetDistrictList(BaseSearchEntity parameters);

        Task<District_Response?> GetDistrictById(long Id);

        Task<IEnumerable<DistrictDataValidationErrors>> ImportDistrict(List<ImportedDistrict> parameters);

        #endregion

        #region City 

        Task<int> SaveCity(City_Request parameters);

        Task<IEnumerable<City_Response>> GetCityList(BaseSearchEntity parameters);

        Task<City_Response?> GetCityById(long Id);

        Task<IEnumerable<CityDataValidationErrors>> ImportCity(List<ImportedCity> parameters);

        #endregion

        #region Area 

        Task<int> SaveArea(Area_Request parameters);

        Task<IEnumerable<Area_Response>> GetAreaList(BaseSearchEntity parameters);

        Task<Area_Response?> GetAreaById(long Id);

        #endregion

        #region City Grade 

        Task<int> SaveCityGrade(CityGrade_Request parameters);

        Task<IEnumerable<CityGrade_Response>> GetCityGradeList(BaseSearchEntity parameters);

        Task<CityGrade_Response?> GetCityGradeById(long Id);

        #endregion

        #region Territories 

        Task<int> SaveTerritories(Territories_Request parameters);

        Task<IEnumerable<Territories_Response>> GetTerritoriesList(BaseSearchEntity parameters);

        Task<Territories_Response?> GetTerritoriesById(long Id);

        Task<IEnumerable<Territories_State_Dist_City_Area_Response>> GetTerritories_State_Dist_City_Area_List_ById(Territories_State_Dist_City_Area_Search parameters);

        Task<IEnumerable<TerritoriesDataValidationErrors>> ImportTerritories(List<ImportedTerritories> parameters);

        #endregion
    }
}
