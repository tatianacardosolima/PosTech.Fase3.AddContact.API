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

            var result = RegionHelper.Get()
            .SelectMany(region => region.States) 
            .SelectMany(state => state.Codes, (state, code) => new { StateName = state.Name, Code = code })
            .FirstOrDefault(x => x.Code.Number == code);

            if (result == null) return NotFound();

            return Ok(new { Number = result.Code.Number, State = result.StateName });
        }
    }
}
