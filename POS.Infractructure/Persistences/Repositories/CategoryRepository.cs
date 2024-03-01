using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infractructure.Commons.Bases;
using POS.Infractructure.Commons.Bases.Request;
using POS.Infractructure.Persistences.Context;
using POS.Infractructure.Persistences.Interfaces;

namespace POS.Infractructure.Persistences.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
       

        public CategoryRepository(PosContext context) : base(context) { }
       

        public async Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<Category>();
            //Devolover registro de categoria que no han sido eliminado por algun usuario
            /*
            var categories = (from c in _context.Categories
                              where c.AuditDeleteUser == null && c.AuditDeleteDate == null 
                              select c).AsNoTracking().AsQueryable();
            */
            var categories = GetEntityQuery(x => x.AuditDeleteUser == null && x.AuditDeleteDate == null);

            //Filtro
            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        categories = categories.Where(x => x.Name!.Contains(filters.TextFilter)); break;
                        case 2:
                        categories = categories.Where(x => x.Description!.Contains(filters.TextFilter)); break;

                }

            }
            //Filtro
            if (filters.StateFilter is not null)
            {
                categories = categories.Where(x => x.State.Equals(filters.StateFilter));
            }
            //Filtro
            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                categories = categories.Where(x => x.AuditCreateDate >= 
                Convert.ToDateTime(filters.StartDate) && x.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }
            //Filtro
            if (filters.Sort is null)
            {
                filters.Sort = "Id";
            }
            //Total de registros
            response.TotalRecords = await categories.CountAsync();
            //total de item que yo quiero mostrar
            response.Items = await Ordering(filters, categories, !(bool)filters.Download!).ToListAsync();
            return response;
        }

    }
}
