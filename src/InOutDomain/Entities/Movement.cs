using InOut.Domain.Enums;

namespace InOut.Domain.Entities
{
    public class Movement : Base
    {
        public DateTime Date { get; set; }
        public EMovementType Type { get; set; }
        public int Quantity { get; set; }

        public Product Product { get; set; }


        public ICollection<UserMovement> UserMovements { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
