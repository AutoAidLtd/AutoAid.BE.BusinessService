using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;
using System.Xml;

namespace AutoAid.Infrastructure.Repository.Helper
{
    /// <summary>
    /// There are some helper methods for generic repository 
    /// </summary>
    public static class QueryHelpers
    {
        /// <summary>
        /// Require: QueryHelper<TEntity> queryHelper
        /// 
        /// Apply conditions for query helper (AsNoTracking) 
        /// 
        /// AddExistCondition -> ApplyIncludes -> WhereCondition -> SelectField
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="queryHelper"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ToQueryable<TEntity>(this DbSet<TEntity> dbSet,
            QueryHelper<TEntity> queryHelper)
            where TEntity : class
        {
            if (dbSet == null)
                throw new ArgumentNullException(nameof(dbSet));

            queryHelper.Filter = queryHelper.Filter.AddExistCondition();

            IQueryable<TEntity> query = dbSet.AsNoTracking();

            return query.ApplyIncludes(queryHelper)
                        .AsSplitQuery()
                        .WhereCondition(queryHelper.Filter);
        }

        /// <summary>
        /// Require: QueryHelper<TEntity, TResult> queryHelper
        /// 
        /// Apply conditions for query helper (AsNoTracking)
        /// 
        /// AddExistCondition -> ApplyIncludes -> WhereCondition -> SelectField
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="queryHelper"></param>
        /// <returns></returns>
        public static IQueryable<TResult> ToQueryable<TEntity, TResult>(this DbSet<TEntity> dbSet,
            QueryHelper<TEntity, TResult> queryHelper,
            Guid? id = null)
            where TEntity : class where TResult : class
        {
            if (dbSet == null)
                throw new ArgumentNullException(nameof(dbSet));

            queryHelper.Filter = queryHelper.Filter.AddExistCondition();

            IQueryable<TEntity> query = dbSet.AsNoTracking();

            return query.ApplyIncludes(queryHelper)
                        .AsSplitQuery()
                        .Where(queryHelper.Filter)
                        .SelectField(queryHelper.Selector);
        }

        private static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query, QueryHelper<T> queryHelpers) where T : class
        {
            if (queryHelpers.Include == null)
                return query;

            return queryHelpers.Include(query);
        }

        /// <summary>
        /// Add where condition for query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="whereCondition"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static IQueryable<T> WhereCondition<T>(this IQueryable<T> query, Expression<Func<T, bool>>? whereCondition) where T : class
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            return query;
        }

        /// <summary>
        /// Add select field for query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static IQueryable<TResult> SelectField<TSource, TResult>(this IQueryable<TSource> query,
            Expression<Func<TSource, TResult>>? selector)
            where TSource : class
            where TResult : class
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (selector == null)
                return query.Select(CreateProjection<TSource, TResult>());

            return query.Select(selector);
        }

        /// <summary>
        /// Add select field for query by selected selectedFields
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="selectedFields"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        private static IQueryable<T> SelectField<T>(this IQueryable<T> query, string[] selectedFields)
            where T : class
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            if (selectedFields != null && selectedFields.Length > 0)
            {
                query = query.Select(CreateProjection<T>(selectedFields));
            }

            return query;
        }

        /// <summary>
        /// Add condition IsDeleted = false for query
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> AddExistCondition<TEntity>(this Expression<Func<TEntity, bool>>? filter)
            where TEntity : class
        {
            PropertyInfo? isDeletedProperty = typeof(TEntity).GetProperty("IsDeleted");

            if (isDeletedProperty == null)
                throw new ArgumentNullException($"Entity {typeof(TEntity).Name} has not IsDeleted property");

            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "isDeleted");
            MemberExpression isDeletedPropertyAccess = Expression.Property(parameter, isDeletedProperty);
            ConstantExpression isDeleted_is_false = Expression.Constant(false);
            BinaryExpression equalityExpression = Expression.Equal(isDeletedPropertyAccess, isDeleted_is_false);
            Expression<Func<TEntity, bool>> isNotDeleteCondition = Expression.Lambda<Func<TEntity, bool>>(equalityExpression, parameter);

            return filter == null
                ? isNotDeleteCondition
                : PredicateBuilder.And(isNotDeleteCondition, filter);
        }

        #region Dynamic query extension
        /// <summary>
        /// Function create expression for select field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="selectedFields"></param>
        /// <returns>Expression of enity, but just get fields in selectedFields</returns>
        private static Expression<Func<T, T>> CreateProjection<T>(string[] selectedFields)
        {
            return entity => CreateProjectedInstance(entity, selectedFields);
        }
        /// <summary>
        /// Function create expression for select field
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns>Expression of mapping between TEntity and TResult (Enity - DTO)</returns>
        private static Expression<Func<TEntity, TResult>> CreateProjection<TEntity, TResult>()
            where TEntity : class
            where TResult : class
        {
            var properties = typeof(TResult).GetProperties();
            return entity => CreateProjectedInstance<TEntity, TResult>(entity, properties);
        }

        private static TResult CreateProjectedInstance<TSource, TResult>(TSource source, PropertyInfo[]? resultProperties)
        {
            if (resultProperties == null)
            {
                throw new ArgumentNullException(nameof(resultProperties));
            }

            var resultInstance = Activator.CreateInstance<TResult>();

            var sourceProperties = typeof(TSource).GetProperties();

            foreach (var resultProperty in resultProperties)
            {
                if (resultProperty == null)
                {
                    continue;
                }

                var sourceProperty = sourceProperties.FirstOrDefault(p => p.Name == resultProperty.Name);

                if (sourceProperty != null)
                {
                    var sourceValue = sourceProperty.GetValue(source);
                    resultProperty.SetValue(resultInstance, sourceValue);
                }
            }

            return resultInstance;
        }

        private static T CreateProjectedInstance<T>(T source, string[] selectedFields)
        {
            var projectedInstance = Activator.CreateInstance<T>();

            var properties = typeof(T).GetProperties();

            foreach (var field in selectedFields)
            {
                var property = properties.FirstOrDefault(p => p.Name == field);

                if (property != null)
                {
                    var value = property.GetValue(source);
                    property.SetValue(projectedInstance, value);
                }
            }

            return projectedInstance;
        }

        private static IQueryable<TEntity> CrateIOrderedQueryable<TEntity>(this IQueryable<TEntity> query, string propertyName, bool isAscOrder = true, bool isOrderBy = true)
        {
            // Use reflection to get property info by name
            PropertyInfo? propertyInfo = typeof(TEntity).GetProperty(propertyName);

            if (propertyInfo == null)
            {
                throw new ArgumentException($"Property '{propertyName}' not found in type {typeof(TEntity).Name}");
            }

            // Build the sorting expression
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "x");
            MemberExpression propertyExpression = Expression.Property(parameter, propertyInfo);
            LambdaExpression orderByExpression = Expression.Lambda(propertyExpression, parameter);

            // Create a generic method for OrderBy or OrderByDescending
            MethodInfo orderByMethod;

            if (isOrderBy)
            {
                orderByMethod = isAscOrder
                    ? typeof(Queryable).GetMethods().First(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                    : typeof(Queryable).GetMethods().First(m => m.Name == "OrderByDescending" && m.GetParameters().Length == 2);
            }
            else
            {
                orderByMethod = isAscOrder
                    ? typeof(Queryable).GetMethods().First(m => m.Name == "ThenBy" && m.GetParameters().Length == 2)
                    : typeof(Queryable).GetMethods().First(m => m.Name == "ThenByDescending" && m.GetParameters().Length == 2);
            }


            MethodInfo orderByGeneric = orderByMethod.MakeGenericMethod(typeof(TEntity), propertyInfo.PropertyType);

            // Use the dynamic expression with OrderBy or OrderByDescending
            var orderedQuery = orderByGeneric.Invoke(null, new object[] { query.AsQueryable(), orderByExpression });

            return (IQueryable<TEntity>)(orderedQuery ??= new object());
        }
        #endregion Dynamic query extension
    }
}
