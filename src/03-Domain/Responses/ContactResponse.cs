using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Fase3.AddContact.Domain.Responses
{
    public class ContactResponse
    {
        public ContactResponse(string messege)
        {
            Message = messege;
        }
        public string Message { get; set; }
    }
}
