using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/23 10:35:43；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Mongo
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public abstract class DbSet<TEntity> : IMongoCollection<TEntity> where TEntity : class
    {
        public abstract CollectionNamespace CollectionNamespace { get; }
        public abstract IMongoDatabase Database { get; }
        public abstract IBsonSerializer<TEntity> DocumentSerializer { get; }
        public abstract IMongoIndexManager<TEntity> Indexes { get; }
        public abstract MongoCollectionSettings Settings { get; }

        public abstract IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);
        public abstract IAsyncCursor<TResult> Aggregate<TResult>(IClientSessionHandle session, PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(IClientSessionHandle session, PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);
        public abstract void AggregateToCollection<TResult>(PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);
        public abstract void AggregateToCollection<TResult>(IClientSessionHandle session, PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task AggregateToCollectionAsync<TResult>(PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task AggregateToCollectionAsync<TResult>(IClientSessionHandle session, PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default);
        public abstract BulkWriteResult<TEntity> BulkWrite(IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default);
        public abstract BulkWriteResult<TEntity> BulkWrite(IClientSessionHandle session, IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<BulkWriteResult<TEntity>> BulkWriteAsync(IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<BulkWriteResult<TEntity>> BulkWriteAsync(IClientSessionHandle session, IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default);
        public abstract long Count(FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default);
        public abstract long Count(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<long> CountAsync(FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<long> CountAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default);
        public abstract long CountDocuments(FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default);
        public abstract long CountDocuments(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<long> CountDocumentsAsync(FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<long> CountDocumentsAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default);
        public abstract DeleteResult DeleteMany(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default);
        public abstract DeleteResult DeleteMany(FilterDefinition<TEntity> filter, DeleteOptions options, CancellationToken cancellationToken = default);
        public abstract DeleteResult DeleteMany(IClientSessionHandle session, FilterDefinition<TEntity> filter, DeleteOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default);
        public abstract Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter, DeleteOptions options, CancellationToken cancellationToken = default);
        public abstract Task<DeleteResult> DeleteManyAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, DeleteOptions options = null, CancellationToken cancellationToken = default);
        public abstract DeleteResult DeleteOne(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default);
        public abstract DeleteResult DeleteOne(FilterDefinition<TEntity> filter, DeleteOptions options, CancellationToken cancellationToken = default);
        public abstract DeleteResult DeleteOne(IClientSessionHandle session, FilterDefinition<TEntity> filter, DeleteOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default);
        public abstract Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter, DeleteOptions options, CancellationToken cancellationToken = default);
        public abstract Task<DeleteResult> DeleteOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, DeleteOptions options = null, CancellationToken cancellationToken = default);
        public abstract IAsyncCursor<TField> Distinct<TField>(FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter, DistinctOptions options = null, CancellationToken cancellationToken = default);
        public abstract IAsyncCursor<TField> Distinct<TField>(IClientSessionHandle session, FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter, DistinctOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter, DistinctOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<IAsyncCursor<TField>> DistinctAsync<TField>(IClientSessionHandle session, FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter, DistinctOptions options = null, CancellationToken cancellationToken = default);
        public abstract long EstimatedDocumentCount(EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<long> EstimatedDocumentCountAsync(EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract TProjection FindOneAndDelete<TProjection>(FilterDefinition<TEntity> filter, FindOneAndDeleteOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract TProjection FindOneAndDelete<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, FindOneAndDeleteOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<TEntity> filter, FindOneAndDeleteOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract Task<TProjection> FindOneAndDeleteAsync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, FindOneAndDeleteOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract TProjection FindOneAndReplace<TProjection>(FilterDefinition<TEntity> filter, TEntity replacement, FindOneAndReplaceOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract TProjection FindOneAndReplace<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, FindOneAndReplaceOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<TEntity> filter, TEntity replacement, FindOneAndReplaceOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract Task<TProjection> FindOneAndReplaceAsync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, FindOneAndReplaceOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract TProjection FindOneAndUpdate<TProjection>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract TProjection FindOneAndUpdate<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract Task<TProjection> FindOneAndUpdateAsync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract IAsyncCursor<TProjection> FindSync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default);
        public abstract void InsertMany(IEnumerable<TEntity> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default);
        public abstract void InsertMany(IClientSessionHandle session, IEnumerable<TEntity> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task InsertManyAsync(IEnumerable<TEntity> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task InsertManyAsync(IClientSessionHandle session, IEnumerable<TEntity> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default);
        public abstract void InsertOne(TEntity document, InsertOneOptions options = null, CancellationToken cancellationToken = default);
        public abstract void InsertOne(IClientSessionHandle session, TEntity document, InsertOneOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task InsertOneAsync(TEntity document, CancellationToken _cancellationToken);
        public abstract Task InsertOneAsync(TEntity document, InsertOneOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task InsertOneAsync(IClientSessionHandle session, TEntity document, InsertOneOptions options = null, CancellationToken cancellationToken = default);
        public abstract IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TEntity, TResult> options = null, CancellationToken cancellationToken = default);
        public abstract IAsyncCursor<TResult> MapReduce<TResult>(IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TEntity, TResult> options = null, CancellationToken cancellationToken = default);
        public abstract Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TEntity, TResult> options = null, CancellationToken cancellationToken = default);
        public abstract Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TEntity, TResult> options = null, CancellationToken cancellationToken = default);
        public abstract IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>() where TDerivedDocument : TEntity;
        public abstract ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default);
        public abstract ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options, CancellationToken cancellationToken = default);
        public abstract ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default);
        public abstract ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options, CancellationToken cancellationToken = default);
        public abstract Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options, CancellationToken cancellationToken = default);
        public abstract Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options, CancellationToken cancellationToken = default);
        public abstract UpdateResult UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
        public abstract UpdateResult UpdateMany(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<UpdateResult> UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<UpdateResult> UpdateManyAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
        public abstract UpdateResult UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
        public abstract UpdateResult UpdateOne(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<UpdateResult> UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<UpdateResult> UpdateOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default);
        public abstract IChangeStreamCursor<TResult> Watch<TResult>(PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default);
        public abstract IChangeStreamCursor<TResult> Watch<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default);
        public abstract Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default);
        public abstract IMongoCollection<TEntity> WithReadConcern(ReadConcern readConcern);
        public abstract IMongoCollection<TEntity> WithReadPreference(ReadPreference readPreference);
        public abstract IMongoCollection<TEntity> WithWriteConcern(WriteConcern writeConcern);
    }
}
