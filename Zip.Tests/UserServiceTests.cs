using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Application.Contracts;
using Zip.Application.Services;
using Zip.Domain.Entities;

namespace Zip.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllUsers_WhenUsersExist()
    {
        // Arrange
        var expectedUsers = new List<User>() 
        { 
            new ()
            {
                Id = Guid.NewGuid(),
                Email = "test1@test.com",
                Name = "Test1",
                Salary = 10000,
                Expenses = 2000
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email = "test2@test.com",
                Name = "Test2",
                Salary = 11000,
                Expenses = 3000
            }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(expectedUsers);
        var service = new UserService(_mockRepository.Object);

        // Act
        var result = await _userService.GetAll();

        // Assert
        result.IsSuccessful.Should().BeTrue();
        result.Value.ToList().Should().HaveCount(expectedUsers.Count);
        result.Value.Should().BeEquivalentTo(expectedUsers);
    }

    [Fact]
    public async Task GetAll_ShouldReturnEmptyResult_WhenNoUsersExist()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<User>());


        // Act
        var result = await _userService.GetAll();

        // Assert
        result.IsSuccessful.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task GetById_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedUser = new User() { Id = userId };
        _mockRepository.Setup(repo => repo.GetByIdAsync(expectedUser.Id)).ReturnsAsync(expectedUser);

        // Act
        var result = await _userService.GetById(userId);

        // Assert
        result.Should().NotBeNull();
        result.IsSuccessful.Should().BeTrue();
        result.Value.Id.Should().Be(expectedUser.Id);
    }

    [Fact]
    public async Task GetById_ShouldReturnNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);

        // Act
        var result = await _userService.GetById(Guid.NewGuid());

        // Assert
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().Be("User not found");
    }

    [Fact]
    public async Task Create_ShouldReturnCreatedUser_WhenUserIsNew()
    {
        // Arrange
        var newUser = new User();
        _mockRepository.Setup(repo => repo.GetByIdAsync(newUser.Id)).ReturnsAsync((User)null);
        _mockRepository.Setup(repo => repo.GetByEmailAsync(newUser.Email)).ReturnsAsync((User)null);
        _mockRepository.Setup(repo => repo.CreateAsync(newUser)).ReturnsAsync(newUser);

        // Act
        var result = await _userService.Create(newUser);

        // Assert
        result.IsSuccessful.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(newUser);
    }

    [Fact]
    public async Task Create_ShouldReturnEmailAlreadyExists_WhenEmailExists()
    {
        // Arrange
        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            Email = "test1@test.com",
            Name = "Test1",
            Salary = 10000,
            Expenses = 2000
        };
        _mockRepository.Setup(repo => repo.GetByIdAsync(newUser.Id)).ReturnsAsync((User)null);
        _mockRepository.Setup(repo => repo.GetByEmailAsync(newUser.Email)).ReturnsAsync(newUser);

        // Act
        var result = await _userService.Create(newUser);

        // Assert
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().Be("Email already exists");
    }
}
