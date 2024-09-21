using Microsoft.AspNetCore.Mvc;
using PosTech.Fase3.AreaCode.API.Model;
using PosTech.Fase3.AreaCodeModel.API.Helpers;

namespace PosTech.Fase3.AreaCode.API.Controllers
{
    [ApiController]
    [Route("areacodes")]
    public class AreaCodeController : ControllerBase
    {

        private readonly ILogger<AreaCodeController> _logger;

        public AreaCodeController(ILogger<AreaCodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<RegionModel> Get()
        {
            return RegionHelper.Get();
        }

        [HttpGet("{code}")]
        public ActionResult Get([FromRoute] int code)
        {
            var regions = RegionHelper.Get();
            var regionstatecode = regions.Where(x => x.States.Any(y => y.Codes.Any(z => z.Number==code)))
                    .Select(x => new RegionStateCodeModel()
                    {
                        Number = x.States.FirstOrDefault()!.Codes.FirstOrDefault()!.Number,
                        Region = x.Name,
                        State = x.States.FirstOrDefault()!.Name
                    }
                    ).FirstOrDefault();
            return Ok(regionstatecode);
        }
    }
}
