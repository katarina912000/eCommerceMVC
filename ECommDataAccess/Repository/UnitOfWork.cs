﻿using ECommDataAccess.Repository.IRepository;
using eCommerceUdemy.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommDataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }
        public ICompanyRepository Company {  get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IApplicationUser ApplicationUser { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        // IOrderHeaderRepository 

        public UnitOfWork(ApplicationDbContext db)
        {
            _db=    db;
            ShoppingCart = new ShoppingCartRepository(_db);

            Category = new CategoryRepository(_db);
            Product=new ProductRepository(_db);
            Company=new CompanyRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail=new OrderDetailRepository(_db);

        }


        public void Save()
        {
           // SET IDENTITY_INSERT student ON
           
            _db.SaveChanges();
            
        }
    }
}
