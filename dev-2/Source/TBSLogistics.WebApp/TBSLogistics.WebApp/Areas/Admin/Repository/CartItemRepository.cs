using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Repository
{
    public interface ICartItemRepository
    {
        IEnumerable<Tbs_CartItem> GetAll();
        void Add(Tbs_CartItem data);
        void Update(Tbs_CartItem data);
        void Delete(Tbs_CartItem data);
    }
    public class CartItemRepository : ICartItemRepository
    {
        private bool disposed;
        public TBSDBEntities Context { get; set; }
        public CartItemRepository(IUnitOfWork<TBSDBEntities> unitOfWork): this(unitOfWork.Context)
        {

        }
        public CartItemRepository(TBSDBEntities context)
        {
            disposed = false;
            Context = context;
        }
        public void Add(Tbs_CartItem data)
        {
            Context.Set<Tbs_CartItem>().Add(data);
        }

        public void Delete(Tbs_CartItem data)
        {
              Context.Set<Tbs_CartItem>().Remove(data);
        }

        public void Update(Tbs_CartItem data)
        {
           Context.Entry(data).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Tbs_CartItem> GetAll()
        {
            return Context.Tbs_CartItem.ToList();
        }
    }
}