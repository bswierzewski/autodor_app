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
                .ForMember(d => d.FakturaEmail, o => o.MapFrom(s => s.IFirmaSetting.User))
                .ForMember(d => d.DistributorCode, o => o.MapFrom(s => s.PolcarSetting.DistributorCode))
                .ForMember(d => d.BranchId, o => o.MapFrom(s => s.PolcarSetting.BranchId))
                .ForMember(d => d.LanguageId, o => o.MapFrom(s => s.PolcarSetting.LanguageId))
                .ForMember(d => d.CollectionName, o => o.MapFrom(s => s.MongoDBSetting.CollectionName));

            CreateMap<CreateUserSettingCommand, UserSetting>();

            CreateMap<UpdateUserSettingCommand, UserSetting>();
        }
    }
}
