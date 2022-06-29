using InOut.Domain.DTOs;
using InOut.Domain.Entities;
using InOut.Domain.Models.Auth;
using InOut.Domain.Models.User;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;
using System.Transactions;

namespace InOut.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICrypt _crypt;
        private readonly IBranchRepository _branchRepository;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, ICrypt crypt, IBranchRepository branchRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _crypt = crypt;
            _branchRepository = branchRepository;
        }

        public async Task<bool> ExistsByEmailAndPassword(string email, string password)
        {
            return await _accountRepository.ExistsByEmailAndPassword(email, password);
        }

        public async Task<UserAccountModel> GetUserWithAccountByEmailAndPassword(string email, string password)
        {
            var account = await _accountRepository.GetUserWithAccountByEmailAndPassword(email, password);

            return new UserAccountModel
            {
                FirstName = account.User?.FirstName,
                LastName = account.User?.LastName,
                BirthDate = account.User == null ? DateTime.MinValue : account.User.BirthDate,
                Phone = account.User?.Phone,
                CpfCnpj = account.User?.CpfCnpj,
                Email = account.Email,
            };
        }

        public async Task<UserDto> CreateAccountAndUserBySingUpModel(SignUpModel signUpModel)
        {
            UserDto userDto;
            using (var tc = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var user = new User
                {
                    BirthDate = signUpModel.BirthDate,
                    CpfCnpj = signUpModel.CpfCnpj,
                    FirstName = signUpModel.FirstName,
                    LastName = signUpModel.LastName,
                    Phone = signUpModel.Phone,
                    Account = new Account
                    {
                        Email = signUpModel.Email,
                        Password = signUpModel.Password// _crypt.Encrypt(signUpModel.Password, EEncryptionType.Password), vai ser implementado o hash de senha
                    },
                    BranchId = signUpModel.BranchId,
                };

                var createdUser = await _userRepository.Create(user);
                tc.Complete();

                userDto = new UserDto
                {
                    Id = createdUser.Id,
                    FirstName = createdUser.FirstName,
                    AccountId = createdUser.AccountId,
                    BirthDate = createdUser.BirthDate,
                    BranchId = createdUser.BranchId,
                    CpfCnpj = createdUser.CpfCnpj,
                    LastName = createdUser.LastName,
                    Phone = createdUser.Phone,
                };
            }

            return userDto;
        }
    }
}