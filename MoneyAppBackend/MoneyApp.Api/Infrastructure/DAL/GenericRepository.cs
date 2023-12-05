﻿using Microsoft.Extensions.Options;
using MoneyApp.Api.Config;
using MongoDB.Driver;

namespace MoneyApp.Api.Infrastructure.DAL
{
    public class GenericRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        private readonly IOptions<DbConfiguration> _dbConfig;

        public GenericRepository(IOptions<DbConfiguration> dbConfig, string collectionName)
        {
            _dbConfig = dbConfig;
            var mongoClient = new MongoClient(_dbConfig.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_dbConfig.Value.DatabaseName);
            _collection = mongoDatabase.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAll()
        {
            return await _collection.Find(_  => true).ToListAsync();
        }
    }
}
