using Application.Settings.UserSettings.Commands;
using Application.Settings.UserSettings.Queries;
using MediatR;
using Web.Infrastructure;

namespace Web.Endpoints.Settings;

public class UserSettings : EndpointGroupBase
{
    public override string GroupName => "settings/userSettings";

    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetUserSettings)
            .MapGet(GetUserSetting, "{id}")
            .MapPost(CreateUserSetting)
            .MapPut(UpdateUserSetting, "{id}")
            .MapDelete(DeleteUserSetting, "{id}");
    }

    public async Task<List<UserSettingDto>> GetUserSettings(ISender sender)
    {
        return await sender.Send(new GetUserSettingsQuery());
    }

    public async Task<UserSettingDto> GetUserSetting(ISender sender, int id)
    {
        return await sender.Send(new GetUserSettingQuery(id));
    }
    public async Task<int> CreateUserSetting(ISender sender, CreateUserSettingCommand command)
    {
        return await sender.Send(command);
    }

    public async Task<IResult> UpdateUserSetting(ISender sender, int id, UpdateUserSettingCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteUserSetting(ISender sender, int id)
    {
        await sender.Send(new DeleteUserSettingCommand(id));

        return Results.NoContent();
    }
}
