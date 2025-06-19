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
        
        

        // Act
        var result = await service.CreateScreeningAsync(new VeteranScreening
        {
            VeteranName = "John Doe",
            UserId = "user123"
        });

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("John Doe", result.VeteranName);
        Assert.AreEqual("user123", result.UserId);
        
    }

    [TestMethod]
    public async Task GetUserScreeningsAsync_ShouldReturnUserScreenings()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new VeteranScreeningService(context);
        var userId = "user123";

        await service.CreateScreeningAsync(new VeteranScreening
        {
            VeteranName = "John Doe"
        });
            
        await service.CreateScreeningAsync(new VeteranScreening
        {
            VeteranName = "Jane Smith"
        }); 
        await service.CreateScreeningAsync(new VeteranScreening
        {
            VeteranName = "Bob Wilson"
        });

        // Act
        var result = await service.GetAllScreeningsAsync();

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(s => s.UserId == userId));
    }

    
    
    [TestMethod]
    public async Task GetScreeningAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var service = new VeteranScreeningService(context);
        var userId = "user123";

        // Act
        var result = await service.GetScreeningAsync(999);

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

        var screening = await service.CreateScreeningAsync(new VeteranScreening
        {
            VeteranName = "John Doe",
        });
            
        var originalLastModified = screening.LastModified;

        // Wait a small amount to ensure time difference
        await Task.Delay(10);

        screening.HasPcpSignature = true;

        // Act
        var result = await service.UpdateScreeningAsync(screening);

        // Assert
        Assert.IsTrue(result.Id > 0);
        Assert.IsTrue(screening.LastModified > originalLastModified);
    }
}