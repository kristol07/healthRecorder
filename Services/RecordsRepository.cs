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
        private readonly IMongoCollection<Employee> _employees;
        private readonly IMongoCollection<Record> _records;

        public RecordsRepository(IRecordsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _employees = database.GetCollection<Employee>(settings.EmployeesCollectionName);
            _records = database.GetCollection<Record>(settings.RecordsCollectionName);
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees.Find(new BsonDocument()).ToList();
        }

        public Employee GetEmployee(string employeeId)
        {
            var filter = Builders<Employee>.Filter.Eq("EmployeeId", employeeId);
            return _employees.Find(filter).First();
        }

        public IEnumerable<Record> GetAllRecords()
        {
            return _records.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<Record> GetRecordsForEmployee(string employeeId)
        {
            var filter = Builders<Record>.Filter.Eq("EmployeeId", employeeId);
            return _records.Find(filter).ToList();
        }

        public Record GetRecord(string employeeId, DateTime checkDate)
        {
            var records = GetRecordsForEmployee(employeeId);
            return records.ToList().Find(x => x.CheckDate == checkDate);
        }

        public void AddRecord(Record newRecord)
        {
            _records.InsertOne(newRecord);
        }

        public void DeleteRecord(string employeeId, DateTime checkDate)
        {
            var builder = Builders<Record>.Filter;
            var filter = builder.Eq("CheckDate", checkDate) & builder.Eq("EmployeeId", employeeId);
            _records.DeleteOne(filter);
        }

        public bool EmployeeExists(string employeedId)
        {
            var filter = Builders<Employee>.Filter.Eq("EmployeeId", employeedId);
            return _employees.Find(filter).Any();
        }

        public bool RecordExists(string employeeId, DateTime checkDate)
        {
            var builder = Builders<Record>.Filter;
            var filter = builder.Eq("CheckDate", checkDate) & builder.Eq("EmployeeId", employeeId);
            return _records.Find(filter).Any();
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
