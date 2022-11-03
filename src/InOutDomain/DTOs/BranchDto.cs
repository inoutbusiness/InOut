namespace InOut.Domain.DTOs
{
    public class BranchDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public long LocationId { get; set; }
    }
}