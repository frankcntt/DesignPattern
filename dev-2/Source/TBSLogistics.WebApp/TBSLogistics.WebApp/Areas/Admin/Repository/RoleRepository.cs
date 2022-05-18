using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Repository
{
    public interface IRoleRepository
    {
        IEnumerable<AspNetRole> GetAll();
        void Add(AspNetRole data);
        void Update(AspNetRole data);
        void Delete(AspNetRole data);
    }
    public class RoleRepository : IRoleRepository
    {
        private bool disposed;
        public TBSDBEntities Context { get; set; }
        public RoleRepository(IUnitOfWork<TBSDBEntities> unitOfWork): this(unitOfWork.Context)
        {

        }
        public RoleRepository(TBSDBEntities context)
        {
            disposed = false;
            Context = context;
        }
        public void Add(AspNetRole data)
        {
            Context.Set<AspNetRole>().Add(data);
        }

        public void Delete(AspNetRole data)
        {
              Context.Set<AspNetRole>().Remove(data);
        }

        public void Update(AspNetRole data)
        {
           Context.Entry(data).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<AspNetRole> GetAll()
        {
            return Context.AspNetRoles.ToList();
        }
    }
}