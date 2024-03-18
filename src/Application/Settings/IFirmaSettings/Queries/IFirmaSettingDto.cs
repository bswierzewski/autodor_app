using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.IFirmaSettings.Queries;

public class IFirmaSettingDto
{
    public int Id { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<IFirma, IFirmaSettingDto>();
        }
    }
}
