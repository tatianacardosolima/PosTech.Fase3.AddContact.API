using FluentValidation;
using Postech.GroupEight.TechChallenge.ContactManagement.Events;
using PosTech.Fase3.AddContact.Domain.Extensions;
using static PosTech.Fase3.AddContact.Domain.Utils.ErrorMessageHelper;

namespace PosTech.Fase3.AddContact.Application.Validators
{
    public class ContactValidator : AbstractValidator<NewContactRequest>
    {
        public ContactValidator()
        {

            RuleFor(x => new { Name = x.ContactFirstName, LastName = x.ContactLastName})
                 .Custom((value, context) =>
                 {
                     if (value.Name.Equals(value.LastName))
                         context.AddFailure(CONTACT001);
                 });

            RuleFor(x => new { Email = x.ContactEmail })
                 .Custom((value, context) =>
                 {
                     if (!value.Email.IsValidEmail())
                         context.AddFailure(CONTACT002);
                 });

            RuleFor(x => new { PhoneNumber = x.ContactPhoneNumber })
                 .Custom((value, context) =>
                 {
                     if (!value.PhoneNumber.IsValidPhone())
                         context.AddFailure(CONTACT003);
                 });

        }
    }
}
