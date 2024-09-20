using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System;
using FluentAssertions;
using Moq;
using Xunit;
using Zip.Application.Abstractions;
using Zip.Application.Contracts;
using Zip.Application.Services;
using Zip.Domain.Entities;

namespace Zip.Tests;

public class AccountServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<IAccountRepository> _mockAccountRepository;
    private readonly UserService _userService;
    private readonly AccountService _accountService;
    private readonly Mock<IOptions<ApplicationOptions>> _options;

    public AccountServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _mockAccountRepository = new Mock<IAccountRepository>();
        _options = new Mock<IOptions<ApplicationOptions>>();
        _userService = new UserService(_mockUserRepository.Object);
        _accountService = new AccountService(_mockAccountRepository.Object, _mockUserRepository.Object, _options.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnAccount_WhenUserExistsAndHasSufficientCredit()
    {
        // Arrange
        var expectedAccount = new Account();
        var expectedUser = new User { Salary = 1000, Expenses = 500 };
        _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUser);
        _mockAccountRepository.Setup(repo => repo.CreateAsync(It.IsAny<Account>())).ReturnsAsync(expectedAccount);
        _options.Setup(o => o.Value).Returns(new ApplicationOptions { CreditLimit = 250 });

        // Act
        var result = await _accountService.Create(new Account { UserId = Guid.NewGuid() });

        // Assert
        result.IsSuccessful.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedAccount);
    }

    [Fact]
    public async Task Create_ShouldReturnInsufficientCredit_WhenUserDoesntHaveEnoughCredit()
    {
        // Arrange
        var expectedUser = new User { Salary = 100, Expenses = 150 };
        _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedUser);
        _options.Setup(o => o.Value).Returns(new ApplicationOptions { CreditLimit = 100 });

        // Act
        var result = await _accountService.Create(new Account { UserId = Guid.NewGuid() });

        // Assert
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().Be("Insufficient Credit");
    }

    [Fact]
    public async Task Create_ShouldReturnUserNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        _mockUserRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((User)null);

        // Act
        var result = await _accountService.Create(new Account { UserId = Guid.NewGuid() });

        // Assert
        result.IsSuccessful.Should().BeFalse();
        result.Error.Should().Be("User not found");
    }

    [Fact]
    public async Task GetById_ShouldReturnAccount_WhenAccountExists()
    {
        // Arrange
        var expectedAccount = new Account();
        _mockAccountRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedAccount);

        // Act
        var result = await _accountService.GetById(Guid.NewGuid());

        // Assert
        result.IsSuccessful.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedAccount);
    }
}
