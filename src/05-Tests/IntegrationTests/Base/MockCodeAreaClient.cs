using PosTech.Fase3.AddContact.Domain.DTOs;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PosTech.Fase3.AddContact.IntegrationTests.Base
{
    public class MockCodeAreaClient : ICodeAreaClient
    {
        public Task<RegionDto?> GetRegionByCodeAsync(int code)
        {
            return Task.FromResult(new RegionDto());
        }
                
    }
}
