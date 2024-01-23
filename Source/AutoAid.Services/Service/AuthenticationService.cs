using AutoAid.Application.Common;
using AutoAid.Application.Firebase;
using AutoAid.Application.Service.Common;

namespace AutoAid.Bussiness.Service
{
    public class AuthenticationService : BaseService, IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly IFirebaseClient _firebaseClient;

        public AuthenticationService(IUnitOfWork unitOfWork,
            ITokenService tokenService, IFirebaseClient firebaseClient
        ) : base(unitOfWork)
        {
            _tokenService = tokenService;
            _firebaseClient = firebaseClient;
        }

        public async Task<ApiResponse<string>> GenerateAccessToken(string uid)
        {
            try
            {
                var firebaseUser = await _firebaseClient.FirebaseAuth.GetUserAsync(uid);
                var account = await _unitOfWork.Resolve<Account, IAccountRepository>().GetAccountByFirebaseUID(uid);

                if(firebaseUser == null || account == null)
                    throw new ArgumentNullException("Can not get firebase user");

                var token = _tokenService.Encode(new GenerateTokenReq
                {
                    Id = account.AccountId.ToString(),
                    Email = firebaseUser.Email,
                    FullName = firebaseUser.DisplayName,
                    Phone = firebaseUser.PhoneNumber,
                    AvatarUrl = firebaseUser.PhotoUrl
                 });

                return Success(token);
            }
            catch (Exception ex)
            {
                return Failed<string>(message: ex.GetExceptionMessage());
            }
        }

        public async Task<ApiResponse<bool>> ValidateAccessToken(string token)
        {
            var claims = _tokenService.Decode(token);

            if (claims == null)
                return Success(false);

            var claim = claims.FirstOrDefault(c => c.Type.Equals("nameid", StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrEmpty(claim?.Value))
                return Success(false);

            var account = await _unitOfWork.Resolve<Account, IAccountRepository>().FindAsync(int.Parse(claim.Value));

            if (account == null)
                return Success(false);

            return Success(true);
        }
    }
}
