namespace Contracts.DTO
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Btw { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsDeleted { get; set; }
    }
}