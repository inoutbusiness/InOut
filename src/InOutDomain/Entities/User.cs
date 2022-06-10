    namespace InOut.Domain.Entities
{
    public class User : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CpfCnpj { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public Account Account { get; set; }
        public ICollection<UserBusiness> UserBusinesses { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}