using Application.Web.Common;
using Application.Web.DTO.Product;
using Application.Web.InputModel;
using Application.Web.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DartComputers.Web.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
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
        [Authorize(Roles ="ADMIN")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Pagination")]
        public async Task<ActionResult<APIResponse>> GetPagination(PaginationIM paginationIM)
        {
            try
            {
                var product = await _productServices.GetPagination(paginationIM);
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
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Route("Filter")]
        public async Task<ActionResult<APIResponse>> GetFilter(int? brandId , int? catgoryId)
        {
            try
            {
                var product = await _productServices.GetAllByFilterAsync(brandId , catgoryId);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateProductDto createProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessege = CommonMessege.CreateOperationFailed;
                    _response.AddError(ToString());
                    return Ok(_response);
                }

                var product = await _productServices.CreateAsync(createProductDto);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessege = CommonMessege.CreateOperationSuccess;
                _response.Result = product;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessege = CommonMessege.CreateOperationFailed;
                _response.AddError(CommonMessege.SystemError);
            }
            return Ok(_response);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateProductDto updateProductDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessege = CommonMessege.UpdateOperationFailed;
                    _response.AddError(ToString());
                    return Ok(_response);
                }
                var product = await _productServices.GetByIdAsync(updateProductDto.Id);

                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessege = CommonMessege.UpdateOperationFailed;
                    return Ok(_response);
                }

                await _productServices.UpdateAsync(updateProductDto);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessege = CommonMessege.UpdateOperationSuccess;



            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessege = CommonMessege.UpdateOperationFailed;
                _response.AddError(CommonMessege.SystemError);
            }

            return Ok(_response);


        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.DisplayMessege = CommonMessege.DeleteOperationFailed;
                    return Ok(_response);
                }

                var product = await _productServices.GetByIdAsync(id);

                if (product == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessege = CommonMessege.DeleteOperationFailed;
                    return Ok(_response);
                }

                await _productServices.DeleteAsync(id);

                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessege = CommonMessege.DeleteOperationSuccess;
            }
            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessege = CommonMessege.DeleteOperationFailed;
                _response.AddError(CommonMessege.SystemError);
            }

            return Ok(_response);
        }
    }
}
