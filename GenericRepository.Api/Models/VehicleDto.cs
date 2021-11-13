namespace GenericRepository.Api.Models
{
    public class VehicleDto
    {
        public string? Id { get; set; }
        public DateTime DateUpdated { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public int Milleage { get; set; }
        public string Color { get; set; }
        public bool IsDamaged { get; set; }
    }
}
