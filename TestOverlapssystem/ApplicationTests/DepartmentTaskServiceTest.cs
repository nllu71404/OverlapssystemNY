using Moq;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public class DepartmentTaskServiceTest
    {
        private Mock<IDepartmentTaskRepository> _departmentTaskRepoMock;
        private DepartmentTaskService _service;

        [TestInitialize]
        public void Setup()
        {
            _departmentTaskRepoMock = new Mock<IDepartmentTaskRepository>();
            _service = new DepartmentTaskService(_departmentTaskRepoMock.Object);
        }

        // Test for GetAllDepartmentTasksAsync - returns tasks
        [TestMethod]
        public async Task GetAllDepartmentTasksAsync_ReturnsTasks()
        {
            _departmentTaskRepoMock.Setup(r => r.GetAllDepartmentTasksAsync())
                     .ReturnsAsync(new List<DepartmentTaskModel>
                     {
                     new DepartmentTaskModel { DepartmentTaskID = 1 }
                     });

            var result = await _service.GetAllDepartmentTasksAsync();

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
        }

        // Test for GetDepartmentTaskByIdAsync - invalid ID
        [TestMethod]
        public async Task GetDepartmentTaskByIdAsync_InvalidId_ReturnsValidationError()
        {
            var result = await _service.GetDepartmentTaskByIdAsync(0);

            Assert.IsFalse(result.Success);
        }

        // Test for CreateDepartmentTaskAsync - valid task
        [TestMethod]
        public async Task CreateDepartmentTaskAsync_ValidTask_ReturnsCreatedTask()
        {
            var task = new DepartmentTaskModel();

            _departmentTaskRepoMock.Setup(r => r.SaveNewDepartmentTaskAsync(task))
                     .ReturnsAsync(5);

            var result = await _service.SaveNewDepartmentTaskAsync(task);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(5, result.Value);
        }

        // Test for GetTimeOfDayAsync - returns valid shift
        [TestMethod]
        public async Task GetTimeOfDayAsync_ReturnsValidShift()
        {
            var result = await _service.GetTimeOfDayAsync();

            Assert.IsTrue(result.Success);

            // Vi ved ikke præcis hvilken, men den skal være en af dem
            Assert.IsTrue(
                result.Value.ToString() == "Dag" ||
                result.Value.ToString() == "Aften" ||
                result.Value.ToString() == "Nat"
            );
        }

        // Test for DeleteDepartmentTaskAsync - exception
        [TestMethod]
        public async Task DeleteDepartmentTaskAsync_Exception_ReturnsFailure()
        {
            _departmentTaskRepoMock.Setup(r => r.DeleteDepartmentTaskAsync(1))
                 .ThrowsAsync(new System.Exception());

            var result = await _service.DeleteDepartmentTaskAsync(1);

            Assert.IsFalse(result.Success);
        }

    }
}
