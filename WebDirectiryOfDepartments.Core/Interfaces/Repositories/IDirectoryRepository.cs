using WebDirectiryOfDepartments.Core.Model;

namespace WebDirectiryOfDepartments.Core.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория категорий иерархического справочника.
    /// </summary>
    public interface IDirectoryRepository
    {
        /// <summary>
        /// Добавить категорию в репозиторий.
        /// </summary>
        /// <param name="category">Добавляемая категория.</param>
        public Task AddUnitAsync(DirectoryUnit category);

        /// <summary>
        /// Удалить категорию из репозиотрия.
        /// </summary>
        /// <param name="id">Идентификатор удаляемой категории.</param>
        public Task DeleteUnitAsync(Guid id);

        /// <summary>
        /// Сменить родительское подразделение.
        /// </summary>
        /// <param name="categoryId">Идентификатор подразделения, который нужно переместить.</param>
        /// <param name="parentCategoryId">Идентификатор нового родительского подразделения.</param>
        public Task UpdateParentDirectoryUnitAsync(Guid categoryId, Guid parentCategoryId);

        /// <summary>
        /// Получить список всех категорий.
        /// </summary>
        /// <returns>Список категорий.</returns>
        public Task<DirectoryUnit[]> GetAllAsync();
    }
}
