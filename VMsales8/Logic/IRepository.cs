using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace VMSales8.Logic
{

    public interface IRepository<T> : IDisposable where T : class
    {

        private string GetTableName()
        {
            // Get all attributes of type TableAttribute
            var tableAttribute = (TableAttribute)typeof(T)
                .GetCustomAttributes(typeof(TableAttribute), true)
                .FirstOrDefault();

            if (tableAttribute != null)
            {
                return tableAttribute.Name;
            }
            else
            {
                throw new InvalidOperationException($"The class {typeof(T).Name} does not have a [Table] attribute.");
            }
        }

        Task<int> Insert(T entity);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithID(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        void Commit();
        void Revert();
    }
}