using Application.Settings.IFirmaSettings.Commands;
using Application.Settings.IFirmaSettings.Queries;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints.Settings;

public class IFirma : EndpointGroupBase
{
    public override string CustomPath => "settings/";

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetIFirmaSettings)
            .MapGet(GetIFirmaSetting, "{id}")
            .MapDelete(DeleteIFirmaSetting, "{id}");
    }

    public async Task<List<IFirmaSettingDto>> GetIFirmaSettings(ISender sender)
    {
        return await sender.Send(new GetIFirmaSettingsQuery());
    }

    public async Task<IFirmaSettingDto> GetIFirmaSetting(ISender sender, int id)
    {
        return await sender.Send(new GetIFirmaSettingQuery(id));
    }

    public async Task<IResult> DeleteIFirmaSetting(ISender sender, int id)
    {
        await sender.Send(new DeleteIFirmaSettingCommand(id));
        return Results.NoContent();
    }
}
