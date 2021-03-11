using DNBMarket.BaseLogic.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DNBMarket.BaseLogic.Data.Implementation
{
    public class EntityRepositoryBase<TEntity> : IEntityRepositoryBase<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly DbContext _ctx;
        public EntityRepositoryBase(DbContext ctx)
        {
            _ctx = ctx;
        }
        public TEntity Add(TEntity entity)
        {
            _ctx.Set<TEntity>().Add(entity);
            return entity;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _ctx.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public TEntity AddOrUpdate(TEntity entity, int key = 0)
        {
            if (entity == null)
                return null;
            if (key > 0)
            {
                TEntity existing = _ctx.Set<TEntity>().Find(key);
                if (existing != null)
                    ProcessEntityControl();
                _ctx.Entry(existing).CurrentValues.SetValues(entity);
                return existing;
            }
            else
            {
                ProcessEntityControl();
                _ctx.Set<TEntity>().Add(entity);
                return entity;
            }
        }
        public async Task<TEntity> AddOrUpdateAsync(TEntity entity, int key = 0)
        {
            if (entity == null)
                return null;
            if (key > 0)
            {
                TEntity existing = await _ctx.Set<TEntity>().FindAsync(key);
                if (existing != null)
                    ProcessEntityControl();
                _ctx.Entry(existing).CurrentValues.SetValues(entity);
                return existing;
            }
            else
            {
                ProcessEntityControl();
                _ctx.Set<TEntity>().Add(entity);
                return entity;
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _ctx.Set<TEntity>().Where(x => x.Status != -1).AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _ctx.Set<TEntity>().CountAsync(x => x.Status == 1);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return query.Where(x => x.Status != -1).FirstOrDefault();
        }

        public TEntity FirstOrDefaultPassive(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return query.FirstOrDefault();
        }

        public async Task<TEntity> FirtsOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return await query.Where(x => x.Status != -1).FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirtsOrDefaultPassiveAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public Task<int> GetLastId(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task SoftDelete(TEntity entity)
        {
            await Task.Run(() => { _ctx.Set<TEntity>().Remove(entity); });
        }

        public async Task<IList<TEntity>> ToListAsync(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate).Where(x => x.Status != -1);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }

        public async Task<List<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return await query.Where(x => x.Status != -1).ToListAsync();
        }
        public async Task<List<TEntity>> WherePassive(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _ctx.Set<TEntity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }
        private void ProcessEntityControl()
        {
            var auditEntities = _ctx.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).Select(x => new { x.Entity, x.State });
            if (auditEntities != null)
            {
                foreach (var obj in auditEntities)
                {
                    var auditEntity = obj.Entity;
                    var properties = auditEntities.GetType()
                                                  .GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public)
                                                  .Where(x => x.CanWrite).ToDictionary(x => x.Name, x => x);
                    if (obj.State == EntityState.Added || obj.State == EntityState.Detached)
                    {
                        if (properties.ContainsKey("CreateDate"))
                            properties["CreateDate"].SetValue(auditEntity, DateTime.Now);
                    }
                    if (properties.ContainsKey("UpdatedDate"))
                        properties["UpdateDate"].SetValue(auditEntity, DateTime.Now);
                }
            }
        }
    }
}
