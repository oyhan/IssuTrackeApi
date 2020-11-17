using Asanobat.IssueTracker.Models.Entity.Message;
using Microsoft.EntityFrameworkCore;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services.Message
{
    public class MessageService : IMessageService
    {
        private readonly IAsyncRepository<MessageModel, int> _repository;
        public DbContext _dbContext { get => _repository._dbContext; set => throw new NotImplementedException(); }

        public MessageService(IAsyncRepository<MessageModel, int> repository)
        {
            _repository = repository;
        }


        public Task<MessageModel> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<MessageModel>> GetAllAsync()
        {
            return _repository.GetAllAsync();

        }

        public Task<IReadOnlyList<MessageModel>> ListAsync(params ISpecification<MessageModel>[] spec)
        {
            return _repository.ListAsync(spec);

        }

        public Task<MessageModel> AddAsync(MessageModel entity)
        {

            return _repository.AddAsync(entity);

        }

        public Task UpdateAsync(MessageModel entity)
        {
            return _repository.UpdateAsync(entity);

        }


        public Task DeleteAsync(MessageModel entity)
        {
            return _repository.DeleteAsync(entity);

        }

        public Task DeleteByIdAsync(int id)
        {
            return _repository.DeleteByIdAsync(id);

        }

        public Task<int> CountAsync(params ISpecification<MessageModel>[] spec)
        {
            return _repository.CountAsync(spec);

        }

        public Task<int> ApplyChangesAsync()
        {
            return _repository.ApplyChangesAsync();
        }

        public Task<MessageModel> GetByIdAsync(int id, Expression<Func<MessageModel, object>> includes)
        {
            return _repository.GetByIdAsync(id, includes);

        }

        public Task<MessageModel> GetByIdAsync(int id, params ISpecification<MessageModel>[] spec)
        {
            return _repository.GetByIdAsync(id, spec);

        }
    }
}