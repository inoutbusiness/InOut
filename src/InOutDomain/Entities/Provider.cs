namespace InOut.Domain.Entities
{
    public class Provider : Base
    {
        public string Name { get; set; }
        public string CNPJ { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
