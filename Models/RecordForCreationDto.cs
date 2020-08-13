using healthRecorder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Models
{
    /// <summary>
    /// The record for adding to database
    /// </summary>
    public class RecordForCreationDto
    {
        /// <summary>
        /// Checkdate of this **record**
        /// </summary>
        public DateTime CheckDate { get; set; }

        public bool? HasHighRiskRegionTravelHistory { get; set; }

        public bool? HasSymptoms { get; set; }

        public double? Temperature { get; set; }

        /// <summary>
        /// The id of associated employee
        /// </summary>
        public string EmployeeId { get; set; }
    }
}
