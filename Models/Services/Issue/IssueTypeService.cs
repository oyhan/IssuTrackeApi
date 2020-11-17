using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Specifications;
using Microsoft.EntityFrameworkCore;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public class IssueTypeService : IIssueTypeService
    {
        private readonly IAsyncRepository<IssueTypeModel,int> _repository;
        public DbContext _dbContext { get => _repository._dbContext; set => throw new NotImplementedException(); }

        public IssueTypeService(IAsyncRepository<IssueTypeModel,int> repository)
        {
            _repository = repository;
        }


        public async Task<IssueTypeModel> GetByIdAsync(int id)
        {
            _dbContext.Set<IssueTypeModel>().Include(s=>s.IssueUsers).ThenInclude(s=>s.User);
            return await _repository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<IssueTypeModel>> GetAllAsync()
        {
            return _repository.GetAllAsync();

        }

        public Task<IReadOnlyList<IssueTypeModel>> ListAsync(params ISpecification<IssueTypeModel>[] spec)
        {
            return _repository.ListAsync(spec);

        }

        public Task<IssueTypeModel> AddAsync(IssueTypeModel entity)
        {

            return _repository.AddAsync(entity);

        }

        public Task UpdateAsync(IssueTypeModel entity)
        {
            var users= _dbContext.Set<IssueTypeModelUserModel>().Where(m => m.IssueTypeId == entity.Id);

            foreach (var user in users)
            {
                _dbContext.Remove(user);
            }
            foreach (var user in entity.IssueUsers)
            {
                _dbContext.Add(user);
            }

         
            foreach (var property in entity.Propertys)
            {

                _dbContext.Add(property);
            }

            return _repository.UpdateAsync(entity);


        }

      
        public Task DeleteAsync(IssueTypeModel entity)
        {
            return _repository.DeleteAsync(entity);

        }

        public Task DeleteByIdAsync(int id)
        {
            return _repository.DeleteByIdAsync(id);

        }

        public Task<int> CountAsync(params ISpecification<IssueTypeModel>[] spec)
        {
            return _repository.CountAsync(spec);

        }

        public Task<int> ApplyChangesAsync()
        {
            return _repository.ApplyChangesAsync();
        }

        public Task<IssueTypeModel> GetByIdAsync(int id, Expression<Func<IssueTypeModel, object>> includes)
        {
            return _repository.GetByIdAsync(id, includes);
        }

        public Task<IssueTypeModel> GetByIdAsync(int id, params ISpecification<IssueTypeModel>[] spec)
        {
            return _repository.GetByIdAsync(id, spec);

        }

        public Task UpdateAsync(IssueTypeModel entity, List<IssueTypePropertyModel> removedAttributes)
        {
            var users = _dbContext.Set<IssueTypeModelUserModel>().Where(m => m.IssueTypeId == entity.Id);

            foreach (var user in users)
            {
                _dbContext.Remove(user);
            }
            foreach (var user in entity.IssueUsers)
            {
                user.IssueTypeId = entity.Id;
                _dbContext.Add(user);
            }


            foreach (var property in entity.Propertys)
            {
                //edited props
                if (property.Id!=0)
                {
                    _dbContext.Update(property);
                }
                //added props 
                else
                {
                    _dbContext.Add(property);

                }
                
            }

            foreach (var prop in removedAttributes)
            {
                _dbContext.Remove(prop);
            }
            return _repository.UpdateAsync(entity);
        }
    }

}
