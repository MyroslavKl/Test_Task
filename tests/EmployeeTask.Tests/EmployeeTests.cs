using EmployeeTask.API.Models;
using EmployeeTask.API.Repositories.Interfaces;
using EmployeeTask.API.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EmployeeTask.Tests;

public class EmployeeTests
{
    [Fact]
    public async Task GetEmployeeByID_ShouldReturnCorrectHierarchy()
    {
        var mockRepo = new Mock<IEmployeeRepository>();
        var flatData = new List<EmployeeModel>
        {
            new() { Id = 1, Name = "Root", ManagerId = null },
            new() { Id = 2, Name = "Child", ManagerId = 1 }
        };

        mockRepo.Setup(r => r.GetRawData(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(flatData);

        var service = new EmployeeService(mockRepo.Object, Mock.Of<ILogger<EmployeeService>>());

        var result = await service.GetEmployeeByID(1, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
        Assert.Single(result.Employees);
        Assert.Equal(2, result.Employees[0].Id);
    }

    [Fact]
    public async Task EnableEmployee_ShouldCallRepository_AndReturnTrue()
    {
        var mockRepo = new Mock<IEmployeeRepository>();

        mockRepo.Setup(r => r.EnableEmployee(3, true, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

        var service = new EmployeeService(mockRepo.Object, Mock.Of<ILogger<EmployeeService>>());

        var result = await service.EnableEmployee(3, true, CancellationToken.None);

        Assert.True(result);

        mockRepo.Verify(r => r.EnableEmployee(3, true, It.IsAny<CancellationToken>()), Times.Once);
    }
}