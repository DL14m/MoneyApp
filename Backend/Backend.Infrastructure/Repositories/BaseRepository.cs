﻿using Backend.Core.Entities;
using Backend.Core.Repositories;
using Backend.Infrastructure.Data;
using MongoDB.Driver;

namespace Backend.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> collection;

        public BaseRepository(IDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            collection = context.GetCollection<T>(typeof(T).Name);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await collection.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var filter = Builders<T>.Filter.Eq(_ => _.Id, id);
            return await collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> InsertAsync(T entity)
        {
            await collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<bool> RemoveAsync(string id)
        {
            var result = await collection.DeleteOneAsync(Builders<T>.Filter.Eq(_ => _.Id, id));
            return result.DeletedCount > 0;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var filter = Builders<T>.Filter.Eq(_ => _.Id, entity.Id);
            var result = await collection.ReplaceOneAsync(filter, entity);
            return result.ModifiedCount > 0;
        }
    }
}