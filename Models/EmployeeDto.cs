using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Models
{
    /// <summary>
    /// An employee with Id, GinNumber, Name and records fields
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// The id of the **employee**
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The GinNumber of the **employee**
        /// </summary>
        public uint GinNumber { get; set; }

        /// <summary>
        /// The Name of the **employee**
        /// </summary>
        public string Name { get; set; }
    }
}
