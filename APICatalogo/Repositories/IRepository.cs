using System.Linq.Expressions;

namespace APICatalogo.Repositories
{
    // When making generic repositories, be careful not to violate SOLID's ISP: Interface Segregation
    //      Clients shouldn't be forced to depend from interfaces they don't use
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        // Example of a robust Get method
        //      Receives a lambda expression called predicate
        //      This lambda expression receives a T object and returns a boolean
        //      This is used to filter objects in a collection based on this function!
        // T GetFiltered(Expression<Func<T, bool>> predicate); 
        T? Get(Expression<Func<T, bool>> predicate);

        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}
