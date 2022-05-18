using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Repository
{
    public interface ICartRepository
    {
        IEnumerable<Tbs_Cart> GetAll();
        void Add(Tbs_Cart data);
        void Update(Tbs_Cart data);
        void Delete(Tbs_Cart data);
    }
    public class CartRepository : ICartRepository
    {
        private bool disposed;
        public TBSDBEntities Context { get; set; }
        public CartRepository(IUnitOfWork<TBSDBEntities> unitOfWork): this(unitOfWork.Context)
        {

        }
        public CartRepository(TBSDBEntities context)
        {
            disposed = false;
            Context = context;
        }
        public void Add(Tbs_Cart data)
        {
            Context.Set<Tbs_Cart>().Add(data);
        }

        public void Delete(Tbs_Cart data)
        {
              Context.Set<Tbs_Cart>().Remove(data);
        }

        public void Update(Tbs_Cart data)
        {
           Context.Entry(data).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Tbs_Cart> GetAll()
        {
            return Context.Tbs_Cart.ToList();
        }
    }
}