using Application.Common.Interfaces;
using Domain.Entities.Settings;
using Microsoft.AspNetCore.Mvc;
using Web.Infrastructure;

namespace Web.Controllers
{
    public class PolcarSettingsController : ApiControllerBase
    {
        public PolcarSettingsController(IApplicationDbContext dbContext)
        {
        }

        [HttpGet]
        public IList<Polcar> Polcars()
        {
            return new List<Polcar>()
            {
                new Polcar { BranchId = 1 },
                new Polcar { BranchId = 2 },
            };
        }
    }
}
