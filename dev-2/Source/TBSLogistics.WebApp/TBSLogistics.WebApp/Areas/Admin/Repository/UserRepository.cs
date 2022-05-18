using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Repository
{
    public interface IUserRepository
    {
        IEnumerable<AspNetUser> GetAll();
        void Add(AspNetUser user);
        void Update(AspNetUser user);
        void Delete(AspNetUser user);
    }
    public class UserRepository : IUserRepository
    {
        private bool disposed;
        public TBSDBEntities Context { get; set; }
        public UserRepository(IUnitOfWork<TBSDBEntities> unitOfWork): this(unitOfWork.Context)
        {

        }
        public UserRepository(TBSDBEntities context)
        {
            disposed = false;
            Context = context;
        }
        public void Add(AspNetUser user)
        {
            Context.Set<AspNetUser>().Add(user);
        }

        public void Delete(AspNetUser user)
        {
              Context.Set<AspNetUser>().Remove(user);
        }

        public void Update(AspNetUser user)
        {
           Context.Entry(user).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<AspNetUser> GetAll()
        {
            return Context.AspNetUsers.ToList();
        }
    }
}