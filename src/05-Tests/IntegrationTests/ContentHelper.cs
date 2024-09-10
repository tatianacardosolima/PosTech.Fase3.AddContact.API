using System.Text;
using System.Text.Json;

namespace PosTech.Fase3.AddContact.IntegrationTests
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
           => new StringContent(JsonSerializer.Serialize(obj), Encoding.Default, "application/json");
    }
}
