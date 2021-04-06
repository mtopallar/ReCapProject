using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class CreditCard:IEntity
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CardTypeId { get; set; }
        public string CardNumber { get; set; }
        public string FirstNameOnTheCard { get; set; }
        public string LastNameOnTheCard { get; set; }
        public DateTime ExpirationMounth { get; set; }
        public DateTime ExpirationYear { get; set; }
        public string Cvv { get; set; }

    }
}
