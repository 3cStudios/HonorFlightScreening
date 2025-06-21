using HonorFlightScreening.Data;
using Microsoft.EntityFrameworkCore;

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
    /// Get all screenings
    /// </summary>
    public async Task<List<VeteranScreening>> GetAllScreeningsAsync()
    {
        return await _context.VeteranScreenings
            .OrderByDescending(s => s.LastModified)
            .ToListAsync();
    }

    public async Task<List<VeteranScreening>> GetAllScreeningsByHonorFlightIdAsync(int honorFlightId)
    {
        return await _context.VeteranScreenings
            .Where(s => s.HonorFlightId == honorFlightId)
            .OrderByDescending(s => s.LastModified)
            .ToListAsync();
    }


    /// <summary>
    /// Get a specific screening by ID
    /// </summary>
    public async Task<VeteranScreening?> GetScreeningAsync(int id)
    {
        return await _context.VeteranScreenings
            .FirstOrDefaultAsync(s => s.Id == id );
    }

    /// <summary>
    /// Create a new veteran screening
    /// </summary>
    public async Task<VeteranScreening> CreateScreeningAsync(VeteranScreening veteranScreening)
    {
        
        veteranScreening.CreatedDate = DateTime.UtcNow;

        _context.VeteranScreenings.Add(veteranScreening);
        await _context.SaveChangesAsync();

        return veteranScreening;
    }

    /// <summary>
    /// Update an existing screening
    /// </summary>
    public async Task<VeteranScreening> UpdateScreeningAsync(VeteranScreening veteranScreening)
    {
        try
        {
            veteranScreening.LastModified = DateTime.UtcNow;
            _context.VeteranScreenings.Update(veteranScreening);
            await _context.SaveChangesAsync();
            return veteranScreening;
        }
        catch
        {
            return new VeteranScreening();
        }
    }

    /// <summary>
    /// Delete a screening
    /// </summary>
    public async Task<bool> DeleteScreeningAsync(int id, string userId)
    {
        var screening = await GetScreeningAsync(id);
        if (screening == null)
            return false;

        _context.VeteranScreenings.Remove(screening);
        await _context.SaveChangesAsync();
        return true;
    }

  
}