using AutoAid.Infrastructure.Repository.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoAid.Domain.RepositoryHelper
{
    public class SearchQueryHelper<TEntity> : QueryHelper<TEntity> where TEntity : class
    {
        public PagingQuery PagingQuery { get; set; } = new PagingQuery();
    }

    public class SearchQueryHelper<TEntity, TSource> : QueryHelper<TEntity, TSource> 
        where TEntity : class
        where TSource : class
    {
        public PagingQuery PagingQuery { get; set; } = new PagingQuery();
    }
}
