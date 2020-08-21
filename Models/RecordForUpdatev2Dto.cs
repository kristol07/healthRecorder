using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Models
{
    public class RecordForUpdatev2Dto
    {
        public bool? HasHighRiskRegionTravelHistory { get; set; }

        public bool? HasSymptoms { get; set; }

        [Required(ErrorMessage = "Temperature input is required.")]
        [Range(35, 45, ErrorMessage = "Invalid temperature value. 35\"C-45\"C")]
        public double? Temperature { get; set; }

    }
}
