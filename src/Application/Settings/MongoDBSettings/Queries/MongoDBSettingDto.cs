using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.MongoDBSettings.Queries;

public class MongoDBSettingDto
{
    public int Id { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MongoDB, MongoDBSettingDto>();
        }
    }
}
