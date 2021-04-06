using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICreditCardService
    {
        IResult Add(CreditCard creditCard);
        IResult Delete(CreditCard creditCard);
        IDataResult<CreditCard> GetCreditCardByCardId(int creditCardId);
        IDataResult<List<CreditCard>> GetAll(); //silinebilir.
        IDataResult<List<CreditCard>> GetUserCardList(int customerId);
        IDataResult<List<CreditCard>> GetCreditCardByCardTypeId(int cardTypeId);
    }
}
