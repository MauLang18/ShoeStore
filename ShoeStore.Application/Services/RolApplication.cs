using AutoMapper;
using FluentValidation;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Rol.Request;
using ShoeStore.Application.Dtos.Rol.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.Rol;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;

namespace SellTech.Application.Services
{
    public class RolApplication : IRolApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly RolValidator _validator;

        public RolApplication(IUnitOfWork unitOfWork, IMapper mapper, RolValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<RolResponseDto>>> ListRoles(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<RolResponseDto>>();
            try
            {
                var roles = await _unitOfWork.Rol.ListRoles(filters);

                if (roles is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<RolResponseDto>>(roles);
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

        public async Task<BaseResponse<IEnumerable<RolSelectResponseDto>>> ListSelectRoles()
        {
            var response = new BaseResponse<IEnumerable<RolSelectResponseDto>>();
            try
            {
                var roles = await _unitOfWork.Rol.GetAllAsync();

                if (roles is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<IEnumerable<RolSelectResponseDto>>(roles);
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

        public async Task<BaseResponse<RolResponseDto>> RolById(int id)
        {
            var response = new BaseResponse<RolResponseDto>();
            try
            {
                var rol = await _unitOfWork.Rol.GetByIdAsync(id);

                if (rol is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<RolResponseDto>(rol);
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

        public async Task<BaseResponse<bool>> RegisterRol(RolRequestDto request)
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

                var rol = _mapper.Map<TbRol>(request);
                response.Data = await _unitOfWork.Rol.RegisterAsync(rol);

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

        public async Task<BaseResponse<bool>> EditRol(int id, RolRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var rolEdit = await RolById(id);

                if (rolEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var rol = _mapper.Map<TbRol>(request);
                rol.Id = id;
                response.Data = await _unitOfWork.Rol.EditAsync(rol);

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

        public async Task<BaseResponse<bool>> RemoveRol(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var rol = await RolById(id);

                if (rol.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Rol.RemoveAsync(id);

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
