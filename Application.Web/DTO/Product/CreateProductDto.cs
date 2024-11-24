using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.DTO.Product
{
    public class CreateProductDto
    {
        public int BrandId { get; set; }     

        public int CategoryId { get; set; }   

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
