using Application.Settings.IFirmaSettings.Commands;
using Application.Settings.IFirmaSettings.Queries;
using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.IFirmaSettings.Mappings
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<IFirmaSetting, IFirmaSettingDto>()
                .ForMember(d => d.FakturaApiKey, o => o.MapFrom(s => s.FakturaKey))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.User));

            CreateMap<CreateIFirmaSettingCommand, IFirmaSetting>();

            CreateMap<UpdateIFirmaSettingCommand, IFirmaSetting>();
        }
    }
}
