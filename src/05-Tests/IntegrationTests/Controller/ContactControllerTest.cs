﻿using Bogus;
using Microsoft.AspNetCore.Mvc.Testing;
using PosTech.Fase3.AddContact.Domain.Requests;
using PosTech.Fase3.AddContact.Domain.Responses;
using System.Text.Json;

namespace PosTech.Fase3.AddContact.IntegrationTests.Controller
{
    public class ContactControllerTest: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;


        Faker _faker = new Faker();
        public ContactControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact(DisplayName = "Validar o cenário principal de cadastro de contacto")]
        public async Task Validate_SaveContact_ShuldBeTrue()
        {

            var content = ContentHelper.GetStringContent(GetRequest());
            var response = await _client.PostAsync("/contacts", content);

            var result = JsonSerializer.Deserialize<DefaultOutput<ContactResponse>>(response.Content.ReadAsStringAsync().Result);
            Assert.NotNull(result);
            Assert.True(result.Success);

        }

        private NewContactRequest GetRequest()
        {
            return new()
            {

                ContactFirstName = _faker.Name.FirstName(),
                ContactLastName = _faker.Name.LastName(),
                ContactEmail = _faker.Internet.Email(),
                ContactPhoneNumber = _faker.Phone.PhoneNumber("9########"),
                ContactPhoneNumberAreaCode = "11",
            };
        }

    }
}