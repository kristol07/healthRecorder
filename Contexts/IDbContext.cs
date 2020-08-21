using DnsClient.Protocol;
using healthRecorder.Data;
using healthRecorder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Contexts
{
    public interface IDbContext
    {
        IEnumerable<Employee> Employees { get; }
        IEnumerable<Record> Records { get; }

        void AddRecord(Record newRecord);
        void DeleteRecord(string employeeId, DateTime checkDate);
        void DeleteRecord(string recordId);
    }
}
