using AutoMapper;
using ShoppingCart.Data;
using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Database = ShoppingCart.Data.Database;

namespace ShoppingCart.Api
{
    public class ServiceRepository<TClass, TViewModel> : IServiceRepository<TClass, TViewModel> where TClass : BaseModel
    {

        private readonly Database _dbContext;

        public ServiceRepository()
        {
            _dbContext = DependencyResolver.Current.GetService<Database>();
        }
        public TClass Create(TViewModel model)
        {
            try
            {
                var item = Mapper.Map<TClass>(model);
                item.SetId();

                _dbContext.Set<TClass>().Add(item);
                _dbContext.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public TClass Get(Guid id)
        {
            return _dbContext.Set<TClass>().Find(id);
        }

        public List<TViewModel> GetAll()
        {
            return _dbContext.Set<TClass>().ToList()
                        .Select(
                    c => Mapper.Map<TViewModel>(c)
                ).ToList();
        }

        public void Remove(Guid id)
        {
            try
            {
                var item = Get(id);

                _dbContext.Set<TClass>().Remove(item);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TClass Update(Guid id, TViewModel model)
        {
            try
            {

                var item = Get(id);
                if (item != null)
                {
                    //Copying data from vm to entity
                    Mapper.Map(model, item);
                }

                _dbContext.Entry(item).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }

}