using Application.Common.Interfaces;
using Application.Common.Options;
using AutoMapper;
using Infrastructure.MongoDB.Collections;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services;

public class ContractorService : IContractorService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Contractor> _contractorCollection;

    public ContractorService(IOptions<MongoDBOptions> mongoDBOptions, IMapper mapper, IMongoDatabase mongoDatabase)
    {       
        _mapper = mapper;

        _contractorCollection = mongoDatabase.GetCollection<Contractor>(mongoDBOptions.Value.CollectionName);
    }

    public async Task<IEnumerable<Domain.Entities.Contractor>> GetAsync()
    {
        var contractors = await _contractorCollection
            .Find(new BsonDocument())
            .ToListAsync();

        return _mapper.Map<List<Domain.Entities.Contractor>>(contractors);
    }

    public async Task CreateAsync(Domain.Entities.Contractor contractor)
    {
        var contractorToAdd = _mapper.Map<Contractor>(contractor);

        await _contractorCollection.InsertOneAsync(contractorToAdd);
    }

    public async Task DeleteAsync(string id)
    {
        await _contractorCollection.DeleteOneAsync(Builders<Contractor>.Filter.Eq("_id", ObjectId.Parse(id)));
    }
}
