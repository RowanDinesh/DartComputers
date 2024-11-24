using Application.Web.Common;
using Application.Web.DTO.Brand;
using Application.Web.DTO.Category;
using Application.Web.Exceptions;
using Application.Web.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DartComputers.Web.Controllers.v2
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices _brandServices;

        private APIResponse _response;

        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
            _response = new APIResponse();
        }
       
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(CacheProfileName = "Default")]
        [HttpGet]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                var brand = await _brandServices.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = brand;
            }
            catch (Exception)
            {
                _response.StatusCode=HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessege.SystemError);

            }

            return Ok( _response);

        }

        [HttpGet]
        [Route("Details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            
            try
            {
                var brand = await _brandServices.GetByIdAsync(id);

                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess=false;
                    _response.DisplayMessege=CommonMessege.RecordNotFound;
                }

                _response.StatusCode=HttpStatusCode.OK;
                _response.IsSuccess=true;
                _response.Result = brand;


            }

            catch (Exception ex)
            {
                _response.AddError(CommonMessege.SystemError);
                _response.StatusCode = HttpStatusCode.InternalServerError; 
            }

            return _response ;
        }
       
    }
}
