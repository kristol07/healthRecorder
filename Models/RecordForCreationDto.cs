using healthRecorder.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Models
{
    /// <summary>
    /// The record for adding to database
    /// </summary>
    public class RecordForCreationDto : RecordForUpdateDto
    {
        /// <summary>
        /// Checkdate of this **record**
        /// </summary>
        [Required(ErrorMessage = "CheckDate input is required.")]
        //[DataType(DataType.Date, ErrorMessage ="Invalid date value.")]
        public DateTime CheckDate { get; set; }

        [DefaultValue(false)]
        public bool? HasHighRiskRegionTravelHistory { get; set; }

        [DefaultValue(false)]
        public bool? HasSymptoms { get; set; }

        /// <summary>
        /// The id of associated employee
        /// </summary>
        [Required(ErrorMessage = "EmployeeId input is required.")]
        public string EmployeeId { get; set; }
    }
}
