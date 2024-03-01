using AutoMapper;
using POS.Application.Commons.Bases;
using POS.Application.Dtos.Category.Request;
using POS.Application.Dtos.Category.Response;
using POS.Application.Interfaces;
using POS.Application.Validators.Category;
using POS.Domain.Entities;
using POS.Infractructure.Commons.Bases;
using POS.Infractructure.Commons.Bases.Request;
using POS.Infractructure.Persistences.Interfaces;
using POS.Utilities.Static;

namespace POS.Application.Services
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CategoryValidator _validationRules;

        public CategoryApplication(IUnitOfWork unitOfWork, IMapper mapper, CategoryValidator validationRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validationRules = validationRules;
        }

        public async Task<BaseResponse<BaseEntityResponse<CategoryResponseDTO>>> ListCategories(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CategoryResponseDTO>>();
            var categories = await _unitOfWork.category.ListCategories(filters);

            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BaseEntityResponse<CategoryResponseDTO>>(categories);
                response.Message = ReplyMessager.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessager.MESSAGE_QUERY_EMPLY;
            }

            return response;
        }
        public async Task<BaseResponse<IEnumerable<CategorySelectResponseDTO>>> ListSelectCategories()
        {
            var response = new BaseResponse<IEnumerable<CategorySelectResponseDTO>>();
            var categories = await _unitOfWork.category.GetAllAsync();
            if (categories is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<IEnumerable<CategorySelectResponseDTO>>(categories);
                response.Message = ReplyMessager.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessager.MESSAGE_QUERY_EMPLY;

            }

            return response;

        }
        public async Task<BaseResponse<CategoryResponseDTO>> CategoryById(int categoryId)
        {
            var response = new BaseResponse<CategoryResponseDTO>();
            var category = await _unitOfWork.category.GetByIdAsync(categoryId);

            if (category is not null)
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<CategoryResponseDTO>(category);
                response.Message = ReplyMessager.MESSAGE_QUERY;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessager.MESSAGE_QUERY_EMPLY;
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterCategory(CategoryRequestDTO requestDTO)
        {
            var response = new BaseResponse<bool>();
            var validacionResulta = await _validationRules.ValidateAsync(requestDTO);
            if (!validacionResulta.IsValid)
            {
                response.IsSuccess = false;
                //Errores de validavion
                response.Message = ReplyMessager.MESSAGE_VALIDATE;
                //Listar los diferentes erorres que existan
                response.Errors = validacionResulta.Errors;
                return response;
            }

            var category = _mapper.Map<Category>(requestDTO);
            response.Data = await _unitOfWork.category.RegisterAsync(category);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessager.MESSAGE_SAVE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessager.MESSAGE_FAILED;
            }
            return response;

        }
        public async Task<BaseResponse<bool>> EditCategory(int categoryId, CategoryRequestDTO requestDTO)
        {
            var response = new BaseResponse<bool>();
            var categoryEdit = await CategoryById(categoryId);
            if (categoryEdit.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessager.MESSAGE_QUERY_EMPLY;
            }

            var category = _mapper.Map<Category>(requestDTO);
            category.Id = categoryId;
            response.Data = await _unitOfWork.category.EditAsync(category);

            if (response.Data )
            {
                response.IsSuccess = true;
                response.Message = ReplyMessager.MESSAGE_UPDATE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessager.MESSAGE_FAILED;
            }
            return response;
        }

        public async Task<BaseResponse<bool>> RemoveCategory(int categoryId)
        {
            var response = new BaseResponse<bool>();
            var category = await CategoryById(categoryId);
            if (category.Data is null)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessager.MESSAGE_QUERY_EMPLY;
            }

            response.Data = await _unitOfWork.category.RemoveAsync(categoryId);

            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = ReplyMessager.MESSAGE_DELETE;
            }
            else
            {
                response.IsSuccess = false;
                response.Message = ReplyMessager.MESSAGE_QUERY_EMPLY;

            }

            return response;
        }
    }
}
