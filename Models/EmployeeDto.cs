using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Models
{
    public class EmployeeDto
    {
        public uint GinNumber { get; set; }

        public string Name { get; set; }

        public ICollection<RecordDto> Records { get; set; }
            = new List<RecordDto>();
    }
}
