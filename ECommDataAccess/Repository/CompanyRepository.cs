using ECommDataAccess.Repository.IRepository;
using eCommerceUdemy.Data;
using ECommModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommDataAccess.Repository
{
    public class CompanyRepository : Repository<Companyy>, ICompanyRepository
    {
        private ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Companyy company)
        {
           _db.Companies.Update(company);
        }
    }
}
