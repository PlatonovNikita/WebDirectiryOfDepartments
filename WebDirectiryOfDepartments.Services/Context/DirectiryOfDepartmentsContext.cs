using Microsoft.EntityFrameworkCore;
using WebDirectiryOfDepartments.Core.Model;

namespace WebDirectiryOfDepartments.DataServices.Context
{
    public class DirectiryOfDepartmentsContext : DbContext
    {
        public DbSet<DirectoryUnit> DirectoryUnits { get; set; }

        public DirectiryOfDepartmentsContext(DbContextOptions<DirectiryOfDepartmentsContext> options)
        : base(options)
        {
        }
    }
}
