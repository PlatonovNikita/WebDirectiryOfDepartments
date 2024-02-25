using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebDirectiryOfDepartments.Core.Interfaces.Repositories;
using WebDirectiryOfDepartments.DataServices.Context;
using WebDirectiryOfDepartments.DataServices.Repositories;

namespace WebDirectiryOfDepartments.DataServices
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DirectiryOfDepartmentsDb");

            services.AddDbContext<DirectiryOfDepartmentsContext>(options =>
               options.UseSqlServer(connectionString));

            services.AddTransient<IDirectoryRepository, DirectiryRepository>();

            return services;
        }
    }
}
