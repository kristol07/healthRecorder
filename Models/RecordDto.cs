using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Models
{
    public class RecordDto
    {
        public string Id { get; set; }

        public DateTime CheckDate { get; set; }

        public bool? HasHighRiskRegionTravelHistory { get; set; }

        public bool? HasSymptoms { get; set; }

        public double? Temperature { get; set; }
    }
}
