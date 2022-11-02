using InOut.Common;
using InOut.Domain.Entities;
using InOut.Domain.Models.User;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;
using System.Transactions;

namespace InOut.Service.Services
{
    public class UserAccountService : IUserAccountService
    {
        private readonly IUserRepository _userRepository;

        public UserAccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserAccountModel> UpdateUserAccountInfo(UserAccountModel userAccountModel)
        {
            User? userInfo = null;

            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var userUpdated = await _userRepository.GetUserByAccountId(userAccountModel.Id);

                if (userUpdated == null)
                    throw new Exception("Informações não encontradas para esta conta!");

                userUpdated.BirthDate = userAccountModel.BirthDate;
                userUpdated.CpfCnpj = userAccountModel.CpfCnpj;
                userUpdated.FirstName = userAccountModel.FirstName;
                userUpdated.LastName = userAccountModel.LastName;
                userUpdated.Phone = userAccountModel.Phone;

                userInfo = await _userRepository.Update(userUpdated);

                tc.Complete();
            }

            return new UserAccountModel()
            {
                Id = userInfo.Id.ToLong(),
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                BirthDate = userInfo.BirthDate,
                Phone = userInfo.Phone,
                CpfCnpj = userInfo.CpfCnpj,
            };
        }
    }
}
