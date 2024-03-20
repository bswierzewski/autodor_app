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
            CreateMap<IFirma, IFirmaSettingDto>();

            CreateMap<CreateIFirmaSettingCommand, IFirma>();
        }
    }
}
