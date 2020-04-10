using ShoppingCart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Api
{
    interface IServiceRepository<TClass, TViewModel> where TClass : BaseModel
    {
        List<TViewModel> GetAll();
        TClass Get(Guid id);
        TClass Update(Guid id, TViewModel model);
        TClass Create(TViewModel model);
        void Remove(Guid id);
    }
}
