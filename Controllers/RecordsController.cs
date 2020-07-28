using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using healthRecorder.Models;
using healthRecorder.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace healthRecorder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordsService _recordsService;

        public RecordsController(IRecordsService recordsService)
        {
            _recordsService = recordsService;
        }

        // GET: api/<RecordsController>
        [HttpGet]
        public IEnumerable<Record> Get()
        {
            return _recordsService.GetAllRecordsAsync().Result;
        }

        // GET api/<RecordsController>/5
        [HttpGet("{gin}")]
        public string Get(uint gin)
        {
            return "value";
        }

        // GET api/<RecordsController>/2020-01-01
        [HttpGet("{checkDate}")]
        public string Get(DateTime checkDate)
        {
            return "value";
        }

        // POST api/<RecordsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<RecordsController>/5/2020-01-01
        [HttpPut("{id}/{checkDate}")]
        public void Put(uint ginNumber, DateTime checkDate, [FromBody] string value)
        {

        }

        // DELETE api/<RecordsController>/5
        [HttpDelete("{gin}")]
        public void Delete(uint gin)
        {

        }

        // DELETE api/<RecordsController>/5/2020-01-01
        [HttpDelete("{gin}/{checkDate}")]
        public void Delete(uint gin, DateTime checkDate)
        {

        }

    }
}
