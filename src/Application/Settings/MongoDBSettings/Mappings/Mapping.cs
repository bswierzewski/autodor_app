using Application.Settings.MongoDBSettings.Commands;
using Application.Settings.MongoDBSettings.Queries;
using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.MongoDBSettings.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<MongoDB, MongoDBSettingDto>();

            CreateMap<CreateMongoDBSettingCommand, MongoDB>();
        }
    }
}
