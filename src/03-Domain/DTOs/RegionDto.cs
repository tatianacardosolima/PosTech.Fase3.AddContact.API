using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Fase3.AddContact.Domain.DTOs
{
    public class RegionDto
    {
        public int Number { get; set; }

        public string State { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;
    }
}
