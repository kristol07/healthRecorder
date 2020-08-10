using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using healthRecorder.Models;
using healthRecorder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace healthRecorder.Controllers
{
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


        [HttpGet()]
        public ActionResult<IEnumerable<EmployeeDto>> GetAllRecords()
        {
            try
            {
                var allRecords = _recordsRepository.GetAllRecords();
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(allRecords));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("{employeeId}")]
        public ActionResult<IEnumerable<RecordDto>> GetRecordsForEmployee(Guid employeeId)
        {
            try
            {
                var allRecordsForEmployee = _recordsRepository.GetRecordsForEmployee(employeeId);

                if (allRecordsForEmployee == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<RecordDto>(allRecordsForEmployee));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet("{employeeId}/{checkDate:DateTime}", Name = "GetRecord")]
        public ActionResult<RecordDto> GetRecord(Guid employeeId, DateTime checkDate)
        {
            try
            {
                var record = _recordsRepository.GetRecord(employeeId, checkDate);

                if (record == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<RecordDto>(record));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public ActionResult<RecordDto> CreateRecord([FromBody] RecordForCreationDto record)
        {
            try
            {
                var recordEntity = _mapper.Map<Record>(record);
                _recordsRepository.AddRecord(recordEntity);
                _recordsRepository.Save();

                var recordToReturn = _mapper.Map<RecordDto>(recordEntity);
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

        [HttpDelete("{employeeId}/{checkDate:DateTime}")]
        public ActionResult DeleteRecord(Guid employeeId, DateTime checkDate)
        {
            try
            {
                var recordExists = _recordsRepository.RecordExists(employeeId, checkDate);

                if (recordExists == false)
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
