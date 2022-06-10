namespace InOut.Domain.Entities
{
    public class Branch : Base
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }

        public long LocationId { get; set; }
        public Location Location { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
