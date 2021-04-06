using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CreditCardManager:ICreditCardService
    {
        private readonly ICreditCardDal _creditCardDal;

        public CreditCardManager(ICreditCardDal creditCardDal)
        {
            _creditCardDal = creditCardDal;
        }
        [ValidationAspect(typeof(CreditCardValidator))]
        public IResult Add(CreditCard creditCard)
        {
            _creditCardDal.Add(creditCard);
            return new SuccessResult(Messages.CreditCardAddedSuccessfully);
        }

        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult(Messages.CreditCardDeletedSuccessfully);
        }

        public IDataResult<CreditCard> GetCreditCardByCardId(int creditCardId)
        {
            return new SuccessDataResult<CreditCard>(_creditCardDal.Get(c => c.Id == creditCardId),Messages.GetCreditCardByCardIdSuccessfully);
        }

        public IDataResult<List<CreditCard>> GetAll()
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(),
                Messages.GetAllCreditCardsSuccessfully);
        }

        public IDataResult<List<CreditCard>> GetUserCardList(int customerId)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(c => c.CustomerId == customerId),
                Messages.GetUserCardListSuccessfully);
        }

        public IDataResult<List<CreditCard>> GetCreditCardByCardTypeId(int cardTypeId)
        {
            return new SuccessDataResult<List<CreditCard>>(_creditCardDal.GetAll(c => c.CardTypeId == cardTypeId),
                Messages.GetCreditCardByCardTypeIdSuccessfully);
        }
    }
}
