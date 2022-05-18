using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Tbs_Order> GetAll();
        void Add(Tbs_Order data);
        void Update(Tbs_Order data);
        void Delete(Tbs_Order data);
    }
    public class OrderRepository : IOrderRepository
    {
        private bool disposed;
        public TBSDBEntities Context { get; set; }
        public OrderRepository(IUnitOfWork<TBSDBEntities> unitOfWork): this(unitOfWork.Context)
        {

        }
        public OrderRepository(TBSDBEntities context)
        {
            disposed = false;
            Context = context;
        }
        public void Add(Tbs_Order data)
        {
            Context.Set<Tbs_Order>().Add(data);
        }

        public void Delete(Tbs_Order data)
        {
              Context.Set<Tbs_Order>().Remove(data);
        }

        public void Update(Tbs_Order data)
        {
           Context.Entry(data).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Tbs_Order> GetAll()
        {
            return Context.Tbs_Order.ToList();
        }
    }
}