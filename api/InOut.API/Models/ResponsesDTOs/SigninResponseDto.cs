using InOut.Domain.Models.User;
using InOut.Service.Token;

namespace InOut.API.Models.ResponsesDTOs
{
    public class SigninResponseDto
    {
        public UserAccountModel? UserAccountModel { get; set; }
        public TokenData? TokenData { get; set; }
    }
}
