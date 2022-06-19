namespace InOut.Domain.Entities
{
    public class Account : Base
    {
        public string Email { get; set; }
        public byte[] Password { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}