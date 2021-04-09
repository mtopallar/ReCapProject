using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;

namespace Business.Concrete
{
    public class PaymentManager:IPaymentService
    {
        public IResult Payment()
        {
            string[] paymentStatueArray = {"true", "false"};
            bool paymentStatue = Boolean.Parse(paymentStatueArray[new Random().Next(0, 2)]);

            if (paymentStatue)
            {
                return new SuccessResult(Messages.PaymentSuccessfull);
            }

            return new ErrorResult(Messages.PaymentError);
        }
    }
}
