using System.ComponentModel.DataAnnotations;

namespace HonorFlightScreening.Data
{
    public class HonorFlight
    {
        [Key]
        public int Id { get; set; }
        public DateTime FlightDate { get; set; }
        public ICollection<VeteranScreening> VeteranScreenings { get; set; } = new List<VeteranScreening>();
    }
}
