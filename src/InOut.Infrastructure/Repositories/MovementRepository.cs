using InOut.Domain.Entities;
using InOut.Infrastructure.Context;
using InOut.Infrastructure.Repositories.Interfaces;

namespace InOut.Infrastructure.Repositories
{
    public class MovementRepository : BaseRepository<Movement>, IMovementRepository
    {
        public MovementRepository(IInOutContext inOutContext) : base(inOutContext)
        {
        }
    }
}