using Application.Settings.UserSettings.Commands;
using Application.Settings.UserSettings.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Settings.UserSettings.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserSetting, UserSettingDto>()
                .ForMember(d => d.IFirmaEmail, o => o.MapFrom(s => s.IFirmaSetting.User))
                .ForMember(d => d.PolcarDistributorCode, o => o.MapFrom(s => s.PolcarSetting.DistributorCode))
                .ForMember(d => d.MongoDBCollection, o => o.MapFrom(s => s.MongoDBSetting.CollectionName));

            CreateMap<CreateUserSettingCommand, UserSetting>();

            CreateMap<UpdateUserSettingCommand, UserSetting>();
        }
    }
}
