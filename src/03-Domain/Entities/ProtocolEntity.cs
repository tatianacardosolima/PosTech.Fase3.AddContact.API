using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Fase3.AddContact.Domain.Entities
{
    public enum ProtocolStatus
    { 
        Created = 0,
        Processing = 1,
        Processed = 2,
        Error = 3
    }
    [Table("ProtocolEntity")]
    public class ProtocolEntity
    {
        public ProtocolEntity()
        {
            Id = Guid.NewGuid();
            CreatedIn = DateTime.UtcNow;
            Status = ProtocolStatus.Created;
        }
        
        public Guid Id { get; private set; }
        public DateTime CreatedIn { get; private set; }
        public ProtocolStatus Status{ get; private set; }
        public string? Message { get; private set; }
    }
}
