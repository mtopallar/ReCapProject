using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.DTOs
{
    public class CreditCardDto:IDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CardTypeId { get; set; }
        public string CardTypeName { get; set; }
        public string CardNumber { get; set; }
        public string FirstNameOnTheCard { get; set; }
        public string LastNameOnTheCard { get; set; }
        public int ExpirationMonth { get; set; }
        public int ExpirationYear { get; set; }
        public string Cvv { get; set; }
        public bool SelectedCard { get; set; }
    }
}
