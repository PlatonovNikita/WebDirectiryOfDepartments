namespace WebDirectiryOfDepartments.Core.Model
{
    /// <summary>
    /// Подразделение в иерархическом справочнике.
    /// </summary>
    public class DirectoryUnit
    {
        /// <summary>
        /// Идентификатор подразделения.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование Подразделения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание подразделения.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор Родительского подразделения.
        /// </summary>
        public Guid? ParentDirectoryUnitId { get; set; }

        /// <summary>
        /// Родительское подразделение.
        /// </summary>
        public DirectoryUnit ParentDirectoryUnit { get; set; }

        /// <summary>
        /// Дочерние подразделения.
        /// </summary>
        public List<DirectoryUnit> ChildDirectoryUnits { get; set; }
    }
}
