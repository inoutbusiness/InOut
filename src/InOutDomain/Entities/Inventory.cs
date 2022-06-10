namespace InOut.Domain.Entities
{
    public class Inventory : Base
    {
        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long BranchId { get; set; }
        public Branch Branch { get; set; }


        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
