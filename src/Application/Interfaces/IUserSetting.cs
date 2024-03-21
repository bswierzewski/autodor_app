using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserSetting
    {
        public UserSetting GetCurrentUserSetting();
    }
}
