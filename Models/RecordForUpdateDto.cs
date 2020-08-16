using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Models
{
    public class RecordForUpdateDto
    {
        public bool? HasHighRiskRegionTravelHistory { get; set; }

        public bool? HasSymptoms { get; set; }

        [Required]
        public double? Temperature { get; set; }
    }
}
