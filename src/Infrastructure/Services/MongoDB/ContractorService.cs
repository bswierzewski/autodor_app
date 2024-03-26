using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.MongoDB.Collections;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Infrastructure.Services;

public class ContractorService : IContractorService
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<Contractor> _contractorCollection;

    public ContractorService(IUserSetting userSetting, IMapper mapper, IMongoDatabase mongoDatabase)
    {       
        _mapper = mapper;

        var _userSetting = userSetting.GetCurrentUserSetting();

        if (_userSetting.MongoDBSetting is null)
            throw new Exception("MongoDB setting doesn't exist. Please contanct with admin");

        _contractorCollection = mongoDatabase.GetCollection<Contractor>(_userSetting.MongoDBSetting.CollectionName);
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
