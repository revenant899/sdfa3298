using sdfa3298.ViewModels;

namespace sdfa3298.Repositories.Products
{
    public interface IProductRepository
    {
        IQueryable<Models.Product> Products { get; }
        IQueryable<Models.Product> GetByCategory(string category, PaginationVM pagination);
        IQueryable<Models.Product> GetByPagination(PaginationVM pagination);
        IQueryable<Models.Product> SetPagination(IQueryable<Models.Product> product, PaginationVM pagination);
        Models.Product Create(Models.Product product);
        Models.Product? Update(Models.Product product);
        Models.Product? Delete(int id);
        Models.Product? GetById(int id);
        bool IsExist(string name);
        Models.Product? GetByName(string name);
        IQueryable<Models.Product> GetByPrice(double min, double max, PaginationVM pagination);
    }
}
