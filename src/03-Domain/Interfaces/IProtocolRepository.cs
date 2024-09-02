using PosTech.Fase3.AddContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Fase3.AddContact.Domain.Interfaces
{
    public interface IProtocolRepository
    {
        Task InsertAsync(ProtocolEntity entity);
    }
}
