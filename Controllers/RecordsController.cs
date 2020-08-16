using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
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
    [Route("api/records")]
    [ApiController]
    [ProducesResponseType(500)]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsRepository _recordsRepository;
        private readonly IMapper _mapper;

        public RecordsController(IRecordsRepository recordsRepository, IMapper mapper)
        {
            _recordsRepository = recordsRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Get a list of all recprds
        /// </summary>
        /// <returns>List of records saved in database</returns>
        /// <response code="200">An ActionResult of list of type SingleRecordDto</response>
        [HttpGet("", Name = "GetRecords")]
        [ProducesResponseType(typeof(IEnumerable<SingleRecordDto>), 200)]
        public ActionResult<IEnumerable<SingleRecordDto>> GetRecords()
        {
            try
            {
                var allRecords = _recordsRepository.GetAllRecords();
                return Ok(_mapper.Map<IEnumerable<SingleRecordDto>>(allRecords));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get health records of an employee by his/her id 
        /// </summary>
        /// <param name="employeeId">The id of the employee you want to get</param>
        /// <returns>An ActionResult of list of type RecordDto</returns>
        /// <response code="200">All records of this employee returned</response>
        /// <response code="404">Employee not found</response>
        [HttpGet("{employeeId}", Name = "GetRecordsForEmployee")]
        [ProducesResponseType(typeof(IEnumerable<RecordDto>), 200)]
        [ProducesResponseType(404)]
        public ActionResult<IEnumerable<RecordDto>> GetRecordsForEmployee(string employeeId)
        {
            try
            {
                //if (!_recordsRepository.EmployeeExists(employeeId))
                //{
                //    return NotFound();
                //}

                var allRecordsForEmployee = _recordsRepository.GetRecordsForEmployee(employeeId);

                return Ok(_mapper.Map<RecordDto>(allRecordsForEmployee));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get the record of one employee for specific date
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="checkDate"></param>
        /// <returns>An ActionResult of type SingleRecordDto</returns>
        /// <response code="200">Returns requested record</response>
        /// <response code="404">Record not found</response>
        [HttpGet("{employeeId}/{checkDate:DateTime}", Name = "GetRecord")]
        [ProducesResponseType(typeof(SingleRecordDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<SingleRecordDto> GetRecord(string employeeId, DateTime checkDate)
        {
            try
            {
                if (!_recordsRepository.RecordExists(employeeId, checkDate))
                {
                    return NotFound();
                }

                var record = _recordsRepository.GetRecord(employeeId, checkDate);

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
        [HttpPost("", Name = "AddNewRecord")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(SingleRecordDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<SingleRecordDto> CreateRecord([FromBody, Required] RecordForCreationDto record)
        {
            try
            {
                var recordEntity = _mapper.Map<Record>(record);

                if (_recordsRepository.RecordExists(recordEntity.EmployeeId, recordEntity.CheckDate))
                {
                    return StatusCode(StatusCodes.Status403Forbidden);
                }

                _recordsRepository.AddRecord(recordEntity);

                var recordToReturn = _mapper.Map<SingleRecordDto>(recordEntity);
                return CreatedAtRoute("GetRecord",
                        new
                        {
                            employeeId = recordToReturn.EmployeeId,
                            checkDate = recordToReturn.CheckDate
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
        /// <param name="employeeId"></param>
        /// <param name="checkDate"></param>
        /// <param name="updatedRecord"></param>
        /// <returns>An ActionResult of type SingleRecordDto</returns>
        /// <response code="204">Record updated.</response>
        /// <response code="404">Record not found.</response>
        [HttpPut("{employeeId}/{checkDate}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(SingleRecordDto), 204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public ActionResult<SingleRecordDto> Put(string employeeId, DateTime checkDate, [FromBody, Required] RecordForUpdateDto updatedRecord)
        {
            try
            {
                var recordEntity = _recordsRepository.GetRecord(employeeId, checkDate);
                if (recordEntity == null)
                {
                    return NotFound();
                }

                _mapper.Map(updatedRecord, recordEntity);

                _recordsRepository.AddRecord(recordEntity);
                _recordsRepository.Save();

                var recordToReturn = _mapper.Map<SingleRecordDto>(recordEntity);
                return CreatedAtRoute("GetRecord",
                        new
                        {
                            employeeId = recordToReturn.EmployeeId,
                            checkDate = recordToReturn.CheckDate
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
        /// <param name="employeeId"></param>
        /// <param name="checkDate"></param>
        /// <returns>An ActionResult</returns>
        [HttpDelete("{employeeId}/{checkDate:DateTime}", Name = "DeleteRecord")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult DeleteRecord(string employeeId, DateTime checkDate)
        {
            try
            {
                if (!_recordsRepository.RecordExists(employeeId, checkDate))
                {
                    return NotFound();
                }

                _recordsRepository.DeleteRecord(employeeId, checkDate);
                _recordsRepository.Save();

                return NoContent();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
