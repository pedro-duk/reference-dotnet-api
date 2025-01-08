using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbContext context) : base(context) { }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        // Uses the GetAll function from Repository and applies a filter
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
