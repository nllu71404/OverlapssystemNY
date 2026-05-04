using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Services;
using Microsoft.Extensions.Logging;

namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public class EmployeePhoneServiceTest
    {
        private Mock<IEmployeePhoneRepository> _employeePhoneRepositoryMock;
        private Mock<ILogger<EmployeePhoneService>> _loggerMock;
        private EmployeePhoneService _employeePhoneService;

        [TestInitialize]
        public void Setup()
        {
            _employeePhoneRepositoryMock = new Mock<IEmployeePhoneRepository>();
            _loggerMock = new Mock<ILogger<EmployeePhoneService>>();

            _employeePhoneService = new EmployeePhoneService(_employeePhoneRepositoryMock.Object, _loggerMock.Object);
        }

        //GetEmployeePhoneByDepartmentID
        [TestMethod]
        public async Task GetEmployeePhoneByDepartmentIdAsync_WhenSuccess_ReturnsList()
        {
            //Arrange
            var list = new List<EmployeePhoneModel>
            {
                new() { EmployeePhoneID = 1, DepartmentID = 1 }
            };

            _employeePhoneRepositoryMock
                .Setup(r => r.GetEmployeePhonesByDepartmentIdAsync(1))
                .ReturnsAsync(list);

            //Act
            var result = await _employeePhoneService.GetEmployeePhonesByDepartmentIdAsync(1);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
        }
        
        [TestMethod]
        public async Task GetEmployeePhoneByDepartmentIdAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _employeePhoneService.GetEmployeePhonesByDepartmentIdAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt afdelings ID", result.Error.Message);
        }
        
        [TestMethod]
        public async Task GetEmployeePhoneByDepartmentIdAsync_WhenException_ReturnsTechnicalError()
        {
            _employeePhoneRepositoryMock
                .Setup(r => r.GetEmployeePhonesByDepartmentIdAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _employeePhoneService.GetEmployeePhonesByDepartmentIdAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke hendes telefonnumre for afdeling", result.Error.Message);
        }

        //GetAllEmployeePhone
        [TestMethod]
        public async Task GetAllEmployeePhoneNumbersAsync_WhenSuccess_ReturnsList()
        {
            //Arrange
            var list = new List<EmployeePhoneModel>
            {
                new() { EmployeePhoneID = 1}
            };

            _employeePhoneRepositoryMock
                .Setup(r => r.GetAllEmployeePhoneNumbersAsync())
                .ReturnsAsync(list);

            //Act
            var result = await _employeePhoneService.GetAllEmployeePhoneNumbersAsync();

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
        }

        [TestMethod]
        public async Task GetAllEmployeePhoneNumbersAsync_WhenException_ReturnsTechnicalError()
        {
            _employeePhoneRepositoryMock
                .Setup(r => r.GetAllEmployeePhoneNumbersAsync())
                .ThrowsAsync(new Exception());

            var result = await _employeePhoneService.GetAllEmployeePhoneNumbersAsync();

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke hente telefonnummer", result.Error.Message);
        }

        //GetEmployeePhoneById
        [TestMethod]
        public async Task GetEmployeePhoneByIdAsync_WhenSuccess_ReturnsPhone()
        {
            //Arrange
            var phone = new EmployeePhoneModel { EmployeePhoneID = 1 };

            _employeePhoneRepositoryMock
                .Setup(r => r.GetEmployeePhoneByIdAsync(1))
                .ReturnsAsync(phone);

            //Act
            var result = await _employeePhoneService.GetEmployeePhoneByIdAsync(1);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.EmployeePhoneID);
        }
        [TestMethod]
        public async Task GetEmployeePhoneByIdAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _employeePhoneService.GetEmployeePhoneByIdAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt telefonnummer med det ID", result.Error.Message);
        }
        [TestMethod]
        public async Task GetEmployeePhoneByIdAsync_WhenException_ReturnsTechnicalError()
        {
            _employeePhoneRepositoryMock
                .Setup(r => r.GetEmployeePhoneByIdAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _employeePhoneService.GetEmployeePhoneByIdAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke hente medarbejdertelefon", result.Error.Message);
        }

        //AddEmployeePhone/SaveNewEmployeePhone
        [TestMethod]
        public async Task AddEmployeePhone_WhenSuccess_ReturnsId()
        {
            var phone = new EmployeePhoneModel { DepartmentID = 1 };

            _employeePhoneRepositoryMock
                .Setup(r => r.SaveNewEmployeePhoneAsync(phone))
                .ReturnsAsync(5);

            var result = await _employeePhoneService.SaveNewEmployeePhoneAsync(phone);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(5, result.Value);
        }
        [TestMethod]
        public async Task AddEmployeePhone_WhenNull_ReturnsValidationError()
        {
            var result = await _employeePhoneService.SaveNewEmployeePhoneAsync(null);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Telefonnumret må ikke være nul", result.Error.Message);
        }

        [TestMethod]
        public async Task AddEmployeePhone_WhenException_ReturnsTechnicalError()
        {
            var phone = new EmployeePhoneModel { DepartmentID = 1 };

            _employeePhoneRepositoryMock
                .Setup(r => r.SaveNewEmployeePhoneAsync(phone))
                .ThrowsAsync(new Exception());

            var result = await _employeePhoneService.SaveNewEmployeePhoneAsync(phone);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Fejl ved oprettelse af medarbejder telefon!", result.Error.Message);
        }

        //UpdateEmployeePhone
        public async Task UpdateEmployeePhone_WhenSuccess_ReturnsOk()
        {
            var phone = new EmployeePhoneModel
            {
                EmployeePhoneID = 1,
                DepartmentID = 1
            };

            _employeePhoneRepositoryMock
                .Setup(r => r.UpdateEmployeePhoneAsync(phone))
                .Returns(Task.CompletedTask);

            var result = await _employeePhoneService.UpdateEmployeePhoneAsync(phone);

            Assert.IsTrue(result.Success);
        }
        [TestMethod]
        public async Task UpdateEmployeePhone_WhenException_ReturnsTechnicalError()
        {
            var phone = new EmployeePhoneModel
            {
                EmployeePhoneID = 1,
                DepartmentID = 1
            };

            _employeePhoneRepositoryMock
                .Setup(r => r.UpdateEmployeePhoneAsync(phone))
                .ThrowsAsync(new Exception());

            var result = await _employeePhoneService.UpdateEmployeePhoneAsync(phone);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Fejl ved opdatering af medarbejder telefon", result.Error.Message);
        }

        //DeleteEmployeePhone
        [TestMethod]
        public async Task DeleteEmployeePhone_WhenSuccess_ReturnsOk()
        {
            _employeePhoneRepositoryMock
                .Setup(r => r.DeleteEmployeePhoneAsync(1))
                .Returns(Task.CompletedTask);

            var result = await _employeePhoneService.DeleteEmployeePhoneAsync(1);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task DeleteEmployeePhone_WhenException_ReturnsTechnicalError()
        {
            _employeePhoneRepositoryMock
                .Setup(r => r.DeleteEmployeePhoneAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _employeePhoneService.DeleteEmployeePhoneAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Fejl ved sletning af medarbejder telefon!", result.Error.Message);
        }


    }
}
