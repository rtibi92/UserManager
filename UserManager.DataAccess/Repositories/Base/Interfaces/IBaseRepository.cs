using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.DataAccess.Repositories.Base.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        void Remove(TEntity entityToDelete);

        void Add(TEntity entity);
        void Update(TEntity entityToUpdate);

        IEnumerable<TEntity> GetAll();
    }
}
