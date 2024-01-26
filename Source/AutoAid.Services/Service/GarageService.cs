using AutoAid.Application.Common;
using AutoAid.Application.Firebase;
using AutoAid.Domain.Dto.Garage;
using FirebaseAdmin.Auth;
using Mapster;

namespace AutoAid.Bussiness.Service
{
    public class GarageService : BaseService, IGarageService
    {
        private readonly IFirebaseClient _firebaseClient;

        public GarageService(IUnitOfWork unitOfWork, IFirebaseClient firebaseClient) : base(unitOfWork)
        {
            _firebaseClient = firebaseClient;
        }

        public async Task<ApiResponse<bool>> Create(CreateGarageReq req)
        {
            try
            {
                var account = req.Adapt<Account>();
                account.AccountRole = Actor.GARAGE.ToString();
                account.CreatedUser = (int)Actor.SYSTEM;
                account.UpdatedDate = DateTime.UtcNow;

                var garage = req.Adapt<Garage>();
                garage.CreatedUser = (int)Actor.SYSTEM;
                garage.UpdatedUser = (int)Actor.SYSTEM;

                var place = new Place()
                {
                    Lat = req.Lat,
                    Lng = req.Lng,
                };

                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.Resolve<Account>().CreateAsync(account);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.Resolve<Place>().CreateAsync(place);
                await _unitOfWork.SaveChangesAsync();

                garage.PlaceId = place.PlaceId;

                await _unitOfWork.Resolve<Garage>().CreateAsync(garage);
                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.Resolve<GarageAccount>().CreateAsync(new GarageAccount()
                {
                    AccountId = account.AccountId,
                    GarageId = garage.GarageId,
                    IsPrimaryAccount = true,
                });
                await _unitOfWork.SaveChangesAsync();

                var fireBaseUser = await _firebaseClient.FirebaseAuth.CreateUserAsync(new UserRecordArgs()
                {
                    Email = req.Email,
                    PhoneNumber = "+84" + req.PhoneNumber.TrimStart('0'),
                    DisplayName = req.Username,
                    PhotoUrl = req.AvatarUrl,
                    Disabled = false,
                });

                ArgumentNullException.ThrowIfNull(fireBaseUser, "Can not create firebase user");

                account.FirebaseUid= fireBaseUser.Uid;
                await _unitOfWork.Resolve<Account>().UpdateAsync(account);

                await _unitOfWork.CommitTransactionAsync();

                return Success(true);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return Failed<bool>(message: ex.GetExceptionMessage());
            }
        }
    }
}
