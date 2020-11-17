using Asanobat.IssueTracker.Models.Entity.Issue;
using Microsoft.EntityFrameworkCore;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public class IssueTypePropertyService : IIssueTypePropertyService
    {
        private readonly IAsyncRepository<IssueTypePropertyModel, int> _repository;
        public DbContext _dbContext { get => _repository._dbContext; set => throw new NotImplementedException(); }

        public IssueTypePropertyService(IAsyncRepository<IssueTypePropertyModel, int> repository)
        {
            _repository = repository;
        }


        public Task<IssueTypePropertyModel> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<IssueTypePropertyModel>> GetAllAsync()
        {
            return _repository.GetAllAsync();

        }

        public Task<IReadOnlyList<IssueTypePropertyModel>> ListAsync(params ISpecification<IssueTypePropertyModel>[] spec)
        {
            return _repository.ListAsync(spec);

        }

        public Task<IssueTypePropertyModel> AddAsync(IssueTypePropertyModel entity)
        {

            return _repository.AddAsync(entity);

        }

        public Task UpdateAsync(IssueTypePropertyModel entity)
        {
            return _repository.UpdateAsync(entity);

        }


        public Task DeleteAsync(IssueTypePropertyModel entity)
        {
            return _repository.DeleteAsync(entity);

        }

        public Task DeleteByIdAsync(int id)
        {
            return _repository.DeleteByIdAsync(id);

        }

        public Task<int> CountAsync(params ISpecification<IssueTypePropertyModel>[] spec)
        {
            return _repository.CountAsync(spec);

        }

        public Task<int> ApplyChangesAsync()
        {
            return _repository.ApplyChangesAsync();
        }

        public Task<IssueTypePropertyModel> GetByIdAsync(int id, Expression<Func<IssueTypePropertyModel, object>> includes)
        {
            return _repository.GetByIdAsync(id, includes);

        }

        public Task<IssueTypePropertyModel> GetByIdAsync(int id, params ISpecification<IssueTypePropertyModel>[] spec)
        {
            return _repository.GetByIdAsync(id, spec);

        }
    }
}
