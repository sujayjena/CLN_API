using CLN.Application.Helpers;
using CLN.Application.Interfaces;
using CLN.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text;
using System.Threading.Tasks;

namespace CLN.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            //var connectionString = configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));

            services.AddScoped<IGenericRepository, GenericRepository>();
            services.AddScoped<IJwtUtilsRepository, JwtUtilsRepository>();
            services.AddScoped<IFileManager, FileManager>();

            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<ITerritoryRepository, TerritoryRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped<IContractCycleRepository, ContractCycleRepository>();
            services.AddScoped<ICompanyTypeRepository, CompanyTypeRepository>();
            services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
            services.AddScoped<IMasterDataRepository, MasterDataRepository>();
            services.AddScoped<IPMCycleRepository, PMCycleRepository>();
            services.AddScoped<ITicketsRepository, TicketsRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<IComplaintStatusRepository, ComplaintStatusRepository>();
            services.AddScoped<IOrderTypeRepository, OrderTypeRepository>();
            services.AddScoped<IUOMRepository, UOMRepository>();
            services.AddScoped<IProblemReportedRepository, ProblemReportedRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<ISpareDetailsRepository, SpareDetailsRepository>();
            services.AddScoped<IReceiveModeRepository, ReceiveModeRepository>();
            services.AddScoped<IWarrantyStatusRepository, WarrantyStatusRepository>();
            services.AddScoped<ISourceChannelRepository, SourceChannelRepository>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();
            services.AddScoped<IBloodGroupRepository, BloodGroupRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProblemReportedByEngRepository, ProblemReportedByEngRepository>();
            services.AddScoped<IRectificationActionRepository, RectificationActionRepository>();
            services.AddScoped<IDetailsOfAdjustmentRepository, DetailsOfAdjustmentRepository>();
            services.AddScoped<IManageSalesRepository, ManageSalesRepository>();
            services.AddScoped<IProtectionsRepository, ProtectionsRepository>();
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IContactDetailRepository, ContactDetailRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<ILeaveRepository, LeaveRepository>();
        }
    }
}
