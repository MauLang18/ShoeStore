using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.MetodoPago.Request;
using ShoeStore.Application.Dtos.MetodoPago.Response;
using ShoeStore.Application.Dtos.OpcionEnvio.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.MetodoPago;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;

namespace ShoeStore.Application.Services
{
    public class MetodoPagoApplication : IMetodoPagoApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly MetodoPagoValidator _validator;

        public MetodoPagoApplication(IUnitOfWork unitOfWork, IMapper mapper, MetodoPagoValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<MetodoPagoResponseDto>>> ListMetodosPagos(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<MetodoPagoResponseDto>>();
            try
            {
                var metodosPagos = await _unitOfWork.MetodoPago.ListMetodosPagos(filters);

                if (metodosPagos is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<MetodoPagoResponseDto>>(metodosPagos);
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

        public async Task<BaseResponse<IEnumerable<MetodoPagoSelectResponseDto>>> ListSelectMetodosPagos()
        {
            var response = new BaseResponse<IEnumerable<MetodoPagoSelectResponseDto>>();
            try
            {
                var metodosPagos = await _unitOfWork.MetodoPago.GetAllAsync();

                if (metodosPagos is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<MetodoPagoSelectResponseDto>>(metodosPagos);
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

        public async Task<BaseResponse<MetodoPagoResponseDto>> MetodoPagoById(int id)
        {
            var response = new BaseResponse<MetodoPagoResponseDto>();
            try
            {
                var metodoPago = await _unitOfWork.MetodoPago.GetByIdAsync(id);

                if (metodoPago is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<MetodoPagoResponseDto>(metodoPago);
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

        public async Task<BaseResponse<bool>> RegisterMetodoPago(MetodoPagoRequestDto request)
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

                var metodoPago = _mapper.Map<TbMetodoPago>(request);
                response.Data = await _unitOfWork.MetodoPago.RegisterAsync(metodoPago);

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

        public async Task<BaseResponse<bool>> EditMetodoPago(int id, MetodoPagoRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var metodoPagoEdit = await MetodoPagoById(id);

                if (metodoPagoEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var metodoPago = _mapper.Map<TbMetodoPago>(request);
                metodoPago.Id = id;
                response.Data = await _unitOfWork.MetodoPago.EditAsync(metodoPago);

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

        public async Task<BaseResponse<bool>> RemoveMetodoPago(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var metodoPago = await MetodoPagoById(id);

                if (metodoPago.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.MetodoPago.RemoveAsync(id);

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
