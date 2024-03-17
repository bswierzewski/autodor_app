using Application.Common.Interfaces;
using Application.Snippets.Queries;
using Application.Snippets.Queries.GetSnippets;
using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure;

namespace Web.Controllers
{
    public class IFirmaSettingsController : ApiControllerBase
    {
        public IFirmaSettingsController(IApplicationDbContext dbContext)
        {
        }

        [HttpGet]
        public async Task<IList<IFirmaSettingDto>> GetIFirmaSettings()
        {
            return await Sender.Send(new GetIFirmaSettingsQuery());
        }

        [HttpGet("{id}")]
        public async Task<IFirmaSettingDto> GetIFirmaSetting(int id)
        {
            return await Sender.Send(new GetIFirmaSettingQuery(id));
        }
    }
}
