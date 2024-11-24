using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Exceptions
{
    public class BadRequestExeption : Exception
    {
        public IDictionary<string, string[]> ValidationErrors { get; set; } 

        public BadRequestExeption(string messege) : base(messege)
        {

        }

        public BadRequestExeption(string messege, ValidationResult validationResult): base(messege)
        {
            ValidationErrors = validationResult.ToDictionary();
        }
    }
}
