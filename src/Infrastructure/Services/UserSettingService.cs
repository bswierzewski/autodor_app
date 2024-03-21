using Application.Common.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UserSettingService(IUser user, IApplicationDbContext context) : IUserSetting
    {
        public UserSetting GetCurrentUserSetting()
            => context.UserSettings
                .Include(setting => setting.IFirmaSetting)
                .Include(setting => setting.MongoDBSetting)
                .Include(setting => setting.PolcarSetting)
                .FirstOrDefault(setting => setting.Auth0Id == user.Id);
    }
}
