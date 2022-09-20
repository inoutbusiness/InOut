using InOut.Domain.DTOs;
using InOut.Domain.Models.Branch;

namespace InOut.Service.Services.Interfaces
{
    public interface IBranchService
    {
        Task<BranchDto> CreateBranch(BranchModel branchModel);
    }
}