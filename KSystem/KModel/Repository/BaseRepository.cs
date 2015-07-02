using KModel.Entity;
using KModel.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KModel.Repository
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public KBaseEntities _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(KBaseEntities context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}
