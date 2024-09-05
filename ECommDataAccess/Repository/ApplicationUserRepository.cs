using ECommDataAccess.Repository.IRepository;
using eCommerceUdemy.Data;
using eCommerceUdemy.Models;
using ECommModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommDataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>,IApplicationUser
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

       
    }
}
