using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Props

        protected readonly DatabaseContext _context;

        protected DbSet<TEntity> DbSet
        {
            get
            {
                return _context.Set<TEntity>();
            }
        }

        #endregion

        #region Constructor

        public Repository(DatabaseContext context)
        {
            _context = context;
        }

        #endregion

        #region CRUD

        public TEntity Create(TEntity model)
        {
            try
            {
                if (model is BaseEntity)
                    (model as BaseEntity).CreatedOn = DateTime.Now;

                DbSet.Add(model);
                Save();
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Create Many
        public List<TEntity> Create(List<TEntity> models)
        {
            try
            {
                DbSet.AddRange(models);
                Save();
                return models;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Update(TEntity model)
        {
            try
            {
                if (model is BaseEntity)
                    (model as BaseEntity).UpdatedOn = DateTime.Now;

                EntityEntry<TEntity> entry = SetEntry(model);

                DbSet.Attach(model);

                entry.State = EntityState.Modified;

                return Save() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private EntityEntry<TEntity> SetEntry(TEntity model)
        {
            return _context.Entry(model);
        }

        public bool Update(List<TEntity> models)
        {
            try
            {
                foreach (TEntity register in models)
                {
                    if(register is BaseEntity)
                        (register as BaseEntity).UpdatedOn = DateTime.Now;

                    EntityEntry<TEntity> entry = _context.Entry(register);
                    DbSet.Attach(register);
                    entry.State = EntityState.Modified;
                }

                return Save() > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(TEntity model)
        {
            try
            {
                if (model is BaseEntity)
                {
                    (model as BaseEntity).IsActive = false;
                    (model as BaseEntity).UpdatedOn = DateTime.Now;
                    EntityEntry<TEntity> _entry = _context.Entry(model);

                    DbSet.Attach(model);

                    _entry.State = EntityState.Modified;
                }
                else
                {
                    EntityEntry<TEntity> _entry = _context.Entry(model);
                    DbSet.Attach(model);
                    _entry.State = EntityState.Deleted;
                }

                return Save() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(params object[] Keys)
        {
            try
            {
                TEntity model = DbSet.Find(Keys);
                return (model != null) && Delete(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                TEntity model = DbSet.Where<TEntity>(where).FirstOrDefault<TEntity>();

                return (model != null) && Delete(model);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Finds

        public TEntity Find(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.AsNoTracking().FirstOrDefault(where);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return DbSet.Where(where);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region CloseConnection
        public void Dispose()
        {
            try
            {
                if (_context != null)
                    _context.Dispose();
                GC.SuppressFinalize(this);
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}
