using APICatalogo.Context;

namespace APICatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository? _produtoRepo;
        private ICategoriaRepository? _categoriaRepo;

        // Context needs to be public
        //      By using it here, we make sure every repo is using the same context instance in a request!
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        // Exposing the the ProdutoRepository variable
        public IProdutoRepository ProdutoRepository
        {
            // If instance exists, we pass the same instance.
            //      We could inject in the constructor, but that would create a new instance
            //      This isn't 'wrong' but in complex systems it may not be always needed
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context); // null coalescence
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }

        // Commits every change in a DB context!
        public void Commit()
        {
            _context.SaveChanges();
        }

        // Releases resources associated to context
        //      DBContext on EFCore allocates unmanaged resources, like DB Connections
        //      This method is also automatically called if you implement IDisposable
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
