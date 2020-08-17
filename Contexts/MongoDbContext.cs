using DnsClient.Protocol;
using healthRecorder.Data;
using healthRecorder.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Contexts
{
    public class MongoDbContext : IDbContext
    {
        public IEnumerable<Employee> Employees
        {
            get
            {
                return _employeesCollect.Find(new BsonDocument()).ToList();
            }
        }
        public IEnumerable<Record> Records
        {
            get
            {
                return _recordsCollect.Find(new BsonDocument()).ToList();
            }
        }

        private IMongoCollection<Record> _recordsCollect;
        private IMongoCollection<Employee> _employeesCollect;

        public MongoDbContext(IRecordsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _employeesCollect = database.GetCollection<Employee>(settings.EmployeesCollectionName);
            _recordsCollect = database.GetCollection<Record>(settings.RecordsCollectionName);
        }

        public void AddRecord(Record newRecord)
        {
            _recordsCollect.InsertOne(newRecord);
        }

        public void DeleteRecord(string employeeId, DateTime checkDate)
        {
            var builder = Builders<Record>.Filter;
            var filter = builder.Eq("CheckDate", checkDate) & builder.Eq("EmployeeId", employeeId);
            _recordsCollect.DeleteOne(filter);
        }
    }
}
