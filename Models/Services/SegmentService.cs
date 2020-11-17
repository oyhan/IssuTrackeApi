using Asanobat.IssueTracker.Models.Entity;
using Microsoft.EntityFrameworkCore;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public class SegmentService : ISegmentService
    {
        private readonly IAsyncRepository<SegmentModel,int> _repository;
        public DbContext _dbContext { get => _repository._dbContext; set => throw new NotImplementedException(); }

        public SegmentService(IAsyncRepository<SegmentModel,int> repository)
        {
            _repository = repository;
        }


        public Task<SegmentModel> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<SegmentModel>> GetAllAsync()
        {
            return _repository.GetAllAsync();

        }

        public Task<IReadOnlyList<SegmentModel>> ListAsync(params ISpecification<SegmentModel>[] spec)
        {
            return _repository.ListAsync(spec);

        }

        public Task<SegmentModel> AddAsync(SegmentModel entity)
        {

            return _repository.AddAsync(entity);

        }

        public Task UpdateAsync(SegmentModel entity)
        {
            return _repository.UpdateAsync(entity);

        }

      
        public Task DeleteAsync(SegmentModel entity)
        {
            return _repository.DeleteAsync(entity);

        }

        public Task DeleteByIdAsync(int id)
        {
            return _repository.DeleteByIdAsync(id);

        }

        public Task<int> CountAsync(params ISpecification<SegmentModel>[] spec)
        {
            return _repository.CountAsync(spec);

        }

        public Task<int> ApplyChangesAsync()
        {
            return _repository.ApplyChangesAsync();
        }

        public Task<SegmentModel> GetByIdAsync(int id, Expression<Func<SegmentModel, object>> includes)
        {
            return _repository.GetByIdAsync(id, includes);

        }

        public Task<SegmentModel> GetByIdAsync(int id, params ISpecification<SegmentModel>[] spec)
        {
            return _repository.GetByIdAsync(id, spec);

        }
    }

}
