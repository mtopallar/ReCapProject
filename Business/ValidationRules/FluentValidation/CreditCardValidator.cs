using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CreditCardValidator:AbstractValidator<CreditCard>
    {
        public CreditCardValidator()
        {
            RuleFor(c => c.CardNumber).NotEmpty();
            RuleFor(c => c.CardNumber.Length).Equal(16);
            RuleFor(c => c.FirstNameOnTheCard).NotEmpty();
            RuleFor(c => c.LastNameOnTheCard).NotEmpty();
            RuleFor(c => c.ExpirationMounth).NotEmpty();
            RuleFor(c => c.ExpirationMounth).InclusiveBetween(1,12);
            RuleFor(c => c.ExpirationYear).NotEmpty();
            RuleFor(c => c.ExpirationYear).InclusiveBetween(2020, 9999);
            RuleFor(c => c.Cvv).NotEmpty();
            RuleFor(c => c.Cvv.Length).Equal(3);
        }
    }
}
