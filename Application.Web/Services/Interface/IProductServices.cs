using Application.Web.DTO.Product;
using Application.Web.InputModel;
using Application.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Services.Interface
{
    public interface IProductServices
    {
        Task<PaginationVM<ProductDto>>GetPagination(PaginationIM paginationIM);

        Task<ProductDto> CreateAsync(CreateProductDto createProductDto);

        Task<IEnumerable<ProductDto>> GetAllAsync();

        Task<IEnumerable<ProductDto>> GetAllByFilterAsync(int? brandId , int? categoryId);

        Task<ProductDto> GetByIdAsync(int id);

        Task UpdateAsync(UpdateProductDto updateProductDto);

        Task DeleteAsync(int id);
    }
}
