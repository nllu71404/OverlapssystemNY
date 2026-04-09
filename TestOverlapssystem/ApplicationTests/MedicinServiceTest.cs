using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Services;

namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public sealed class MedicinServiceTest
    {
       
            
            private Mock<IMedicinRepository> _medicinRepoMock;
            private MedicinService _service;

            // GLOBAL ARRANGE (kører før hver test)
            [TestInitialize]
            public void Setup()
            {
                //Opretter mock-repository
                _medicinRepoMock = new Mock<IMedicinRepository>();

                //Opretter MedicinService, med mocked afhængigheder
                _service = new MedicinService(_medicinRepoMock.Object);
            }

            [TestMethod]
            public async Task SetMedicinCheckedAsync_WhenCheckedIsTrue_SetsTimeStampAndCallUpdate()
            {
           
            // Arrange
            var medTime = new MedicinModel
            {
                MedicinTimeID = 1,
                MedicinTime = new DateTime(2026, 4, 9, 8, 30, 0),
                IsChecked = false,
                MedicinCheckTimeStamp = null
            };

            _medicinRepoMock
               .Setup(r => r.GetMedicinByIdAsync(medTime.MedicinTimeID))
               .ReturnsAsync(medTime);

            // Act
            await _service.SetMedicinCheckedAsync(medTime.MedicinTimeID, true);

            // Assert
            Assert.IsTrue(medTime.IsChecked);
            Assert.IsNotNull(medTime.MedicinCheckTimeStamp);
            _medicinRepoMock.Verify(r => r.UpdateMedicinAsync(medTime), Times.Once);
        }

        [TestMethod]
        public async Task SetMedicinCheckedAsync_WhenCheckedIsFalse_ClearsTimestampAndCallUpdate()
        {
            // Arrange
            var medTime = new MedicinModel
            {
                MedicinTimeID = 2,
                MedicinTime = new DateTime(2026, 3, 9, 9, 30, 0),
                IsChecked = true,
                MedicinCheckTimeStamp = DateTime.UtcNow
            };

            _medicinRepoMock
                .Setup(r => r.GetMedicinByIdAsync(medTime.MedicinTimeID))
                .ReturnsAsync(medTime);

            // Act
            await _service.SetMedicinCheckedAsync(medTime.MedicinTimeID, false);

            // Assert
            Assert.IsFalse(medTime.IsChecked);
            Assert.IsNull(medTime.MedicinCheckTimeStamp);
            _medicinRepoMock.Verify(r => r.UpdateMedicinAsync(medTime), Times.Once);
        }
    }
}
