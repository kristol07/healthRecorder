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

        IEnumerable<Record> GetAllRecords();
        IEnumerable<Record> GetRecordsForEmployee(string employeeId);
        Record GetRecord(string employeeId, DateTime checkDate);

        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployee(string employeeId);

        void AddRecord(Record newRecord);
        void UpdateRecord(Record newRecord, string employeeId, DateTime checkDate);
        void DeleteRecord(string employeeId, DateTime checkDate);
        bool EmployeeExists(string employeedId);
        bool RecordExists(string employeeId, DateTime checkDate);
        bool Save();
    }

    public interface IRecordsRepositoryAsync
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<IEnumerable<Record>> GetRecordsForEmployeeAsync(string employeeId);
        Task<Record> GetRecordAsync(string employeeId, DateTime checkDate);

        Task AddRecordAsync(Record newRecord);
        Task UpdateRecordAsync(Record newRecord, string employeeId, DateTime checkDate);
        Task DeleteRecordAsync(string employeeId, DateTime checkDate);
        Task EmployeeExistsAsync(string employeedId);
        Task RecordExistsAsync(string employeeId, DateTime checkDate);
        Task SaveAsync();
    }
}
