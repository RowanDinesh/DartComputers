using Application.Web.DTO.Brand;
using Application.Web.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Services.Interface
{
    public interface ICategoryServices 
    {
        Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto);

        Task<IEnumerable<CategoryDto>> GetAllAsync();

        Task<CategoryDto>GetByIdAsync(int id);

        Task UpdateAsync(UpdateCategoryDto updateCategoryDto);

        Task DeleteAsync(int id);
    }
}
