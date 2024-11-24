using Application.Web.Common;
using Application.Web.InputModel;
using Application.Web.Services;
using Application.Web.Services.Interface;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DartComputers.Web.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        protected APIResponse _response;

        public UserController(IAuthService authService)
        {
            _authService = authService;
            _response = new APIResponse();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Register")]
        public async Task<ActionResult<APIResponse>> Register(Register register)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommonMessege.RegistrationFailed);
                    return _response;
                }

                var result = await _authService.Register(register);

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
                _response.DisplayMessege = CommonMessege.RegistrationSuccess;
                _response.Result = result;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessege.SystemError);
            }

            return Ok(_response);
        
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Login")]
        public async Task<ActionResult<APIResponse>> Login(Login login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.AddError(ModelState.ToString());
                    _response.AddWarning(CommonMessege.LoginFailed);
                    return _response;
                }

                var result = await _authService.Login(login);

                if(result is string)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    _response.DisplayMessege= CommonMessege.LoginFailed;
                    _response.Result = result;
                    return Ok(_response);
                }

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.DisplayMessege = CommonMessege.LoginSuccess;
                _response.Result = result;

            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessege.SystemError);
            }

            return Ok(_response);

        }
    }
}
