namespace WebDirectiryOfDepartments.OutputModels.Directory
{
    public class DirectoryUnitOutputModel
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
        /// Дочерние подразделения.
        /// </summary>
        public DirectoryUnitOutputModel[] ChildDirectoryUnits { get; set; }
    }
}
