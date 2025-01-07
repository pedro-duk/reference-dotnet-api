using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;
    public ProdutoRepository(AppDbContext context)
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
    public IQueryable<Produto> GetProdutos()
    {
        return _context.Produtos;
    }

    public Produto GetProduto(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if (produto is null)
            throw new InvalidOperationException("Produto é null");

        return produto;
    }

    public Produto Create(Produto produto)
    {
        if (produto is null)
            throw new InvalidOperationException("Produto é null");

        _context.Produtos.Add(produto);
        _context.SaveChanges();
        return produto;
    }

    public bool Update(Produto produto)
    {
        if (produto is null)
            throw new InvalidOperationException("Produto é null");

        if (_context.Produtos.Any(p => p.ProdutoId == produto.ProdutoId))
        {
            _context.Produtos.Update(produto); // Same as changing the EntityState
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool Delete(int id)
    {
        var produto = _context.Produtos.Find(id);

        if (produto is not null) // same as produto != null
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}
