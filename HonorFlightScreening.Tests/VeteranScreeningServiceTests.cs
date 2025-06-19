using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HonorFlightScreening.Data;
using HonorFlightScreening.Services;

namespace HonorFlightScreening.Tests;

[TestClass]
public class VeteranScreeningServiceTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new ApplicationDbContext(options);
    }

    [TestMethod]
    public async Task CreateScreeningAsync_ShouldCreateNewScreening()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new VeteranScreeningService(context);
        var veteranName = "John Doe";
        var userId = "user123";

        // Act
        var result = await service.CreateScreeningAsync(veteranName);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(veteranName, result.VeteranName);
        Assert.AreEqual(userId, result.UserId);
        Assert.AreEqual(ScreeningStatus.InProgress, result.Status);
    }

    [TestMethod]
    public async Task GetUserScreeningsAsync_ShouldReturnUserScreenings()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new VeteranScreeningService(context);
        var userId = "user123";

        await service.CreateScreeningAsync("John Doe");
        await service.CreateScreeningAsync("Jane Smith");
        await service.CreateScreeningAsync("Bob Wilson");

        // Act
        var result = await service.GetUserScreeningsAsync(userId);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(s => s.UserId == userId));
    }

    [TestMethod]
    public async Task CompleteScreeningAsync_ShouldUpdateStatusToCompleted()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new VeteranScreeningService(context);
        var userId = "user123";

        var screening = await service.CreateScreeningAsync("John Doe");

        // Act
        var result = await service.CompleteScreeningAsync(screening.Id, userId);

        // Assert
        Assert.IsTrue(result);
        var updatedScreening = await service.GetScreeningAsync(screening.Id, userId);
        Assert.AreEqual(ScreeningStatus.Completed, updatedScreening?.Status);
    }

    [TestMethod]
    public async Task GetScreeningAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new VeteranScreeningService(context);
        var userId = "user123";

        // Act
        var result = await service.GetScreeningAsync(999, userId);

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task UpdateScreeningAsync_ShouldUpdateLastModified()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new VeteranScreeningService(context);
        var userId = "user123";

        var screening = await service.CreateScreeningAsync("John Doe");
        var originalLastModified = screening.LastModified;

        // Wait a small amount to ensure time difference
        await Task.Delay(10);

        screening.HasPcpSignature = true;

        // Act
        var result = await service.UpdateScreeningAsync(screening);

        // Assert
        Assert.IsTrue(result);
        Assert.IsTrue(screening.LastModified > originalLastModified);
    }
}