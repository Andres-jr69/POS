using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Infractructure.Commons.Bases;
using POS.Infractructure.Commons.Bases.Request;

namespace POS.Application.Interfaces
{
    public interface ICategoryApplication
    {
        Task<BaseResponse<BaseEntityResponse<CategoryResponseDTO>>> ListCategories(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<CategorySelectResponseDTO>>> ListSelectCategories();
        Task<BaseResponse<CategoryResponseDTO>> CategoryById(int categoryId);
        Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDTO requestDTO);
        Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDTO requestDTO);
        Task<BaseResponse<bool>> RemoveCategory(int categoryId);
    }
}
