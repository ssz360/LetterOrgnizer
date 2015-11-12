//-----------------------------------------------------------------------
// <copyright file="IRepository.cs" company="ssz">>
//     Copyright 2015.
// </copyright>
// <author>SSZ</author>
//-----------------------------------------------------------------------
namespace SSZ.Accounting.DAL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using SSZ.Accounting.Entities;

    /// <summary>
    /// The repository interface
    /// </summary>
    /// <typeparam name="T">T should be BaseModel</typeparam>
    public interface IRepository<T> where T : BaseModel
    {
        /// <summary>
        /// Inserts a single entity into the DbSet
        /// </summary>
        /// <param name="entity">The entity.</param>
        T Insert(T entity);

        /// <summary>
        /// Returns an entity based on primary key.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        /// <returns>
        /// The found instance
        /// </returns>
        T GetById(int id);

        /// <summary>
        /// Returns an IEnumerable based on the query, order clause and the properties included
        /// </summary>
        /// <param name="query">Link query for filtering.</param>
        /// <param name="orderBy">Link query for sorting.</param>
        /// <param name="includeProperties">Navigation properties separated by comma for eager loading.</param>
        /// <returns>IEnumerable containing the resulting entity set.</returns>
        IEnumerable<T> GetByQuery(
                    Expression<Func<T, bool>> query = null,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                    string includeProperties = "");

        /// <summary>
        /// Returns the first matching entity based on the query.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>
        /// The first match
        /// </returns>
        T GetFirst(Expression<Func<T, bool>> predicate);


        IEnumerable<T> GetAll();

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">
        /// The Entity
        /// </param>
        void Update(T entity);

        /// <summary>
        /// Updates an entity by primary key.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        void UpdateById(int id);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="entity">
        /// The Entity
        /// </param>
        T Delete(T entity);

        /// <summary>
        /// Deletes an entity by primary key.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        T DeleteByID(int id);
    }
}
