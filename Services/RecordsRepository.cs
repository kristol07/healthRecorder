using healthRecorder.Entities;
using healthRecorder.Models;
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
        private readonly IMongoCollection<Employee> _records;

        public RecordsRepository(IRecordsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _records = database.GetCollection<Employee>(settings.RecordsCollectionName);
        }

        public IEnumerable<Employee> GetAllRecords()
        {
            return _records.Find(new BsonDocument()).ToList();
        }

        public IEnumerable<Record> GetRecordsForEmployee(Guid employeeId)
        {
            var filter = Builders<Employee>.Filter.Eq("Id", employeeId);
            var projection = Builders<Employee>.Projection.Include("Records");
            var recordsDoc = _records.Find(filter).Project(projection).First();
            return BsonSerializer.Deserialize<IEnumerable<Record>>(recordsDoc);
        }

        public Record? GetRecord(Guid employeeId, DateTime checkDate)
        {
            var records = GetRecordsForEmployee(employeeId);
            return records.ToList().Find(x => x.CheckDate == checkDate);
        }

        public void AddRecord(Record newRecord)
        {
            throw new NotImplementedException();
        }

        public void DeleteRecord(Guid employeeId, DateTime checkDate)
        {
            throw new NotImplementedException();
        }

        public bool EmployeeExists(Guid employeedId)
        {
            throw new NotImplementedException();
        }

        public bool RecordExists(Guid employeeId, DateTime checkDate)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateRecord(Record newRecord, Guid employeeId, DateTime checkDate)
        {
            throw new NotImplementedException();
        }

    }
}
