namespace InOut.Domain.Entities
{
    public class UserBusiness
    {
        public long PersonId { get; set; }
        public User Person { get; set; }

        public long BusinessId { get; set; }
        public Business Business { get; set; }
    }
}