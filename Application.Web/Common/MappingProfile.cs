using Application.Web.DTO.Brand;
using Application.Web.DTO.Category;
using Application.Web.DTO.Product;
using AutoMapper;
using Domain.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Brand, CreateBrandDto>().ReverseMap();
            CreateMap<Brand, UpdateBrandDto>().ReverseMap();
            CreateMap<Brand, BrandDto> ().ReverseMap();

            CreateMap<Category, CreateCategoryDto > ().ReverseMap();
            CreateMap<Category, UpdateCategoryDto > ().ReverseMap();
            CreateMap<Category, CategoryDto > ().ReverseMap();

            CreateMap<Product, CreateProductDto> ().ReverseMap();
            CreateMap<Product, UpdateProductDto> ().ReverseMap();
            CreateMap<Product, ProductDto> ()
                .ForMember(x=>x.Brand , options => options.MapFrom(source=> source.Brand.BrandName))
                .ForMember(x=>x.Category , options => options.MapFrom(source=> source.Category.CategoryName));
        }
    }
}
