using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        // Using context from base class repository
        public CategoriaRepository(AppDbContext context) : base(context) { }
    }
}
