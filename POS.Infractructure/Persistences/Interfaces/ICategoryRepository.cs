using POS.Domain.Entities;
using POS.Infractructure.Commons.Bases;
using POS.Infractructure.Commons.Bases.Request;

namespace POS.Infractructure.Persistences.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters);
        
    }
}
