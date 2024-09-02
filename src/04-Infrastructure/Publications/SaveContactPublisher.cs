using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Fase3.AddContact.Infrastructure.Publications
{
    public class SaveContactPublisher : ISaveContactPublisher
    {
        public Task<bool> PublishAsync(NewContactRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
