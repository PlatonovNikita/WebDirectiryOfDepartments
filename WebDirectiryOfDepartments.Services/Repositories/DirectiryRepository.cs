using Microsoft.EntityFrameworkCore;
using System.Data;
using WebDirectiryOfDepartments.Core.Interfaces.Repositories;
using WebDirectiryOfDepartments.Core.Model;
using WebDirectiryOfDepartments.DataServices.Context;

namespace WebDirectiryOfDepartments.DataServices.Repositories
{
    internal class DirectiryRepository : IDirectoryRepository
    {
        private readonly DirectiryOfDepartmentsContext _dbContext;

        public DirectiryRepository(DirectiryOfDepartmentsContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddUnitAsync(DirectoryUnit directoryUnit)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(
                (directoryUnit.ParentDirectoryUnitId != null)
                ? IsolationLevel.Serializable
                : IsolationLevel.ReadCommitted);

            await _dbContext.DirectoryUnits.AddAsync(directoryUnit);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        public async Task DeleteUnitAsync(Guid id)
        {
            if (_dbContext.DirectoryUnits.Any(_ => _.ParentDirectoryUnitId == id))
            {
                throw new InvalidOperationException($"Подразделение с идентификатором {id} не может быть удалена, так как есть подразделения ссылающиеся на данное подразделение!");
            }

            _dbContext.DirectoryUnits.Remove(new DirectoryUnit { Id = id });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<DirectoryUnit[]> GetAllAsync()
        {
            await _dbContext.DirectoryUnits.LoadAsync();
            return await _dbContext.DirectoryUnits
                .Where(_ => _.ParentDirectoryUnitId == null)
                .ToArrayAsync();
        }

        public async Task UpdateParentDirectoryUnitAsync(Guid directoryUnitId, Guid parentDirectoryUnitId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.Serializable);

            var directoryUnit = await _dbContext.DirectoryUnits.FirstOrDefaultAsync(_ => _.Id == directoryUnitId);

            if (directoryUnit == null)
            {
                throw new InvalidOperationException($"Подразделение с идентификатором {directoryUnitId} нет в репозитории.");
            }

            var parentDirectoryUnit = await _dbContext.DirectoryUnits.FirstOrDefaultAsync(_ => _.Id == parentDirectoryUnitId);

            if (parentDirectoryUnit == null)
            {
                throw new InvalidOperationException($"Подразделение с идентификатором {parentDirectoryUnitId} нет в репозитории.");
            }

            directoryUnit.ParentDirectoryUnitId = parentDirectoryUnitId;
            directoryUnit.ParentDirectoryUnit = parentDirectoryUnit;

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }
    }
}
