using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using PosTech.Fase3.AddContact.Application.Validators;
using PosTech.Fase3.AddContact.Domain.Exceptions;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Domain.Responses;

namespace PosTech.Fase3.AddContact.Application.UseCases
{
    public class SaveContactUseCase : ISaveContactUseCase
    {
        private readonly ISaveContactPublisher _publisher;
        private readonly ICodeAreaClient _client;
        
        public SaveContactUseCase(ISaveContactPublisher publisher, ICodeAreaClient client)
        {
            _publisher = publisher;
            _client = client;         
        }
        public async Task<DefaultOutput<ContactResponse>> SaveNewContactAsync(NewContactRequest request)
        {

            var validator = new ContactValidator();
            var validation = validator.Validate(request);
            if (!validation.IsValid)
            {
                var error = validation.Errors.ToList().First();
                throw new DomainException(error.ErrorMessage);
            }

            var areacode = await _client.GetRegionByCodeAsync(int.Parse(request.ContactPhoneNumberAreaCode));
            DomainException.ThrowWhen(areacode == null, "Área code não encontrada");
                        
            var published = await _publisher.PublishAsync(request);
            return new DefaultOutput<ContactResponse>(published, new ContactResponse("Registro salvo com sucesso"));

        }
    }
}
