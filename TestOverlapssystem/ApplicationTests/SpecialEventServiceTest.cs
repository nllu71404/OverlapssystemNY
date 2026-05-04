using Moq;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Services;
using Microsoft.Extensions.Logging;

namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public class SpecialEventServiceTest
    {
        private Mock<ISpecialEventRepository> _repoMock;
        private Mock<ILogger<SpecialEventService>> _loggerMock;
        private SpecialEventService _service;

        [TestInitialize]
        public void Setup()
        {
            _repoMock = new Mock<ISpecialEventRepository>();
            _loggerMock = new Mock<ILogger<SpecialEventService>>();

            _service = new SpecialEventService(_repoMock.Object, _loggerMock.Object);
        }

     

        [TestMethod]
        public async Task GetSpecialEventByResidentIdAsync_WhenSuccess_ReturnsFilteredList()
        {
            var now = DateTime.Now;

            var list = new List<SpecialEventModel>
            {
                new() { SpecialEventID = 1, ResidentID = 1, SpecialEventDateTime = now.AddHours(-1) },
                new() { SpecialEventID = 2, ResidentID = 1, SpecialEventDateTime = now.AddHours(-50) } // filtreres fra
            };

            _repoMock
                .Setup(r => r.GetSpecialEventByResidentId(1))
                .ReturnsAsync(list);

            var result = await _service.GetSpecialEventByResidentIdAsync(1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
            Assert.AreEqual(1, result.Value.First().SpecialEventID);
        }

        [TestMethod]
        public async Task GetSpecialEventByResidentIdAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.GetSpecialEventByResidentIdAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task GetSpecialEventByResidentIdAsync_WhenException_ReturnsTechnicalError()
        {
            _repoMock
                .Setup(r => r.GetSpecialEventByResidentId(1))
                .ThrowsAsync(new Exception());

            var result = await _service.GetSpecialEventByResidentIdAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke hente særlige hændelser", result.Error.Message);
        }

        [TestMethod]
        public async Task CreateSpecialEventAsync_WhenSuccess_ReturnsId()
        {
            var model = new SpecialEventModel { ResidentID = 1 };

            _repoMock
                .Setup(r => r.SaveNewSpecialEvent(model))
                .ReturnsAsync(7);

            var result = await _service.CreateSpecialEventAsync(model);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(7, result.Value);
        }

        [TestMethod]
        public async Task CreateSpecialEventAsync_WhenInvalidResidentId_ReturnsValidationError()
        {
            var model = new SpecialEventModel { ResidentID = 0 };

            var result = await _service.CreateSpecialEventAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task CreateSpecialEventAsync_WhenException_ReturnsTechnicalError()
        {
            var model = new SpecialEventModel { ResidentID = 1 };

            _repoMock
                .Setup(r => r.SaveNewSpecialEvent(model))
                .ThrowsAsync(new Exception());

            var result = await _service.CreateSpecialEventAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke oprette særlig hændelse", result.Error.Message);
        }


        [TestMethod]
        public async Task UpdateSpecialEventAsync_WhenSuccess_ReturnsOk()
        {
            var model = new SpecialEventModel
            {
                SpecialEventID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdateSpecialEvent(model))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateSpecialEventAsync(model);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task UpdateSpecialEventAsync_WhenInvalidId_ReturnsValidationError()
        {
            var model = new SpecialEventModel
            {
                SpecialEventID = 0,
                ResidentID = 1
            };

            var result = await _service.UpdateSpecialEventAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdateSpecialEventAsync_WhenInvalidResidentId_ReturnsValidationError()
        {
            var model = new SpecialEventModel
            {
                SpecialEventID = 1,
                ResidentID = 0
            };

            var result = await _service.UpdateSpecialEventAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdateSpecialEventAsync_WhenNotFound_ReturnsNotFoundError()
        {
            var model = new SpecialEventModel
            {
                SpecialEventID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdateSpecialEvent(model))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _service.UpdateSpecialEventAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
            Assert.AreEqual("Kunne ikke finde særlig hændelse at opdatere", result.Error.Message);
        }

        [TestMethod]
        public async Task UpdateSpecialEventAsync_WhenException_ReturnsTechnicalError()
        {
            var model = new SpecialEventModel
            {
                SpecialEventID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdateSpecialEvent(model))
                .ThrowsAsync(new Exception());

            var result = await _service.UpdateSpecialEventAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke opdatere særlig hændelse", result.Error.Message);
        }


        [TestMethod]
        public async Task DeleteSpecialEventAsync_WhenSuccess_ReturnsOk()
        {
            _repoMock
                .Setup(r => r.DeleteSpecialEvent(1))
                .Returns(Task.CompletedTask);

            var result = await _service.DeleteSpecialEventAsync(1);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task DeleteSpecialEventAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.DeleteSpecialEventAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt særlig hændelse ID", result.Error.Message);
        }

        [TestMethod]
        public async Task DeleteSpecialEventAsync_WhenNotFound_ReturnsNotFoundError()
        {
            _repoMock
                .Setup(r => r.DeleteSpecialEvent(1))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _service.DeleteSpecialEventAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
            Assert.AreEqual("Kunne ikke finde særlig hændelse at slette", result.Error.Message);
        }

        [TestMethod]
        public async Task DeleteSpecialEventAsync_WhenException_ReturnsTechnicalError()
        {
            _repoMock
                .Setup(r => r.DeleteSpecialEvent(1))
                .ThrowsAsync(new Exception());

            var result = await _service.DeleteSpecialEventAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke slette særlig hændelse", result.Error.Message);
        }
    }
}
