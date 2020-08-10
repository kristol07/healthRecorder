using healthRecorder.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Entities
{
    public class Employee
    {
        [Key]
        [BsonId]
        [BsonElement("Id")]
        public Guid Id { get; set; }

        [BsonElement("GinNumber")]
        [BsonRepresentation(BsonType.Int32)]
        [Required]
        public uint GinNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [BsonElement("Records")]
        public ICollection<Record> Records { get; set; }
            = new List<Record>();
    }
}
