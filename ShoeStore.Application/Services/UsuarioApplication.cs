using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Usuario.Request;
using ShoeStore.Application.Dtos.Usuario.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.Usuario;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;
using BC = BCrypt.Net.BCrypt;

namespace ShoeStore.Application.Services
{
    public class UsuarioApplication : IUsuarioApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UsuarioValidator _validator;

        public UsuarioApplication(IUnitOfWork unitOfWork, IMapper mapper, UsuarioValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<UsuarioResponseDto>>> ListUsuarios(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<UsuarioResponseDto>>();
            try
            {
                var usuarios = await _unitOfWork.Usuario.ListUsuarios(filters);

                if (usuarios is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<UsuarioResponseDto>>(usuarios);
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

        public async Task<BaseResponse<UsuarioResponseDto>> UsuarioById(int id)
        {
            var response = new BaseResponse<UsuarioResponseDto>();
            try
            {
                var usuarios = await _unitOfWork.Usuario.GetByIdAsync(id);

                if (usuarios is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<UsuarioResponseDto>(usuarios);
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

        public async Task<BaseResponse<bool>> RegisterUsuario(UsuarioRequestDto request)
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

                var account = _mapper.Map<TbUsuario>(request);
                account.Contrasena = BC.HashPassword(account.Contrasena);

                response.Data = await _unitOfWork.Usuario.RegisterAsync(account);

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

        public async Task<BaseResponse<bool>> EditUsuario(int id, UsuarioRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var usuarioEdit = await UsuarioById(id);

                if (usuarioEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var usuario = _mapper.Map<TbUsuario>(request);
                usuario.Id = id;

                if (request.Contrasena is not null)
                {
                    usuario.Contrasena = BC.HashPassword(usuario.Contrasena);
                }
                else
                {
                    usuario.Contrasena = usuarioEdit.Data.Contrasena!;
                }

                response.Data = await _unitOfWork.Usuario.EditAsync(usuario);

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

        public async Task<BaseResponse<bool>> RemoveUsuario(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var usuario = await UsuarioById(id);

                if (usuario.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.Usuario.RemoveAsync(id);

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