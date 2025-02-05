﻿using Business.Abstract;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class SocialManager : ISocialService
    {
        private readonly ISocialDal _socialDal;

        public SocialManager(ISocialDal socialDal)
        {
            _socialDal = socialDal;
        }

        [CacheAspect]
        public IDataResult<List<SocialMedia>> GetAll()
        {
            return new SuccessDataResult<List<SocialMedia>>(_socialDal.GetAll().OrderByDescending(x => x.Id).ToList());
        }

        [CacheAspect]
        public IDataResult<SocialMedia> GetById(int id)
        {
            return new SuccessDataResult<SocialMedia>(_socialDal.Get(x => x.Id == id));
        }

        [CacheAspect]
        public IDataResult<List<SocialMedia>> GetByType(string type)
        {
            return new SuccessDataResult<List<SocialMedia>>(_socialDal.GetAll(x => x.Type == type));
        }

        [CacheRemoveAspect("ISocialService.Get")]
        public IResult Add(SocialMedia socialMedia)
        {
            _socialDal.Add(socialMedia);
            return new SuccessResult(Messages.SocialMediaAdded);
        }

        [CacheRemoveAspect("ISocialService.Get")]
        public IResult Delete(SocialMedia socialMedia)
        {
            _socialDal.Delete(socialMedia);
            return new SuccessResult(Messages.SocialMediaDeleted);

        }

        [CacheRemoveAspect("ISocialService.Get")]
        public IResult Update(SocialMedia socialMedia)
        {
            _socialDal.Update(socialMedia);
            return new SuccessResult(Messages.SocialMediaUpdated);

        }
    }
}