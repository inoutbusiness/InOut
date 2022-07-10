using EscNet.Cryptography.Interfaces;
using EscNet.Hashers.Interfaces.Algorithms;
using InOut.Common;
using AutoMapper;
using InOut.Domain.DTOs;
using InOut.Domain.Entities;
using InOut.Domain.Models.Auth;
using InOut.Domain.Models.User;
using InOut.Infrastructure.Repositories.Interfaces;
using InOut.Service.Services.Interfaces;
using System.Transactions;
using InOut.Domain.Exceptions;

namespace InOut.Service.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserRepository _userRepository;
        private readonly IArgon2IdHasher _hasher;
        private readonly IRijndaelCryptography _rijndaelCryptography;
        private readonly IMapper _mapper;

        public AccountService(IAccountRepository accountRepository, IUserRepository userRepository, IArgon2IdHasher hasher, 
                              IRijndaelCryptography rijndaelCryptography, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _hasher = hasher;
            _rijndaelCryptography = rijndaelCryptography;
            _mapper = mapper;
        }

        public async Task<UserAccountModel> GetUserWithAccountByEmailAndPassword(string email, string password)
        {
            var encryptedEmail = _rijndaelCryptography.Encrypt(email);
            var hashedPassword = _hasher.Hash(password);

            var account = await _accountRepository.GetUserWithAccountByEmailAndPassword(encryptedEmail, hashedPassword);

            if (account == null || account.User == null)
                throw new NotFoundedException("Conta ou Usuário não encontrado");

            return new UserAccountModel
            {
                Id = account.Id.ToLong(),
                FirstName = account.User.FirstName,
                LastName = account.User.LastName,
                BirthDate = account.User == null ? DateTime.MinValue : account.User.BirthDate,
                Phone = account.User.Phone,
                CpfCnpj = account.User.CpfCnpj,
                Email = account.Email,
            };
        }

        public async Task<UserDto> CreateAccountAndUserBySingUpModel(SignUpModel signUpModel)
        {
            if (await _userRepository.ExistsByCpfCnpj(signUpModel.CpfCnpj))
                throw new AlreadyExistsException("Já existe um usuário com o CPF/CNPJ cadastrado");

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
                        Email = _rijndaelCryptography.Encrypt(signUpModel.Email),
                        Password = _hasher.Hash(signUpModel.Password),
                    },
                    BranchId = signUpModel.BranchId,
                };

                var createdUser = await _userRepository.Create(user);
                tc.Complete();

                userDto = _mapper.Map<UserDto>(createdUser);
            }
            return userDto;
        }
    }
}