using System;
using Xunit;
using healthRecorder.Models;
namespace HealthRecorderUnitTest
{
    public class ModelTests
    {
        [Fact]
        public void EmployeeDtoTest()
        {
            var employeeDto = new EmployeeDto()
            {
                Id = "TestId",
                GinNumber = 101,
                Name = "TestName"
            };

            Assert.Equal("TestId", employeeDto.Id);
            Assert.Equal("101", employeeDto.GinNumber.ToString());
            Assert.Equal("TestName", employeeDto.Name);
        }

        [Fact]
        public void ErrorViewModelTest()
        {
            var errorViewModel = new ErrorViewModel();
            Assert.False(errorViewModel.ShowRequestId);

            errorViewModel.RequestId = "TestId";
            Assert.Equal("TestId", errorViewModel.RequestId);
            Assert.True(errorViewModel.ShowRequestId);
        }

        [Fact]
        public void RecordDtoTest()
        {
            var recordDto = new RecordDto()
            {
                Id = "TestId",
                CheckDate = new DateTime(2020,8,16),
                HasHighRiskRegionTravelHistory = true,
                HasSymptoms = true,
                Temperature = 36.5
            };
            
            Assert.Equal("TestId", recordDto.Id);
            Assert.Equal("2020/8/16", recordDto.CheckDate.ToShortDateString());
            Assert.True(recordDto.HasHighRiskRegionTravelHistory);
            Assert.True(recordDto.HasSymptoms);
            Assert.Equal("36.5", recordDto.Temperature.ToString());
        }

        [Fact]
        public void RecordForCreationDtoTest()
        {
            var recordForCreationDto = new RecordForCreationDto()
            {
                EmployeeId = "TestId",
                CheckDate = new DateTime(2020, 8, 16),
                HasHighRiskRegionTravelHistory = true,
                HasSymptoms = true,
                Temperature = 36.5
            };

            Assert.Equal("TestId", recordForCreationDto.EmployeeId);
            Assert.Equal("2020/8/16", recordForCreationDto.CheckDate.ToShortDateString());
            Assert.True(recordForCreationDto.HasHighRiskRegionTravelHistory);
            Assert.True(recordForCreationDto.HasSymptoms);
            Assert.Equal("36.5", recordForCreationDto.Temperature.ToString());
        }

        [Fact]
        public void SingleRecordDtoTest()
        {
            var singleRecordDto = new SingleRecordDto()
            {
                EmployeeId = "TestId"
            };

            Assert.Equal("TestId", singleRecordDto.EmployeeId);
        }
    }
}
