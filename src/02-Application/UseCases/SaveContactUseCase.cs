using PosTech.Fase3.AddContact.Application.Validators;
using PosTech.Fase3.AddContact.Domain.Exceptions;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Domain.Requests;
using PosTech.Fase3.AddContact.Domain.Responses;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using PosTech.Fase3.AddContact.Domain.Entities;

namespace PosTech.Fase3.AddContact.Application.UseCases
{
    public class SaveContactUseCase : ISaveContactUseCase
    {
        private readonly ISaveContactPublisher _publisher;
        private readonly ICodeAreaClient _client;
        private readonly IProtocolRepository _repository;

        public SaveContactUseCase(ISaveContactPublisher publisher, ICodeAreaClient client, IProtocolRepository repository)
        {
            _publisher = publisher;
            _client = client;
            _repository = repository;
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
            return new DefaultOutput<ContactResponse>(published, new ContactResponse(entity.Id));

        }
    }
}
