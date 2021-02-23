using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/23 11:02:26；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Mongo
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public class MongoDbSet<TEntity> : DbSet<TEntity> where TEntity : class
    {
        private readonly IMongoCollection<TEntity> MongoCollection;
        public MongoDbSet(IMongoDatabase MongoDatabase)
        {
            MongoCollection = MongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public override CollectionNamespace CollectionNamespace => MongoCollection.CollectionNamespace;

        public override IMongoDatabase Database => MongoCollection.Database;

        public override IBsonSerializer<TEntity> DocumentSerializer => MongoCollection.DocumentSerializer;

        public override IMongoIndexManager<TEntity> Indexes => MongoCollection.Indexes;

        public override MongoCollectionSettings Settings => MongoCollection.Settings;

        public override IAsyncCursor<TResult> Aggregate<TResult>(PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.Aggregate(pipeline, options, cancellationToken);
        }

        public override IAsyncCursor<TResult> Aggregate<TResult>(IClientSessionHandle session, PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.Aggregate(session, pipeline, options, cancellationToken);
        }

        public override Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.AggregateAsync(pipeline, options, cancellationToken);
        }

        public override Task<IAsyncCursor<TResult>> AggregateAsync<TResult>(IClientSessionHandle session, PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.AggregateAsync(session, pipeline, options, cancellationToken);
        }

        public override void AggregateToCollection<TResult>(PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            MongoCollection.AggregateToCollection(pipeline, options, cancellationToken);
        }

        public override void AggregateToCollection<TResult>(IClientSessionHandle session, PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            MongoCollection.AggregateToCollection(session, pipeline, options, cancellationToken);
        }

        public override Task AggregateToCollectionAsync<TResult>(PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.AggregateToCollectionAsync(pipeline, options, cancellationToken);
        }

        public override Task AggregateToCollectionAsync<TResult>(IClientSessionHandle session, PipelineDefinition<TEntity, TResult> pipeline, AggregateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.AggregateToCollectionAsync(session, pipeline, options, cancellationToken);
        }

        public override BulkWriteResult<TEntity> BulkWrite(IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.BulkWrite(requests, options, cancellationToken);
        }

        public override BulkWriteResult<TEntity> BulkWrite(IClientSessionHandle session, IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.BulkWrite(session, requests, options, cancellationToken);
        }

        public override Task<BulkWriteResult<TEntity>> BulkWriteAsync(IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.BulkWriteAsync( requests, options, cancellationToken);
        }

        public override Task<BulkWriteResult<TEntity>> BulkWriteAsync(IClientSessionHandle session, IEnumerable<WriteModel<TEntity>> requests, BulkWriteOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.BulkWriteAsync(session,requests, options, cancellationToken);
        }

        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override long Count(FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return MongoCollection.Count(filter, options, cancellationToken);
        }

        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override long Count(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return MongoCollection.Count(session, filter, options, cancellationToken);
        }

        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override async Task<long> CountAsync(FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return await MongoCollection.CountAsync(filter, options, cancellationToken).ConfigureAwait(false);
        }

        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override Task<long> CountAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return MongoCollection.CountAsync(session, filter, options, cancellationToken);
        }

        public override long CountDocuments(FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.CountDocuments(filter, options, cancellationToken);
        }

        public override long CountDocuments(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.CountDocuments(session,filter, options, cancellationToken);
        }

        public override Task<long> CountDocumentsAsync(FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.CountDocumentsAsync(filter, options, cancellationToken);
        }

        public override Task<long> CountDocumentsAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, CountOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.CountDocumentsAsync(session, filter, options, cancellationToken);
        }

        public override DeleteResult DeleteMany(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteMany(filter, cancellationToken);
        }

        public override DeleteResult DeleteMany(FilterDefinition<TEntity> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteMany(filter,options, cancellationToken);
        }

        public override DeleteResult DeleteMany(IClientSessionHandle session, FilterDefinition<TEntity> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteMany(session,filter, options, cancellationToken);
        }

        public override Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteManyAsync( filter, cancellationToken);
        }

        public override Task<DeleteResult> DeleteManyAsync(FilterDefinition<TEntity> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteManyAsync(filter, options, cancellationToken);
        }

        public override Task<DeleteResult> DeleteManyAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteManyAsync(session,filter, options, cancellationToken);
        }

        public override DeleteResult DeleteOne(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteOne( filter,  cancellationToken);
        }

        public override DeleteResult DeleteOne(FilterDefinition<TEntity> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteOne( filter, options, cancellationToken);
        }

        public override DeleteResult DeleteOne(IClientSessionHandle session, FilterDefinition<TEntity> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteOne(session, filter, options, cancellationToken);
        }

        public override Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteOneAsync( filter,  cancellationToken);
        }

        public override Task<DeleteResult> DeleteOneAsync(FilterDefinition<TEntity> filter, DeleteOptions options, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteOneAsync( filter, options, cancellationToken);
        }

        public override Task<DeleteResult> DeleteOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, DeleteOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DeleteOneAsync(session, filter, options, cancellationToken);
        }

        public override IAsyncCursor<TField> Distinct<TField>(FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.Distinct(field,filter, options, cancellationToken);
        }

        public override IAsyncCursor<TField> Distinct<TField>(IClientSessionHandle session, FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.Distinct(session,field, filter, options, cancellationToken);
        }

        public override Task<IAsyncCursor<TField>> DistinctAsync<TField>(FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DistinctAsync( field, filter, options, cancellationToken);
        }

        public override Task<IAsyncCursor<TField>> DistinctAsync<TField>(IClientSessionHandle session, FieldDefinition<TEntity, TField> field, FilterDefinition<TEntity> filter, DistinctOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.DistinctAsync(session, field, filter, options, cancellationToken);
        }

        public override long EstimatedDocumentCount(EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.EstimatedDocumentCount(options, cancellationToken);
        }

        public override Task<long> EstimatedDocumentCountAsync(EstimatedDocumentCountOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.EstimatedDocumentCountAsync(options, cancellationToken);
        }

        public override Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindAsync(filter,options, cancellationToken);
        }

        public override Task<IAsyncCursor<TProjection>> FindAsync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindAsync(session, filter, options, cancellationToken);
        }

        public override TProjection FindOneAndDelete<TProjection>(FilterDefinition<TEntity> filter, FindOneAndDeleteOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndDelete(filter, options, cancellationToken);
        }

        public override TProjection FindOneAndDelete<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, FindOneAndDeleteOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndDelete(session, filter, options, cancellationToken);
        }

        public override Task<TProjection> FindOneAndDeleteAsync<TProjection>(FilterDefinition<TEntity> filter, FindOneAndDeleteOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndDeleteAsync( filter, options, cancellationToken);
        }

        public override Task<TProjection> FindOneAndDeleteAsync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, FindOneAndDeleteOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndDeleteAsync(session,filter, options, cancellationToken);
        }

        public override TProjection FindOneAndReplace<TProjection>(FilterDefinition<TEntity> filter, TEntity replacement, FindOneAndReplaceOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndReplace( filter,replacement, options, cancellationToken);
        }

        public override TProjection FindOneAndReplace<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, FindOneAndReplaceOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndReplace(session,filter, replacement, options, cancellationToken);
        }

        public override Task<TProjection> FindOneAndReplaceAsync<TProjection>(FilterDefinition<TEntity> filter, TEntity replacement, FindOneAndReplaceOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndReplaceAsync(filter, replacement, options, cancellationToken);
        }

        public override Task<TProjection> FindOneAndReplaceAsync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, FindOneAndReplaceOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndReplaceAsync(session, filter, replacement, options, cancellationToken);
        }

        public override TProjection FindOneAndUpdate<TProjection>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndUpdate(filter, update, options, cancellationToken);
        }

        public override TProjection FindOneAndUpdate<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndUpdate(session,filter, update, options, cancellationToken);
        }

        public override Task<TProjection> FindOneAndUpdateAsync<TProjection>(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndUpdateAsync(filter, update, options, cancellationToken);
        }

        public override Task<TProjection> FindOneAndUpdateAsync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, FindOneAndUpdateOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindOneAndUpdateAsync(session,filter, update, options, cancellationToken);
        }

        public override IAsyncCursor<TProjection> FindSync<TProjection>(FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindSync(filter, options, cancellationToken);
        }

        public override IAsyncCursor<TProjection> FindSync<TProjection>(IClientSessionHandle session, FilterDefinition<TEntity> filter, FindOptions<TEntity, TProjection> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.FindSync(session,filter, options, cancellationToken);
        }

        public override void InsertMany(IEnumerable<TEntity> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            MongoCollection.InsertMany(documents,  options, cancellationToken);
        }

        public override void InsertMany(IClientSessionHandle session, IEnumerable<TEntity> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
            MongoCollection.InsertMany(session, documents, options, cancellationToken);
        }

        public override Task InsertManyAsync(IEnumerable<TEntity> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
           return MongoCollection.InsertManyAsync(documents, options, cancellationToken);
        }

        public override Task InsertManyAsync(IClientSessionHandle session, IEnumerable<TEntity> documents, InsertManyOptions options = null, CancellationToken cancellationToken = default)
        {
           return MongoCollection.InsertManyAsync(session, documents, options, cancellationToken);
        }

        public override void InsertOne(TEntity document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            MongoCollection.InsertOne(document, options, cancellationToken);
        }

        public override void InsertOne(IClientSessionHandle session, TEntity document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            MongoCollection.InsertOne(session, document, options, cancellationToken);
        }

        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override Task InsertOneAsync(TEntity document, CancellationToken _cancellationToken)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return MongoCollection.InsertOneAsync( document,  _cancellationToken);
        }

        public override Task InsertOneAsync(TEntity document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.InsertOneAsync(document,options, cancellationToken);
        }

        public override Task InsertOneAsync(IClientSessionHandle session, TEntity document, InsertOneOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.InsertOneAsync(session,document, options, cancellationToken);
        }

        public override IAsyncCursor<TResult> MapReduce<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TEntity, TResult> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.MapReduce(map, reduce, options, cancellationToken);
        }

        public override IAsyncCursor<TResult> MapReduce<TResult>(IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TEntity, TResult> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.MapReduce(session,map, reduce, options, cancellationToken);
        }

        public override Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TEntity, TResult> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.MapReduceAsync( map, reduce, options, cancellationToken);
        }

        public override Task<IAsyncCursor<TResult>> MapReduceAsync<TResult>(IClientSessionHandle session, BsonJavaScript map, BsonJavaScript reduce, MapReduceOptions<TEntity, TResult> options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.MapReduceAsync(session,map, reduce, options, cancellationToken);
        }

        public override IFilteredMongoCollection<TDerivedDocument> OfType<TDerivedDocument>()
        {
            return MongoCollection.OfType<TDerivedDocument>();
        }

        public override ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.ReplaceOne(filter, replacement, options, cancellationToken);
        }

        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override ReplaceOneResult ReplaceOne(FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options, CancellationToken cancellationToken = default)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return MongoCollection.ReplaceOne(filter, replacement, options, cancellationToken);
        }

        public override ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.ReplaceOne(session,filter, replacement, options, cancellationToken);
        }
        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override ReplaceOneResult ReplaceOne(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options, CancellationToken cancellationToken = default)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return MongoCollection.ReplaceOne(session, filter, replacement, options, cancellationToken);
        }

        public override Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.ReplaceOneAsync( filter, replacement, options, cancellationToken);
        }
        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override Task<ReplaceOneResult> ReplaceOneAsync(FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options, CancellationToken cancellationToken = default)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return MongoCollection.ReplaceOneAsync(filter, replacement, options, cancellationToken);
        }

        public override Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, ReplaceOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.ReplaceOneAsync(session,filter, replacement, options, cancellationToken);
        }

        [Obsolete("过时的")]
#pragma warning disable CS0809 // 过时成员重写未过时成员
        public override Task<ReplaceOneResult> ReplaceOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, TEntity replacement, UpdateOptions options, CancellationToken cancellationToken = default)
#pragma warning restore CS0809 // 过时成员重写未过时成员
        {
            return MongoCollection.ReplaceOneAsync(session, filter, replacement, options, cancellationToken);
        }

        public override UpdateResult UpdateMany(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.UpdateMany( filter, update, options, cancellationToken);
        }

        public override UpdateResult UpdateMany(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.UpdateMany(session, filter, update, options, cancellationToken);
        }

        public override Task<UpdateResult> UpdateManyAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.UpdateManyAsync(filter, update, options, cancellationToken);
        }

        public override Task<UpdateResult> UpdateManyAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.UpdateManyAsync(session, filter, update, options, cancellationToken);
        }

        public override UpdateResult UpdateOne(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.UpdateOne( filter, update, options, cancellationToken);
        }

        public override UpdateResult UpdateOne(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.UpdateOne(session, filter, update, options, cancellationToken);
        }

        public override Task<UpdateResult> UpdateOneAsync(FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.UpdateOneAsync( filter, update, options, cancellationToken);
        }

        public override Task<UpdateResult> UpdateOneAsync(IClientSessionHandle session, FilterDefinition<TEntity> filter, UpdateDefinition<TEntity> update, UpdateOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.UpdateOneAsync(session, filter, update, options, cancellationToken);
        }

        public override IChangeStreamCursor<TResult> Watch<TResult>(PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.Watch(pipeline, options, cancellationToken);
        }

        public override IChangeStreamCursor<TResult> Watch<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.Watch(session,pipeline, options, cancellationToken);
        }

        public override Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.WatchAsync( pipeline, options, cancellationToken);
        }

        public override Task<IChangeStreamCursor<TResult>> WatchAsync<TResult>(IClientSessionHandle session, PipelineDefinition<ChangeStreamDocument<TEntity>, TResult> pipeline, ChangeStreamOptions options = null, CancellationToken cancellationToken = default)
        {
            return MongoCollection.WatchAsync(session, pipeline, options, cancellationToken);
        }

        public override IMongoCollection<TEntity> WithReadConcern(ReadConcern readConcern)
        {
            return MongoCollection.WithReadConcern(readConcern);
        }

        public override IMongoCollection<TEntity> WithReadPreference(ReadPreference readPreference)
        {
            return MongoCollection.WithReadPreference(readPreference);
        }

        public override IMongoCollection<TEntity> WithWriteConcern(WriteConcern writeConcern)
        {
            return MongoCollection.WithWriteConcern(writeConcern);
        }
    }
}
