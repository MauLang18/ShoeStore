using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Pedido.Request;
using ShoeStore.Application.Dtos.Pedido.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.Pedido;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;

namespace ShoeStore.Application.Services
{
    public class PedidoApplication : IPedidoApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly PedidoValidator _validator;

        public PedidoApplication(IUnitOfWork unitOfWork, IMapper mapper, PedidoValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<PedidoResponseDto>>> ListPedidos(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<PedidoResponseDto>>();
            try
            {
                var pedido = await _unitOfWork.Pedido.ListPedidos(filters);

                if (pedido is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<PedidoResponseDto>>(pedido);
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

        public async Task<BaseResponse<IEnumerable<PedidoSelectResponseDto>>> ListPedidosByUser(int userId)
        {
            var response = new BaseResponse<IEnumerable<PedidoSelectResponseDto>>();
            try
            {
                var pedidos = await _unitOfWork.Pedido.ListPedidosByUser(userId);

                if (pedidos is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<PedidoSelectResponseDto>>(pedidos);
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

        public async Task<BaseResponse<PedidoResponseDto>> PedidoById(int id)
        {
            var response = new BaseResponse<PedidoResponseDto>();
            try
            {
                var pedidos = await _unitOfWork.Pedido.GetByIdAsync(id);

                if (pedidos is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<PedidoResponseDto>(pedidos);
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

        public async Task<BaseResponse<PedidoResponseDto>> PedidoByProductoByUser(int productoId, int userId)
        {
            var response = new BaseResponse<PedidoResponseDto>();
            try
            {
                var pedido = await _unitOfWork.Pedido.PedidoByProdcutoByUser(productoId, userId);

                if (pedido is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<PedidoResponseDto>(pedido);
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

        public async Task<BaseResponse<bool>> RegisterPedido(PedidoRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var pedidoEdit = await PedidoByProductoByUser(request.Producto, request.Comprador);

                if (pedidoEdit.Data is not null)
                {
                    await EditPedidoByProductoByUser(request.Producto, request.Comprador, request);
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

                var pedido = _mapper.Map<TbPedido>(request);
                response.Data = await _unitOfWork.Pedido.RegisterAsync(pedido);

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

        public async Task<BaseResponse<bool>> EditPedido(int id, PedidoRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var pedidoEdit = await PedidoById(id);

                if (pedidoEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var pedido = _mapper.Map<TbPedido>(request);
                pedido.Id = id;
                response.Data = await _unitOfWork.Pedido.EditAsync(pedido);

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

        public async Task<BaseResponse<bool>> EditPedidoByProductoByUser(int productoId, int userId, PedidoRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var pedido = _mapper.Map<TbPedido>(request);
                response.Data = await _unitOfWork.Pedido.EditPedidoByProductoByUser(productoId, userId, pedido.Cantidad);

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

        public async Task<BaseResponse<bool>> RemoveAllPedidos(int userId)
        {
            var response = new BaseResponse<bool>();
            try
            {
                response.Data = await _unitOfWork.Pedido.DeletePedidoByUser(userId);

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

        public async Task<BaseResponse<bool>> RemovePedido(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var pedido = await PedidoById(id);

                if (pedido.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Pedido.RemoveAsync(id);

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
