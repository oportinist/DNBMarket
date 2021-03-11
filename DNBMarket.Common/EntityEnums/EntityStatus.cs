using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DNBMarket.Common.EntityEnums
{
    public enum EntityStatus
    {
        [Display(Name = "Silinmiş")]
        Deleted = -1,

        [Display(Name = "Pasif")]
        Passive = 0,

        [Display(Name = "Aktif")]
        Active = 1,

        [Display(Name = "Beklemede")]
        Waiting = 2
    }
}
