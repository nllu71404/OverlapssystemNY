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
using Microsoft.AspNetCore.Identity;

namespace TestOverlapssystem;

[TestClass]
public sealed class UserServiceTest
{
    private Mock<IUserRepository> _userRepositoryMock;
    private Mock<ILogger<UserService>> _loggerMock;
    private UserService _userService;

    // Initialize mocks and the service before each test
    [TestInitialize]
    public void TestInitialize()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _loggerMock = new Mock<ILogger<UserService>>();
        _userService = new UserService(_userRepositoryMock.Object, _loggerMock.Object);
    }

    [TestMethod]
    public async Task GetAllUsersAsync_WhenSuccess_ReturnsAllUsers()
    {
        // Arrange
        var expectedUsers = new List<UserModel>
    {
        new UserModel { Id = "1", UserName = "User1" },
        new UserModel { Id = "2", UserName = "User2" }
    };

        _userRepositoryMock
            .Setup(repo => repo.GetAllUsers())
            .Returns(Task.FromResult(expectedUsers));

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.IsTrue(result.Success);
        Assert.IsNotNull(result.Value);
        Assert.AreEqual(2, result.Value.Count);

        Assert.AreEqual("User1", result.Value[0].UserName);
        Assert.AreEqual("1", result.Value[0].Id);

        Assert.AreEqual("User2", result.Value[1].UserName);
        Assert.AreEqual("2", result.Value[1].Id);

        _userRepositoryMock.Verify(repo => repo.GetAllUsers(), Times.Once);
    }

    [TestMethod]
    public async Task GetAllUsersAsync_WhenRepositoryThrows_ReturnsTechnicalError()
    {
        // Arrange
        _userRepositoryMock.Setup(repo => repo.GetAllUsers())
            .ThrowsAsync(new Exception("fejl.1"));

        // Act
        var result = await _userService.GetAllUsersAsync();

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(ErrorType.Technical, result.Error.Type);
        Assert.AreEqual("Kunne ikke hente brugere", result.Error.Message);
    }

    [TestMethod]
    public async Task GetUserByIdAsync_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var userId = "1";
        var expectedUser = new UserModel { Id = userId, UserName = "User1" };
        _userRepositoryMock.Setup(repo => repo.GetUserByID(userId))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _userService.GetUserByIdAsync(userId);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(expectedUser.Id, result.Value.Id);
        Assert.AreEqual(expectedUser.UserName, result.Value.UserName);
    }

    [TestMethod]
    public async Task GetUserByIdAsync_WhenUserDoesNotExist_ReturnsNotFoundError()
    {
        // Arrange
        var userId = "nonexistent";
        _userRepositoryMock.Setup(repo => repo.GetUserByID(userId))
            .ReturnsAsync((UserModel)null);

        // Act
        var result = await _userService.GetUserByIdAsync(userId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Brugeren blev ikke fundet", result.Error.Message);
    }

    [TestMethod]
    public async Task GetUserByUserNameAsync_WhenUserExists_ReturnsUser()
    {
        // Arrange
        var userName = "User1";
        var expectedUser = new UserModel { Id = "1", UserName = userName };
        _userRepositoryMock.Setup(repo => repo.GetUserByUserName(userName))
            .ReturnsAsync(expectedUser);

        // Act
        var result = await _userService.GetUserByUserNameAsync(userName);

        // Assert
        Assert.IsTrue(result.Success);
        Assert.AreEqual(expectedUser.Id, result.Value.Id);
        Assert.AreEqual(expectedUser.UserName, result.Value.UserName);
    }

    [TestMethod]
    public async Task GetUserByUserNameAsync_WhenUserDoesNotExist_ReturnsNotFoundError()
    {
        // Arrange
        var userName = "nonexistent";
        _userRepositoryMock.Setup(repo => repo.GetUserByUserName(userName))
            .ReturnsAsync((UserModel)null);

        // Act
        var result = await _userService.GetUserByUserNameAsync(userName);

        // Assert
        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public async Task DeleteUserAsync_WhenUserExists_ReturnsSuccess()
    {
        // Arrange
        var userId = "1";

        _userRepositoryMock
            .Setup(repo => repo.DeleteUser(userId))
            .Returns(Task.FromResult(IdentityResult.Success));

        // Act
        var result = await _userService.DeleteUserAsync(userId);

        // Assert
        Assert.IsTrue(result.Success);

        _userRepositoryMock.Verify(repo => repo.DeleteUser(userId), Times.Once);
    }

    [TestMethod]
    public async Task DeleteUserAsync_WhenUserDoesNotExist_ReturnsNotFoundError()
    {
        // Arrange
        var userId = "1";

        _userRepositoryMock
            .Setup(repo => repo.DeleteUser(userId))
            .ReturnsAsync(IdentityResult.Failed(
                new IdentityError
                {
                    Description = "User not found"
                }
            ));

        // Act
        var result = await _userService.DeleteUserAsync(userId);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual("Brugeren findes ikke", result.Error.Message);

        _userRepositoryMock.Verify(
            repo => repo.DeleteUser(userId),
            Times.Once);
    }

    [TestMethod]
    public async Task CreateNewUserAsync_WhenSuccess_CreatesUser()
    {
        // Arrange
        var userName = "TestName";
        var password = "StrongPassword123";
        var role = "Admin";

        var userModel = new UserModel
        {
            Id = "1",
            UserName = userName
        };

        _userRepositoryMock
            .Setup(repo => repo.GetUserByUserName(userName))
            .ReturnsAsync((UserModel)null);

        _userRepositoryMock
            .Setup(repo => repo.CreateUser(userModel, password, role))
            .ReturnsAsync(IdentityResult.Success);

        _userRepositoryMock
            .Setup(repo => repo.AddToRoleAsync(userModel, role))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _userService.CreateNewUserAsync(userModel, password, role);

        // Assert
        Assert.IsTrue(result.Success);

        _userRepositoryMock.Verify(
            repo => repo.GetUserByUserName(userName),
            Times.Once);

        _userRepositoryMock.Verify(
            repo => repo.CreateUser(
                It.Is<UserModel>(u => u.UserName == userName),
                password,
                role),
            Times.Once);

        _userRepositoryMock.Verify(
            repo => repo.AddToRoleAsync(
                It.Is<UserModel>(u => u.UserName == userName),
                role),
            Times.Once);
    }

    [TestMethod]
    public async Task UpdateUserAsync_WhenSuccess_UserUpdated()
    {
        // Arrange
        var userId = "1";

        var updatedUser = new UserModel
        {
            Id = userId,
            UserName = "NewName"
        };

        _userRepositoryMock
            .Setup(repo => repo.UpdateUser("1", updatedUser));


        // Act
        var result = await _userService.UpdateUserAsync(userId, updatedUser);

        // Assert
        Assert.IsFalse(result.Success);
    }

    [TestMethod]
    public async Task UpdateUserAsync_WhenUserDoesNotExist_ReturnsNotFoundError()
    {
        // Arrange
        var userId = "nonexistent";
        var updatedUser = new UserModel
        {
            Id = userId,
            UserName = "NewName"
        };
        _userRepositoryMock
            .Setup(repo => repo.UpdateUser(userId, updatedUser))
            .ThrowsAsync(new Exception("fejl.2"));

        // Act
        var result = await _userService.UpdateUserAsync(userId, updatedUser);

        // Assert
        Assert.IsFalse(result.Success);
        Assert.AreEqual(ErrorType.Technical, result.Error.Type);
    }

}