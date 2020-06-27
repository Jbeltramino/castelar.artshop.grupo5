using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ArtShop.Data.Model;
using System.Data.Entity;

namespace ArtShop.Data.Services
{
    public class BaseDataService<T> : IDataService<T> where T : IdentityBase, new()
    {
        private RestoDBContext db;

        public BaseDataService()
        {
            db = new RestoDBContext();
        }
        public T Create(T entity)
        {
            db.Set<T>().Add(entity);
            db.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }

        public virtual List<T> Get(Expression<Func<T, bool>> whereExpression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderFunction = null, string includeEntities = "")
        {
            IQueryable<T> query = db.Set<T>();

            if (whereExpression != null)
            {
                query = query.Where(whereExpression);
            }

            var entity = includeEntities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var model in entity)
            {
                query = query.Include(model);
            }

            if (orderFunction != null)
            {
                query = orderFunction(query);
            }

            return query.ToList();
        }

        public virtual T GetById(int id)
        {
            return db.Set<T>().SingleOrDefault(x => x.Id == id);
        }

        public virtual void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = db.Set<T>().Find(id);
            db.Set<T>().Remove(entity);
            db.SaveChanges();
        }

        public List<ValidationResult> ValidateModel(T model)
        {
            ValidationContext v = new ValidationContext(model);
            List<ValidationResult> r = new List<ValidationResult>();

            bool validate = Validator.TryValidateObject(model, v, r, true);

            if (validate)
            {
                return null;
            }
            else
            {
                return r;
            }
        }
    }
}
