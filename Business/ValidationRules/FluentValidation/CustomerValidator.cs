﻿using System;
using System.Collections.Generic;
using System.Text;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            //Şimdilik her müşterinin bir firması olmayabilir gerekçesi ile bu validator u commentliyorum.
            
            //RuleFor(c => c.CompanyName).NotEmpty();
            //RuleFor(c => c.CompanyName).MinimumLength(5);
        }
    }
}
