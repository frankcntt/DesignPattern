using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Repository
{
    public interface IProductRepository
    {
        IEnumerable<Tbs_Product> GetAll();
        void Add(Tbs_Product data);
        void Update(Tbs_Product data);
        void Delete(Tbs_Product data);
        void DeleteMultiple(IEnumerable<Tbs_Product> datas);
    }
    public class ProductRepository : IProductRepository
    {
        private bool disposed;
        public TBSDBEntities Context { get; set; }
        public ProductRepository(IUnitOfWork<TBSDBEntities> unitOfWork): this(unitOfWork.Context)
        {

        }
        public ProductRepository(TBSDBEntities context)
        {
            disposed = false;
            Context = context;
        }
        public void Add(Tbs_Product data)
        {
            Context.Set<Tbs_Product>().Add(data);
        }

        public void Delete(Tbs_Product data)
        {
              Context.Set<Tbs_Product>().Remove(data);
        }

        public void Update(Tbs_Product data)
        {
           Context.Entry(data).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Tbs_Product> GetAll()
        {
            return Context.Tbs_Product.ToList();

        }

        public void DeleteMultiple(IEnumerable<Tbs_Product> datas)
        {
            Context.Set<Tbs_Product>().RemoveRange(datas);
        }
    }
}