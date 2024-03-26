using AutoMapper;

namespace Infrastructure.Services.MongoDB.Mappings;
public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Infrastructure.MongoDB.Collections.Contractor, Domain.Entities.Contractor>()
            .ReverseMap();
    }
}

