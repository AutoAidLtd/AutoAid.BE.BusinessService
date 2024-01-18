using AutoAid.Application.Firebase;
using AutoAid.Application.Service.Common;
using System.Security.Claims;

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
            var firebaseUser = await _firebaseClient.FirebaseAuth.GetUserAsync(uid);

            var token = _tokenService.Encode(new GenerateTokenReq
            {
                Id = firebaseUser.Uid,
                Email = firebaseUser.Email,
                FullName = firebaseUser.DisplayName,
                Phone = firebaseUser.PhoneNumber,
                AvatarUrl = firebaseUser.PhotoUrl
            });

            return Succsess(token);
        }

        public async Task<ApiResponse<bool>> ValidateAccessToken(string token)
        {
            var claims = _tokenService.Decode(token);
            var userUID = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userUID?.Value))
                return Failed<bool>("Invalid token");

            var firebaseUser = await _firebaseClient.FirebaseAuth.GetUserAsync(userUID?.Value);

            if (firebaseUser == null)
                return Failed<bool>("Invalid token");

            return Succsess(true);
        }
    }
}
