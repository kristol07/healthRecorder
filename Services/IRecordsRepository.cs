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
        IEnumerable<Employee> GetAllRecords();
        IEnumerable<Record> GetRecordsForEmployee(Guid employeeId);
        Record GetRecord(Guid employeeId, DateTime checkDate);

        void AddRecord(Record newRecord);
        void UpdateRecord(Record newRecord, Guid employeeId, DateTime checkDate);
        void DeleteRecord(Guid employeeId, DateTime checkDate);
        bool EmployeeExists(Guid employeedId);
        bool RecordExists(Guid employeeId, DateTime checkDate);
        bool Save();
    }
}
