using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using WebDirectiryOfDepartments.Core.Interfaces.Repositories;
using WebDirectiryOfDepartments.Core.Model;
using WebDirectiryOfDepartments.DataServices.Context;
using WebDirectiryOfDepartments.InputModels.Directory;
using WebDirectiryOfDepartments.OutputModels.Directory;

namespace WebDirectiryOfDepartments.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DirectoryController : ControllerBase
    {
        private readonly IDirectoryRepository _directoryRepository;
        private readonly DirectiryOfDepartmentsContext _dbContext;

        public DirectoryController(IDirectoryRepository directoryRepository)
        {
            _directoryRepository = directoryRepository;
        }

        /// <summary>
        /// Добавить подразделение.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(DirectoryUnit), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDirectoryUnit([FromBody]AddDirectoryUnitInputModel inputModel)
        {
            var directoryUnit = new DirectoryUnit
            {
                Name = inputModel.Name,
                Description = inputModel.Description,
                ParentDirectoryUnitId = inputModel.ParentDirectoryUnitId,
            };

            await _directoryRepository.AddUnitAsync(directoryUnit);

            return CreatedAtAction(nameof(AddDirectoryUnit), directoryUnit);
        }

        /// <summary>
        /// Удалить подразделенние.
        /// </summary>
        /// <param name="id">Идентификатор удаляемого подразделения</param>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteDirectoryUnit([FromQuery]Guid id)
        {
            try
            {
                await _directoryRepository.DeleteUnitAsync(id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Получить все подразделения иерархического справочника подразделений.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DirectoryUnitOutputModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDirectoryUnits()
        {
            var directoryUnitsData = await _directoryRepository.GetAllAsync();

            var directoryUnits = MapDirectoryUnits(directoryUnitsData);

            return Ok(directoryUnits);
        }

        /// <summary>
        /// Изменить родительское подразделение.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateParentDirectoryUnit(UpdateParentDirectoryUnitInputModel inputModel)
        {
            try
            {
                await _directoryRepository.UpdateParentDirectoryUnitAsync(inputModel.DirectoryUnitId, inputModel.ParentDirectoryUnitId);

                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Экспортирует данные в формате XML.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportDataToXML()
        {
            var directoryUnitsData = await _directoryRepository.GetAllAsync();

            var directoryUnits = MapDirectoryUnits(directoryUnitsData).ToArray(); ;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DirectoryUnitOutputModel[]));

            using var memoryStream = new MemoryStream();

            xmlSerializer.Serialize(memoryStream, directoryUnits);

            memoryStream.Position = 0;

            return File(memoryStream.ToArray(), "application/xml", "ExportData.xml");
        }

        /// <summary>
        /// Импортирование данных в формате XML
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ImportDataInXML([Required]IFormFile file)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DirectoryUnit[]));

            var directoryUnits = xmlSerializer.Deserialize(file.OpenReadStream()) as DirectoryUnit[];

            await _dbContext.DirectoryUnits.AddRangeAsync(directoryUnits);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private IEnumerable<DirectoryUnitOutputModel> MapDirectoryUnits(IEnumerable<DirectoryUnit> directoryUnits)
        {
            return directoryUnits?.Select(_ => MapDirectoryUnit(_));
        }

        private DirectoryUnitOutputModel MapDirectoryUnit(DirectoryUnit directoryUnit)
        {
            return new DirectoryUnitOutputModel
            {
                Id = directoryUnit.Id,
                Name = directoryUnit.Name,
                Description = directoryUnit.Description,
                ParentDirectoryUnitId = directoryUnit.ParentDirectoryUnitId,
                ChildDirectoryUnits = MapDirectoryUnits(directoryUnit.ChildDirectoryUnits)?.ToArray(),
            };
        }
    }
}
