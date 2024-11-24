using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Web.Models
{
    public class Product : BaseModel
    {
        
        public int BrandId {  get; set; }
        
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        
        
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        
        [DisplayName("Product Name")]
        public string ProductName {  get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
