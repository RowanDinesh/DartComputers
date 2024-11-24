using Application.Web.Common;
using Application.Web.DTO.Product;
using Application.Web.InputModel;
using Application.Web.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DartComputers.Web.Controllers.v2
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        private APIResponse _response;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
            _response = new APIResponse();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> Get()
        {
            try
            {
                var product = await _productServices.GetAllAsync();
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = product;
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
                var product = await _productServices.GetByIdAsync(id);

                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    _response.DisplayMessege = CommonMessege.RecordNotFound;
                }

                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = product;


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
