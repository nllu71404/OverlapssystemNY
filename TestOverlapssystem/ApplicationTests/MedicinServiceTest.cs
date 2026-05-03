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

namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public class MedicinServiceTest
    {
        private Mock<IMedicinRepository> _medicinRepoMock;
        private Mock<ILogger<MedicinService>> _loggerMock;
        private MedicinService _service;

        [TestInitialize]
        public void Setup()
        {
            _medicinRepoMock = new Mock<IMedicinRepository>();
            _loggerMock = new Mock<ILogger<MedicinService>>();

            _service = new MedicinService(_medicinRepoMock.Object, _loggerMock.Object);
        }

       

        [TestMethod]
        public async Task GetMedicinByResidentIdAsync_WhenSuccess_ReturnsList()
        {
            // Arrange
            var list = new List<MedicinModel>
            {
                new() { MedicinTimeID = 1, ResidentID = 1 }
            };

            _medicinRepoMock
                .Setup(r => r.GetMedicinByResidentIdAsync(1))
                .ReturnsAsync(list);

            // Act
            var result = await _service.GetMedicinByResidentIdAsync(1);

            // Assert
            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
        }

        [TestMethod]
        public async Task GetMedicinByResidentIdAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.GetMedicinByResidentIdAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task GetMedicinByResidentIdAsync_WhenException_ReturnsTechnicalError()
        {
            _medicinRepoMock
                .Setup(r => r.GetMedicinByResidentIdAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _service.GetMedicinByResidentIdAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke hente medicintider", result.Error.Message);
        }

       

        [TestMethod]
        public async Task AddMedicinTimeAsync_WhenSuccess_ReturnsId()
        {
            var medicin = new MedicinModel { ResidentID = 1 };

            _medicinRepoMock
                .Setup(r => r.SaveNewMedicinAsync(medicin))
                .ReturnsAsync(5);

            var result = await _service.AddMedicinTimeAsync(medicin);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public async Task AddMedicinTimeAsync_WhenInvalidResidentId_ReturnsValidationError()
        {
            var medicin = new MedicinModel { ResidentID = 0 };

            var result = await _service.AddMedicinTimeAsync(medicin);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task AddMedicinTimeAsync_WhenException_ReturnsTechnicalError()
        {
            var medicin = new MedicinModel { ResidentID = 1 };

            _medicinRepoMock
                .Setup(r => r.SaveNewMedicinAsync(medicin))
                .ThrowsAsync(new Exception());

            var result = await _service.AddMedicinTimeAsync(medicin);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke oprette medicintid", result.Error.Message);
        }

       

        [TestMethod]
        public async Task DeleteMedicinAsync_WhenSuccess_ReturnsOk()
        {
            _medicinRepoMock
                .Setup(r => r.DeleteMedicinAsync(1))
                .Returns(Task.CompletedTask);

            var result = await _service.DeleteMedicinAsync(1);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task DeleteMedicinAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.DeleteMedicinAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt medicintid ID", result.Error.Message);
        }

        [TestMethod]
        public async Task DeleteMedicinAsync_WhenNotFound_ReturnsNotFoundError()
        {
            _medicinRepoMock
                .Setup(r => r.DeleteMedicinAsync(1))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _service.DeleteMedicinAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
            Assert.AreEqual("Kunne ikke finde medicintid at slette", result.Error.Message);
        }

        [TestMethod]
        public async Task DeleteMedicinAsync_WhenException_ReturnsTechnicalError()
        {
            _medicinRepoMock
                .Setup(r => r.DeleteMedicinAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _service.DeleteMedicinAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke slette medicintid", result.Error.Message);
        }

        // -------------------- UPDATE --------------------

        [TestMethod]
        public async Task UpdateMedicinAsync_WhenSuccess_ReturnsOk()
        {
            var medicin = new MedicinModel
            {
                ResidentID = 1,
                MedicinTimeID = 1
            };

            _medicinRepoMock
                .Setup(r => r.UpdateMedicinAsync(medicin))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateMedicinAsync(medicin);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task UpdateMedicinAsync_WhenInvalidResidentId_ReturnsValidationError()
        {
            var medicin = new MedicinModel
            {
                ResidentID = 0,
                MedicinTimeID = 1
            };

            var result = await _service.UpdateMedicinAsync(medicin);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdateMedicinAsync_WhenInvalidMedicinId_ReturnsValidationError()
        {
            var medicin = new MedicinModel
            {
                ResidentID = 1,
                MedicinTimeID = 0
            };

            var result = await _service.UpdateMedicinAsync(medicin);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdateMedicinAsync_WhenNotFound_ReturnsNotFoundError()
        {
            var medicin = new MedicinModel
            {
                ResidentID = 1,
                MedicinTimeID = 1
            };

            _medicinRepoMock
                .Setup(r => r.UpdateMedicinAsync(medicin))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _service.UpdateMedicinAsync(medicin);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
        }

        // -------------------- SET CHECKED --------------------

        [TestMethod]
        public async Task SetMedicinCheckedAsync_WhenSuccess_SetsCheckedTrue()
        {
            var medicin = new MedicinModel
            {
                MedicinTimeID = 1,
                ResidentID = 1,
                IsChecked = false
            };

            _medicinRepoMock
                .Setup(r => r.GetMedicinByIdAsync(1))
                .ReturnsAsync(medicin);

            var result = await _service.SetMedicinCheckedAsync(1, true);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(medicin.IsChecked);
            Assert.IsNotNull(medicin.MedicinCheckTimeStamp);
        }

        [TestMethod]
        public async Task SetMedicinCheckedAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.SetMedicinCheckedAsync(0, true);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task SetMedicinCheckedAsync_WhenException_ReturnsTechnicalError()
        {
            _medicinRepoMock
                .Setup(r => r.GetMedicinByIdAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _service.SetMedicinCheckedAsync(1, true);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
        }
    }
}

