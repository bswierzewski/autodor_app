using Application.Settings.MongoDBSettings.Commands;
using Application.Settings.MongoDBSettings.Queries;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints.Settings;

public class MongoDB : EndpointGroupBase
{
    public override string CustomPath => "settings/";

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            //.RequireAuthorization()
            .MapGet(GetMongoDBSettings)
            .MapGet(GetMongoDBSetting, "{id}")
            .MapDelete(DeleteMongoDBSetting, "{id}");
    }

    public async Task<List<MongoDBSettingDto>> GetMongoDBSettings(ISender sender)
    {
        return await sender.Send(new GetMongoDBSettingsQuery());
    }

    public async Task<MongoDBSettingDto> GetMongoDBSetting(ISender sender, int id)
    {
        return await sender.Send(new GetMongoDBSettingQuery(id));
    }

    public async Task<IResult> DeleteMongoDBSetting(ISender sender, int id)
    {
        await sender.Send(new DeleteMongoDBSettingCommand(id));
        return Results.NoContent();
    }
}
