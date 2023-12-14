using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Direccion.Request;
using ShoeStore.Application.Dtos.Direccion.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.Direccion;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;

namespace ShoeStore.Application.Services
{
    public class DireccionApplication : IDireccionApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly DireccionValidator _validator;

        public DireccionApplication(IUnitOfWork unitOfWork, IMapper mapper, DireccionValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<DireccionResponseDto>>> ListDirecciones(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<DireccionResponseDto>>();
            try
            {
                var direcciones = await _unitOfWork.Direccion.ListDirecciones(filters);

                if (direcciones is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<DireccionResponseDto>>(direcciones);
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

        public async Task<BaseResponse<DireccionResponseDto>> DireccionById(int id)
        {
            var response = new BaseResponse<DireccionResponseDto>();
            try
            {
                var direccion = await _unitOfWork.Direccion.GetByIdAsync(id);

                if (direccion is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<DireccionResponseDto>(direccion);
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

        public async Task<BaseResponse<BaseEntityResponse<DireccionResponseDto>>> ListDireccionesByUser(BaseFiltersRequest filters, int userId)
        {
            var response = new BaseResponse<BaseEntityResponse<DireccionResponseDto>>();
            try
            {
                var direcciones = await _unitOfWork.Direccion.ListDireccionesByUser(filters, userId);

                if (direcciones is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<DireccionResponseDto>>(direcciones);
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

        public async Task<BaseResponse<IEnumerable<DireccionSelectResponseDto>>> ListSelectDireccionByUser(int userId)
        {
            var response = new BaseResponse<IEnumerable<DireccionSelectResponseDto>>();
            try
            {
                var direcciones = await _unitOfWork.Direccion.ListSelectDireccionByUser(userId);

                if (direcciones is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<DireccionSelectResponseDto>>(direcciones);
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

        public async Task<BaseResponse<bool>> RegisterDireccion(DireccionRequestDto request)
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

                var direccion = _mapper.Map<TbDireccion>(request);
                response.Data = await _unitOfWork.Direccion.RegisterAsync(direccion);

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

        public async Task<BaseResponse<bool>> EditDireccion(int id, DireccionRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var direccionEdit = await DireccionById(id);

                if (direccionEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var direccion = _mapper.Map<TbDireccion>(request);
                direccion.Id = id;
                response.Data = await _unitOfWork.Direccion.EditAsync(direccion);

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

        public async Task<BaseResponse<bool>> RemoveDireccion(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var direccion = await DireccionById(id);

                if (direccion.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Direccion.RemoveAsync(id);

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
