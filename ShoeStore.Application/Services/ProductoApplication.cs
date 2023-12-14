using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Producto.Response;
using ShoeStore.Application.Dtos.Producto.Request;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.Producto;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Application.Services
{
    public class ProductoApplication : IProductoApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ProductoValidator _validator;

        public ProductoApplication(IUnitOfWork unitOfWork, IMapper mapper, ProductoValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<ProductoResponseDto>>> ListProductos(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<ProductoResponseDto>>();
            try
            {
                var productos = await _unitOfWork.Producto.ListProductos(filters);

                if (productos is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<ProductoResponseDto>>(productos);
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

        public async Task<BaseResponse<BaseEntityResponse<ProductoResponseDto>>> ListProductosByUser(BaseFiltersRequest filters, int userId)
        {
            var response = new BaseResponse<BaseEntityResponse<ProductoResponseDto>>();
            try
            {
                var productos = await _unitOfWork.Producto.ListProductoByUser(filters, userId);

                if (productos is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<ProductoResponseDto>>(productos);
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

        public async Task<BaseResponse<ProductoResponseDto>> ProductoById(int id)
        {
            var response = new BaseResponse<ProductoResponseDto>();
            try
            {
                var producto = await _unitOfWork.Producto.GetByIdAsync(id);

                if (producto is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<ProductoResponseDto>(producto);
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

        public async Task<BaseResponse<bool>> RegisterProducto(ProductoRequestDto request)
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

                var producto = _mapper.Map<TbProducto>(request);
                producto.Imagen = await _unitOfWork.ServerStorage.SaveFile(request.Imagen!);
                response.Data = await _unitOfWork.Producto.RegisterAsync(producto);

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

        public async Task<BaseResponse<bool>> EditProducto(int id, ProductoRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var productoEdit = await ProductoById(id);

                if (productoEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var producto = _mapper.Map<TbProducto>(request);
                producto.Id = id;

                if (request.Imagen is not null)
                {
                    producto.Imagen = await _unitOfWork.ServerStorage.SaveFile(request.Imagen!);
                }

                else
                {
                    producto.Imagen = productoEdit.Data.Imagen!;
                }

                response.Data = await _unitOfWork.Producto.EditAsync(producto);

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

        public async Task<BaseResponse<bool>> RemoveProducto(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var producto = await ProductoById(id);

                if (producto.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Producto.RemoveAsync(id);

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
