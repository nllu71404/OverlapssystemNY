using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OverlapssystemDomain.Entities;
using OverlapssystemDomain.Interfaces;
using OverlapssytemApplication.Services;



namespace TestOverlapssystem.ApplicationTests
{
    [TestClass]
    public sealed class ResidentServicesTest
    {
        private Mock<IResidentRepository> _residentRepoMock;
        private Mock<IMedicinRepository> _medicinRepoMock;
        private ResidentServices _service;

        // GLOBAL ARRANGE (kører før hver test)
        [TestInitialize]
        public void Setup()
        {
            //Opretter mock-repositories
            _residentRepoMock = new Mock<IResidentRepository>();
            _medicinRepoMock = new Mock<IMedicinRepository>();

            //Opretter ResidentServices, med mocked afhængigheder
            _service = new ResidentServices(_residentRepoMock.Object, _medicinRepoMock.Object);
        }

        [TestMethod]
        public async Task LoadResidentsAsync_WhenCalled_ReturnsResidentsAndUpdatesState()
        {
            // Arrange
            var residents = new List<ResidentModel>
    {
        new ResidentModel { ResidentId = 1, Name = "Ninna" }
    };

            _residentRepoMock
                .Setup(r => r.GetAllResidentsAsync())
                .ReturnsAsync(residents);

            // Act
            var result = await _service.LoadResidentsAsync();

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Ninna", result[0].Name);
            CollectionAssert.AreEqual(result, _service.Residents);
        }

        [TestMethod]
        public async Task UpdateResidentAsync_WhenCalled_CallsRepositoryAndReloadsResidents()
        {
            // Arrange
            var resident = new ResidentModel { ResidentId = 1, DepartmentId = 2 };

            _residentRepoMock
                .Setup(r => r.GetResidentByDepartmentIdAsync(2))
                .ReturnsAsync(new List<ResidentModel>());

            // Act
            await _service.UpdateResidentAsync(resident);

            // Assert
            _residentRepoMock.Verify(r => r.UpdateResidentAsync(resident), Times.Once);
            _residentRepoMock.Verify(r => r.GetResidentByDepartmentIdAsync(2), Times.Once);
        }

        [TestMethod]
        public async Task DeleteResidentAsync_WhenCalled_CallsRepository()
        {
            // Arrange
            int residentId = 1;

            // Act
            await _service.DeleteResidentAsync(residentId);

            // Assert
            _residentRepoMock.Verify(r => r.DeleteResidentAsync(residentId), Times.Once);
        }

        [TestMethod]
        public async Task CreateResidentAsync_WhenCalled_SavesResidentAndReloadsList()
        {
            // Arrange
            var resident = new ResidentModel { DepartmentId = 2 };

            _residentRepoMock
                .Setup(r => r.GetResidentByDepartmentIdAsync(2))
                .ReturnsAsync(new List<ResidentModel>());

            // Act
            await _service.CreateResidentAsync(resident);

            // Assert
            _residentRepoMock.Verify(r => r.SaveNewResidentAsync(resident), Times.Once);
            _residentRepoMock.Verify(r => r.GetResidentByDepartmentIdAsync(2), Times.Once);
        }

        [TestMethod]
        public void SetDepartment_DepartmentIdIsNotNull_CreatesANewResidentWithSelectedDepartmentId()
        {
            // Arrange
            int departmentId = 5;

            // Act
            _service.SetDepartment(departmentId);

            // Assert
            Assert.AreEqual(departmentId, _service.SelectedDepartmentId);
            Assert.IsNotNull(_service.NewResident);
            Assert.AreEqual(departmentId, _service.NewResident.DepartmentId);
        }

        [TestMethod]
        public async Task LoadMedicinTimesAsync_WhenCalled_LoadsMedicinTimesIntoResident()
        {
            // Arrange
            var resident = new ResidentModel { ResidentId = 1 };
            var medicinTimes = new List<MedicinModel>
            {
                new MedicinModel { MedicinTimeID = 1 },
                new MedicinModel { MedicinTimeID = 2 }
            };

            _medicinRepoMock
                .Setup(r => r.GetMedicinByResidentIdAsync(resident.ResidentId))
                .ReturnsAsync(medicinTimes);

            // Act
            await _service.LoadMedicinTimesAsync(resident);

            // Assert
            Assert.IsNotNull(resident.MedicinTimes);
            Assert.AreEqual(2, resident.MedicinTimes.Count);
            CollectionAssert.AreEqual(medicinTimes, resident.MedicinTimes);
        }

        [TestMethod]
        public async Task AddMedicinTimeAsync_ResidentIdAndTimeIsNotNull_CreatesMedicinTimeAndCallsRepo()
        {
            // Arrange
            int residentId = 3;
            DateTime medTimeDate = new DateTime(2026, 4, 9, 8, 0, 0);
            int expectedId = 4;

            MedicinModel capturedMedTime = null;
            _medicinRepoMock
                .Setup(r => r.SaveNewMedicinAsync(It.IsAny<MedicinModel>()))
                .Callback<MedicinModel>(m => capturedMedTime = m)
                .ReturnsAsync(expectedId);

            // Act
            await _service.AddMedicinTimeAsync(residentId, medTimeDate);

            // Assert
            Assert.IsNotNull(capturedMedTime);
            Assert.AreEqual(residentId, capturedMedTime.ResidentID);
            Assert.AreEqual(medTimeDate, capturedMedTime.MedicinTime);
            Assert.IsNull(capturedMedTime.MedicinCheckTimeStamp);
            _medicinRepoMock.Verify(r => r.SaveNewMedicinAsync(It.IsAny<MedicinModel>()), Times.Once);
        }

        [TestMethod]
        public async Task ToggleMedicinGivenAsync_WhenTimestampIsNull_SetsTimestampAndSaves()
        {
            // Arrange
            var medTime = new MedicinModel
            {
                MedicinTimeID = 1,
                MedicinCheckTimeStamp = null
            };

            // Act
            await _service.ToggleMedicinGivenAsync(medTime);

            // Assert
            Assert.IsNotNull(medTime.MedicinCheckTimeStamp);
            _medicinRepoMock.Verify(m => m.UpdateMedicinAsync(medTime), Times.Once);
        }

        [TestMethod]
        public async Task ToggleMedicinGivenAsync_WhenTimestampExists_NullsTimestamp()
        {
            // Arrange
            var medTime = new MedicinModel
            {
                MedicinTimeID = 1,
                MedicinCheckTimeStamp = DateTime.Now
            };

            // Act
            await _service.ToggleMedicinGivenAsync(medTime);

            // Assert
            Assert.IsNull(medTime.MedicinCheckTimeStamp);
        }



    }
}


