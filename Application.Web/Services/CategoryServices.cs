using Application.Web.DTO.Brand;
using Application.Web.DTO.Category;
using Application.Web.Exceptions;
using Application.Web.Services.Interface;
using AutoMapper;
using Domain.Web.Contracts;
using Domain.Web.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryServices(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var validator = new CreateCategoryDtoValidator();

            var validationResult = await validator.ValidateAsync(createCategoryDto);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestExeption("Invalid Brand Input", validationResult);
            }


            var category = _mapper.Map<Category>(createCategoryDto);

            var entity = await _repository.CreateAsync(category);

            var result = _mapper.Map<CategoryDto>(entity);

            return result;
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _repository.GetByIdAsync(x => x.Id == id);
            await _repository.DeleteAsync(category);
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var category = await _repository.GetAllAsync();
            return _mapper.Map<List<CategoryDto>>(category);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(x => x.Id == id);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task UpdateAsync(UpdateCategoryDto updateCategoryDto)
        {
            var category = _mapper.Map<Category>(updateCategoryDto);
            await _repository.UpdateAsync(category);
        }
    }
}
