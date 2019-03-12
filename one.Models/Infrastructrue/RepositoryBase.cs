using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using one.Data.Models;
using PagedList;



namespace one.Data.Infrastructrue
{
    public abstract class RepositoryBase<T> where T : class
    {
        private OneDotEntities dbContext;
        private readonly IDbSet<T> dbset;

        protected RepositoryBase() {

            dbContext = new DBContext().Get();
            dbset = dbContext.Set<T>();
        }


        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }


        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }


        protected OneDotEntities DataContext
        {
            get { return dbContext ?? (dbContext = DatabaseFactory.Get()); }
        }




        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }


        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }



        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }


        


        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }


        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }


        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }


        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }



        protected virtual System.Data.Entity.EntityState getEntitystate(string Id) {

            if (Id == null)
            {
                return EntityState.Added;
            }
            else {
                return EntityState.Modified;
            }
        }

        protected virtual System.Data.Entity.EntityState getEntitystate(int? Id)
        {

            if (Id == null)
            {
                return EntityState.Added;
            }
            else
            {
                return EntityState.Modified;
            }
        }




        /// <summary>
        /// Return a paged list of entities
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="page">Which page to retrieve</param>
        /// <param name="where">Where clause to apply</param>
        /// <param name="order">Order by to apply</param>
        /// <returns></returns>
        public virtual IPagedList<T> GetPage<TOrder>(Page page, Expression<Func<T, bool>> where, Expression<Func<T, TOrder>> order)
        {
            var results = dbset.OrderBy(order).Where(where).GetPage(page).ToList();
            var total = dbset.Count(where);
            return new StaticPagedList<T>(results, page.PageNumber, page.PageSize, total);
        }





        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }



    }
}
