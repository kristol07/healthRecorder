using healthRecorder.Contexts;
using healthRecorder.Data;
using healthRecorder.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Services
{
    public class RecordsRepository : IRecordsRepository
    {
        private IDbContext _context;

        public RecordsRepository(IDbContext dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees;
        }

        public Employee GetEmployee(string employeeId)
        {
            return _context.Employees.First(e => e.Id == employeeId);
        }

        public IEnumerable<Record> GetAllRecords()
        {
            return _context.Records;
        }

        public IEnumerable<Record> GetRecordsForEmployee(string employeeId)
        {
            return _context.Records.Where(x => x.EmployeeId == employeeId);
        }

        public Record GetRecord(string employeeId, DateTime checkDate)
        {
            return _context.Records.First(x => x.EmployeeId == employeeId && x.CheckDate.Date == checkDate.Date);
        }

        public void AddRecord(Record newRecord)
        {
            _context.AddRecord(newRecord);
        }

        public void DeleteRecord(string employeeId, DateTime checkDate)
        {
            if (RecordExists(employeeId, checkDate))
            {
                _context.DeleteRecord(employeeId, checkDate);
            }
        }

        public bool EmployeeExists(string employeedId)
        {
            return _context.Employees.Any(x => x.Id == employeedId);
        }

        public bool RecordExists(string employeeId, DateTime checkDate)
        {
            return _context.Records.Any(x => x.EmployeeId == employeeId && x.CheckDate.Date == checkDate.Date);
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(Record newRecord, string employeeId, DateTime checkDate)
        {
            if (RecordExists(employeeId, checkDate))
            {
                DeleteRecord(employeeId, checkDate);
                AddRecord(newRecord);
            }
        }

    }
}
