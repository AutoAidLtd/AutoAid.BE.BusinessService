using AutoAid.Domain.Model;

namespace AutoAid.Application.Repository
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Task<Account> GetAccountByFirebaseUID(string uid);
    }
}
