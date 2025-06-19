using System.ComponentModel.DataAnnotations;

namespace HonorFlightScreening.Data;

/// <summary>
/// Represents a veteran screening form with medical and mobility assessments
/// </summary>
public class VeteranScreening
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string VeteranName { get; set; } = string.Empty;
    
    public string UserId { get; set; } = string.Empty;
    
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastModified { get; set; } = DateTime.UtcNow;
    
    // Medical History Review
    public bool? HasPcpSignature { get; set; }
    
    // Medical Assessment
    public bool? RequiresOxygen { get; set; }
    public bool? RequiresInsulin { get; set; }
    public bool? TakesMedications { get; set; }
    
    // Mobility Evaluation
    public bool? RequiresAssistiveDevice { get; set; }
    public AssistiveDeviceType? AssistiveDeviceType { get; set; }
    public bool? HasMobilityLimitations { get; set; }
    public bool? CanWalkLongDistances { get; set; }
    public bool? RequiresWheelchairAccess { get; set; }
    
    // Post-Interview Review
    public bool? HasMedicalAlerts { get; set; }
    public bool? HasMobilityAlerts { get; set; }
    public bool? HasSpecialAlerts { get; set; }
    
    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;
    
    public ScreeningStatus Status { get; set; } = ScreeningStatus.InProgress;
    
    public bool IsCompleted => Status == ScreeningStatus.Completed;
}

/// <summary>
/// Types of assistive devices that veterans may use
/// </summary>
public enum AssistiveDeviceType
{
    None = 0,
    Cane = 1,
    Walker = 2,
    Wheelchair = 3,
    Scooter = 4
}

/// <summary>
/// Status of the veteran screening process
/// </summary>
public enum ScreeningStatus
{
    InProgress = 0,
    Completed = 1,
    Reviewed = 2
}