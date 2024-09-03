using MassTransit;
using MassTransit.Transports;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Domain.Requests;

namespace PosTech.Fase3.AddContact.Infrastructure.Publications
{
    public class SaveContactPublisher : ISaveContactPublisher
    {
        private readonly IBus _bus;

        public SaveContactPublisher(IBus bus)
        {
            _bus = bus;
        }
        public async Task<bool> PublishAsync(NewContactRequest request)
        {
            await _bus.Publish(request, ctx =>
            {
                ctx.SetRoutingKey("NewContactRequest");
            });
            return true;
        }
    }
}
