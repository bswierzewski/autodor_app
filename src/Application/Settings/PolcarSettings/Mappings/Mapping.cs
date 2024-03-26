using Application.Settings.PolcarSettings.Commands;
using Application.Settings.PolcarSettings.Queries;
using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.PolcarSettings.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PolcarSetting, PolcarSettingDto>();

            CreateMap<CreatePolcarSettingCommand, PolcarSetting>();

            CreateMap<UpdatePolcarSettingCommand, PolcarSetting>();
        }
    }
}
