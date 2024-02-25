namespace WebDirectiryOfDepartments.InputModels.Directory
{
    public class UpdateParentDirectoryUnitInputModel
    {
        /// <summary>
        /// Идентификатор подразделения, которе нужно переместить.
        /// </summary>
        public Guid DirectoryUnitId { get; set; }

        /// <summary>
        /// Идентификатор нового родительского подразделения.
        /// </summary>
        public Guid ParentDirectoryUnitId { get; set; }
    }
}
