using Application.Web.DTO.Brand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Services.Interface
{
    public interface IBrandServices
    {
        Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto);

        Task<IEnumerable<BrandDto>>GetAllAsync();

        Task <BrandDto> GetByIdAsync(int id);

        Task UpdateAsync(UpdateBrandDto updateBrandDto);

        Task DeleteAsync(int id);
    }
}
