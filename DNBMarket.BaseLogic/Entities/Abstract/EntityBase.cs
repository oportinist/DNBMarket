using DNBMarket.Common.EntityEnums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNBMarket.BaseLogic.Entities.Abstract
{
    public abstract class EntityBase
    {
        public virtual DateTime CreateDate { get; set; } = DateTime.Now;
        public virtual DateTime UpdateDate { get; set; } = DateTime.Now;
        public virtual int Status { get; set; } = (int)EntityStatus.Active;
    }
}
