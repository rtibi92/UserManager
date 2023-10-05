using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.DataAccess.UnitOfWork.Interface;

namespace UserManager.DataAccess.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly UserManagerDbContext _dbContext;


        public UnitOfWork(UserManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
