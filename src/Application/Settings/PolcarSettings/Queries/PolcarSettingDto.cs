using AutoMapper;
using Domain.Entities.Settings;

namespace Application.Settings.PolcarSettings.Queries;

public class PolcarSettingDto
{
    public int Id { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Polcar, PolcarSettingDto>();
        }
    }
}
