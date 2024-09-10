using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using PosTech.Fase3.AddContact.Domain.Responses;

namespace PosTech.Fase3.AddContact.Domain.Interfaces
{
    public interface ISaveContactUseCase
    {
        Task<DefaultOutput<ContactResponse>> SaveNewContactAsync(NewContactRequest request);
    }
}
