using Application.Web.Common;
using Application.Web.DTO.Category;
using Application.Web.Exceptions;
using Application.Web.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DartComputers.Web.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody] CreateCategoryDto createCategoryDto)
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

                var category = await _categoryServices.CreateAsync(createCategoryDto);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess = true;
                _response.DisplayMessege = CommonMessege.CreateOperationSuccess;
                _response.Result = category;
            }
            catch (BadRequestExeption ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessege = CommonMessege.CreateOperationFailed;
                _response.AddWarning(ex.Message);
                _response.AddError(CommonMessege.SystemError);
                _response.Result = ex.ValidationErrors;
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
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateCategoryDto updateCategoryDto)
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
                var category = await _categoryServices.GetByIdAsync(updateCategoryDto.Id);

                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessege = CommonMessege.UpdateOperationFailed;
                    return Ok(_response);
                }

                await _categoryServices.UpdateAsync(updateCategoryDto);

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

                var category = await _categoryServices.GetByIdAsync(id);

                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessege = CommonMessege.DeleteOperationFailed;
                    return Ok(_response);
                }

                await _categoryServices.DeleteAsync(id);

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
