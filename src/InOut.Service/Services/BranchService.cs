using AutoMapper;
using InOut.Domain.DTOs;
using InOut.Domain.Entities;
using InOut.Domain.Exceptions;
using InOut.Domain.Models.Branch;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;
using System.Transactions;

namespace InOut.Service.Services
{
    public class BranchService : IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository, IMapper mapper)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
        }

        public async Task<BranchDto> CreateBranch(BranchModel branchModel)
        {
            if (await _branchRepository.ExistsByLocationId(branchModel.LocationId))
                throw new AlreadyExistsException("Já existe essa Filial cadastrada!");

            BranchDto branchDto;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var branch = new Branch();
                branch.Name = branchModel.Name;
                branch.Cnpj = branchModel.Cnpj;
                branch.LocationId = branchModel.LocationId;

                var createdBranch = await _branchRepository.Create(branch);

                branchDto = _mapper.Map<BranchDto>(createdBranch);
                tc.Complete();
            }

            return branchDto;
        }
    }
}