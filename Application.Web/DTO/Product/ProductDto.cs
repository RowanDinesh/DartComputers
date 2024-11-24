using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }

        public string Brand {  get; set; }

        public int CategoryId { get; set; }

        public string Category { get; set; }

        [DisplayName("Product Name")]
        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
