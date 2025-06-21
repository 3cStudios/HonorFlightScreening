using HonorFlightScreening.Data;
using Microsoft.EntityFrameworkCore;

namespace HonorFlightScreening.Services;

/// <summary>
/// Service for managing HonorFlight operations
/// </summary>
public class HonorFlightService
{
    private readonly ApplicationDbContext _context;

    public HonorFlightService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all HonorFlight records
    /// </summary>
    public async Task<List<HonorFlight>> GetAllHonorFlightsAsync()
    {
        return await _context.HonorFlight
            .OrderByDescending(f => f.FlightDate)
            .ToListAsync();
    }

    /// <summary>
    /// Create a new HonorFlight record
    /// </summary>
    public async Task<HonorFlight> CreateHonorFlightAsync(HonorFlight honorFlight)
    {
        _context.HonorFlight.Add(honorFlight);
        await _context.SaveChangesAsync();
        return honorFlight;
    }

    /// <summary>
    /// Delete an HonorFlight record by Id
    /// </summary>
    public async Task<bool> DeleteHonorFlightAsync(int id)
    {
        var honorFlight = await _context.HonorFlight.FindAsync(id);
        if (honorFlight == null)
            return false;
        _context.HonorFlight.Remove(honorFlight);
        await _context.SaveChangesAsync();
        return true;
    }
}
