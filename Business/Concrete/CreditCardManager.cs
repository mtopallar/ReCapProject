using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

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
            creditCard.SelectedCard = SelectedCardOperations(creditCard);
            _creditCardDal.Add(creditCard);
            return new SuccessResult(Messages.CreditCardAddedSuccessfully);
        }

        public IResult Delete(CreditCard creditCard)
        {
            _creditCardDal.Delete(creditCard);
            return new SuccessResult(Messages.CreditCardDeletedSuccessfully);
        }

        public IDataResult<CreditCardDto> GetCreditCardByCardId(int creditCardId)
        {
            return new SuccessDataResult<CreditCardDto>(_creditCardDal.GetCreditCardDetailsWithTypeName(c => c.Id==creditCardId).Single(),Messages.GetCreditCardByCardIdSuccessfully);
        }

        public IDataResult<List<CreditCardDto>> GetAll()
        {
            return new SuccessDataResult<List<CreditCardDto>>(_creditCardDal.GetCreditCardDetailsWithTypeName(),
                Messages.GetAllCreditCardsSuccessfully);
        }

        public IDataResult<List<CreditCardDto>> GetCustomerCardListByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<CreditCardDto>>(_creditCardDal.GetCreditCardDetailsWithTypeName(c => c.CustomerId == customerId),
                Messages.GetUserCardListSuccessfully);
        }

        public IDataResult<CreditCardDto> GetCustomerSelectedCardByCustomerId(int customerId)
        {
            return new SuccessDataResult<CreditCardDto>(
                _creditCardDal
                    .GetCreditCardDetailsWithTypeName(c => c.SelectedCard == true && c.CustomerId == customerId)
                    .Single(), Messages.SelectedCardGetsSuccessfully);
        }

        public IDataResult<List<CreditCardDto>> GetCreditCardListByCardTypeId(int cardTypeId)
        {
            
            return new SuccessDataResult<List<CreditCardDto>>(_creditCardDal.GetCreditCardDetailsWithTypeName(c => c.CardTypeId == cardTypeId),
                Messages.GetCreditCardByCardTypeIdSuccessfully);
        }


        private bool SelectedCardOperations(CreditCard creditCard)
        {
            var selectedCardIndex = _creditCardDal.GetAll(c => c.CustomerId == creditCard.CustomerId && c.SelectedCard);

            if (selectedCardIndex.Count == 0)
            {
                return true;
            }
            if (creditCard.SelectedCard && selectedCardIndex.Count > 0)
            {
                selectedCardIndex.Find(c=>c.SelectedCard);
                foreach (var selectedCardToFalse in selectedCardIndex)
                {
                    selectedCardToFalse.SelectedCard = false;
                    _creditCardDal.Update(selectedCardToFalse);
                }

                return true;
            }
            return false;
        }
    }
}
