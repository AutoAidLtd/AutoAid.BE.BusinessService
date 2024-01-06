
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Infrastructure.Repository.Helper
{
    public class QueryHelper<TEntity> where TEntity : class
    {
        public virtual Expression<Func<TEntity, TEntity>>? Selector { get; set; } = null;
        public Expression<Func<TEntity, bool>>? Filter { get; set; } = null;
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; set; } = null;
        public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? Include { get; set; } = null;
        public string[]? OrderByFields { get; set; } = null;
    }
    public class QueryHelper<TEntity, TResult> : QueryHelper<TEntity> 
        where TResult : class 
        where TEntity : class
    {
        public new Expression<Func<TEntity, TResult>>? Selector { get; set; } = null;
    }

}
