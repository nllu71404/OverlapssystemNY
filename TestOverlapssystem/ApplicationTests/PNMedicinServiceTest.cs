using Moq;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Services;
using Microsoft.Extensions.Logging;

namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public class PNMedicinServiceTest
    {
        private Mock<IPNMedicinRepository> _repoMock;
        private Mock<ILogger<PNMedicinService>> _loggerMock;
        private PNMedicinService _service;

        [TestInitialize]
        public void Setup()
        {
            _repoMock = new Mock<IPNMedicinRepository>();
            _loggerMock = new Mock<ILogger<PNMedicinService>>();

            _service = new PNMedicinService(_repoMock.Object, _loggerMock.Object);
        }

       

        [TestMethod]
        public async Task GetPNMedicinByResidentIdAsync_WhenSuccess_ReturnsFilteredList()
        {
            // Arrange
            var now = DateTime.Now;

            var list = new List<PNMedicinModel>
            {
                new() { PNMedicinID = 1, ResidentID = 1, PNTime = now.AddHours(-1) },
                new() { PNMedicinID = 2, ResidentID = 1, PNTime = now.AddHours(-50) } // skal filtreres fra
            };

            _repoMock
                .Setup(r => r.GetPNMedicinByResidentIdAsync(1))
                .ReturnsAsync(list);

            // Act
            var result = await _service.GetPNMedicinByResidentIdAsync(1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual(1, result.Value.First().PNMedicinID);
        }

        [TestMethod]
        public async Task GetPNMedicinByResidentIdAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.GetPNMedicinByResidentIdAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task GetPNMedicinByResidentIdAsync_WhenException_ReturnsTechnicalError()
        {
            _repoMock
                .Setup(r => r.GetPNMedicinByResidentIdAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _service.GetPNMedicinByResidentIdAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke hente PN medicintider", result.Error.Message);
        }


        [TestMethod]
        public async Task CreatePNMedicinAsync_WhenSuccess_ReturnsId()
        {
            var model = new PNMedicinModel { ResidentID = 1 };

            _repoMock
                .Setup(r => r.SaveNewPNMedicinAsync(model))
                .ReturnsAsync(10);

            var result = await _service.CreatePNMedicinAsync(model);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(10, result.Value);
        }

        [TestMethod]
        public async Task CreatePNMedicinAsync_WhenInvalidResidentId_ReturnsValidationError()
        {
            var model = new PNMedicinModel { ResidentID = 0 };

            var result = await _service.CreatePNMedicinAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task CreatePNMedicinAsync_WhenException_ReturnsTechnicalError()
        {
            var model = new PNMedicinModel { ResidentID = 1 };

            _repoMock
                .Setup(r => r.SaveNewPNMedicinAsync(model))
                .ThrowsAsync(new Exception());

            var result = await _service.CreatePNMedicinAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke oprette PN medicintid", result.Error.Message);
        }

      

        [TestMethod]
        public async Task DeletePNMedicinAsync_WhenSuccess_ReturnsOk()
        {
            _repoMock
                .Setup(r => r.DeletePNMedicinAsync(1))
                .Returns(Task.CompletedTask);

            var result = await _service.DeletePNMedicinAsync(1);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task DeletePNMedicinAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.DeletePNMedicinAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt PN medicintid ID", result.Error.Message);
        }

        [TestMethod]
        public async Task DeletePNMedicinAsync_WhenNotFound_ReturnsNotFoundError()
        {
            _repoMock
                .Setup(r => r.DeletePNMedicinAsync(1))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _service.DeletePNMedicinAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
            Assert.AreEqual("Kunne ikke finde PN Medicintid at slette", result.Error.Message);
        }

        [TestMethod]
        public async Task DeletePNMedicinAsync_WhenException_ReturnsTechnicalError()
        {
            _repoMock
                .Setup(r => r.DeletePNMedicinAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _service.DeletePNMedicinAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke slette PN medicintid", result.Error.Message);
        }

     

        [TestMethod]
        public async Task UpdatePNMedicinAsync_WhenSuccess_ReturnsOk()
        {
            var model = new PNMedicinModel
            {
                PNMedicinID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdatePNMedicinAsync(model))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdatePNMedicinAsync(model);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task UpdatePNMedicinAsync_WhenInvalidId_ReturnsValidationError()
        {
            var model = new PNMedicinModel
            {
                PNMedicinID = 0,
                ResidentID = 1
            };

            var result = await _service.UpdatePNMedicinAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdatePNMedicinAsync_WhenInvalidResidentId_ReturnsValidationError()
        {
            var model = new PNMedicinModel
            {
                PNMedicinID = 1,
                ResidentID = 0
            };

            var result = await _service.UpdatePNMedicinAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdatePNMedicinAsync_WhenNotFound_ReturnsNotFoundError()
        {
            var model = new PNMedicinModel
            {
                PNMedicinID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdatePNMedicinAsync(model))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _service.UpdatePNMedicinAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdatePNMedicinAsync_WhenException_ReturnsTechnicalError()
        {
            var model = new PNMedicinModel
            {
                PNMedicinID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdatePNMedicinAsync(model))
                .ThrowsAsync(new Exception());

            var result = await _service.UpdatePNMedicinAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke opdatere PN medicin", result.Error.Message);
        }
    }
}
