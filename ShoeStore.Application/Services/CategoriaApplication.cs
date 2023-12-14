using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Categoria.Request;
using ShoeStore.Application.Dtos.Categoria.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.Categoria;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;

namespace ShoeStore.Application.Services
{
    public class CategoriaApplication : ICategoriaApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CategoriaValidator _validator;

        public CategoriaApplication(IUnitOfWork unitOfWork, IMapper mapper, CategoriaValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<CategoriaResponseDto>>> ListCategorias(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CategoriaResponseDto>>();
            try
            {
                var categorias = await _unitOfWork.Categoria.ListCategorias(filters);

                if (categorias is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<CategoriaResponseDto>>(categorias);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<CategoriaResponseDto>> CategoriaById(int id)
        {
            var response = new BaseResponse<CategoriaResponseDto>();
            try
            {
                var categoria = await _unitOfWork.Categoria.GetByIdAsync(id);

                if (categoria is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<CategoriaResponseDto>(categoria);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<IEnumerable<CategoriaSelectResponseDto>>> ListSelectCategorias()
        {
            var response = new BaseResponse<IEnumerable<CategoriaSelectResponseDto>>();
            try
            {
                var categorias = await _unitOfWork.Categoria.GetAllAsync();

                if (categorias is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<CategoriaSelectResponseDto>>(categorias);
                    response.Message = ReplyMessage.MESSAGE_QUERY;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RegisterCategoria(CategoriaRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;
                    return response;
                }

                var categoria = _mapper.Map<TbCategorium>(request);
                response.Data = await _unitOfWork.Categoria.RegisterAsync(categoria);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_SAVE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> EditCategoria(int id, CategoriaRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var categoriaEdit = await CategoriaById(id);

                if (categoriaEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var categoria = _mapper.Map<TbCategorium>(request);
                categoria.Id = id;
                response.Data = await _unitOfWork.Categoria.EditAsync(categoria);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_UPDATE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse<bool>> RemoveCategoria(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var categoria = await CategoriaById(id);

                if (categoria.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Categoria.RemoveAsync(id);

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_DELETE;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_FAILED;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ReplyMessage.MESSAGE_EXCEPTION;
                WatchLogger.Log(ex.Message);
            }

            return response;
        }
    }
}
