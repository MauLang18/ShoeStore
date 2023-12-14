using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.CalificacionResena.Request;
using ShoeStore.Application.Dtos.CalificacionResena.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.CalificacionResena;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;

namespace ShoeStore.Application.Services
{
    public class CalificacionResenaApplication : ICalificacionResenaApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CalificacionResenaValidator _validator;

        public CalificacionResenaApplication(IUnitOfWork unitOfWork, IMapper mapper, CalificacionResenaValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<CalificacionResenaResponseDto>>> ListCalificacionesResenas(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CalificacionResenaResponseDto>>();
            try
            {
                var CalificacionResena = await _unitOfWork.CalificacionResena.ListCalificacionesResenas(filters);

                if (CalificacionResena is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<CalificacionResenaResponseDto>>(CalificacionResena);
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

        public async Task<BaseResponse<CalificacionResenaResponseDto>> CalificacionResenaById(int id)
        {
            var response = new BaseResponse<CalificacionResenaResponseDto>();
            try
            {
                var CalificacionResena = await _unitOfWork.CalificacionResena.GetByIdAsync(id);

                if (CalificacionResena is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<CalificacionResenaResponseDto>(CalificacionResena);
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

        public async Task<BaseResponse<IEnumerable<CalificacionResenaSelectResponseDto>>> CalificacionesResenasByProductoId(int productoId)
        {
            var response = new BaseResponse<IEnumerable<CalificacionResenaSelectResponseDto>>();
            try
            {
                var calificacionesResenas = await _unitOfWork.CalificacionResena.ListCalificacionesResenasByProductoId(productoId);

                if (calificacionesResenas is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<CalificacionResenaSelectResponseDto>>(calificacionesResenas);
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

        public async Task<BaseResponse<bool>> RegisterCalificacionResena(CalificacionResenaRequestDto request)
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

                var CalificacionResena = _mapper.Map<TbCalificacionResena>(request);
                response.Data = await _unitOfWork.CalificacionResena.RegisterAsync(CalificacionResena);

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

        public async Task<BaseResponse<bool>> EditCalificacionResena(int id, CalificacionResenaRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var CalificacionResenaEdit = await CalificacionResenaById(id);

                if (CalificacionResenaEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var CalificacionResena = _mapper.Map<TbCalificacionResena>(request);
                CalificacionResena.Id = id;
                response.Data = await _unitOfWork.CalificacionResena.EditAsync(CalificacionResena);

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

        public async Task<BaseResponse<bool>> RemoveCalificacionResena(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var CalificacionResena = await CalificacionResenaById(id);

                if (CalificacionResena.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.CalificacionResena.RemoveAsync(id);

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
