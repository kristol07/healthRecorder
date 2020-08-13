using System;
using System.Collections.Generic;
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
        /// Get a list of records for all employees
        /// </summary>
        /// <returns>An ActionResult of type IEnumerable of EmployeeDto</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployees()
        {
            try
            {
                var allRecords =  _recordsRepository.GetAllEmployees();
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(allRecords));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get health records of an employee by his/her id 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>An ActionResult of type SingleRecordDto </returns>
        [HttpGet("{employeeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<SingleRecordDto>> GetRecordsForEmployee(string employeeId)
        {
            try
            {
                if (!_recordsRepository.EmployeeExists(employeeId))
                {
                    return NotFound();
                }

                var allRecordsForEmployee =  _recordsRepository.GetRecordsForEmployee(employeeId);

                return Ok(_mapper.Map<SingleRecordDto>(allRecordsForEmployee));
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
        [HttpGet("{employeeId}/{checkDate:DateTime}", Name = "GetRecord")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<SingleRecordDto> CreateRecord([FromBody] RecordForCreationDto record)
        {
            try
            {
                var recordEntity = _mapper.Map<Record>(record);
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


        //[HttpPut("{id}/{checkDate}")]
        //public void Put(int gin, DateTime checkDate, [FromBody] RecordModel recordModel)
        //{

        //}

        //[HttpDelete("{employeeId}")]
        //public ActionResult DeleteRecordsForEmployee(Guid employeeId)
        //{
        //    var employeeExists = _recordsRepository.EmployeeExists(employeeId);

        //    if(employeeExists == false)
        //    {
        //        return NotFound();
        //    }

        //    _recordsRepository.de
        //}

        /// <summary>
        /// Delete one record
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="checkDate"></param>
        /// <returns>An ActionResult</returns>
        [HttpDelete("{employeeId}/{checkDate:DateTime}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
