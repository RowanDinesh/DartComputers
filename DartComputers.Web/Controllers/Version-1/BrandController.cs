using Application.Web.Common;
using Application.Web.DTO.Brand;
using Application.Web.DTO.Category;
using Application.Web.Exceptions;
using Application.Web.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DartComputers.Web.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandServices _brandServices;
        private readonly ILogger<BrandController> _logger;

        private APIResponse _response;

        public BrandController(IBrandServices brandServices, ILogger<BrandController> logger)
        {
            _brandServices = brandServices;
            _logger = logger;
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

                _logger.LogInformation("Brand Record Fetched Successfully");
            }
            catch (Exception)
            {
                _logger.LogError("Brand Record Fetching Opertaion Failed");
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
                    _logger.LogInformation($"Brand Record Fetched By {id} Successfully");
                }

                _response.StatusCode=HttpStatusCode.OK;
                _response.IsSuccess=true;
                _response.Result = brand;


            }

            catch (Exception ex)
            {
                _logger.LogError($"Brand Record Fetching Opertaion By {id} Failed");
                _response.AddError(CommonMessege.SystemError);
                _response.StatusCode = HttpStatusCode.InternalServerError; 
            }

            return _response ;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ActionResult<APIResponse>> Create([FromBody]CreateBrandDto createBrandDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;                    
                    _response.DisplayMessege = CommonMessege.CreateOperationFailed;
                    _response.AddError(ToString());
                    return Ok(_response);
                }

                var brand = await _brandServices.CreateAsync(createBrandDto);

                _response.StatusCode = HttpStatusCode.Created;
                _response.IsSuccess= true;
                _response.DisplayMessege=CommonMessege.CreateOperationSuccess;
                _response.Result = brand;
            }
            catch (BadRequestExeption ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.DisplayMessege = CommonMessege.CreateOperationFailed;
                _response.AddWarning(ex.Message);
                _response.AddError(CommonMessege.SystemError);
                _response.Result= ex.ValidationErrors;
            }

            catch (Exception)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;                
                _response.DisplayMessege=CommonMessege.CreateOperationFailed;
                _response.AddError(CommonMessege.SystemError);
            }
            return Ok(_response);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult<APIResponse>> Update([FromBody] UpdateBrandDto updateBrandDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    _response.StatusCode=HttpStatusCode.BadRequest;                   
                    _response.DisplayMessege=CommonMessege.UpdateOperationFailed;
                    _response.AddError(ToString());
                    return Ok(_response);
                }
               var brand = await _brandServices.GetByIdAsync(updateBrandDto.Id);

                if(brand == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    _response.DisplayMessege= CommonMessege.UpdateOperationFailed;
                    return Ok(_response);
                }

                await _brandServices.UpdateAsync(updateBrandDto);

                _response.StatusCode=HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                _response.DisplayMessege = CommonMessege.UpdateOperationSuccess;

                    

            }
            catch (Exception)
            {
                _response.StatusCode=HttpStatusCode.InternalServerError;                
                _response.DisplayMessege = CommonMessege.UpdateOperationFailed;
                _response.AddError(CommonMessege.SystemError);
            }

            return Ok(_response) ;


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

                var brand = await _brandServices.GetByIdAsync(id);

                if (brand == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.DisplayMessege = CommonMessege.DeleteOperationFailed;
                    return Ok(_response );
                }
                
                await _brandServices.DeleteAsync(id);

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

            return Ok( _response);
        }
    }
}
