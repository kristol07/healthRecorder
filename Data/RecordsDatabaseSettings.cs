using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Data
{
    public interface IRecordsDatabaseSettings
    {
        public string EmployeesCollectionName { get; set; }
        public string RecordsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class RecordsDatabaseSettings : IRecordsDatabaseSettings
    {
        public string EmployeesCollectionName { get; set; }
        public string RecordsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
