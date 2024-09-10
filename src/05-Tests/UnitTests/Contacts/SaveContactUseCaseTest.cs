using Bogus;
using FluentAssertions;
using Moq;
using Org.BouncyCastle.Asn1.Ocsp;
using PosTech.Fase3.AddContact.Application.UseCases;
using PosTech.Fase3.AddContact.Domain.DTOs;
using PosTech.Fase3.AddContact.Domain.Exceptions;
using PosTech.Fase3.AddContact.Domain.Interfaces;
using PosTech.Fase3.AddContact.Domain.Requests;
using PosTech.Fase3.AddContact.Domain.Responses;
using static PosTech.Fase3.AddContact.Domain.Utils.ErrorMessageHelper;

namespace PosTech.Fase3.AddContact.UnitTests.Contacts
{
    public class SaveContactUseCaseTest
    {

        private readonly Faker _faker = new("pt_BR");

        [Fact(DisplayName = "Registrar um novo contato")]
        [Trait("Action", "Handle")]
        public async Task UseCase_NewContactRegistration_ShouldRegisterWithSuccess()
        {
            // Arrange
            var request = GetRequest();
            Mock<ISaveContactPublisher> publisher = new();
            publisher.Setup(c => c.PublishAsync(request)).ReturnsAsync(true);

            var client = GetClient(request);

            SaveContactUseCase useCase = new(publisher.Object, client.Object);

            // Act
            DefaultOutput<ContactResponse> output = await useCase.SaveNewContactAsync(request);

            // Assert
            output.Success.Should().BeTrue();
            output.Data.Should().NotBeNull();
            
        }

        [Fact(DisplayName = "Validar se o nome é igual o sobrenome")]
        [Trait("Action", "UseCase")]
        public async Task UseCase_NameAndLastNameEquals_ShouldRegisterWithError()
        {
            // Arrange
            var request = GetRequest();
            request.ContactLastName = request.ContactFirstName;

            var publisher = GetMockPublisher(request);

            var client = GetClient(request);

            SaveContactUseCase useCase = new(publisher.Object, client.Object);

            // Act            
            DomainException exception = await Assert.ThrowsAsync<DomainException>(() => useCase.SaveNewContactAsync(request));

            // Assert            
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be(CONTACT001);            
        }
        ////CONTACT002
        [Theory(DisplayName = "Email inválido")]
        [Trait("Action", "UseCase")]
        [InlineData("cleiton dias@gmail.com")]
        [InlineData("jair.raposo@")]
        [InlineData("milton.morgado4@hotmail")]
        [InlineData("leticia-mariagmail.com")]
        [InlineData("@yahoo.com")]
        [InlineData("")]
        [InlineData(" ")]
        public async Task UseCase_EmailInvalid_ShouldError(string email)
        {
            // Arrange
            var request = GetRequest();
            request.ContactEmail = email;

            var publisher = GetMockPublisher(request);

            var client = GetClient(request);

            SaveContactUseCase useCase = new(publisher.Object, client.Object);

            // Act            
            DomainException exception = await Assert.ThrowsAsync<DomainException>(() => useCase.SaveNewContactAsync(request));

            // Assert
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be(CONTACT002);                     
        }
        
        [Theory(DisplayName = "Telefone inválido")]
        [Trait("Action", "UseCase")]
        [InlineData("0123456789")]
        [InlineData("1122334455")]
        [InlineData("9876543200")]
        [InlineData("1111111111")]
        [InlineData("2222222222")]
        [InlineData("123456789012")]
        [InlineData("87654321A")]
        [InlineData("8#7654321")]
        [InlineData("(123)456-7890")]
        [InlineData("987.654.3210")]
        [InlineData("8765432@10")]
        [InlineData("")]
        [InlineData(" ")]
        public async Task UseCase_PhoneInvalid_ShouldError(string phone)
        {
            // Arrange
            var request = GetRequest();
            request.ContactPhoneNumber = phone;

            var publisher = GetMockPublisher(request);

            var client = GetClient(request);

            SaveContactUseCase useCase = new(publisher.Object, client.Object);

            // Act            
            DomainException exception = await Assert.ThrowsAsync<DomainException>(() => useCase.SaveNewContactAsync(request));

            // Assert
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be(CONTACT003);
        }

        [Fact(DisplayName = "Validar a área code")]
        [Trait("Action", "UseCase")]
        public async Task UseCase_AreaCodeInvalid_ShouldWithError()
        {
            // Arrange
            var request = GetRequest();
            
            var publisher = GetMockPublisher(request);

            var client = GetClient(request, true);

            SaveContactUseCase useCase = new(publisher.Object, client.Object);

            // Act            
            DomainException exception = await Assert.ThrowsAsync<DomainException>(() => useCase.SaveNewContactAsync(request));

            // Assert            
            exception.Message.Should().NotBeNullOrEmpty();
            exception.Message.Should().Be("Área code não encontrada");
        }

        private Mock<ICodeAreaClient> GetClient(NewContactRequest request, bool returnNull = false)
        {
            Mock<ICodeAreaClient> client = new();
            int areaCode = int.Parse(request.ContactPhoneNumberAreaCode);
            if (returnNull)
            {
                client.Setup(r => r.GetRegionByCodeAsync(areaCode));
            }
            else
            {

                client.Setup(r => r.GetRegionByCodeAsync(areaCode)).ReturnsAsync(
                        new RegionDto()
                        {
                            Number = 11,
                            Region = "São Paulo",
                            State = "São Paulo"
                        });
            }
            return client;
        }
        
        private Mock<ISaveContactPublisher>  GetMockPublisher(NewContactRequest request)
        {
            Mock<ISaveContactPublisher> publisher = new();
            publisher.Setup(c => c.PublishAsync(request)).ReturnsAsync(true);
            return publisher;
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
