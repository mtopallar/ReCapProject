using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarRentalDto
    {
        
        public string BrandName { get; set; }
        public string CarName { get; set; }
        public string CustomerFullName { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

    }
}
