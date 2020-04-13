using AutoMapper;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicationDbContext = ShoppingCart.Data.ApplicationDbContext;

namespace ShoppingCart.Api
{
    public class ServiceRepository<TEntity, TViewModel> : IServiceRepository<TEntity, TViewModel> where TEntity : BaseModel
    {

        private readonly ApplicationDbContext _dbContext;

        public ServiceRepository()
        {
            _dbContext = DependencyResolver.Current.GetService<ApplicationDbContext>();
        }
        public TEntity Create(TViewModel model)
        {
            try
            {
                var entity = Mapper.Map<TEntity>(model);
                entity.SetId();

                _dbContext.Set<TEntity>().Add(entity);
                _dbContext.SaveChanges();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TEntity Get(Guid id)
        {
            return _dbContext.Set<TEntity>().Find(id);
        }

        public List<TViewModel> GetAll()
        {
            return _dbContext.Set<TEntity>().ToList()
                        .Select(
                    c => Mapper.Map<TViewModel>(c)
                ).ToList();
        }

        public void Remove(Guid id)
        {
            try
            {
                var entity = Get(id);

                _dbContext.Set<TEntity>().Remove(entity);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TViewModel Update(Guid id, TViewModel model)
        {
            try
            {

                var entity = Get(id);

                if (entity != null)
                {
                    //Copying data from vm to entity
                    Mapper.Map(model, entity);
                    entity.SetUpdatedTimeStamp();
                }

                _dbContext.Entry(entity).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return Mapper.Map<TViewModel>(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}