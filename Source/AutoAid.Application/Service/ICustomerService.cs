using AutoAid.Domain.Common;
using AutoAid.Domain.Dto.Customer;

namespace AutoAid.Application.Service
{
    public interface ICustomerService
    {
        public Task<ApiResponse<bool>> CreateCustomer(CreateCustomerReq req);
    }
}
