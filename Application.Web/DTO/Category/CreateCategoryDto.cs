using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Web.DTO.Brand;
using FluentValidation;

namespace Application.Web.DTO.Category
{
    public class CreateCategoryDto
    {        

        [Required]
        [DisplayName("Category Name")]
        public string CategoryName { get; set; }
    }

    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryName).NotEmpty();
        }
    }
}
