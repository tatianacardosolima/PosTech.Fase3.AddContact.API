using Postech.GroupEight.TechChallenge.ContactManagement.Events;

namespace PosTech.Fase3.AddContact.Domain.Interfaces
{
    public interface ISaveContactPublisher
    {
        public Task<bool> PublishAsync(NewContactRequest request);
    }
}
