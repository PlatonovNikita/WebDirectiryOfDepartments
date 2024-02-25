namespace WebDirectiryOfDepartments.InputModels.Directory
{
    public class AddDirectoryUnitInputModel
    {
        /// <summary>
        /// Наименование подразделения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание подразделения.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор родительского подразделения
        /// </summary>
        public Guid? ParentDirectoryUnitId { get; set; }
    }
}
