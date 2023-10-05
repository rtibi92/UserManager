using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManager.DataAccess.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
       // IUserRepository BooksRepository { get; }

        Task CompleteAsync();
    }
}
