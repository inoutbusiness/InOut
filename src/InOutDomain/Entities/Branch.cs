﻿namespace InOut.Domain.Entities
{
    public class Branch : Base
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }

        public long LocationId { get; set; }
        public Location Location { get; set; }

        public Inventory Inventory { get; set; } // Avaliar se precisa mesmo

        public ICollection<User> Employees { get; set; }

        public override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}