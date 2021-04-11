using System;
using System.Collections.Generic;
using System.Text;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class FindexManager:IFindeksService
    {
        private IFindeksDal _findeksDal;

        public FindexManager(IFindeksDal findexDal)
        {
            _findeksDal = findexDal;
        }

        public IDataResult<Findeks> GetFindexByCustomerId(int customerId)
        {
            var result = _findeksDal.Get(f => f.CustomerId == customerId);
            if (result == null)
            {
                return new ErrorDataResult<Findeks>(Messages.UserHasNoFindex);
            }
            return new SuccessDataResult<Findeks>(result);
        }

        public IResult Add(Findeks findeks)
        {
            var calculatedFindex = CalculateFindeksScore(findeks);
            _findeksDal.Add(calculatedFindex.Data);
            return new SuccessResult(Messages.FindexAdded);
        }

        public IDataResult<Findeks> CalculateFindeksScore(Findeks findeks)
        {
            findeks.Score = (short) new Random().Next(0, 1901);
            return new SuccessDataResult<Findeks>(findeks);
        }

       
    }
}
