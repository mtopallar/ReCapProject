using System;
using System.Collections.Generic;
using System.Text;
using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
   public interface IFindeksService
   {
       IDataResult<Findeks> GetFindexByCustomerId(int customerId);
       IResult Add(Findeks findeks);
       IDataResult<Findeks> CalculateFindeksScore(Findeks findeks);
   }
}
