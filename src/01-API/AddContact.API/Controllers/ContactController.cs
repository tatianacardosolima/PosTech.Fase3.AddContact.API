using Microsoft.AspNetCore.Mvc;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Domain.Requests;

namespace PosTech.Fase3.AddContact.API.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ISaveContactUseCase _useCase;

        public ContactController(ILogger<ContactController> logger, ISaveContactUseCase useCase)
        {
            _logger = logger;
            _useCase = useCase;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NewContactRequest request)
        {
            return Ok(await _useCase.SaveNewContactAsync(request));
        }
    }
}
