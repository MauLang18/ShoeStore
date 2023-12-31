﻿using Microsoft.EntityFrameworkCore;
using ShoeStore.Infrastructure.Helpers;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly SHOESTOREContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(SHOESTOREContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity
                .Where(x => x.Estado.Equals((int)StateTypes.Activo) && x.FechaEliminacionAuditoria == null && x.UsuarioEliminacionAuditoria == null)
                .AsNoTracking()
                .ToListAsync();

            return getAll;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity!
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null);

            return getById!;
        }

        public async Task<bool> RegisterAsync(T entity)
        {
            entity.UsuarioCreacionAuditoria = 1;
            entity.FechaCreacionAuditoria = DateTime.Now;

            await _context.AddAsync(entity);

            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public async Task<bool> EditAsync(T entity)
        {
            entity.UsuarioActualizacionAuditoria = 1;
            entity.FechaActualizacionAuditoria = DateTime.Now;

            _context.Update(entity);
            _context.Entry(entity).Property(x => x.UsuarioCreacionAuditoria).IsModified = false;
            _context.Entry(entity).Property(x => x.FechaCreacionAuditoria).IsModified = false;

            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            entity!.UsuarioEliminacionAuditoria = 1;
            entity.FechaEliminacionAuditoria = DateTime.Now;

            _context.Update(entity);

            var recordsAffected = await _context.SaveChangesAsync();

            return recordsAffected > 0;
        }

        public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;

            if (filter != null) query = query.Where(filter);

            return query;
        }

        public IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} descending") : queryable.OrderBy($"{request.Sort} ascending");

            if (pagination) queryDto = queryDto.Paginate(request);

            return queryDto;
        }
    }
}