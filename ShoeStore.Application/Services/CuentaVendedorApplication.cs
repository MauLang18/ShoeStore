using AutoMapper;
using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.CuentaVendedor.Request;
using ShoeStore.Application.Dtos.CuentaVendedor.Response;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Validators.CuentaVendedor;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;
using WatchDog;

namespace ShoeStore.Application.Services
{
    public class CuentaVendedorApplication : ICuentaVendedorApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CuentaVendedorValidator _validator;

        public CuentaVendedorApplication(IUnitOfWork unitOfWork, IMapper mapper, CuentaVendedorValidator validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<BaseResponse<BaseEntityResponse<CuentaVendedorResponseDto>>> ListCuentasVendedores(BaseFiltersRequest filters)
        {
            var response = new BaseResponse<BaseEntityResponse<CuentaVendedorResponseDto>>();
            try
            {
                var cuentasVendedores = await _unitOfWork.CuentaVendedor.ListCuentasVendedores(filters);

                if (cuentasVendedores is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<BaseEntityResponse<CuentaVendedorResponseDto>>(cuentasVendedores);
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

        public async Task<BaseResponse<CuentaVendedorResponseDto>> CuentaVendedorById(int id)
        {
            var response = new BaseResponse<CuentaVendedorResponseDto>();
            try
            {
                var cuentaVendedor = await _unitOfWork.CuentaVendedor.GetByIdAsync(id);

                if (cuentaVendedor is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<CuentaVendedorResponseDto>(cuentaVendedor);
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

        public async Task<BaseResponse<CuentaVendedorResponseDto>> CuentaVendedorByUser(int userId)
        {
            var response = new BaseResponse<CuentaVendedorResponseDto>();
            try
            {
                var cuentaVendedor = await _unitOfWork.CuentaVendedor.CuentaVendedorByUser(userId);

                if (cuentaVendedor is not null)
                {
                    response.IsSuccess = true;
                    response.Data = _mapper.Map<CuentaVendedorResponseDto>(cuentaVendedor);
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

        public async Task<BaseResponse<bool>> RegisterCuentVendedor(CuentaVendedorRequestDto request)
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

                var cuentaVendedor = _mapper.Map<TbCuentaVendedor>(request);
                response.Data = await _unitOfWork.CuentaVendedor.RegisterAsync(cuentaVendedor);

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

        public async Task<BaseResponse<bool>> EditCuentaVendedor(int id, CuentaVendedorRequestDto request)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var cuentaVendedorEdit = await CuentaVendedorById(id);

                if (cuentaVendedorEdit.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                var cuentaVendedor = _mapper.Map<TbCuentaVendedor>(request);
                cuentaVendedor.Id = id;
                response.Data = await _unitOfWork.CuentaVendedor.EditAsync(cuentaVendedor);

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

        public async Task<BaseResponse<bool>> RemoveCuentaVendedor(int id)
        {
            var response = new BaseResponse<bool>();
            try
            {
                var cuentaVendedor = await CuentaVendedorById(id);

                if (cuentaVendedor.Data is null)
                {
                    response.IsSuccess = false;
                    response.Message = ReplyMessage.MESSAGE_QUERY_EMPTY;
                    return response;
                }

                response.Data = await _unitOfWork.CuentaVendedor.RemoveAsync(id);

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
