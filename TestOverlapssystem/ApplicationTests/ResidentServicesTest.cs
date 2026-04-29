using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public sealed class ResidentServicesTest
    {
        private  Mock<IResidentRepository> _residentRepoMock;
        private  Mock<ILogger<ResidentServices>> _loggerMock;
        private  ResidentServices _service;

        // GLOBAL ARRANGE (kører før hver test)
        [TestInitialize]
        public void Setup()
        {
            //Opretter mock-repositories
            _residentRepoMock = new Mock<IResidentRepository>();
            _loggerMock = new Mock<ILogger<ResidentServices>>();

            //Opretter ResidentServices, med mocked afhængigheder
            _service = new ResidentServices(_residentRepoMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task LoadResidentsAsync_WhenSuccess_ReturnsResidents()
        {
            // Arrange
            var residents = new List<ResidentModel>
    {
        new() { ResidentId = 1, Name = "Ninna" }
    };

            _residentRepoMock
                .Setup(r => r.GetAllResidentsAsync())
                .ReturnsAsync(residents);

            // Act
            var result = await _service.LoadResidentsAsync();

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual("Ninna", result.Value[0].Name);
            CollectionAssert.AreEqual(result.Value, _service.Residents);
        }


        [TestMethod]
        public async Task LoadResidentsAsync_WhenException_ReturnsTechnicalError()
        {
            //Arrange
            _residentRepoMock.Setup(r => r.GetAllResidentsAsync())
                     .ThrowsAsync(new Exception("fejl"));

            //Act
            var result = await _service.LoadResidentsAsync();

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Fejl ved hentning af beboere", result.Error.Message);
        
        }

        [TestMethod]
        public async Task LoadResidentsByDepartmentAsync_WhenSuccess_ReturnsResidents()
        {
            //Arrange
            _residentRepoMock.Setup(r => r.GetResidentByDepartmentIdAsync(1))
                     .ReturnsAsync(new List<ResidentModel>
                     {
                 new() { ResidentId = 1 }
                     });

            //Act
            var result = await _service.LoadResidentsByDepartmentAsync(1);

            //Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
            
        }

        [TestMethod]
        public async Task LoadResidentsByDepartmentAsync_WhenInvalidId_ReturnsValidationError()
        {
            //Act
            var result = await _service.LoadResidentsByDepartmentAsync(0);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt afdelingsID", result.Error.Message);
        }

        [TestMethod]
        public async Task LoadResidentsByDepartmentAsync_WhenExeption_ReturnsTechnicalError()
        {

            //Arrange
            _residentRepoMock.Setup(r => r.GetResidentByDepartmentIdAsync(1))
                     .ThrowsAsync(new Exception("fejl"));

            //Act
            var result = await _service.LoadResidentsByDepartmentAsync(1);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Fejl ved hentning af beboere for afdeling", result.Error.Message);
        }

        [TestMethod]
        public async Task CreateResidentAsync_WhenSuccess_CreatesResidentAndReturnsId()
        {
            // Arrange
            var resident = new ResidentModel { DepartmentId = 2, Name = "Ny beboer" };

            _residentRepoMock
                .Setup(r => r.SaveNewResidentAsync(resident))
                .ReturnsAsync(10);

            // Act
            var result = await _service.CreateResidentAsync(resident);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(10, result.Value);
            
        }

        [TestMethod]
        public async Task CreateResidentAsync_WhenInvalidDepartment_ReturnsValidationError()
        {
            //Arrange
            var resident = new ResidentModel { DepartmentId = 0, Name = "Ny beboer" };

            //Act
            var result = await _service.CreateResidentAsync(resident);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Afdelings ID er ikke sat", result.Error.Message);
        }

        [TestMethod]
        public async Task UpdateResidentAsync_WhenSuccess_UpdatesResidentAndReturnsOk()
        {
            //Arrange
            var resident = new ResidentModel
            {
                ResidentId = 1,
                Name = "Ninna"
            };

            _residentRepoMock.Setup(r => r.UpdateResidentAsync(resident))
                     .Returns(Task.CompletedTask);
            //Act
            var result = await _service.UpdateResidentAsync(resident);

            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task UpdateResidentAsync_WhenInvalidId_ReturnsValidationError()
        {
            //Arrange
            var resident = new ResidentModel
            {
                ResidentId = 0,
                Name = "Ninna"
            };

            //Act
            var result = await _service.UpdateResidentAsync(resident);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task UpdateResidentAsync_WhenEmptyName_ReturnsValidationError()
        {
            //Arrange
            var resident = new ResidentModel
            {
                ResidentId = 1,
                Name = ""
            };

            //Act
            var result = await _service.UpdateResidentAsync(resident);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Navn er påkrævet", result.Error.Message);
        }

        [TestMethod]
        public async Task UpdateResidentAsync_WhenNotFoundException_ReturnsNotFoundError()
        {
            //Arrange
            var resident = new ResidentModel
            {
                ResidentId = 1,
                Name = "Ninna"
            };

            _residentRepoMock.Setup(r => r.UpdateResidentAsync(resident))
                     .ThrowsAsync(new KeyNotFoundException());

            //Act
            var result = await _service.UpdateResidentAsync(resident);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
            Assert.AreEqual("Kunne ikke finde beboer at opdatere", result.Error.Message);
        }

        [TestMethod]
        public async Task DeleteResidentAsync_WhenSuccess_ReturnsOk()
        {
            //Arrange
            _residentRepoMock.Setup(r => r.DeleteResidentAsync(1))
                     .Returns(Task.CompletedTask);

            //Act
            var result = await _service.DeleteResidentAsync(1);

            //Assert
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task DeleteResidentAsync_WhenInvalidId_ReturnsValidationError()
        {
            //Act
            var result = await _service.DeleteResidentAsync(0);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task DeleteResidentAsync_WhenNotFoundException_ReturnsNotFoundError()
        {
            
            //Arrange
            _residentRepoMock.Setup(r => r.DeleteResidentAsync(1))
                     .ThrowsAsync(new KeyNotFoundException());

            //Act
            var result = await _service.DeleteResidentAsync(1);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
            Assert.AreEqual("Kunne ikke finde beboer at slette", result.Error.Message);

        }

        [TestMethod]
        public async Task DeleteResidentAsync_WhenException_ReturnsTechnicalError()
        {

            //Arrange
            _residentRepoMock.Setup(r => r.DeleteResidentAsync(1))
                     .ThrowsAsync(new Exception());

            //Act
            var result = await _service.DeleteResidentAsync(1);

            //Assert
            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke slette beboer", result.Error.Message);
        }

        [TestMethod]
        public void SetDepartment_WhenSuccess_SetsValues()
        {
            _service.SetDepartment(5);

            Assert.AreEqual(5, _service.SelectedDepartmentId);
            Assert.AreEqual(5, _service.NewResident.DepartmentId);
        }

 


    }
}


