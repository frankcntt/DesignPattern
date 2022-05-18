using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TBSLogistics.WebApp.Models;

namespace TBSLogistics.WebApp.Areas.Admin.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Tbs_Category> GetAll();
        void Add(Tbs_Category data);
        void Update(Tbs_Category data);
        void Delete(Tbs_Category data);
    }
    public class CategoryRepository : ICategoryRepository
    {
        private bool disposed;
        public TBSDBEntities Context { get; set; }
        public CategoryRepository(IUnitOfWork<TBSDBEntities> unitOfWork): this(unitOfWork.Context)
        {

        }
        public CategoryRepository(TBSDBEntities context)
        {
            disposed = false;
            Context = context;
        }
        public void Add(Tbs_Category data)
        {
            Context.Set<Tbs_Category>().Add(data);
        }

        public void Delete(Tbs_Category data)
        {
              Context.Set<Tbs_Category>().Remove(data);
        }

        public void Update(Tbs_Category data)
        {
           Context.Entry(data).State = System.Data.Entity.EntityState.Modified;
        }

        public IEnumerable<Tbs_Category> GetAll()
        {
            return Context.Tbs_Category.ToList();
        }
    }
}