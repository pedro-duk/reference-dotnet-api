namespace APICatalogo.Repositories
{
    public interface IUnitOfWork
    {
        // Properties to obtain instances of the repositories
        //      Better to use properties to expose the functionalities
        //      This encapsulates the access and allows for better control of how they are exposed and used
        //      Also makes it easier to make unit tests
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }

        // Method to persist changes - synchronous
        //      Commits all pending changes
        void Commit();
    }
}
