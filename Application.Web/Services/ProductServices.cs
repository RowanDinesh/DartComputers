using Application.Web.DTO.Product;
using Application.Web.InputModel;
using Application.Web.Services.Interface;
using Application.Web.ViewModel;
using AutoMapper;
using Domain.Web.Contracts;
using Domain.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Services
{
    public class ProductServices : IProductServices
    {
        private readonly IProductRepository _repository;
        private readonly IPaginationServices<ProductDto, Product> _pagination;
        private readonly IMapper _mapper;

        public ProductServices(IProductRepository repository, IMapper mapper, IPaginationServices<ProductDto, Product> pagination)
        {
            _repository = repository;
            _mapper = mapper;
            _pagination = pagination;
        }
        public async Task<ProductDto> CreateAsync(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);

            var entity = await _repository.CreateAsync(product);

            var result = _mapper.Map<ProductDto>(entity);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(x => x.Id == id);
            await _repository.DeleteAsync(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var product = await _repository.GetAllProductAsync();
            return _mapper.Map<List<ProductDto>>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllByFilterAsync(int? brandId, int? categoryId)
        {
            var data = await _repository.GetAllProductAsync();

            IEnumerable<Product> query = data;

            if (brandId > 0)
            {
                query = query.Where(x => x.BrandId == brandId);
            }

            if(categoryId > 0)
            {
                query = query.Where(x => x.CategoryId == categoryId);
            }

            var result = _mapper.Map<List<ProductDto>>(query);

            return result;
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _repository.GetProductByIdAsync(id);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<PaginationVM<ProductDto>> GetPagination(PaginationIM paginationIM)
        {
            var source = await _repository.GetAllProductAsync();

            var result = _pagination.GetPagination(source, paginationIM);

            return result;
        }

        public async Task UpdateAsync(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            await _repository.UpdateAsync(product);
        }
    }
}
