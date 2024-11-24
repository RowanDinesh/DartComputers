using Application.Web.DTO.Brand;
using Application.Web.Exceptions;
using Application.Web.Services.Interface;
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
    public class BrandServices : IBrandServices
    {
        private readonly IBrandRepository _repository;
        private readonly IMapper _mapper;

        public BrandServices(IBrandRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<BrandDto> CreateAsync(CreateBrandDto createBrandDto)
        {
            var validator = new CreateBrandDtoValidator();

            var validationResult = await validator.ValidateAsync(createBrandDto);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestExeption("Invalid Brand Input", validationResult);
            }

            var brand =  _mapper.Map<Brand>(createBrandDto);

            var entity = await _repository.CreateAsync(brand);

            var result = _mapper.Map<BrandDto>(entity);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
           var brand = await _repository.GetByIdAsync(x => x.Id == id);
           await _repository.DeleteAsync(brand);
        }

        public async Task<IEnumerable<BrandDto>> GetAllAsync()
        {
            var brand = await _repository.GetAllAsync();
            return _mapper.Map <List<BrandDto>>(brand);
        }

        public async Task<BrandDto> GetByIdAsync(int id)
        {
            var brand = await _repository.GetByIdAsync(x =>x.Id == id);
            return _mapper.Map<BrandDto>(brand);
        }

        public async Task UpdateAsync(UpdateBrandDto updateBrandDto)
        {
            var brand =  _mapper.Map<Brand>(updateBrandDto);
            await _repository.UpdateAsync(brand);
        }
    }
}
