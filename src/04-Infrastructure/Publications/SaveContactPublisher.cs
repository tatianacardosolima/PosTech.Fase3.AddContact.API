using MassTransit;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using PosTech.Fase3.AddContact.Domain.Interfaces;

namespace PosTech.Fase3.AddContact.Infrastructure.Publications
{
    public class SaveContactPublisher : ISaveContactPublisher
    {
        private readonly IBus _bus;

        public SaveContactPublisher(IBus bus)
        {
            _bus = bus;
        }
        public async Task<bool> PublishAsync(CreateContactEvent request)
        {
            await _bus.Publish(request, ctx =>
            {
                ctx.SetRoutingKey("NewContactRequest");
            });
            return true;
        }
    }
}
