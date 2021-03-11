using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DNBMarket.Common.EntityEnums
{
    public enum PaymentTypes
    {  
        [Display(Name = "Nakit")]
        Cash = 1,

        [Display(Name = "Kredi Kartı")]
        CreditCard = 2
    }
}
