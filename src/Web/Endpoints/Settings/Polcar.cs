using Application.Settings.PolcarSettings.Commands;
using Application.Settings.PolcarSettings.Queries;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints.Settings;

public class Polcar : EndpointGroupBase
{
    public override string CustomPath => "settings/";

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetPolcarSettings)
            .MapGet(GetPolcarSetting, "{id}")
            .MapDelete(DeletePolcarSetting, "{id}");
    }

    public async Task<List<PolcarSettingDto>> GetPolcarSettings(ISender sender)
    {
        return await sender.Send(new GetPolcarSettingsQuery());
    }

    public async Task<PolcarSettingDto> GetPolcarSetting(ISender sender, int id)
    {
        return await sender.Send(new GetPolcarSettingQuery(id));
    }

    public async Task<IResult> DeletePolcarSetting(ISender sender, int id)
    {
        await sender.Send(new DeletePolcarSettingCommand(id));
        return Results.NoContent();
    }
}
