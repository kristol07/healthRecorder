using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using healthRecorder.Models;
using healthRecorder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace healthRecorder.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/employees")]
    [ApiController]
    [ProducesResponseType(500)]
    public class EmployeesController : ControllerBase
    {

        private readonly IRecordsRepository _recordsRepository;
        private readonly IMapper _mapper;

        public EmployeesController(IRecordsRepository recordsRepository, IMapper mapper)
        {
            _recordsRepository = recordsRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Get a list of all employees
        /// </summary>
        /// <returns>List of employees saved in database</returns>
        /// <response code="200">Employees returned</response>
        [HttpGet("", Name = "GetEmployees")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), 200)]
        public ActionResult<IEnumerable<EmployeeDto>> GetEmployees()
        {
            try
            {
                var allRecords = _recordsRepository.GetAllEmployees();
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(allRecords));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get  an employee by his/her id 
        /// </summary>
        /// <param name="employeeId">The id of the employee you want to get</param>
        /// <returns>An employee with id, ginNumber, name and records fields</returns>
        /// <response code="200">Information of this employee returned</response>
        /// <response code="404">Employee not found</response>
        [HttpGet("{employeeId}", Name = "GetEmployee")]
        [ProducesResponseType(typeof(IEnumerable<EmployeeDto>), 200)]
        [ProducesResponseType(404)]
        public ActionResult<EmployeeDto> GetEmployee(string employeeId)
        {
            try
            {
                if (!_recordsRepository.EmployeeExists(employeeId))
                {
                    return NotFound();
                }

                var employee = _recordsRepository.GetEmployee(employeeId);

                return Ok(_mapper.Map<EmployeeDto>(employee));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
