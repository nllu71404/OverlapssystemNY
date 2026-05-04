using Moq;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Common.Errors;
using OverlapssytemApplication.Services;
using Microsoft.Extensions.Logging;

namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public class ShoppingServiceTest
    {
        private Mock<IShoppingRepository> _repoMock;
        private Mock<ILogger<ShoppingService>> _loggerMock;
        private ShoppingService _service;

        [TestInitialize]
        public void Setup()
        {
            _repoMock = new Mock<IShoppingRepository>();
            _loggerMock = new Mock<ILogger<ShoppingService>>();

            _service = new ShoppingService(_repoMock.Object, _loggerMock.Object);
        }



        [TestMethod]
        public async Task GetShoppingByResidentIdAsync_WhenSuccess_ReturnsList()
        {
            var list = new List<ShoppingModel>
            {
                new() { ShoppingID = 1, ResidentID = 1 }
            };

            _repoMock
                .Setup(r => r.GetShoppingByResidentIdAsync(1))
                .ReturnsAsync(list);

            var result = await _service.GetShoppingByResidentIdAsync(1);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(1, result.Value.Count);
        }

        [TestMethod]
        public async Task GetShoppingByResidentIdAsync_WhenNullFromRepo_ReturnsEmptyList()
        {
            _repoMock
                .Setup(r => r.GetShoppingByResidentIdAsync(1))
                .ReturnsAsync((List<ShoppingModel>)null);

            var result = await _service.GetShoppingByResidentIdAsync(1);

            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(0, result.Value.Count);
        }

        [TestMethod]
        public async Task GetShoppingByResidentIdAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.GetShoppingByResidentIdAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task GetShoppingByResidentIdAsync_WhenException_ReturnsTechnicalError()
        {
            _repoMock
                .Setup(r => r.GetShoppingByResidentIdAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _service.GetShoppingByResidentIdAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke hente handledage", result.Error.Message);
        }


        [TestMethod]
        public async Task CreateShoppingAsync_WhenSuccess_ReturnsId()
        {
            var model = new ShoppingModel { ResidentID = 1 };

            _repoMock
                .Setup(r => r.SaveNewShoppingAsync(model))
                .ReturnsAsync(5);

            var result = await _service.CreateShoppingAsync(model);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(5, result.Value);
        }

        [TestMethod]
        public async Task CreateShoppingAsync_WhenInvalidResidentId_ReturnsValidationError()
        {
            var model = new ShoppingModel { ResidentID = 0 };

            var result = await _service.CreateShoppingAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt beboer ID", result.Error.Message);
        }

        [TestMethod]
        public async Task CreateShoppingAsync_WhenException_ReturnsTechnicalError()
        {
            var model = new ShoppingModel { ResidentID = 1 };

            _repoMock
                .Setup(r => r.SaveNewShoppingAsync(model))
                .ThrowsAsync(new Exception());

            var result = await _service.CreateShoppingAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke oprette handledag", result.Error.Message);
        }

  

        [TestMethod]
        public async Task DeleteShoppingAsync_WhenSuccess_ReturnsOk()
        {
            _repoMock
                .Setup(r => r.DeleteShoppingAsync(1))
                .Returns(Task.CompletedTask);

            var result = await _service.DeleteShoppingAsync(1);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task DeleteShoppingAsync_WhenInvalidId_ReturnsValidationError()
        {
            var result = await _service.DeleteShoppingAsync(0);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
            Assert.AreEqual("Ugyldigt handledag ID", result.Error.Message);
        }

        [TestMethod]
        public async Task DeleteShoppingAsync_WhenNotFound_ReturnsNotFoundError()
        {
            _repoMock
                .Setup(r => r.DeleteShoppingAsync(1))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _service.DeleteShoppingAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
            Assert.AreEqual("Kunne ikke finde handledag at slette", result.Error.Message);
        }

        [TestMethod]
        public async Task DeleteShoppingAsync_WhenException_ReturnsTechnicalError()
        {
            _repoMock
                .Setup(r => r.DeleteShoppingAsync(1))
                .ThrowsAsync(new Exception());

            var result = await _service.DeleteShoppingAsync(1);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke slette handledag", result.Error.Message);
        }

   

        [TestMethod]
        public async Task UpdateShoppingAsync_WhenSuccess_ReturnsOk()
        {
            var model = new ShoppingModel
            {
                ShoppingID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdateShoppingAsync(model))
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateShoppingAsync(model);

            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public async Task UpdateShoppingAsync_WhenInvalidShoppingId_ReturnsValidationError()
        {
            var model = new ShoppingModel
            {
                ShoppingID = 0,
                ResidentID = 1
            };

            var result = await _service.UpdateShoppingAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdateShoppingAsync_WhenInvalidResidentId_ReturnsValidationError()
        {
            var model = new ShoppingModel
            {
                ShoppingID = 1,
                ResidentID = 0
            };

            var result = await _service.UpdateShoppingAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Validation, result.Error.Type);
        }

        [TestMethod]
        public async Task UpdateShoppingAsync_WhenNotFound_ReturnsNotFoundError()
        {
            var model = new ShoppingModel
            {
                ShoppingID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdateShoppingAsync(model))
                .ThrowsAsync(new KeyNotFoundException());

            var result = await _service.UpdateShoppingAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.NotFound, result.Error.Type);
            Assert.AreEqual("Kunne ikke finde handledag at opdatere", result.Error.Message);
        }

        [TestMethod]
        public async Task UpdateShoppingAsync_WhenException_ReturnsTechnicalError()
        {
            var model = new ShoppingModel
            {
                ShoppingID = 1,
                ResidentID = 1
            };

            _repoMock
                .Setup(r => r.UpdateShoppingAsync(model))
                .ThrowsAsync(new Exception());

            var result = await _service.UpdateShoppingAsync(model);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(ErrorType.Technical, result.Error.Type);
            Assert.AreEqual("Kunne ikke opdatere handledag", result.Error.Message);
        }
    }
}
