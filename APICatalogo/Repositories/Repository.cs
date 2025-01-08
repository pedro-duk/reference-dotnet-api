using System.Linq.Expressions;
using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class // Makes sure T is a class
    {
        // Protected is needed so it's accessed by the hierarchy
        protected readonly AppDbContext _context;
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        // IQueryable is different from IEnumerable
        //      Returns a QUERY IQueryable which represents the selection of every product on the database
        //      The query is not executed immediately. It can be modified or extended before being executed.
        //          Lazy loading! Allows for filters, ordering, projections...
        //          Allows us to optimize queries!
        //          The query is actually executed at the last operator applied!
        //          Operators: Where, OrderBy, Select, etc
        public IEnumerable<T> GetAll()
        {
            // Obtains the Set corresponding to T on the database.
            //      Through this we can make any operation.
            return _context.Set<T>().ToList();
        }

        public T? Get(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public T Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _context.Set<T>().Update(entity); // This marks every column as updated, so EF Core generates a SQL to update every column.
            //_context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}
