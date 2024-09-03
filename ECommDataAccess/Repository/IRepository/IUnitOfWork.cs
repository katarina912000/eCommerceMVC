using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommDataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //here we will have all repos
        //we will  be using this for all common methods

        ICategoryRepository Category{ get; }
        IProductRepository Product { get; }
        void Save();

    }
   
}
