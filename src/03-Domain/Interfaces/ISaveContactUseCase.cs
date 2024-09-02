using PosTech.Fase3.AddContact.Domain.Requests;
using PosTech.Fase3.AddContact.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Fase3.AddContact.Domain.Interfaces
{
    public interface ISaveContactUseCase
    {
        Task<DefaultOutput<ContactResponse>> SaveNewContactAsync(NewContactRequest request);
    }
}
