namespace GenericRepository.Dal.Entities
{
    public class Vehicle: BaseEntity
    {
        public string Model { get; set; }
        public string Make { get; set; }
        public int Milleage { get; set; }
        public string Color { get; set; }
        public bool IsDamaged { get; set; }
    }
}
