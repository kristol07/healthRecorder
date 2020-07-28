using healthRecorder.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Services
{
    public interface IRecordsService
    {
        Task<List<Record>> GetAllRecordsAsync();
    }

    public class RecordsService : IRecordsService
    {
        private readonly IMongoCollection<Record> _records;

        public RecordsService(IRecordsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _records = database.GetCollection<Record>(settings.RecordsCollectionName);
        }

        public Task<List<Record>> GetAllRecordsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
