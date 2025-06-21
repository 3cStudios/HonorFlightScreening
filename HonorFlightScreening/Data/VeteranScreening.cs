using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace HonorFlightScreening.Data;

/// <summary>
/// Represents a veteran screening form with medical and mobility assessments
/// </summary>
public class VeteranScreening
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(250)]
    public string VeteranName { get; set; } = string.Empty;
    [Required]
    [StringLength(250)]
    public string GuardianName { get; set; } = string.Empty;
    [Required]
    [StringLength(12)]
    public string GuardianPhone { get; set; } = string.Empty;

    [Required]
    public int SoundOffNumber { get; set; }

    public string UserId { get; set; } = string.Empty;
    
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? LastModified { get; set; } = DateTime.UtcNow;
    
    // Medical History Review
    public bool? HasPcpSignature { get; set; }
    
    // Medical Assessment
    public bool? UseOxygen { get; set; }
    [StringLength(500)]
    public string HowMuchOxygen{ get; set; } = string.Empty;
    public bool? UseInsulin { get; set; }
    public bool? HelpWithInsulin { get; set; }
    public bool? FluidPills { get; set; }
    public bool? MedicalConcerns { get; set; }
    [StringLength(500)]
    public string MedicalConcernsDetails { get; set; } = string.Empty;

    // Mobility Evaluation
    public bool? RequiresAssistiveDevice { get; set; }
    public AssistiveDeviceType? AssistiveDeviceType { get; set; }
    public bool? HasMobilityLimitations { get; set; }
    public bool? ConcernsWalkingStairsBus { get; set; }
    public bool? ConcernsFlying { get; set; }
    
    // Post-Interview Review
    public bool? HasMedicalAlerts { get; set; }
    public bool? HasMobilityAlerts { get; set; }
    public bool? HasSpecialAlerts { get; set; }
    [StringLength(500)]
    public string SpecialAlertsDetails { get; set; } = string.Empty;
    [StringLength(500)]
    public string MobilityAlertsDetails { get; set; } = string.Empty;
    [StringLength(500)]
    public string? LiftRequired { get; set; }

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;
    public int HonorFlightId { get; set; }
    public HonorFlight HonorFlight { get; set; } = default!;


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

