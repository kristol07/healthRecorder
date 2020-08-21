using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using healthRecorder.Entities;
using healthRecorder.Models;
using healthRecorder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace healthRecorder.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/v2/records")]
    [ApiExplorerSettings(GroupName = "HealthRecordsOpenAPISpecificationV2")]
    [ApiController]
    [ProducesResponseType(500)]
    public class RecordsV2Controller : ControllerBase
    {
        private readonly IRecordsRepository _recordsRepository;
        private readonly IMapper _mapper;

        public RecordsV2Controller(IRecordsRepository recordsRepository, IMapper mapper)
        {
            _recordsRepository = recordsRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Get a list of all recprds
        /// </summary>
        /// <param name="employeeId">Filter records for employeeId</param>
        /// <returns>List of records saved in database</returns>
        /// <response code="200">An ActionResult of list of type SingleRecordDto</response>
        [HttpGet("", Name = "GetRecordsv2")]
        [ProducesResponseType(typeof(IEnumerable<SingleRecordDto>), 200)]
        public ActionResult<IEnumerable<SingleRecordDto>> GetRecords([FromQuery] string employeeId)
        {
            try
            {
                var allRecords = _recordsRepository.GetAllRecords(employeeId);

                return Ok(_mapper.Map<IEnumerable<SingleRecordDto>>(allRecords));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get specific record
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>An ActionResult of type SingleRecordDto</returns>
        /// <response code="200">Returns requested record</response>
        /// <response code="404">Record not found</response>
        [HttpGet("{recordId}", Name = "GetRecordv2")]
        [ProducesResponseType(typeof(SingleRecordDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<SingleRecordDto> GetRecord(string recordId)
        {
            try
            {
                if (!_recordsRepository.RecordExists(recordId))
                {
                    return NotFound();
                }

                var record = _recordsRepository.GetRecord(recordId);

                return Ok(_mapper.Map<SingleRecordDto>(record));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Add a new record for specific employee
        /// </summary>
        /// <param name="record"></param>
        /// <returns>An ActionResult of type SingleRecordDto</returns>
        /// <response code="201">Record Added.</response>
        /// <response code="403">Operation forbidden, check the employeeId or checkDate value.</response>
        [HttpPost("", Name = "AddNewRecordv2")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(SingleRecordDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public ActionResult<SingleRecordDto> CreateRecord([FromBody, Required] RecordForCreationDto record)
        {
            try
            {
                var recordEntity = _mapper.Map<Record>(record);

                if (!_recordsRepository.EmployeeExists(recordEntity.EmployeeId))
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                if (_recordsRepository.RecordExists(recordEntity.Id))
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                _recordsRepository.AddRecord(recordEntity);

                var recordToReturn = _mapper.Map<SingleRecordDto>(recordEntity);
                return CreatedAtRoute("GetRecordv2",
                        new
                        {
                            recordId = recordToReturn.Id
                        },
                        recordToReturn);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update an existed record
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="updatedRecord"></param>
        /// <returns>An ActionResult of type SingleRecordDto</returns>
        /// <response code="204">Record updated.</response>
        /// <response code="404">Record not found.</response>
        [HttpPut("{recordId}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(SingleRecordDto), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<SingleRecordDto> Put(string recordId, [FromBody, Required] RecordForUpdatev2Dto updatedRecord)
        {
            try
            {
                var recordEntity = _recordsRepository.GetRecord(recordId);
                if (recordEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(updatedRecord, recordEntity);

                _recordsRepository.UpdateRecord(recordEntity, recordId);

                var recordToReturn = _mapper.Map<SingleRecordDto>(recordEntity);
                return CreatedAtRoute("GetRecordv2",
                        new
                        {
                            recordId = recordToReturn.Id
                        },
                        recordToReturn);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Delete one record
        /// </summary>
        /// <param name="recordId"></param>
        /// <returns>An ActionResult</returns>
        /// <response code="204">Record deleted.</response>
        /// <response code="404">Record not found.</response>
        [HttpDelete("{recordId}", Name = "DeleteRecordv2")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult DeleteRecord(string recordId)
        {
            try
            {
                if (!_recordsRepository.RecordExists(recordId))
                {
                    return NotFound();
                }

                _recordsRepository.DeleteRecord(recordId);

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
