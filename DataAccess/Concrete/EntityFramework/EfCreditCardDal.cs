using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCreditCardDal:EfEntityRepositoryBase<CreditCard,ReCapContext>,ICreditCardDal
    {
        public List<CreditCardDto> GetCreditCardDetailsWithTypeName(Expression<Func<CreditCardDto, bool>> filter = null)
        {
            using (ReCapContext contex = new ReCapContext())
            {
                var result = from creditCard in contex.CreditCards
                    join creditCardType in contex.CreditCardTypes on creditCard.CardTypeId equals creditCardType.Id
                    select new CreditCardDto
                    {
                        Id = creditCard.Id,
                        CustomerId = creditCard.CustomerId,
                        CardTypeId = creditCard.CardTypeId,
                        CardTypeName = creditCardType.TypeName,
                        CardNumber = creditCard.CardNumber,
                        FirstNameOnTheCard = creditCard.FirstNameOnTheCard,
                        LastNameOnTheCard = creditCard.LastNameOnTheCard,
                        ExpirationMonth = creditCard.ExpirationMonth,
                        ExpirationYear = creditCard.ExpirationYear,
                        Cvv = creditCard.Cvv,
                        SelectedCard = creditCard.SelectedCard
                        
                    };
                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
