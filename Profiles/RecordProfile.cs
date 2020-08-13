using AutoMapper;
using healthRecorder.Entities;
using healthRecorder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace healthRecorder.Data
{
    public class RecordProfile: Profile
    {
        public RecordProfile()
        {
            CreateMap<Record, RecordDto>();

            CreateMap<RecordForCreationDto, Record>();

            CreateMap<Record, SingleRecordDto>();
        }
    }
}
