using Application.Web.Common;
using Application.Web.DTO.Category;
using Application.Web.Exceptions;
using Application.Web.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DartComputers.Web.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;

        private APIResponse _response;

        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                var category = await _categoryServices.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = category;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.AddError(CommonMessege.SystemError);

            }

            return Ok(_response);

        }

        [HttpGet]
        [Route("Details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get(int id)
        {

            try
            {
                var category = await _categoryServices.GetByIdAsync(id);

                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.DisplayMessege = CommonMessege.RecordNotFound;
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = category;


            }

            catch (Exception ex)
            {
                _response.AddError(CommonMessege.SystemError);
                _response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _response;
        }
       
    }
}
