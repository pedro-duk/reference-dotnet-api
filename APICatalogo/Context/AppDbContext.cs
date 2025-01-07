using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Context;


// ## Classe de Contexto
// - Definir mapeamento entre entidades e tabelas
// - Uma instância DbContext representa uma sessão com o banco de dados;
// - DbContextOption para configurar o contexto do EF Core na classe base(define provedor de banco e string de conexão)
// - DbSet: Representa coleção de entidades no contexto q podem ser realizadas operações CRUD
//   - Tipo T define qual é a entidade mapeada

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options ) : base( options )
    {           
    }

    public DbSet<Categoria>? Categorias { get; set; } // Indica uma tabela "Categorias" com as colunas correspondendo às propriedades da entidade "Categoria"
    public DbSet<Produto>? Produtos { get; set; }
}
