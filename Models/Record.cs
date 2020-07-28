using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Models
{
    public class Record
    {
        public Guid Id { get; set; }

        public Identity Identity { get; set; }

        [Required]
        public DateTime CheckDate { get; set; }

        public HealthInfo HealthInfo { get; set; }
    }
}
