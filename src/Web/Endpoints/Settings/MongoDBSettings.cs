using Application.Settings.MongoDBSettings.Commands;
using Application.Settings.MongoDBSettings.Queries;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints.Settings;

public class MongoDBSettings : EndpointGroupBase
{
    public override string GroupName => "settings/mongodbSettings";

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetMongoDBSettings)
            .MapGet(GetMongoDBSetting, "{id}")
            .MapPost(CreateMongoDBSetting)
            .MapPut(UpdateMongoDBSetting, "{id}")
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

    public async Task<int> CreateMongoDBSetting(ISender sender, CreateMongoDBSettingCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateMongoDBSetting(ISender sender, int id, UpdateMongoDBSettingCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteMongoDBSetting(ISender sender, int id)
    {
        await sender.Send(new DeleteMongoDBSettingCommand(id));

        return Results.NoContent();
    }
}
