using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text;

namespace AmazedDataContext.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    public class MongoDbContext
    {
        private MongoDbContextOptionsBuilder MongoDbContextOptionsBuilder;
        /// <summary>
        /// 
        /// </summary>
        public MongoDbContext()
        {
            OnModelCreating(MongoDbContextOptionsBuilder);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public virtual void OnConfiguring(MongoDbContextOptionsBuilder optionsBuilder)
        {
            MongoDbContextOptionsBuilder = optionsBuilder;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual MongoClient GetMongoClient()
        {
            return MongoDbContextOptionsBuilder.MongoClient;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IMongoDatabase GetMongoDatabase()
        {
            return MongoDbContextOptionsBuilder.MongoDatabase;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        public virtual void OnModelCreating(MongoDbContextOptionsBuilder optionsBuilder)
        {
            
            
        }
    }
}
