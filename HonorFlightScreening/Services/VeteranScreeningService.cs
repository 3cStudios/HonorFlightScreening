using Microsoft.EntityFrameworkCore;
using HonorFlightScreening.Data;

namespace HonorFlightScreening.Services;

/// <summary>
/// Service for managing veteran screening operations
/// </summary>
public class VeteranScreeningService
{
    private readonly ApplicationDbContext _context;
    
    public VeteranScreeningService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Get all screenings for a specific user
    /// </summary>
    public async Task<List<VeteranScreening>> GetUserScreeningsAsync(string userId)
    {
        return await _context.VeteranScreenings
            .Where(s => s.UserId == userId)
            .OrderByDescending(s => s.LastModified)
            .ToListAsync();
    }
    
    /// <summary>
    /// Get a specific screening by ID
    /// </summary>
    public async Task<VeteranScreening?> GetScreeningAsync(int id, string userId)
    {
        return await _context.VeteranScreenings
            .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);
    }
    
    /// <summary>
    /// Create a new veteran screening
    /// </summary>
    public async Task<VeteranScreening> CreateScreeningAsync(string veteranName, string userId)
    {
        var screening = new VeteranScreening
        {
            VeteranName = veteranName,
            UserId = userId,
            CreatedDate = DateTime.UtcNow,
            LastModified = DateTime.UtcNow
        };
        
        _context.VeteranScreenings.Add(screening);
        await _context.SaveChangesAsync();
        
        return screening;
    }
    
    /// <summary>
    /// Update an existing screening
    /// </summary>
    public async Task<bool> UpdateScreeningAsync(VeteranScreening screening)
    {
        try
        {
            screening.LastModified = DateTime.UtcNow;
            _context.VeteranScreenings.Update(screening);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Delete a screening
    /// </summary>
    public async Task<bool> DeleteScreeningAsync(int id, string userId)
    {
        var screening = await GetScreeningAsync(id, userId);
        if (screening == null)
            return false;
            
        _context.VeteranScreenings.Remove(screening);
        await _context.SaveChangesAsync();
        return true;
    }
    
    /// <summary>
    /// Mark screening as completed
    /// </summary>
    public async Task<bool> CompleteScreeningAsync(int id, string userId)
    {
        var screening = await GetScreeningAsync(id, userId);
        if (screening == null)
            return false;
            
        screening.Status = ScreeningStatus.Completed;
        screening.LastModified = DateTime.UtcNow;
        
        await _context.SaveChangesAsync();
        return true;
    }
}