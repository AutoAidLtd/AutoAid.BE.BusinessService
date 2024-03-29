﻿using AutoAid.Application.Repository;
using AutoAid.Domain.Common.PagedList;
using AutoAid.Domain.Model;
using AutoAid.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;

namespace AutoAid.Infrastructure.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(DbContext context) : base(context)
        {
        }

        public async Task<Account> GetAccountByFirebaseUID(string uid)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.FirebaseUid == uid);
        }

        public override Task<IPagedList<Account>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }
    }
}
