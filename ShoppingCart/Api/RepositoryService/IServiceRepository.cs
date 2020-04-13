using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Api
{
    interface IServiceRepository<TEntity, TViewModel> where TEntity : BaseModel
    {
        List<TViewModel> GetAll();
        TEntity Get(Guid id);
        TViewModel Update(Guid id, TViewModel model);
        TEntity Create(TViewModel model);
        void Remove(Guid id);
    }
}
