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
    }
}
