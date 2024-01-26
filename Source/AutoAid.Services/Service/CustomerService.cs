using AutoAid.Application.Common;
using AutoAid.Application.Firebase;
using AutoAid.Domain.Dto.Customer;
using FirebaseAdmin.Auth;
using Mapster;

namespace AutoAid.Bussiness.Service
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly IFirebaseClient _firebaseClient;

        public CustomerService(IUnitOfWork unitOfWork, IFirebaseClient firebaseClient) : base(unitOfWork)
        {
            _firebaseClient = firebaseClient;
        }

        public async Task<ApiResponse<bool>> CreateCustomer(CreateCustomerReq req)
        {
            try
            {
                var account = req.Adapt<Account>();
                account.AccountRole = Actor.CUSTOMER.ToString();
                account.CreatedUser = (int)Actor.SYSTEM;
                account.UpdatedUser = (int)Actor.SYSTEM;

                var customer = req.Adapt<Customer>();

                //transaction
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.Resolve<Account>().CreateAsync(account);
                await _unitOfWork.SaveChangesAsync();

                customer.AccountId = account.AccountId;
                customer.DateOfBirth = DateOnly.FromDateTime(req.Birthdate ?? DateTime.Now);

                await _unitOfWork.Resolve<Customer>().CreateAsync(customer);
                await _unitOfWork.SaveChangesAsync();

                var firebaseUser = await _firebaseClient.FirebaseAuth.CreateUserAsync(new UserRecordArgs()
                {
                    Email = req.Email,
                    PhoneNumber = "+84" + req.PhoneNumber.TrimStart('0'),
                    DisplayName = req.Username,
                    PhotoUrl = req.AvatarUrl,
                    Disabled = false,
                });

                ArgumentNullException.ThrowIfNull(firebaseUser, "Can not create firebase user");

                account.FirebaseUid = firebaseUser.Uid;
                await _unitOfWork.Resolve<Account>().UpdateAsync(account);
                await _unitOfWork.SaveChangesAsync();

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
