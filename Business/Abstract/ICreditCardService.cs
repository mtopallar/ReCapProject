using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IResult Add(CreditCard creditCard);
        IResult Delete(CreditCard creditCard);
        IDataResult<CreditCardDto> GetCreditCardByCardId(int creditCardId);
        IDataResult<List<CreditCardDto>> GetAll(); //gerekmezse silinebilir.(api de kullanılmadı)
        IDataResult<List<CreditCardDto>> GetCustomerCardListByCustomerId(int customerId);
        IDataResult<CreditCardDto> GetCustomerSelectedCardByCustomerId(int customerId);
        IDataResult<List<CreditCardDto>> GetCreditCardListByCardTypeId(int cardTypeId);
    }
}
