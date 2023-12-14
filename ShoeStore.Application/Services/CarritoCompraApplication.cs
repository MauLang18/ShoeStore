using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.CarritoCompra.Request;
using ShoeStore.Application.Dtos.CarritoCompra.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.CarritoCompra;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;

namespace ShoeStore.Application.Services
{
    public class CarritoCompraApplication : ICarritoCompraApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CarritoCompraValidator _validator;

        public CarritoCompraApplication(IUnitOfWork unitOfWork, IMapper mapper, CarritoCompraValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<CarritoCompraResponseDto>>> ListCarritoCompra(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CarritoCompraResponseDto>>();
            try
            {
                var carritoCompra = await _unitOfWork.CarritoCompra.ListCarritoCompra(filters);

                if (carritoCompra is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<CarritoCompraResponseDto>>(carritoCompra);
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

        public async Task<BaseResponse<CarritoCompraResponseDto>> CarritoCompraById(int id)
        {
            var response = new BaseResponse<CarritoCompraResponseDto>();
            try
            {
                var carritoCompra = await _unitOfWork.CarritoCompra.GetByIdAsync(id);

                if (carritoCompra is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<CarritoCompraResponseDto>(carritoCompra);
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

        public async Task<BaseResponse<IEnumerable<CarritoCompraSelectResponseDto>>> ListCarritoCompraByUser(int userId)
        {
            var response = new BaseResponse<IEnumerable<CarritoCompraSelectResponseDto>>();
            try
            {
                var carritoCompra = await _unitOfWork.CarritoCompra.ListCarritoCompraByUser(userId);

                if (carritoCompra is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<CarritoCompraSelectResponseDto>>(carritoCompra);
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

        public async Task<BaseResponse<bool>> RegisterCarritoCompra(CarritoCompraRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var carritoCompraEdit = await CarritoCompraByProductoByUser(request.Producto, request.Comprador);

                if (carritoCompraEdit.Data is not null)
                {
                    await EditCarritoCompraByProductoByUser(request.Producto, request.Comprador, request);
                    response.IsSuccess = true;
                    response.Message = ReplyMessage.MESSAGE_UPDATE;

                    return response;
                }

                var validationResult = await _validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_VALIDATE;
                    response.Errors = validationResult.Errors;
                    return response;
                }

                var carritoCompra = _mapper.Map<TbCarritoCompra>(request);
                response.Data = await _unitOfWork.CarritoCompra.RegisterAsync(carritoCompra);

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

        public async Task<BaseResponse<bool>> EditCarritoCompra(int id, CarritoCompraRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var carritoCompraEdit = await CarritoCompraById(id);

                if (carritoCompraEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var carritoCompra = _mapper.Map<TbCarritoCompra>(request);
                carritoCompra.Id = id;
                response.Data = await _unitOfWork.CarritoCompra.EditAsync(carritoCompra);

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

        public async Task<BaseResponse<bool>> RemoveCarritoCompra(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var carritoCompra = await CarritoCompraById(id);

                if (carritoCompra.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.CarritoCompra.RemoveAsync(id);

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

        public async Task<BaseResponse<bool>> RemoveAllCarritoCompra(int userId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                response.Data = await _unitOfWork.CarritoCompra.DeleteCarritoCompraByUser(userId);

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

        public async Task<BaseResponse<bool>> EditCarritoCompraByProductoByUser(int productoId, int userId, CarritoCompraRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var carritoCompra = _mapper.Map<TbCarritoCompra>(request);
                response.Data = await _unitOfWork.CarritoCompra.EditCarritoCompraByProductoByUser(productoId,userId,carritoCompra.Cantidad);

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

        public async Task<BaseResponse<CarritoCompraResponseDto>> CarritoCompraByProductoByUser(int productoId, int userId)
        {
            var response = new BaseResponse<CarritoCompraResponseDto>();
            try
            {
                var carritoCompra = await _unitOfWork.CarritoCompra.CarritoCompraByProdcutoByUser(productoId,userId);

                if (carritoCompra is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<CarritoCompraResponseDto>(carritoCompra);
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
    }
}
