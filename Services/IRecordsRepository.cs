using healthRecorder.Entities;
using healthRecorder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Services
{
    public interface IRecordsRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Record> GetRecordsForEmployee(string employeeId);
        Record GetRecord(string employeeId, DateTime checkDate);

        void AddRecord(Record newRecord);
        void UpdateRecord(Record newRecord, string employeeId, DateTime checkDate);
        void DeleteRecord(string employeeId, DateTime checkDate);
        bool EmployeeExists(string employeedId);
        bool RecordExists(string employeeId, DateTime checkDate);
        bool Save();
    }
}
