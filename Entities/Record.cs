using healthRecorder.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Entities
{
    public class Record
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        [BsonElement("CheckDate")]
        public DateTime CheckDate { get; set; }

        public bool? HasHighRiskRegionTravelHistory { get; set; }

        public bool? HasSymptoms { get; set; }

        public double? Temperature { get; set; }

        public string EmployeeId { get; set; }

        //[BsonIgnore]
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
